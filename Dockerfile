# Use uma imagem base oficial do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copie o arquivo .csproj e restaure as dependências
COPY GerContatos.API/GerContatos.API.csproj ./GerContatos.API/
RUN dotnet restore ./GerContatos.API/GerContatos.API.csproj

# Copie todo o restante dos arquivos do projeto
COPY . ./GerContatos.API/
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
