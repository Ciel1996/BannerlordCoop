﻿
using System;

namespace Coop.Core.Server.States
{
    public class InitialServerState : ServerState
    {
        public InitialServerState(IServerLogic context) : base(context)
        {
        }

        public override void StartServer()
        {
            throw new NotImplementedException();
        }

        public override void StopServer()
        {
            throw new NotImplementedException();
        }
    }
}
