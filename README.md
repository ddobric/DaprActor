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

### How does it work?
When one application is started inside of the *dapr* runtime, the service (deamon) daprd.exe (on Windows) is started. This service establishes the connecttion between your application and all other service placed inside of the *dapr* runtime. The service *daprd* acts as a proxy (by outgoing calls) and reverse proxy (by incomming calls) in the *SideCar* architecture. Every service provides one HTTP and  one gRPC endpoint. Applications, running also outside of the runtime, can use these endpoints to communicate with application inside of the *dapr* environment.
Following picture shows the example described in this post.

![image](https://user-images.githubusercontent.com/1756871/95105991-34c7bb80-0738-11eb-9061-1a9bb44eb01f.png)

### Talking to actor
You can invoke Actor methods without remoting (directly over http), if the Actor method accepts at-most one argument. Actor runtime will deserialize the incoming request body from client and use it as method argument to invoke the actor method. When making non-remoting calls Actor method arguments and return types are serialized, deserialized as JSON.

Save Data Following curl call will save data for actor id "abc" (below calls on MacOs, Linux & Windows are exactly the same except for escaping quotes on Windows for curl)

On Linux, MacOS:

~~~
curl -X POST http://127.0.0.1:3500/v1.0/actors/MachineActor/abc/method/SetDataAsync -d '{ "Temperature": 100, "Speed": 200 }'
~~~

On Windows in command prompt:

~~~
curl -X POST http://127.0.0.1:3500/v1.0/actors/MachineActor/abc/method/SetDataAsync -d "{ \"Temperature\": 28, \"Speed\": 200 }"
~~~
Get Data Following curl call will get data for actor id "abc" (below calls on MacOs, Linux & Windows are exactly the same except for escaping quotes on Windows for curl)

On Linux, MacOS:

~~~
curl -X POST http://127.0.0.1:3500/v1.0/actors/MachineActor/abc/method/GetDataAsync
~~~
On Windows:

~~~
curl -X POST http://127.0.0.1:3500/v1.0/actors/MachineActor/abc/method/GetDataAsync
~~~

**NOTE**

If you get following exception:

~~~
Invoke-WebRequest : A parameter cannot be found that matches parameter name 'X'.
At line:1 char:6
+ curl -X POST http://127.0.0.1:3500/v1.0/actors/MachineActor/abc/metho ...
+      ~~
+ CategoryInfo          : InvalidArgument: (:) [Invoke-WebRequest], ParameterBindingException
+ FullyQualifiedErrorId : NamedParameterNotFound,Microsoft.PowerShell.Commands.InvokeWebRequestCommand
~~~

Then execute 

~~~
remove-item alias:\curl
~~~

Finally, exeecute the *curl* command again.

