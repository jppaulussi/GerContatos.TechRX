# Usar uma imagem base do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o csproj e restaurar as dependências
COPY *.sln .
COPY *.csproj .
RUN dotnet restore

# Copiar o restante dos arquivos e compilar a aplicação
COPY . .
RUN dotnet publish -c Release -o out

# Criar a imagem final usando a imagem do .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Expor a porta da aplicação
EXPOSE 80
ENTRYPOINT ["dotnet", "GerContatos.API.dll"]
