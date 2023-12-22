#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["l2404/l2404.csproj", "l2404/"]
COPY ["Authentication/Authentication.csproj", "Authentication/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["ViewModel/ViewModel.csproj", "ViewModel/"]
COPY ["Entity/Entity.csproj", "Entity/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Utility/Utility.csproj", "Utility/"]
COPY ["Ioc/Ioc.csproj", "Ioc/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Logging/Logging.csproj", "Logging/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "l2404/l2404.csproj"
COPY . .
WORKDIR "/src/l2404"
RUN dotnet build "l2404.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "l2404.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "l2404.dll"]