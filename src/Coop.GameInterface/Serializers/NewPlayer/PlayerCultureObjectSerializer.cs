using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace Coop.GameInterface.Serializers.NewPlayer
{
    [Serializable]
    internal class PlayerCultureObjectSerializer : CustomSerializer
    {
        string stringId;
        public PlayerCultureObjectSerializer(CultureObject culture)
        {
            // TODO Find way to work better with other mods
            stringId = culture.StringId;
        }

        public override object Deserialize()
        {
            CultureObject cultureObject = MBObjectManager.Instance.GetObject<CultureObject>(stringId);
            return cultureObject;
        }

        public override void ResolveReferenceGuids()
        {
            throw new NotImplementedException();
        }
    }
}