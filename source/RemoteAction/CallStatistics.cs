﻿using System.Diagnostics;
using Common;
using RailgunNet.System.Types;

namespace RemoteAction
{
    public struct CallTrace
    {
        public MethodCall Call { get; set; }
        public Tick Tick { get; set; }
    }
    
    public class CallStatistics : DropoutStack<CallTrace>
    {
        private const int HistoryLength = 10;
        public CallStatistics() : base(HistoryLength)
        {
        }

        [Conditional("DEBUG")]
        public void Push(MethodCall call, Tick tick)
        {
            Push(new CallTrace()
            {
                Call = call,
                Tick = tick
            });
        }
    }
}