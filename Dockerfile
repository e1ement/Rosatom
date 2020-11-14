FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Rosatom/*.csproj ./Rosatom/
WORKDIR /app/Rosatom
RUN dotnet restore Rosatom.csproj
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT='Development'
ENV logPath='/var/log'

# Copy everything else and build
WORKDIR /app
COPY . .
WORKDIR /app/Rosatom
ENV ASPNETCORE_ENVIRONMENT='Development'
RUN dotnet build Rosatom.csproj
WORKDIR /app/Rosatom
RUN dotnet publish Rosatom.csproj -c Release -o ../out
RUN dotnet ef database update

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV logPath='/var/log'
ENV ASPNETCORE_ENVIRONMENT='Development'
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Rosatom.dll"]