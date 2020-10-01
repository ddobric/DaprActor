using Dapr.Actors;
using System;
using System.Threading.Tasks;

namespace DaperActor.Interfaces
{
    public interface IMachineActor : IActor
    {
        Task<string> SetDataAsync(MachineData data);
        Task<MachineData> GetDataAsync();
        Task RegisterReminder();
        Task UnregisterReminder();
        Task RegisterTimer();
        Task UnregisterTimer();
    }
}
