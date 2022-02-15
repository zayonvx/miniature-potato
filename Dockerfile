FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7136

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OnePlan.Web/OnePlan.Web.csproj", "OnePlan.Web/"]
COPY ["OnePlan.Data/OnePlan.Data.csproj", "OnePlan.Data/"]
COPY ["OnePlan.Business/OnePlan.Business.csproj", "OnePlan.Business/"]
COPY ["OnePlan.Core/OnePlan.Core.csproj", "OnePlan.Core/"]
RUN dotnet restore "OnePlan.Web/OnePlan.Web.csproj"
COPY . .
WORKDIR "/src/OnePlan.Web"
RUN dotnet build "OnePlan.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnePlan.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:7136
ENV ASPNETCORE_ENVIRONMENT "Development"
ENTRYPOINT ["dotnet", "OnePlan.Web.dll"]
