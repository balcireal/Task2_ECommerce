FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Services/AuthService/AuthService.API/AuthService.API.csproj", "Services/AuthService/AuthService.API/"]
COPY ["Services/AuthService/AuthService.Core/AuthService.Core.csproj", "Services/AuthService/AuthService.Core/"]
COPY ["Services/AuthService/AuthService.Application/AuthService.Application.csproj", "Services/AuthService/AuthService.Application/"]
COPY ["Services/AuthService/AuthService.Infrastructure/AuthService.Infrastructure.csproj", "Services/AuthService/AuthService.Infrastructure/"]


RUN dotnet restore "Services/AuthService/AuthService.API/AuthService.API.csproj"

COPY . .
WORKDIR "/src/Services/AuthService/AuthService.API"
RUN dotnet publish "AuthService.API.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]