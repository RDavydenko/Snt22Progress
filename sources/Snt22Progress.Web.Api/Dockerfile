#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Snt22Progress.Web.Api/Snt22Progress.Web.Api.csproj", "Snt22Progress.Web.Api/"]
COPY ["Snt22Progress.Contracts/Snt22Progress.Contracts.csproj", "Snt22Progress.Contracts/"]
COPY ["Snt22Progress.DataAccess/Snt22Progress.DataAccess.csproj", "Snt22Progress.DataAccess/"]
COPY ["Snt22Progress.BussinesLogic/Snt22Progress.BussinesLogic.csproj", "Snt22Progress.BussinesLogic/"]
COPY ["Snt22Progress.FileManager/Snt22Progress.FileManager.csproj", "Snt22Progress.FileManager/"]
COPY ["Snt22Progress.Logging/Snt22Progress.Logging.csproj", "Snt22Progress.Logging/"]
RUN dotnet restore "Snt22Progress.Web.Api/Snt22Progress.Web.Api.csproj"
COPY . .
WORKDIR "/src/Snt22Progress.Web.Api"
RUN dotnet build "Snt22Progress.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snt22Progress.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snt22Progress.Web.Api.dll"]