cd ../../CodeBlockDiagram
dotnet publish -c Release -f net5.0 -r win-x64 -o ../docker/win10  /p:PublishSingleFile=true /p:PublishReadyToRun=true /p:PublishTrimmed=true
cd ../docker/win10

docker build -t codeblockdiagram .
docker rm -f cbdTest
docker run -itd --name cbdTest codeblockdiagram cmd
