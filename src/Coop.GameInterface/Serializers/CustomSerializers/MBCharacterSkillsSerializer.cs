using System;
using System.Reflection;
using Coop.GameInterface.Serializers.PropertyOwnerSerializers;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace Coop.GameInterface.Serializers.CustomSerializers
{
    [Serializable]
    public class MBCharacterSkillsSerializer : ICustomSerializer
    {
        CharacterSkillsSerializer characterSkillsSerializer;
        public MBCharacterSkillsSerializer(MBCharacterSkills value)
        {
            characterSkillsSerializer = new CharacterSkillsSerializer(value.Skills);
        }

        public object Deserialize()
        {
            MBCharacterSkills skills = MBObjectManager.Instance.CreateObject<MBCharacterSkills>();
            typeof(MBCharacterSkills).GetProperty("Skills")
                .SetValue(skills, characterSkillsSerializer.Deserialize(), BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);
            return skills;
        }

        public void ResolveReferenceGuids()
        {
            // No references
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}