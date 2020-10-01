# DaprActor
Machine Twin implementation with dapr Actor Model

### Run the service, which hostst the **MachineActor**
~~~csharp
$ dapr run --app-id machineactor --app-port 3000 --dapr-http-port 3500 dotnet run
~~~

### Run client as dapr application
$ dapr run --app-id actorclient --app-port 3000 --dapr-http-port 3500 dotnet run

### Run client as console application
The client can be run as console application too.

If you look for a tutorial, please read this: https://github.com/dapr/dotnet-sdk/blob/master/docs/get-started-dapr-actor.md
