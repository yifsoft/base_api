FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN ls
COPY ["Presentation/Admin/Admin.csproj", "Admin/"]
RUN ls
RUN dotnet restore "Presentation/Admin/Admin/Admin.csproj"
COPY . .
RUN ls
WORKDIR "/src/Presentation/Admin"
RUN ls
RUN dotnet build "Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admin.dll]