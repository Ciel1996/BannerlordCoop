using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.ObjectSystem;

namespace Coop.GameInterface.Serializers.CustomSerializers
{
    [Serializable]
    public class TraitObjectSerializer : CustomSerializer
    {
        string StringId;
        public TraitObjectSerializer(MBObjectBase obj) : base(obj)
        {
            StringId = obj.StringId;
        }

        public override object Deserialize()
        {

            TraitObject newTraitObject = new TraitObject(StringId);
            return base.Deserialize(newTraitObject);
        }

        public override void ResolveReferenceGuids()
        {
            // No references
        }
    }
}
