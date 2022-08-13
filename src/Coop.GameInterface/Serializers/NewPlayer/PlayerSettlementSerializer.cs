using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace Coop.GameInterface.Serializers.NewPlayer
{
    [Serializable]
    internal class PlayerSettlementSerializer : CustomSerializer
    {
        private string settlementId;
        public PlayerSettlementSerializer(Settlement settlement)
        {
            if(settlement != null)
            {
                settlementId = settlement.StringId;
            }
            
        }

        public override object Deserialize()
        {
            if (settlementId != null)
            {
                return Settlement.Find(settlementId);
            }

            return null;
        }

        public override void ResolveReferenceGuids()
        {
            throw new NotImplementedException();
        }
    }
}