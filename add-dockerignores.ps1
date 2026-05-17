# Script to add .dockerignore files to all services

$dotnetDockerignore = @"
bin/
obj/
out/
*.user
*.suo
*.cache
*.log
.vs/
.vscode/
.git/
.gitignore
*.md
Dockerfile
docker-compose*.yml
.env
.env.*
*.Development.json
appsettings.Development.json
"@

$nodeDockerignore = @"
node_modules/
npm-debug.log
yarn-error.log
dist/
build/
.git/
.gitignore
*.md
Dockerfile
docker-compose*.yml
.env
.env.*
"@

# Get all .NET service directories
$services = Get-ChildItem -Path . -Directory -Filter "*.Service" | Where-Object { $_.Name -like "*.Service" }

foreach ($service in $services) {
    $dockerignorePath = Join-Path $service.FullName ".dockerignore"
    if (-not (Test-Path $dockerignorePath)) {
        $dotnetDockerignore | Out-File -FilePath $dockerignorePath -Encoding utf8
        Write-Host "Created .dockerignore for $($service.Name)" -ForegroundColor Green
    } else {
        Write-Host ".dockerignore already exists for $($service.Name)" -ForegroundColor Yellow
    }
}

# Frontend React
$frontendReactPath = ".\frontend-react\.dockerignore"
if (-not (Test-Path $frontendReactPath)) {
    $nodeDockerignore | Out-File -FilePath $frontendReactPath -Encoding utf8
    Write-Host "Created .dockerignore for frontend-react" -ForegroundColor Green
} else {
    Write-Host ".dockerignore already exists for frontend-react" -ForegroundColor Yellow
}

# Frontend LoadPlanner3D
$frontendLoadPlannerPath = ".\frontend-loadplanner3d\.dockerignore"
if (-not (Test-Path $frontendLoadPlannerPath)) {
    $nodeDockerignore | Out-File -FilePath $frontendLoadPlannerPath -Encoding utf8
    Write-Host "Created .dockerignore for frontend-loadplanner3d" -ForegroundColor Green
} else {
    Write-Host ".dockerignore already exists for frontend-loadplanner3d" -ForegroundColor Yellow
}

# Frontend
$frontendPath = ".\frontend\.dockerignore"
if (-not (Test-Path $frontendPath)) {
    $nodeDockerignore | Out-File -FilePath $frontendPath -Encoding utf8
    Write-Host "Created .dockerignore for frontend" -ForegroundColor Green
} else {
    Write-Host ".dockerignore already exists for frontend" -ForegroundColor Yellow
}

Write-Host "Done!" -ForegroundColor Cyan
