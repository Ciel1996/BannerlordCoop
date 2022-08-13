using System;
using TaleWorlds.CampaignSystem;

namespace Coop.GameInterface.Serializers.CustomSerializers
{
    public class FactionSerializer : ICustomSerializer
    {
        CustomSerializer serializer;

        public FactionSerializer(IFaction faction)
        {
            if(faction is Clan clan)
            {
                serializer = new ClanSerializer(clan);
            }
            else if(faction is Kingdom kingdom)
            {
                serializer = new KingdomSerializer(kingdom);
            }
            else
            {
                throw new ArgumentException($"{faction.GetType()} is not handled by serializer.");
            }
        }

        public object Deserialize()
        {
            return serializer.Deserialize();
        }

        public void ResolveReferenceGuids()
        {
            serializer.ResolveReferenceGuids();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
