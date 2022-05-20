using System;
using System.Numerics;

namespace RSA.Classes
{
    public enum PrimalityTestMode
    {
        Fermat,
        SolovayStrassen,
        MillerRabin
    }

    public sealed class RSA
    {
        #region Nested

        private struct Key
        {
            public BigInteger n;
            public BigInteger e;
            public BigInteger d;
        }

        private sealed class RSAKeyGenerator
        {
            private readonly PrimalityTestMode _mode;
            private readonly double _minProbability;
            private readonly ulong _length;

            public RSAKeyGenerator(PrimalityTestMode mode, double minProbability, ulong length)
            {
                _mode = mode;
                _minProbability = minProbability;
                _length = length;
            }

            public Key GenerateKey()
            {
                var key = new Key();
                var p = GenerateRandomPrimeNumber();
                var q = GenerateRandomPrimeNumber();
                key.n = BigInteger.Multiply(p, q);
                var euler = BigInteger.Multiply(p - 1, q - 1);
                
                var random = new Random();
                var buffer = new byte[_length];
                while (true)
                {
                    while (true)
                    {
                        random.NextBytes(buffer);
                        var e = new BigInteger(buffer);
                        if (e > 3 && e < euler && BigInteger.GreatestCommonDivisor(e, euler) == 1)
                        {
                            key.e = e;
                            break;
                        }
                    }

                    BigInteger x;
                    BigInteger y;
                    var g = Utils.MathUtils.ExtendedEuclideanAlgorithm(key.e, euler, out x, out y);
                    if (g != 1)
                    {
                        throw new ArgumentException();
                    }
                    while (x < 0)
                    {
                        x += euler;
                    }
                    if (x <= (BigInteger) (0.3 * Math.Pow((double) key.n, 0.25)))
                    {
                        continue;
                    }
                    key.d = x;
                    break;
                }

                return key;
            }

            private BigInteger GenerateRandomPrimeNumber()
            {
                var random = new Random();
                var buffer = new byte[_length];
                while (true)
                {
                    random.NextBytes(buffer);
                    var primeNumber = new BigInteger(buffer);
                    if (primeNumber < 2)
                        continue;

                    switch (_mode)
                    {
                        case PrimalityTestMode.Fermat:
                        {
                            var test = new FermatPrimalityTest();
                            if (test.SimplicityTest(primeNumber, _minProbability))
                                return primeNumber;
                            break;
                        }
                        case PrimalityTestMode.SolovayStrassen:
                        {
                            var test = new SolovayStrassenPrimalityTest();
                            if (test.SimplicityTest(primeNumber, _minProbability))
                                return primeNumber;
                            break;
                        }
                        case PrimalityTestMode.MillerRabin:
                        {
                            var test = new MillerRabinPrimalityTest();
                            if (test.SimplicityTest(primeNumber, _minProbability))
                                return primeNumber;
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        #endregion

        #region Variables

        private readonly RSAKeyGenerator _keygen;
        private Key _key;

        #endregion

        #region Methods

        public RSA(PrimalityTestMode mode, double minProbability, ulong length)
        {
            _keygen = new RSAKeyGenerator(mode, minProbability, length);
            _key = _keygen.GenerateKey();
        }

        public BigInteger Encrypt(BigInteger origin)
        {
            return BigInteger.ModPow(origin, _key.e, _key.n);
        }

        public BigInteger Decrypt(BigInteger origin)
        {
            return BigInteger.ModPow(origin, _key.d, _key.n);
        }
        
        public void GenerateNewKey()
        {
            _key = _keygen.GenerateKey();
        }

        #endregion
    }
}
