using System;
using Common;

namespace Coop.GameInterface.Serializers
{
    [Serializable]
    public abstract class CustomSerializerWithGuid : CustomSerializer
    {
        public Guid Guid { get; private set; }

        protected CustomSerializerWithGuid(object obj) : base(obj)
        {
            Guid = CoopObjectManager.AddObject(obj);
        }

        protected override object Deserialize(object newObj)
        {
            object existingObj = CoopObjectManager.GetObject(Guid);
            if(existingObj != null)
            {
                return existingObj;
            }

            object deserialized = base.Deserialize(newObj);
            CoopObjectManager.RegisterExistingObject(Guid, deserialized);
            return deserialized;
        }
    }
}
