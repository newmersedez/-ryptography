﻿using System;
using System.Numerics;
using RSA.Interfaces;

namespace RSA.Classes
{
    public class FermaPrimalityTest : IPrimalityTest
    {
        public bool SimplicityTest(BigInteger n, double minProbability)
        {
            if (minProbability is not (>= 0.5 and < 1))
                throw new ArgumentOutOfRangeException(nameof(minProbability), "minProbability must be [0.5, 1)");
            if (n == 1)
                return false;
            
            //TODO: Set of generated numbers
            for (var i = 0; 1.0 - Math.Pow(2, -i) <= minProbability; ++i)
            {
                var a = Utils.MathUtils.GenerateRandomInteger(2, n - 1);
                if (BigInteger.ModPow(a, n - 1, n) != 1)
                    return false;
            }

            return true;
        }
    }
}   