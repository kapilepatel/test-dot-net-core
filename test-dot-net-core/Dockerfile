From mcr.microsoft.com/dotnet/core/sdk:2.2
RUN mkdir /app
WORKDIR /app
COPY AG_MS_Authentication.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out
CMD ["dotnet", "out/AG_MS_Authentication.dll"]
