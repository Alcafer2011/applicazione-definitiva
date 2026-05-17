<#
Bring-up helper for AI-Enterprise-OS

Usage (PowerShell, run as admin if possible):
  .\scripts\bringup.ps1

This script will:
- check docker availability
- test NuGet connectivity (informational)
- pull official images
- build images declared in docker-compose
- start all services
- collect container logs to `.compose-logs/`
- attempt an automatic fix for Loki by recreating the Loki volume if common errors are detected
#>

Param(
    [switch]$Force
)

# Move to repo root
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Push-Location (Join-Path $scriptDir '..')

Function ExitWithError([string]$msg){
    Write-Host $msg -ForegroundColor Red
    Pop-Location
    exit 1
}

Write-Host "Checking Docker daemon..."
docker info > $null 2>&1
if ($LASTEXITCODE -ne 0) { ExitWithError 'Docker does not appear to be running or accessible. Start Docker Desktop and retry.' }

Write-Host "Checking NuGet connectivity (informational)..."
try {
    Invoke-WebRequest -Uri 'https://api.nuget.org/v3/index.json' -UseBasicParsing -TimeoutSec 10 > $null
    Write-Host 'NuGet reachable.' -ForegroundColor Green
} catch {
    Write-Warning 'NuGet not reachable. Builds for .NET services may fail; script will continue.'
}

Write-Host "Pulling declared external images (ignore failures)..."
docker-compose pull --ignore-pull-failures

Write-Host "Building images (this may take a long time)..."
docker-compose build --parallel --pull

Write-Host "Starting services (detached)..."
docker-compose up -d

Write-Host "Waiting 8s for containers to initialize..."
Start-Sleep -Seconds 8

# Prepare log directory
$logDir = Join-Path $scriptDir '..' | Join-Path -ChildPath '.compose-logs'
New-Item -ItemType Directory -Path $logDir -Force | Out-Null

Write-Host "Collecting logs for all containers into $logDir"
$allContainers = docker ps -a --format "{{.Names}}"
foreach ($c in $allContainers) {
    $safeName = $c -replace '[\\/:*?"<>| ]','_'
    docker logs --tail 500 $c > (Join-Path $logDir "$safeName.log") 2>&1
}

# Report non-running containers
$nonRunning = docker ps --format "{{.Names}} {{.Status}}" | Where-Object { $_ -notmatch 'Up' }
if ($nonRunning) {
    Write-Warning "Some containers are not running:";
    $nonRunning | ForEach-Object { Write-Host " - $_" }
} else {
    Write-Host 'All containers reported Up.' -ForegroundColor Green
}

# Heuristic fix for Loki
$lokiLog = Join-Path $logDir 'bos-loki.log'
if (Test-Path $lokiLog) {
    $lokiText = Get-Content $lokiLog -Raw -ErrorAction SilentlyContinue
    if ($lokiText -match 'permission|panic|failed|error|no such file|permission denied') {
        Write-Warning 'Detected potential Loki error in logs. Attempting automated remediation: recreate loki volume and restart Loki.'
        # Find volumes with 'loki' in name
        $lokiVols = docker volume ls --format "{{.Name}}" | Where-Object { $_ -match 'loki' }
        foreach ($v in $lokiVols) {
            Write-Host "Removing volume $v" -ForegroundColor Yellow
            docker volume rm -f $v | Out-Null
        }
        Write-Host 'Re-creating Loki container...'
        docker-compose up -d loki
        Start-Sleep -Seconds 5
        docker logs --tail 200 bos-loki > (Join-Path $logDir 'bos-loki-after-recreate.log') 2>&1
        Write-Host 'Loki restart attempt complete. Check logs.'
    }
}

Write-Host "Summary: container status"
docker ps --format 'table {{.Names}}\t{{.Status}}\t{{.Ports}}'

Write-Host "Logs saved in: $logDir"
Pop-Location

Write-Host 'Done.' -ForegroundColor Cyan
