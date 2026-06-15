FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TestDevCore.Api/TestDevCore.Api.csproj", "src/TestDevCore.Api/"]
COPY ["src/TestDevCore.Application/TestDevCore.Application.csproj", "src/TestDevCore.Application/"]
COPY ["src/TestDevCore.Core/TestDevCore.Core.csproj", "src/TestDevCore.Core/"]
COPY ["src/TestDevCore.Domain/TestDevCore.Domain.csproj", "src/TestDevCore.Domain/"]
COPY ["src/TestDevCore.Infrastructure/TestDevCore.Infrastructure.csproj", "src/TestDevCore.Infrastructure/"]
RUN dotnet restore "src/TestDevCore.Api/TestDevCore.Api.csproj"
COPY . .
WORKDIR "/src/src/TestDevCore.Api"
RUN dotnet build "./TestDevCore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TestDevCore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestDevCore.Api.dll"]