param(
    [string] = "C:\Users\infoa\Desktop\AI-Enterprise-OS",
    [string] = "your-registry"
)

Write-Host "Building backend API (Release)..." -ForegroundColor Cyan
dotnet publish "\backend-api\backend-api.csproj" -c Release -o "\publish\backend-api"

Write-Host "Building Docker image: API..." -ForegroundColor Cyan
docker build -t /ai-enterprise-api:latest -f "\backend-api\Dockerfile" "\publish\backend-api"

Write-Host "Building frontend (npm build)..." -ForegroundColor Cyan
# TODO: qui il tuo comando npm run build nel frontend
# npm install
# npm run build

Write-Host "Building Docker image: frontend..." -ForegroundColor Cyan
docker build -t /ai-enterprise-frontend:latest -f "\frontend-react\Dockerfile" "\frontend-react\dist"

Write-Host "Pushing images to registry..." -ForegroundColor Cyan
docker push /ai-enterprise-api:latest
docker push /ai-enterprise-frontend:latest

Write-Host "=== IMAGES READY FOR CUSTOMERS (NO SOURCE) ===" -ForegroundColor Green
