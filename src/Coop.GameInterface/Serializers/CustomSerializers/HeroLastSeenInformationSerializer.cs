using System;
using Common;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using static TaleWorlds.CampaignSystem.Hero;

namespace Coop.GameInterface.Serializers.CustomSerializers
{
    [Serializable]
    public class HeroLastSeenInformationSerializer : ICustomSerializer
    {
        [NonSerialized]
        HeroLastSeenInformation newHeroLastSeenInformation;

        private Guid lastSeenPlace;
        private CampaignTimeSerializer lastSeenDate;
        private bool isSettlementNearby;

        public HeroLastSeenInformationSerializer(HeroLastSeenInformation heroLastSeenInformation)
        {
            if(heroLastSeenInformation.LastSeenPlace != null)
            {
                lastSeenPlace = CoopObjectManager.GetGuid(heroLastSeenInformation.LastSeenPlace);
            }
            
            lastSeenDate = new CampaignTimeSerializer(heroLastSeenInformation.LastSeenDate);
            isSettlementNearby = heroLastSeenInformation.IsNearbySettlement;
        }

        public object Deserialize()
        {
            newHeroLastSeenInformation = new HeroLastSeenInformation
            { 
                LastSeenPlace = null,
                LastSeenDate = (CampaignTime)lastSeenDate.Deserialize(),
                IsNearbySettlement = isSettlementNearby,
            };
            return newHeroLastSeenInformation;
        }

        public void ResolveReferenceGuids()
        {
            newHeroLastSeenInformation.LastSeenPlace = CoopObjectManager.GetObject<Settlement>(lastSeenPlace);
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}