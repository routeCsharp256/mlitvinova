FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src 

COPY ["src/MerchandiseService/MerchandiseService.csproj","src/MerchandiseService/"]

RUN dotnet restore "src/MerchandiseService/MerchandiseService.csproj"

COPY . .

WORKDIR "/src/src/MerchandiseService"

RUN dotnet build "MerchandiseService.csproj" -c -Release -o /app/build

FROM build as publish

RUN dotnet publish "MerchandiseService.csproj" -c -Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "MerchandiseService.dll" ]