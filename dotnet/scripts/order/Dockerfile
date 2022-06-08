FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY ./order_svc/ .
RUN dotnet restore
RUN dotnet publish -o /app/order_svc

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/order_svc /app
ENV ASPNETCORE_URLS=http://+:3001
ENTRYPOINT [ "dotnet", "/app/order_svc.dll", "--server.urls", "http://*:3001"]
