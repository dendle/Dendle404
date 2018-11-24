FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 8080

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Dendle404/Dendle404.csproj", "Dendle404/"]
RUN dotnet restore "Dendle404/Dendle404.csproj"
COPY . .
WORKDIR "/src/Dendle404"
RUN dotnet build "Dendle404.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Dendle404.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dendle404.dll"]