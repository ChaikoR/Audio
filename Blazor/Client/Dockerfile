#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7777
EXPOSE 5555
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blazor/Client/Blazor.Client.csproj", "Blazor/Client/"]
COPY ["Blazor/Shared/Blazor.Shared.csproj", "Blazor/Shared/"]
RUN dotnet restore "Blazor/Client/Blazor.Client.csproj"
COPY . .
WORKDIR "/src/Blazor/Client"
RUN dotnet build "Blazor.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor.Client.dll"]
