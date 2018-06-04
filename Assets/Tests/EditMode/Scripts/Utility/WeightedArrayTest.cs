using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace CAFU.Flow.Utility
{
    public class WeightedArrayTest
    {
        class WeightedArraySpy<TKey> : WeightedArray<TKey>
        {
            public float RandomValue = 0;

            public WeightedArraySpy(IDictionary<TKey, int> dictionary) : base(dictionary)
            {
            }

            protected override float GetRandomValue()
            {
                return this.RandomValue;
            }
        }

        [Test]
        public void LotTest()
        {
            // normal
            {
                var array = new WeightedArraySpy<int>(new Dictionary<int, int>()
                {
                    {
                        1, 7
                    },
                    {
                        2, 2
                    },
                    {
                        3, 1
                    }
                });

                array.RandomValue = 0;
                Assert.AreEqual(1, array.Lot());
                array.RandomValue = 0.7f;
                Assert.AreEqual(1, array.Lot());

                array.RandomValue = 0.71f;
                Assert.AreEqual(2, array.Lot());
                array.RandomValue = 0.9f;
                Assert.AreEqual(2, array.Lot());

                array.RandomValue = 0.91f;
                Assert.AreEqual(3, array.Lot());
                array.RandomValue = 1.0f;
                Assert.AreEqual(3, array.Lot());
            }

            // single
            {
                var array = new WeightedArraySpy<int>(new Dictionary<int, int>()
                {
                    {
                        1, 1
                    }
                });

                array.RandomValue = 0;
                Assert.AreEqual(1, array.Lot());
                array.RandomValue = 1f;
                Assert.AreEqual(1, array.Lot());
            }

            // empty
            {
                var array = new WeightedArraySpy<int>(new Dictionary<int, int>()
                {
                });

                array.RandomValue = 0;
                Assert.Throws<InvalidDataException>(() => array.Lot());
            }
        }
    }
}