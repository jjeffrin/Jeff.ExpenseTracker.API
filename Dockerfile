FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Jeff.ExpenseTracker.API/Jeff.ExpenseTracker.API.csproj", "Jeff.ExpenseTracker.API/"] 
COPY ["Jeff.ExpenseTracker.Contracts/Jeff.ExpenseTracker.Contracts.csproj", "Jeff.ExpenseTracker.Contracts/"] 
COPY ["Jeff.ExpenseTracker.Core/Jeff.ExpenseTracker.Core.csproj", "Jeff.ExpenseTracker.Core/"] 
COPY ["Jeff.ExpenseTracker.DAL/Jeff.ExpenseTracker.DAL.csproj", "Jeff.ExpenseTracker.DAL/"] 
COPY ["Jeff.ExpenseTracker.Infrastructure/Jeff.ExpenseTracker.Infrastructure.csproj", "Jeff.ExpenseTracker.Infrastructure/"] 
RUN dotnet restore "Jeff.ExpenseTracker.API/Jeff.ExpenseTracker.API.csproj"
COPY . .
WORKDIR "/src/Jeff.ExpenseTracker.API"
RUN dotnet build "Jeff.ExpenseTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
run dotnet publish "Jeff.ExpenseTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jeff.ExpenseTracker.API.dll"]