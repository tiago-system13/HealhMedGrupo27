# Use a imagem oficial do Microsoft SQL Server para Linux
FROM mcr.microsoft.com/mssql/server:latest

# Variáveis de ambiente necessárias para a configuração do SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Express

# Exponha a porta padrão do SQL Server
EXPOSE 1433

# Comando para iniciar o SQL Server
CMD ["/opt/mssql/bin/sqlservr"]