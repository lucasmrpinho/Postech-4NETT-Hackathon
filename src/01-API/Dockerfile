FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011
ENV ASPNETCORE_HTTP_PORTS=5011

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/01-API/PosTech.Hackathon.Pacientes.API/PosTech.Hackathon.Pacientes.API.csproj", "src/01-API/PosTech.Hackathon.Pacientes.API/"]

RUN dotnet restore "src/01-API/PosTech.Hackathon.Pacientes.API/PosTech.Hackathon.Pacientes.API.csproj"
COPY . .
WORKDIR "/src/src/01-API/PosTech.Hackathon.Pacientes.API"
RUN dotnet build "PosTech.Hackathon.Pacientes.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "PosTech.Hackathon.Pacientes.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PosTech.Hackathon.Pacientes.API.dll"]