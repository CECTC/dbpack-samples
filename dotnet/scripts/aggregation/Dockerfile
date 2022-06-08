FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY ./aggregation_svc/ .
RUN dotnet restore
RUN dotnet publish -o /app/aggregation_svc

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/aggregation_svc /app
ENV ASPNETCORE_URLS=http://+:3000
ENTRYPOINT [ "dotnet", "/app/aggregation_svc.dll", "--server.urls", "http://*:3000"]
