# Используем официальный .NET 8 SDK для сборки
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Переходим к папке приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY ["TazzkerAPI/TazzkerAPI.csproj", "TazzkerAPI/"]
RUN dotnet restore "TazzkerAPI/TazzkerAPI.csproj"

# Копируем остальной код и билдим
COPY . .
WORKDIR "/src/TazzkerAPI"
RUN dotnet publish "TazzkerAPI.csproj" -c Release -o /app/publish

# Запуск
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TazzkerAPI.dll"]
