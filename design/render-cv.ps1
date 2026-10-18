param([string]$Lang = "ru")
$ErrorActionPreference = "Stop"
$src = Join-Path $PSScriptRoot "cv-$Lang.html"
$out = Join-Path ([Environment]::GetFolderPath("Desktop")) "CV Yeldos Sozakbay ($($Lang.ToUpper())).pdf"
$edge = @(
    "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
    "$env:ProgramFiles\Microsoft\Edge\Application\msedge.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
$url = "file:///" + ($src -replace "\\", "/")
& $edge --headless=new --disable-gpu --no-pdf-header-footer --virtual-time-budget=6000 "--print-to-pdf=$out" $url | Out-Null
Start-Sleep 2
Write-Host $out
