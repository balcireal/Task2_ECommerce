FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Services/ProductService/ECommerceTask.API/ECommerceTask.API.csproj", "Services/ProductService/ECommerceTask.API/"]
COPY ["Services/ProductService/ECommerceTask.Core/ECommerceTask.Core.csproj", "Services/ProductService/ECommerceTask.Core/"]
COPY ["Services/ProductService/ECommerceTask.Application/ECommerceTask.Application.csproj", "Services/ProductService/ECommerceTask.Application/"]
COPY ["Services/ProductService/ECommerceTask.Infrastructure/ECommerceTask.Infrastructure.csproj", "Services/ProductService/ECommerceTask.Infrastructure/"]

RUN dotnet restore "Services/ProductService/ECommerceTask.API/ECommerceTask.API.csproj"

COPY . .

WORKDIR "/src/Services/ProductService/ECommerceTask.API"
RUN dotnet publish "ECommerceTask.API.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ECommerceTask.API.dll"]