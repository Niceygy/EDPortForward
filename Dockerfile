FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /home

COPY EDPortTest/bin/Release/net9.0/linux-arm64/publish .

LABEL org.opencontainers.image.description="ED Port Test server, written in C#"
LABEL org.opencontainers.image.authors="Niceygy (Ava Whale)"

RUN chmod 777 EDPortTest

CMD [ "./EDPortTest" ]