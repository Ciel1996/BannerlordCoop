namespace Coop.GameInterface.Serializers
{
    public interface ICustomSerializer
    {
        byte[] Serialize();
        object Deserialize();
        void ResolveReferenceGuids();
    }
}
