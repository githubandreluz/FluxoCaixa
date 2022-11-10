FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FluxoDeCaixa.API/FluxoDeCaixa.API.csproj", "FluxoDeCaixa.API/"]
COPY ["FluxoDeCaixa.Application/FluxoDeCaixa.Application.csproj", "FluxoDeCaixa.Application/"]
COPY ["FluxoDeCaixa.Autenticacao/FluxoDeCaixa.Autenticacao.csproj", "FluxoDeCaixa.Autenticacao/"]
COPY ["FluxoDeCaixa.Consolidado.Service/FluxoDeCaixa.Consolidado.Service.csproj", "FluxoDeCaixa.Consolidado.Service/"]
COPY ["FluxoDeCaixa.Domain/FluxoDeCaixa.Domain.csproj", "FluxoDeCaixa.Domain/"]
COPY ["FluxoDeCaixa.Infra.Data/FluxoDeCaixa.Infra.Data.csproj", "FluxoDeCaixa.Infra.Data/"]
COPY ["FluxoDeCaixa.IoC/FluxoDeCaixa.IoC.csproj", "FluxoDeCaixa.IoC/"]


RUN dotnet restore "FluxoDeCaixa.API/FluxoDeCaixa.API.csproj"
COPY . .
WORKDIR "/src/FluxoDeCaixa.API"
RUN dotnet build "FluxoDeCaixa.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoDeCaixa.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FluxoDeCaixa.API.dll"]