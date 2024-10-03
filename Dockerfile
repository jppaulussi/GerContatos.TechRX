# Use uma imagem base oficial do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie todos os arquivos de projeto para o contêiner
COPY GerContatos.API/GerContatos.API.csproj ./GerContatos.API/
COPY Business/Business.csproj ./Business/
COPY Core/Core.csproj ./Core/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/

# Restaure as dependências
RUN dotnet restore ./GerContatos.API/GerContatos.API.csproj

# Copie o restante dos arquivos do projeto
COPY . .

# Defina o diretório de trabalho para o projeto GerContatos.API
WORKDIR /app/GerContatos.API

# Faça o build da aplicação, especificando o arquivo .csproj
RUN dotnet publish ./GerContatos.API.csproj -c Release -o out

# Use uma imagem do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/GerContatos.API/out .

# Exponha a porta que a aplicação vai usar
EXPOSE 80

# Comando de entrada
ENTRYPOINT ["dotnet", "GerContatos.API.dll"]
