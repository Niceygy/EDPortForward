dotnet publish -c Release -r linux-arm64 --self-contained true /p:PublishSingleFile=true

# Build the Docker image
docker build -t niceygy/edportforward .

# Tag the Docker image
docker tag niceygy/edportforward ghcr.io/niceygy/edportforward:latest

# Push the Docker image to GH registry
docker push ghcr.io/niceygy/edportforward:latest

#Update local container

cd /opt/stacks/elite_apps

docker compose pull

docker compose down

docker compose up -d

docker logs edportforward -f