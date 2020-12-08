using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PISLabs.Storage
{
    [System.Serializable]
    public class IncorrectTicketsDataException : System.Exception
    {
        public IncorrectTicketsDataException() { }
        public IncorrectTicketsDataException(string message) : base(message) 
        {
            Log.Error($"[TicketsData] [Exeption] {message}");       
        }
        public IncorrectTicketsDataException(string message, System.Exception inner) : base(message, inner) 
        {
            Log.Error($"[TicketsData] [Exeption] {message}; {inner.Message}");
        }
        protected IncorrectTicketsDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) 
        {
            Log.Error($"[TicketsData] [Exeption] {info.ToString()}; {context.ToString()}");
        }
    }
}
