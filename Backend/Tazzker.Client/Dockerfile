# Строим клиент
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Tazzker.Client/Tazzker.Client.csproj", "Tazzker.Client/"]
RUN dotnet restore "Tazzker.Client/Tazzker.Client.csproj"
COPY . .
WORKDIR "/src/Tazzker.Client"
RUN dotnet publish "Tazzker.Client.csproj" -c Release -o /app/dist

# Используем nginx для раздачи клиента
FROM nginx:alpine
COPY --from=build /app/dist/wwwroot /usr/share/nginx/html
