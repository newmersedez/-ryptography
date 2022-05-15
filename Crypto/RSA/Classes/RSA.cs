using System;
using System.Numerics;

namespace RSA.Classes
{
    public enum PrimalityTestMode
    {
        Ferma,
        SolovayStrassen,
        MillerRabin
    }

    public sealed class RSA
    {
        private struct Key
        {
            public BigInteger n;    // p * q
            public BigInteger e;    // public RSA key
            public BigInteger d;    // private RSA key
        }

        private sealed class RSAKeyGenerator
        {
            private readonly PrimalityTestMode _mode;
            private readonly BigInteger _minProbability;
            private readonly ulong _length;

            public RSAKeyGenerator(PrimalityTestMode mode, BigInteger minProbability, ulong length)
            {
                _mode = mode;
                _minProbability = minProbability;
                _length = length;
            }

            public Key GenerateKey()
            {
                throw new NotImplementedException();
            }

            private BigInteger GenerateRandomPrimeNumber()
            {
                throw new NotImplementedException();
            }
        }
        
        private readonly RSAKeyGenerator _keygen;
        private Key _key;

        public RSA(PrimalityTestMode mode, BigInteger minProbability, ulong length)
        {
            _keygen = new RSAKeyGenerator(mode, minProbability, length);
            _key = _keygen.GenerateKey();
        }

        public BigInteger Encrypt(BigInteger origin)
        {
            throw new NotImplementedException();
        }

        public BigInteger Decrypt(BigInteger origin)
        {
            throw new NotImplementedException();
        }

        public void GenerateNewKey()
        {
            _key = _keygen.GenerateKey();
        }
    }
}
