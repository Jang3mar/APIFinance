FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY api/api.csproj api/
RUN dotnet restore api/api.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "api.dll"]