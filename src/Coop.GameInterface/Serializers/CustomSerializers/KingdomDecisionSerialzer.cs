using TaleWorlds.CampaignSystem.Election;

namespace Coop.GameInterface.Serializers.CustomSerializers
{
    public class KingdomDecisionSerialzer : CustomSerializer
    {
        public KingdomDecisionSerialzer(KingdomDecision obj) : base(obj)
        {

        }

        public override object Deserialize()
        {
            //TODO
            return null;
        }

        public override void ResolveReferenceGuids()
        {
            //TODO
        }
    }
}