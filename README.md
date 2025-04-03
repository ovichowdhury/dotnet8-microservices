# dotnet8-microservices

Demo Application for .NET 8 based Microservice Application Development

# SSL Certificate Generation Process

```bash
> dotnet dev-certs https --clean
> dotnet dev-certs https -ep ~/.aspnet/https/aspnetapp.pfx -p 1234
> dotnet dev-certs https --trust
```
