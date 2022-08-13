using System;
using Coop.GameInterface.Serializers.CustomSerializers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;

namespace Coop.GameInterface.Serializers.NewPlayer
{
    [Serializable]
    internal class PlayerHeroLastSeenInformationSerializer : ICustomSerializer
    {
        private PlayerSettlementSerializer lastSeenPlace;
        private CampaignTimeSerializer lastSeenDate;
        private bool isSettlementNearby;

        public PlayerHeroLastSeenInformationSerializer(Hero.HeroLastSeenInformation heroLastSeenInformation)
        {
            lastSeenPlace = new PlayerSettlementSerializer(heroLastSeenInformation.LastSeenPlace);
            lastSeenDate = new CampaignTimeSerializer(heroLastSeenInformation.LastSeenDate);
            isSettlementNearby = heroLastSeenInformation.IsNearbySettlement;
        }

        public object Deserialize()
        {
            return new Hero.HeroLastSeenInformation
            { 
                LastSeenPlace = (Settlement)lastSeenPlace.Deserialize(),
                LastSeenDate = (CampaignTime)lastSeenDate.Deserialize(),
                IsNearbySettlement = isSettlementNearby,
            };
        }

        public void ResolveReferenceGuids()
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}