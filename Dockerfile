FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-preview8-alpine3.9

WORKDIR /app

COPY . /app

CMD ["/app/Prophet"]
