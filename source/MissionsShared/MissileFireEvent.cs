using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionsShared
{
    [ProtoContract]
    public class MissileFireEvent
    {
        [ProtoMember(1)]
        public string agentID { get; set; }

        [ProtoMember(2)]
        public int weaponIndex { get; set; }

        [ProtoMember(3)]
        public float posX { get; set; }

        [ProtoMember(4)]
        public float posY { get; set; }

        [ProtoMember(5)]
        public float posZ { get; set; }

        [ProtoMember(6)]
        public float dirX { get; set; }

        [ProtoMember(7)]
        public float dirY { get; set; }

        [ProtoMember(8)]
        public float dirZ { get; set; }

        [ProtoMember(9)]
        public float sx { get; set; }

        [ProtoMember(10)]
        public float sy { get; set; }

        [ProtoMember(11)]
        public float sz { get; set; }

        [ProtoMember(12)]
        public float fx { get; set; }

        [ProtoMember(13)]
        public float fy { get; set; }

        [ProtoMember(14)]
        public float fz { get; set; }

        [ProtoMember(15)]
        public float ux { get; set; }

        [ProtoMember(16)]
        public float uy { get; set; }

        [ProtoMember(17)]
        public float uz { get; set; }

        [ProtoMember(18)]
        public bool hasRigidBody { get; set; }

        [ProtoMember(19)]
        public float baseSpeed { get; set; }

        [ProtoMember(20)]
        public float speed { get; set; }
    }
}
