$ErrorActionPreference = "Stop"

$isAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
    [Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) { throw "Запустите PowerShell от имени администратора." }

foreach ($name in "Caddy", "Portfolio") {
    $svc = Get-Service $name -ErrorAction SilentlyContinue
    if ($svc) {
        if ($svc.Status -ne "Stopped") { Stop-Service $name }
        sc.exe delete $name | Out-Null
        Write-Host "Служба $name удалена."
    }
}

if (Get-ScheduledTask -TaskName "Portfolio Auto-Update" -ErrorAction SilentlyContinue) {
    Unregister-ScheduledTask -TaskName "Portfolio Auto-Update" -Confirm:$false
    Write-Host "Задача автообновления удалена."
}

Get-NetFirewallRule -DisplayName "Portfolio HTTP *" -ErrorAction SilentlyContinue | Remove-NetFirewallRule
Write-Host "Правила брандмауэра удалены."
Write-Host "Папки C:\Sites\Portfolio и C:\Caddy можно удалить вручную."
