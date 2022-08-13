using System;
using System.Xml;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace Coop.GameInterface.Serializers.PropertyOwnerSerializers
{
    [Serializable]
    public class CharacterPerksSerializer : PropertyOwnerSerializer<PerkObject>
    {
        public CharacterPerksSerializer(CharacterPerks characterPerks) : base(characterPerks) { }

        public override object Deserialize()
        {
            XmlNode node = (XmlNode)base.Deserialize();
            CharacterPerks characterPerks = new CharacterPerks();
            characterPerks.Deserialize(Game.Current.ObjectManager, node);
            return characterPerks;
        }
    }
}