<#
Convert Traefik certificate .pfx -> .crt and .key

Requires OpenSSL on PATH. Usage:
  .\scripts\convert-certs.ps1 -PfxPath .\certs\localhost.pfx -OutDir .\certs
#>
param(
    [string]$PfxPath = "./certs/localhost.pfx",
    [string]$OutDir = "./certs",
    [string]$PfxPassword = ""
)

if (-not (Test-Path $PfxPath)) { Write-Error "PFX not found: $PfxPath"; exit 1 }

$opensslExe = $null
if (Get-Command openssl -ErrorAction SilentlyContinue) {
    $opensslExe = (Get-Command openssl).Source
} else {
    $commonPaths = @(
        'C:\Program Files\OpenSSL\bin\openssl.exe',
        'C:\Program Files (x86)\OpenSSL\bin\openssl.exe'
    )
    foreach ($path in $commonPaths) {
        if (Test-Path $path) {
            $opensslExe = $path
            break
        }
    }
}
if (-not $opensslExe) {
    Write-Error "OpenSSL not found in PATH or common install directories. Install OpenSSL or run equivalent conversion manually."
    exit 2
}

$crt = Join-Path $OutDir 'localhost.crt'
$key = Join-Path $OutDir 'localhost.key'

Write-Host "Extracting cert and key to $OutDir"

$pwArg = ""
if ($PfxPassword -ne "") { $pwArg = "-password pass:$PfxPassword" }

Function Generate-SelfSignedCert {
    param(
        [string]$OutCrt,
        [string]$OutKey
    )
    Write-Host "Generating self-signed certificate for localhost..." -ForegroundColor Yellow
    $tempConf = [IO.Path]::GetTempFileName()
    Set-Content -Path $tempConf -Value @"
[req]
distinguished_name=req_distinguished_name
req_extensions=v3_req
prompt=no
[req_distinguished_name]
CN=localhost
[v3_req]
subjectAltName=DNS:localhost,IP:127.0.0.1
"@
    & $opensslExe req -x509 -nodes -days 365 -newkey rsa:2048 -keyout $OutKey -out $OutCrt -config $tempConf -extensions v3_req
    $exitCode = $LASTEXITCODE
    Remove-Item -Force $tempConf -ErrorAction SilentlyContinue
    return $exitCode
}

# Extract cert
Write-Host "Extracting certificate from .pfx..."
& $opensslExe pkcs12 -in $PfxPath $pwArg -nokeys -out $crt -nodes 2>&1 | Tee-Object -Variable pfxOutput
if ($LASTEXITCODE -ne 0) {
    Write-Warning "PFX conversion failed: $pfxOutput"
    Write-Warning "Falling back to self-signed certificate generation."
    $rc = Generate-SelfSignedCert -OutCrt $crt -OutKey $key
    if ($rc -ne 0) {
        Write-Error "Self-signed certificate generation failed"
        exit 3
    }
    Write-Host "Self-signed certificate created: $crt and $key"
    exit 0
}

# Extract key
Write-Host "Extracting key from .pfx..."
& $opensslExe pkcs12 -in $PfxPath $pwArg -nocerts -out $key -nodes 2>&1 | Tee-Object -Variable pfxKeyOutput
if ($LASTEXITCODE -ne 0) {
    Write-Warning "PFX key conversion failed: $pfxKeyOutput"
    Write-Warning "Falling back to self-signed certificate generation."
    $rc = Generate-SelfSignedCert -OutCrt $crt -OutKey $key
    if ($rc -ne 0) {
        Write-Error "Self-signed certificate generation failed"
        exit 4
    }
    Write-Host "Self-signed certificate created: $crt and $key"
    exit 0
}

Write-Host "Conversion complete: $crt and $key"
