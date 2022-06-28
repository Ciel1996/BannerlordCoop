﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace CoopTestMod
{
    public class NetworkAgent
    {
        public int ClientID { get; private set; }
        public int AgentIndex { get; private set; }
        public string AgentID { get; private set; }
        public Agent Agent { get; private set; }
        public bool IsHostSyncAgent {get; private set; }

        public NetworkAgent(int clientId, int agentIndex, string agentId, Agent agent, bool isHostSyncAgent)
        {
            ClientID = clientId;
            AgentID = agentId;
            Agent = agent;
            IsHostSyncAgent = isHostSyncAgent;
            AgentIndex = agentIndex;
        }
    }
}