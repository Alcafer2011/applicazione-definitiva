param(
    [string] = "C:\Users\infoa\Desktop\AI-Enterprise-OS",
    [string] = "C:\Users\infoa\Desktop\AI-Enterprise-OS\dist"
)

if (-not (Test-Path )) {
    New-Item -ItemType Directory -Path  -Force | Out-Null
}

Write-Host "Building solution in Release..." -ForegroundColor Cyan
dotnet build "" -c Release

Write-Host "Publishing backend API..." -ForegroundColor Cyan
dotnet publish "\backend-api\backend-api.csproj" -c Release -o "\backend-api"

Write-Host "Publishing other services (TODO)..." -ForegroundColor Cyan

Write-Host "Packing for customer distribution (binaries only, no source)..." -ForegroundColor Cyan
# TODO: qui puoi aggiungere compressione in .zip, firma, ecc.
