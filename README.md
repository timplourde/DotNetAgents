Reads in config from env vars, appsettings.json, command line args

```
cd DotNetAgent.CLI
dotnet publish -c Release
docker build -t dotnetagentcli:latest -f .\Dockerfile .
docker run -it -e AgentConfig:MaxParallelRequests=999 --rm dotnetagentcli
```

## Ideas

Sqlite Table
    Ticker
    LatestPrice
    LastUpdatedUtc
    LastFailedUtc
    ConsecutiveFailures

At X interval, ensure all tickers exist in the table (Timed)

On an ongoing basis (Background)

* Get top X jobs by LastUpdatedUtc where LastFailedUtc older than Y hours ago, for each async (task.whenall)
* if success: update job as successfully done (ConsecFailures = 0, LastUpdatedUtc = NowUtc)
* if fail: log error, ConsecFailures++, LastFailedUtc = NowUtc

## Links

* https://github.com/dfederm/GenericHostConsoleApp
* https://andrewlock.net/extending-the-shutdown-timeout-setting-to-ensure-graceful-ihostedservice-shutdown/
* https://girishgodage.in/blog/customize-hostedservices