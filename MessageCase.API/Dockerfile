#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MessageCase.API/MessageCase.API.csproj", "MessageCase.API/"]
COPY ["MessageCase.Repository/MessageCase.Repository.csproj", "MessageCase.Repository/"]
COPY ["MessageCase.Data/MessageCase.Data.csproj", "MessageCase.Data/"]
RUN dotnet restore "MessageCase.API/MessageCase.API.csproj"
COPY . .
WORKDIR "/src/MessageCase.API"
RUN dotnet build "MessageCase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageCase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageCase.API.dll"]