using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionsShared
{
    [ProtoContract]
    public class AgentMountEvent
    {
        [ProtoMember(1)]
        public string mountAgentID { get; set; }

        [ProtoMember(2)]
        public string agentID { get; set; }

        [ProtoMember(3)]
        public bool isDismount { get; set; }
    }
}
