cd ../../CodeBlockDiagram
rem linux not support /p:PublishReadyToRun=true
dotnet publish -c Release -f net5.0 -r linux-x64 -o ../docker/linux /p:PublishSingleFile=true                            /p:PublishTrimmed=true
cd ../docker/linux

docker build -t codeblockdiagram .
docker rm -f cbdTest
docker run -itd --name cbdTest codeblockdiagram /bin/bash