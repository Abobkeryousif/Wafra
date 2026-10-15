
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["./Wafra.api/Wafra.api.csproj", "Wafra.api/"]
COPY ["./Wafra.Application/Wafra.Application.csproj", "Wafra.Application/"]
COPY ["./Wafra.Core/Wafra.Core.csproj", "Wafra.Core/"]
COPY ["./Wafra.Infrastructure/Wafra.Infrastructure.csproj", "Wafra.Infrastructure/"]

RUN dotnet restore "./Wafra.api/Wafra.api.csproj"

COPY . .

WORKDIR "/src/Wafra.api"
RUN dotnet build "Wafra.api.csproj" -c Release -o /app/build

RUN dotnet publish "Wafra.api.csproj" -c Release -o /app/publish /p:UseAppHost=flase

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

RUN apt-get update \
    && apt-get install -y libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Wafra.api.dll"]