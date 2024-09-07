FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StudySignalR.ConsoleApp/StudySignalR.ConsoleApp.csproj", "StudySignalR.ConsoleApp/"]
RUN dotnet restore "StudySignalR.ConsoleApp/StudySignalR.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/StudySignalR.ConsoleApp"
RUN dotnet build "StudySignalR.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudySignalR.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudySignalR.ConsoleApp.dll"]
