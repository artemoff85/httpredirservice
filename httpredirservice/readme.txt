This service redirect http/https requests domain.name to www.domain.name.

Service installation with cmd:
sc create httpredirservice displayname= "httpredirservice" binpath= "c:\httpredirservice\httpredirservice.exe" start= auto

Removing service:
sc delete httpredirservice

For ssl connections you can create bind (replace certhash for your certificate footprint):
netsh http add sslcert ipport=0.0.0.0:443 certhash=e312908390a309183010938083f1 appid={89da6384-3455-42a1-9123-87bb4bc65816}