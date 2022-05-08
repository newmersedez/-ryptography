using System;
using System.Numerics;
using RSA.Interfaces;

namespace RSA.Classes
{
    public class FermaPrimalityTest : IPrimalityTest
    {
        public bool SimplicityTest(BigInteger probability, double min)
        {
            if (min is not (>= 0.5 and < 1))
                throw new ArgumentOutOfRangeException(nameof(min), "Incorrect minimal probability");
            
            
            throw new System.NotImplementedException();
        }
    }
}