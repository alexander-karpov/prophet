## Issues

[How to use .NET Core on RHEL 6 / CentOS 6](https://github.com/dotnet/core/blob/master/Documentation/build-and-install-rhel6-prerequisites.md)

[Docker for Mac stuck on "is starting"](https://github.com/docker/for-mac/issues/2420)

Если при проксировании через nginx, на запросы к приложению возвращается `502 Bad Gateway`, а в логах
`Permissions denied`, то дело в системе контроля прав SELinux. Лечится так

```
setsebool -P httpd_can_network_connect on
```

## Run container

`docker run -d -p 5000:80 -v /data:/data --restart=always --name=prophet alexanderkarpov/prophet`
