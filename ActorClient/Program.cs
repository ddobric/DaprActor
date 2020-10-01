using DaperActor.Interfaces;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello from Actor Client!");

            bool i = true;

            while (i)
            {
                Thread.Sleep(1000);
            }

            for (int k = 0; k < 10; k++)
            {
                await SendTelemetryData(k.ToString());
            }

            for (int k = 0; k < 10; k++)
            {
                await ReadActorState (k.ToString());
            }
        }

        static async Task SendTelemetryData(string id)
        {
            var actorID = new ActorId(id);

            // Create Actor Proxy instance to invoke the methods defined in the interface
            var proxy = ActorProxy.Create(actorID, "MachineActor");

            // Need to specify the method name and response type explicitly
            var response = await proxy.InvokeAsync<MachineData, string>(nameof(IMachineActor.SetDataAsync), new MachineData()
            {
                Temperature = DateTime.Now.Second,
                Speed = DateTime.Now.Ticks,
                Timestamp = DateTime.Now
            });

            Console.WriteLine($"ActorId: {id} - {response}");
        }


        static async Task ReadActorState(string id)
        {
            var actorID = new ActorId(id);

            // Create Actor Proxy instance to invoke the methods defined in the interface
            var proxy = ActorProxy.Create(actorID, "MachineActor");

            // Need to specify the method name and response type explicitly
            var response = await proxy.InvokeAsync<MachineData, string>(nameof(IMachineActor.SetDataAsync), new MachineData()
            {
                Temperature = DateTime.Now.Second,
                Speed = DateTime.Now.Ticks,
                Timestamp = DateTime.Now
            });

            var telemetryData = await proxy.InvokeAsync<MachineData>(nameof(IMachineActor.GetDataAsync));

            Console.WriteLine(telemetryData);

            Console.WriteLine($"ActorId: {id} - {telemetryData}");
        }
    }
}
