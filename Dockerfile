FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /build

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -

RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY /CRMServer/CRMServer/CRMServer.csproj .
RUN dotnet restore

# copy everything else and build app
COPY /CRMServer/. .
WORKDIR /build
RUN dotnet publish -c release -o published --no-cache

# final stage/image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /build/published ./
ENTRYPOINT ["dotnet", "CRMServer.dll"]