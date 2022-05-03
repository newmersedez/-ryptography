namespace DES.Interfaces
{
    public interface ICypherTransform
    {
        public byte[] CypherTransform(byte[] block, byte[] roundKey);
    }
}