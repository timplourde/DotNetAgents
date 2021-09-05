Reads in config from env vars, appsettings.json, command line args

```
cd DotNetAgent.CLI
dotnet publish
docker build -t dotnetagentcli:latest -f .\Dockerfile .
docker run -it -e AgentConfig:MaxParallelRequests=999 --rm dotnetagentcli
```