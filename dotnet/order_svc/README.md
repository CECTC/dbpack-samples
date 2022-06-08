dotnet new webapi

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Pomelo.EntityFrameworkCore.MySql

dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold Name=OrderDB Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --namespace order_svc.Models --context-namespace order_svc.Data --context OrderContext -f --no-onconfiguring

dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson