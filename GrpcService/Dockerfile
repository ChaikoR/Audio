#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#https://medium.com/@vedkoditkar/connect-to-local-ms-sql-server-from-docker-container-9d2b3d33e5e9
#https://vivekcek.wordpress.com/2018/06/10/connecting-to-local-or-remote-sql-server-from-docker-container/
#Connecting to local or remote SQL Server from Docker
#docker ps -a

#> net stop winnat
#> docker start ...
#> net start winnat

#1460

#7298



FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 1460
EXPOSE 80
EXPOSE 443




FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcService/GrpcService.csproj", "GrpcService/"]
RUN dotnet restore "GrpcService/GrpcService.csproj"
COPY . .
WORKDIR "/src/GrpcService"
RUN dotnet build "GrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcService.csproj" -c Release -o /app/publish

#run -p 127.0.0.1:7298:80

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
