#Assembly stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

#copy sln and csproj-files

COPY *.sln ./
COPY TazzkerAPI/*.csproj ./TazzkerAPI/
COPY Tazzker.Application/*.csproj ./Tazzker.Application/
COPY Tazzker.Infrastructure/*.csproj ./Tazzker.Infrastructure/
COPY Tazzker.Domain/*.csproj ./Tazzker.Domain/

#restoring dependencies
RUN dotnet restore

#copying remaining code
COPY . .

#project assembly

RUN dotnet publish TazzkerAPI/TazzkerAPI.csproj -c Release -o /app/out

#launch stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "TazzkerAPI.dll"]