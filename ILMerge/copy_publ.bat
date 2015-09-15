@ECHO OFF

set ilm_path="c:\GitHub\D3Hot\ILMerge\ILMerge.exe"
set z_path="c:\GitHub\D3Hot\ILMerge\7z.exe"
set touch_path="c:\GitHub\D3Hot\ILMerge\FileTouch.exe"
set target_path=%2
set target_file=%~nx2
set target_dir=%~dp2

REM set rel = c:\GitHub\D3Hot\D3Hot\bin\Debug\D3Hot.exe
REM If exist c:\GitHub\D3Hot\D3Hot\bin\Release\D3Hot.exe set rel = c:\GitHub\D3Hot\D3Hot\bin\Release\D3Hot.exe

echo %target_path%
echo %target_file%
echo %target_dir%
echo %z_path%

REM pause

rem #    set output path and result file path
set result="C:\Users\dolenin\Dropbox\Public\D3Hot.7z"

rem #    run merge cmd
%ilm_path% /wildcards /out:%target_dir%\D3H.exe %target_dir%\D3Hot.exe %target_dir%\*.dll
REM WindowsInput

echo "%z_path%" a %result%" "%target_path%"
REM del /q /f %result%
%z_path% a %result% %target_dir%\D3H.exe
%touch_path% /c %result%

REM pause

rem #    if succeded
IF %ErrorLevel% EQU 0 (
    
        @echo Result: %target_file% "->  %target_path%"
       @echo Result: %target_file% "->  %result%" 
   
   set status=succeded
   set errlvl=0    
) ELSE (
    set status=failed 
    set errlvl=1
    )

@echo Merge %status%

REM del /F/Q c:\GitHub\D3Hot\D3Hot\bin\Release\*.*

exit %errlvl% 

REM copy_publ "c:\GitHub\D3Hot\" "c:\GitHub\D3Hot\D3Hot\bin\Debug\D3Hot.exe"