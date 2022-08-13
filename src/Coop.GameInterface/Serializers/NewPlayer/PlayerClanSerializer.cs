﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Coop.GameInterface.Serializers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace Coop.Mod.Serializers
{
    [Serializable]
    class PlayerClanSerializer : CustomSerializer
    {
        /// <summary>
        /// Used for circular reference
        /// </summary>
        [NonSerialized]
        Hero _leader;

        /// <summary>
        /// Serialized Natively Non Serializable Objects (SNNSO)
        /// </summary>
        Dictionary<FieldInfo, ICustomSerializer> SNNSO = new Dictionary<FieldInfo, ICustomSerializer>();

        string stringId;

        public PlayerClanSerializer(Clan clan) : base(clan)
        {
            List<string> UnmanagedFields = new List<string>();

            stringId = clan.StringId;

            foreach (FieldInfo fieldInfo in NonSerializableObjects)
            {
                // Get value from fieldInfo
                object value = fieldInfo.GetValue(clan);

                // If value is null, no need to serialize
                if (value == null)
                {
                    continue;
                }

                // Assign serializer to nonserializable objects
                switch (fieldInfo.Name)
                {
                    case "<Id>k__BackingField":
                        // Ignore current MB id
                        break;
                    case "<Name>k__BackingField":
                    case "<InformalName>k__BackingField":
                    case "<EncyclopediaText>k__BackingField":
                        SNNSO.Add(fieldInfo, new TextObjectSerializer((TextObject)value));
                        break;

                    case "<Culture>k__BackingField":
                        SNNSO.Add(fieldInfo, new PlayerCultureObjectSerializer((CultureObject)value));
                        break;
                    case "<LastFactionChangeTime>k__BackingField":
                        SNNSO.Add(fieldInfo, new Custom.CampaignTimeSerializer((CampaignTime)value));
                        break;
                    case "<SupporterNotables>k__BackingField":
                        foreach (Hero hero in (MBReadOnlyList<Hero>)value)
                        {
                            throw new Exception("Should be no Supporters");
                        }
                        break;
                    case "<Companions>k__BackingField":
                        foreach (Hero hero in (MBReadOnlyList<Hero>)value)
                        {
                            throw new Exception("Should be no compainions");
                        }
                        break;
                    case "<CommanderHeroes>k__BackingField":
                        foreach (Hero hero in (MBReadOnlyList<Hero>)value)
                        {
                            throw new Exception("Should be no Commanders");
                        }
                        break;
                    case "_basicTroop":
                        SNNSO.Add(fieldInfo, new PlayerCharacterObjectSerializer((CharacterObject)value));
                        break;
                    case "_leader":
                        // Assigned by SetHeroReference on deserialization
                        break;
                    case "_banner":
                        SNNSO.Add(fieldInfo, new Custom.BannerSerializer((Banner)value));
                        break;
                    case "_home":
                        SNNSO.Add(fieldInfo, new PlayerSettlementSerializer((Settlement)value));
                        break;
                    case "<NotAttackableByPlayerUntilTime>k__BackingField":
                        SNNSO.Add(fieldInfo, new Custom.CampaignTimeSerializer((CampaignTime)value));
                        break;
                    case "_defaultPartyTemplate":
                        SNNSO.Add(fieldInfo, new Custom.DefaultPartyTemplateSerializer((PartyTemplateObject)value));
                        break;
                    case "<Fiefs>k__BackingField":
                    case "_villagesReadOnlyCache":
                    case "_settlementsReadOnlyCache":
                    case "_clanMidSettlement":
                    case "<Lords>k__BackingField":
                    case "<Heroes>k__BackingField":
                    case "<WarPartyComponents>k__BackingField":
                    case "_kingdom":
                        // TODO fix
                        break;
                    default:
                        UnmanagedFields.Add(fieldInfo.Name);
                        break;
                }
            }

            if (!UnmanagedFields.IsEmpty())
            {
                throw new NotImplementedException($"Cannot serialize {UnmanagedFields}");
            }

            // TODO manage collections

            // Remove non serializable objects before serialization
            // They are marked as nonserializable in CustomSerializer but still tries to serialize???
            NonSerializableCollections.Clear();
            NonSerializableObjects.Clear();
        }

        /// <summary>
        /// For assigning PlayerHeroSerializer reference for deserialization
        /// </summary>
        /// <param name="leader">PlayerHeroSerializer used by _leader</param>
        public void SetHeroReference(Hero leader)
        {
            _leader = leader;
        }

        public override object Deserialize()
        {
            Clan newClan = Clan.CreateClan(stringId);

            // Circular referenced object needs assignment before deserialize
            if (_leader == null)
            {
                throw new SerializationException("Must set hero reference before deserializing. Use SetHeroReference()");
            }

            // Circular referenced objects
            newClan.GetType().GetField("_leader", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(newClan, _leader);

            // Objects requiring a custom serializer
            foreach (KeyValuePair<FieldInfo, ICustomSerializer> entry in SNNSO)
            {
                switch (entry.Value)
                {
                    case PlayerCharacterObjectSerializer characterObjectSerializer:
                        characterObjectSerializer.SetHeroReference(_leader);
                        break;
                }

                entry.Key.SetValue(newClan, entry.Value.Deserialize());
            }
            
            return base.Deserialize(newClan);
        }

        public override void ResolveReferenceGuids()
        {
            throw new NotImplementedException();
        }
    }
}
