FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["StudySignalR/StudySignalR.csproj", "StudySignalR/"]
RUN dotnet restore "StudySignalR/StudySignalR.csproj"
COPY . .
WORKDIR "/src/StudySignalR"
RUN dotnet build "StudySignalR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudySignalR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudySignalR.dll"]
