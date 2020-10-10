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

            //while (i)
            //{
            //    Thread.Sleep(1000);
            //}

            Console.WriteLine("Writing actor data...");

            await StartReminder();

            return;

            for (int k = 0; k < 10; k++)
            {
                await SendTelemetryData(k.ToString());
            }

            Console.WriteLine("Reading actor data...");

            for (int k = 0; k < 10; k++)
            {
                await ReadActorState (k.ToString());
            }

            Console.WriteLine("Press any key to exit...");

            Console.ReadLine();
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

        /// <summary>
        /// Reads the actor telemetry data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static async Task ReadActorState(string id)
        {
            var actorID = new ActorId(id);

            // Create Actor Proxy instance to invoke the methods defined in the interface
            var proxy = ActorProxy.Create(actorID, "MachineActor");

            var telemetryData = await proxy.InvokeAsync<MachineData>(nameof(IMachineActor.GetDataAsync));

            Console.WriteLine($"ActorId: {id} - {telemetryData}");

            var typedProxy = ActorProxy.Create<IMachineActor>(actorID, "MachineActor");

            telemetryData = await typedProxy.GetDataAsync();

            Console.WriteLine($"ActorId: {id} - {telemetryData}");
        }

        static async Task StartReminder()
        {
            var actorID = new ActorId("1");

            var typedProxy = ActorProxy.Create<IMachineActor>(actorID, "MachineActor");

            await typedProxy.RegisterReminder();

            Console.WriteLine("Reminder registered!");

        }
    }
}
