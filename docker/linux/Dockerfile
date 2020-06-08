#linux
FROM mcr.microsoft.com/dotnet/runtime-deps:5.0
LABEL maintainer https://github.com/WaToI/ \
      version="1.0"

WORKDIR /app

#not work PublishSingleFile
COPY "./bin/CodeBlockDiagram" CodeBlockDiagram 
COPY "./bin/CodeBlockDiagram.pdb" CodeBlockDiagram.pdb

#alpine #not work
#https://github.com/dotnet/dotnet-docker/blob/5e45addd0fca6b52a49eb7780fd13a7ee5b7ce07/5.0/runtime-deps/alpine3.11/amd64/Dockerfile