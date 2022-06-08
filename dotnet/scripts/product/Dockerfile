FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY ./product_svc/ .
RUN dotnet restore
RUN dotnet publish -o /app/product_svc

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/product_svc /app
ENV ASPNETCORE_URLS=http://+:3002
ENTRYPOINT [ "dotnet", "/app/product_svc.dll", "--server.urls", "http://*:3002"]
