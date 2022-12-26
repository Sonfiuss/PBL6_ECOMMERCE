FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Website_Ecommerce.API/Website_Ecommerce.API.csproj", "Website_Ecommerce.API/"]
RUN dotnet restore "Website_Ecommerce.API/Website_Ecommerce.API.csproj" \
    && dotnet tool install --global dotnet-ef --version 6.0.0
COPY . .
WORKDIR "/src/Website_Ecommerce.API"
RUN dotnet publish "Website_Ecommerce.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Website_Ecommerce.API.dll"]
