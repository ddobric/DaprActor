using System;
using System.Collections.Generic;
using System.Text;

namespace DaperActor.Interfaces
{
    public class MachineData
    {
        public float Temperature { get; set; }

        public float Speed { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"T={this.Temperature} / S={this.Speed} / TS={this.Timestamp}";
        }
    }
}
