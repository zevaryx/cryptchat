FROM mcr.microsoft.com/dotnet/core/runtime:3.1

COPY bin/Release/Linux/ server/

WORKDIR server

ENTRYPOINT ["CryptChatServerTCP"]
