Background Windows Service to connect remote server by ssh with key

to create service open terminal and execute

```
sc create {WindowsServiceName} binpath="{FullPathToExeFile}" start="auto" displayname="{DisplayName}"
```
