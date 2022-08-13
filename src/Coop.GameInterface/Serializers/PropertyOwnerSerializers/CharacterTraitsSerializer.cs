using System;
using System.Xml;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace Coop.GameInterface.Serializers.PropertyOwnerSerializers
{
    [Serializable]
    public class CharacterTraitsSerializer : PropertyOwnerSerializer<TraitObject>
    {

        public CharacterTraitsSerializer(CharacterTraits characterTraits) : base(characterTraits) { }

        public override object Deserialize()
        {
            XmlNode node = (XmlNode)base.Deserialize();
            CharacterTraits characterTraits = new CharacterTraits();
            characterTraits.Deserialize(Game.Current.ObjectManager, node);
            return characterTraits;
        }
    }
}