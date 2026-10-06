param(
    [string]$Domain = "",
    [string]$Email = "",
    [string[]]$RedirectFrom = @(),
    [string]$InstallDir = "C:\Sites\Portfolio",
    [string]$CaddyDir = "C:\Caddy",
    [int]$AppPort = 5000
)

$ErrorActionPreference = "Stop"
$ServiceName = "Portfolio"
$CaddyService = "Caddy"
$RepoRoot = Split-Path $PSScriptRoot -Parent
$Project = Join-Path $RepoRoot "Portfolio\Portfolio.csproj"

function Step($text) { Write-Host "`n==> $text" -ForegroundColor Cyan }

function Set-ServiceCommand($name, $command) {
    Set-ItemProperty "HKLM:\SYSTEM\CurrentControlSet\Services\$name" -Name ImagePath -Value $command
    Set-Service $name -StartupType Automatic
}

$isAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
    [Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) { throw "Запустите PowerShell от имени администратора." }
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "Не найден .NET SDK. Установите .NET 10 SDK: https://dotnet.microsoft.com/download"
}

Step "Публикация сайта в $InstallDir"
$svc = Get-Service $ServiceName -ErrorAction SilentlyContinue
if ($svc -and $svc.Status -ne "Stopped") { Stop-Service $ServiceName; Start-Sleep 2 }
dotnet publish $Project -c Release -r win-x64 --self-contained false -o $InstallDir
if ($LASTEXITCODE -ne 0) { throw "Ошибка публикации." }

Step "Служба Windows '$ServiceName'"
$exe = Join-Path $InstallDir "Portfolio.exe"
$binPath = "`"$exe`" --urls http://127.0.0.1:$AppPort --environment Production"
if (-not $svc) {
    New-Service -Name $ServiceName -BinaryPathName $binPath -DisplayName "Portfolio website" `
        -Description "Сайт-портфолио (ASP.NET Core)" -StartupType Automatic | Out-Null
} else {
    Set-ServiceCommand $ServiceName $binPath
}
sc.exe failure $ServiceName reset= 86400 actions= restart/5000/restart/5000/restart/30000 | Out-Null
Start-Service $ServiceName

Step "Caddy в $CaddyDir"
New-Item -ItemType Directory -Force $CaddyDir | Out-Null
$caddyExe = Join-Path $CaddyDir "caddy.exe"
if (-not (Test-Path $caddyExe)) {
    Write-Host "Скачиваю Caddy с официального сайта caddyserver.com..."
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    Invoke-WebRequest "https://caddyserver.com/api/download?os=windows&arch=amd64" -OutFile $caddyExe -UseBasicParsing
}

$caddyfile = Join-Path $CaddyDir "Caddyfile"
if ($Domain) {
    $global = if ($Email) { "{`n    email $Email`n}`n`n" } else { "" }
    $aliases = @("www.$Domain") + ($RedirectFrom | ForEach-Object { $_; "www.$_" })
    $config = "$global$Domain {`n    encode gzip zstd`n    reverse_proxy 127.0.0.1:$AppPort`n}`n`n" +
              "$($aliases -join ', ') {`n    redir https://$Domain{uri} permanent`n}`n"
} else {
    $config = ":80 {`n    encode gzip zstd`n    reverse_proxy 127.0.0.1:$AppPort`n}`n"
}
[IO.File]::WriteAllText($caddyfile, $config)
& $caddyExe validate --config $caddyfile --adapter caddyfile
if ($LASTEXITCODE -ne 0) { throw "Ошибка в Caddyfile." }

$caddySvc = Get-Service $CaddyService -ErrorAction SilentlyContinue
$caddyBin = "`"$caddyExe`" run --config `"$caddyfile`" --adapter caddyfile"
if (-not $caddySvc) {
    New-Service -Name $CaddyService -BinaryPathName $caddyBin -DisplayName "Caddy web server" `
        -Description "Реверс-прокси и HTTPS для сайта-портфолио" -StartupType Automatic | Out-Null
} else {
    if ($caddySvc.Status -ne "Stopped") { Stop-Service $CaddyService }
    Set-ServiceCommand $CaddyService $caddyBin
}
sc.exe failure $CaddyService reset= 86400 actions= restart/5000/restart/5000/restart/30000 | Out-Null
Start-Service $CaddyService

Step "Брандмауэр: открываю порты 80 и 443"
foreach ($p in 80, 443) {
    $name = "Portfolio HTTP $p"
    if (-not (Get-NetFirewallRule -DisplayName $name -ErrorAction SilentlyContinue)) {
        New-NetFirewallRule -DisplayName $name -Direction Inbound -Protocol TCP -LocalPort $p -Action Allow | Out-Null
    }
}

Step "Проверка"
Start-Sleep 3
try {
    $code = (Invoke-WebRequest "http://127.0.0.1:$AppPort" -UseBasicParsing -TimeoutSec 10).StatusCode
    Write-Host "Сайт отвечает локально: HTTP $code" -ForegroundColor Green
} catch {
    Write-Warning "Сайт не отвечает на http://127.0.0.1:$AppPort — смотрите журнал: Просмотр событий -> Журналы Windows -> Приложение"
}

$url = if ($Domain) { "https://$Domain" } else { "http://<ваш-белый-IP>" }
Write-Host "`nГотово. Снаружи сайт будет доступен по адресу: $url" -ForegroundColor Green
Write-Host "Не забудьте пробросить порты 80 и 443 на роутере на этот ПК."
