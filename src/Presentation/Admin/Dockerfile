FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder
WORKDIR /sln

COPY ./*.sln ./NuGet.config  ./

# Copy the main source project files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

RUN dotnet restore

# Remainder of build process