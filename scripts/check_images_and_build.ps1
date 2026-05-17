<#
Check images and build/pull missing ones, then run bringup.ps1

Usage:
  .\scripts\check_images_and_build.ps1
#>

# Move to repo root
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Push-Location (Join-Path $scriptDir '..')

Function Get-Compose-Services {
    $y = Get-Content docker-compose.yml -Raw
    $servicesSection = $y -split "(?m)^services:\s*$" | Select-Object -Last 1
    $lines = $servicesSection -split "\r?\n"
    $services = @()
    $current = $null
    foreach ($line in $lines) {
        if ($line -match "^\s{2}([a-z0-9_\-]+):\s*$") {
            $current = $matches[1]
            $services += @{ name = $current; block = @() }
        } elseif ($current) {
            $services[-1].block += $line
        }
    }
    return $services
}

$services = Get-Compose-Services
Write-Host "Found $($services.Count) services in docker-compose.yml"

$toBuild = @()
$toPull = @()

foreach ($s in $services) {
    $block = $s.block -join "\n"
    if ($block -match "^\s*build:\s*\n\s*context:\s*(.+)$") {
        $context = $matches[1].Trim()
        $toBuild += @{ name = $s.name; context = $context }
    } elseif ($block -match "^\s*image:\s*(.+)$") {
        $image = $matches[1].Trim()
        $toPull += @{ name = $s.name; image = $image }
    }
}

Write-Host "Services to build: $($toBuild.Count)  remote images: $($toPull.Count)"

# Check local images and build/pull if missing
Function ImageExists($imageName) {
    $out = docker images --format "{{.Repository}}:{{.Tag}}" | Where-Object { $_ -eq $imageName }
    return -not [string]::IsNullOrEmpty($out)
}

foreach ($b in $toBuild) {
    # Use docker-compose build for consistency
    Write-Host "Building service: $($b.name) (context: $($b.context))"
    docker-compose build $($b.name)
    if ($LASTEXITCODE -ne 0) { Write-Warning "Build failed for $($b.name)" }
}

foreach ($p in $toPull) {
    $img = $p.image
    if (-not (ImageExists $img)) {
        Write-Host "Pulling image $img for service $($p.name)"
        docker pull $img
        if ($LASTEXITCODE -ne 0) { Write-Warning "Failed to pull $img" }
    } else {
        Write-Host "Image $img already present locally"
    }
}

# Finally call bringup
Write-Host "Invoking bringup.ps1 to start containers"
& .\scripts\bringup.ps1

Pop-Location
