$ErrorActionPreference = "Stop"
$root = Split-Path $PSScriptRoot -Parent
$html = Join-Path $PSScriptRoot "og-image.html"
$png = Join-Path $root "Portfolio\wwwroot\og.png"

$edge = @(
    "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Microsoft\Edge\Application\msedge.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $edge) { throw "Не найден Microsoft Edge." }

$url = "file:///" + ($html -replace "\\", "/")
& $edge --headless=new --disable-gpu --hide-scrollbars --force-device-scale-factor=1 `
    --virtual-time-budget=5000 --window-size=1200,630 "--screenshot=$png" $url | Out-Null
Start-Sleep 1
if (-not (Test-Path $png)) { throw "Картинка не создана." }
Write-Host "Готово: $png"
