# Usar uma imagem base do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar a solução e todos os projetos
COPY GerContatos.TechRX.sln ./
COPY Business/Business.csproj Business/
COPY Core/Core.csproj Core/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Model/Model.csproj Model/
COPY GerContatos.API/GerContatos.API.csproj GerContatos.API/
COPY Testes/Testes.csproj Testes/

# Restaurar as dependências
RUN dotnet restore

# Copiar o restante dos arquivos e compilar a aplicação
COPY . .

# Compilar a aplicação
RUN dotnet publish GerContatos.API/GerContatos.API.csproj -c Release -o out

# Criar a imagem final usando a imagem do .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Definir a URL da aplicação para escutar na porta 5046
ENV ASPNETCORE_URLS=http://+:5046

# Expor a porta da aplicação
EXPOSE 5046
ENTRYPOINT ["dotnet", "GerContatos.API.dll"]
