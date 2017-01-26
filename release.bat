set version=1.0

"\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" ReplaceWords.sln /p:Configuration=Release /p:Platform="Any CPU"
if errorlevel 1 goto :eof

md ReplaceWords-%version%
copy App.config ReplaceWords-%version%
copy LICENSE ReplaceWords-%version%
copy README.md ReplaceWords-%version%
copy bin\Release\ReplaceWords.exe ReplaceWords-%version%

del ReplaceWords-%version%.zip
7z a ReplaceWords-%version%.zip ReplaceWords-%version%
7z l ReplaceWords-%version%.zip

rd /q /s ReplaceWords-%version%
