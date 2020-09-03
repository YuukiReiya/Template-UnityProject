@echo off
set content=%~1
set title=%~2

setlocal

echo MsgBox "%content%" ,vbExclamation, "%title%" > %TEMP%\msgbox.vbs & %TEMP%\msgbox.vbs
del /Q %TEMP%\msgbox.vbs

endlocal