using System.Numerics;

namespace RSA.Interfaces
{
    public interface IPrimalityTest
    {
        public bool SimplicityTest(BigInteger probability, double min);
    }
}