using System.Collections.Generic;
using System.Linq;
using CAFU.Flow.Utility;
using NUnit.Framework;

namespace CAFU.Flow.Domain.Model
{
    public class FlowModelTest
    {
        public class FlowModelSpy : FlowModel
        {
            public IList<KeyValuePair<int, float>> EmitList
            {
                get { return this.emitList; }
                set { this.emitList = value; }
            }

            public FlowModelSpy(float start, float end, float intervalTime, float intervalDivationTime, int units,
                WeightedArray<int> lotteryWeightArray) : base(start, end, intervalTime, intervalDivationTime, units,
                lotteryWeightArray)
            {
            }
        }

        [Test]
        public void EmitItemsTest()
        {
            var model1 = new FlowModelSpy(
                start: 0f,
                end: 10f,
                intervalTime: 1f,
                intervalDivationTime: 0f,
                units: 1,
                lotteryWeightArray: new WeightedArray<int>(new Dictionary<int, int>()
                {
                    {
                        1, 1
                    }
                })
            );

            model1.EmitList =
                new List<KeyValuePair<int, float>>()
                {
                    new KeyValuePair<int, float>(1, 0f),
                    new KeyValuePair<int, float>(2, 0.1f),
                    new KeyValuePair<int, float>(1, 0.9f),
                    new KeyValuePair<int, float>(2, 1.0f),
                };

            Assert.AreEqual(new int[]
            {
            }, model1.EmitItems(-0.1f).ToArray());

            Assert.AreEqual(new int[]
            {
                1
            }, model1.EmitItems(0f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model1.EmitItems(0f).ToArray());

            Assert.AreEqual(new int[]
            {
                2
            }, model1.EmitItems(1f).ToArray());

            Assert.AreEqual(new int[]
            {
                1
            }, model1.EmitItems(1f).ToArray());

            Assert.AreEqual(new int[]
            {
                2
            }, model1.EmitItems(1f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model1.EmitItems(1f).ToArray());

            var model2 = new FlowModelSpy(
                start: 0f,
                end: 10f,
                intervalTime: 1f,
                intervalDivationTime: 0f,
                units: 2,
                lotteryWeightArray: new WeightedArray<int>(new Dictionary<int, int>()
                {
                    {
                        1, 1
                    }
                })
            );

            model2.EmitList =
                new List<KeyValuePair<int, float>>()
                {
                    new KeyValuePair<int, float>(1, 0f),
                    new KeyValuePair<int, float>(2, 0.1f),
                    new KeyValuePair<int, float>(1, 0.9f),
                    new KeyValuePair<int, float>(2, 1.0f),
                    new KeyValuePair<int, float>(1, 2.1f),
                    new KeyValuePair<int, float>(2, 2.0f),
                };

            //
            Assert.AreEqual(new int[]
            {
            }, model2.EmitItems(-0.1f).ToArray());

            Assert.AreEqual(new int[]
            {
                1
            }, model2.EmitItems(0f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model2.EmitItems(0f).ToArray());

            //
            Assert.AreEqual(new int[]
            {
                2, 1
            }, model2.EmitItems(1f).ToArray());

            Assert.AreEqual(new int[]
            {
                2
            }, model2.EmitItems(1f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model2.EmitItems(1f).ToArray());

            //
            Assert.AreEqual(new int[]
            {
                2
            }, model2.EmitItems(2f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model2.EmitItems(2f).ToArray());

            //
            Assert.AreEqual(new int[]
            {
                1
            }, model2.EmitItems(2.1f).ToArray());

            Assert.AreEqual(new int[]
            {
            }, model2.EmitItems(2.1f).ToArray());
        }

        [Test]
        public void GenerateItemsTest()
        {
            var model1 = new FlowModelSpy(
                start: 0f,
                end: 10f,
                intervalTime: 1f,
                intervalDivationTime: 0f,
                units: 2,
                lotteryWeightArray: new WeightedArray<int>(new Dictionary<int, int>()
                {
                    {
                        1, 1
                    }
                })
            );

            model1.GenerateItems(-2f);
            Assert.AreEqual(0, model1.GeneratedCount);

            model1.GenerateItems(-1f);
            Assert.AreEqual(2, model1.GeneratedCount);
            Assert.AreEqual(new[]
            {
                new KeyValuePair<int, float>(1, 0f),
                new KeyValuePair<int, float>(1, 0f),
            }, model1.EmitList.ToArray());

            model1.GenerateItems(-1f);
            Assert.AreEqual(2, model1.GeneratedCount);
            Assert.AreEqual(new[]
            {
                new KeyValuePair<int, float>(1, 0f),
                new KeyValuePair<int, float>(1, 0f),
            }, model1.EmitList.ToArray());

            model1.GenerateItems(0f);
            Assert.AreEqual(4, model1.GeneratedCount);
            Assert.AreEqual(new[]
            {
                new KeyValuePair<int, float>(1, 0f),
                new KeyValuePair<int, float>(1, 0f),
                new KeyValuePair<int, float>(1, 1f),
                new KeyValuePair<int, float>(1, 1f),
            }, model1.EmitList.ToArray());
        }
    }
}