# FROM mcr.microsoft.com/dotnet/runtime:9.0 base

# WORKDIR /home

# COPY EDPortTest/ /home

# LABEL org.opencontainers.image.description="ED Port Test server, written in C#"
# LABEL org.opencontainers.image.authors="Niceygy (Ava Whale)"

# # RUN chmod 777 EDPortTest

# # RUN ls /
# # RUN ls /home

# CMD [ "dotnet run" ]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore 
# Build and publish a release
RUN dotnet publish -o out --arch arm64 --os linux

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:9.0-azurelinux3.0-distroless
WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["./EDPortTest"]