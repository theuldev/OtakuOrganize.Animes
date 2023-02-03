

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["/AnimesControl.Api/AnimesControl.Api.csproj", "/AnimesControl.Api/"]
COPY ["/AnimesControl.Core/AnimesControl.Core.csproj","/AnimesControl.Core/"]
COPY ["/AnimesControl.Application/AnimesControl.Application.csproj","/AnimesControl.Application/"]
COPY ["/AnimesControl.Infra/AnimesControl.Infra.csproj","/AnimesControl.Infra/"]

RUN dotnet restore "/AnimesControl.Api/AnimesControl.Api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AnimesControl.Api/AnimesControl.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./AnimesControl.Api/AnimesControl.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AnimesControl.Api.dll"]