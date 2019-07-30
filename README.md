# httpredirservice
HTTP/HTTPS redirect service.

This windows service redirect http (and https) requests, that going to domain controller (on address - domain.name), to webserver (on address - www.domain.name).

## System requirements
OS Windows, .NET Framework 2.0 or higher.

## Installation
Service installation (on domain controller) with cmd:
```
sc create httpredirservice displayname= "httpredirservice" binpath= "c:\httpredirservice\httpredirservice.exe" start= auto
```

Removing service:
```
sc delete httpredirservice
```

For ssl connections (https) need to create bind (replace certhash value for your certificate footprint value):
```
netsh http add sslcert ipport=0.0.0.0:443 certhash=e3129083900000000000000001 appid={89da6384-3455-42a1-9123-87bb4bc65816}
```
