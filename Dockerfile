# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app 

COPY src/ .

WORKDIR /app/CashFlow.API

RUN dotnet restore 
RUN dotnet publish -c Release -o /app/out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

# 🔥 ESSENCIAL PRO AZURE
ENV ASPNETCORE_URLS=http://+:80

# 🔥 ESSENCIAL PRO AZURE
EXPOSE 80

ENTRYPOINT ["dotnet", "CashFlow.API.dll"]