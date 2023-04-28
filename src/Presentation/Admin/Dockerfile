#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/Admin/Admin.csproj", "src/Presentation/Admin/"]
COPY ["src/Infrastructure/Infrastructure/Infrastructure.csproj", "src/Infrastructure/Infrastructure/"]
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Resource/Resource.csproj", "src/Core/Resource/"]
COPY ["src/Infrastructure/Persistence/Persistence.csproj", "src/Infrastructure/Persistence/"]
RUN dotnet restore "src/Presentation/Admin/Admin.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Admin"
RUN dotnet build "Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Admin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admin.dll"]