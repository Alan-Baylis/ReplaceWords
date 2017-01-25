MSBuild.exe ReplaceWords.sln /p:Configuration=Debug /p:Platform="Any CPU"
if errorlevel 1 goto :eof
echo "the quick brown fox jumped over the lazy dog" >text.txt
echo dog,cat >words.csv
bin\Debug\ReplaceWords text.txt
type text.txt
