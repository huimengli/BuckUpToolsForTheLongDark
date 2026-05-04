@echo off
powershell -ExecutionPolicy Bypass -File "%~dp0MD5.ps1" %1 && exit
pause