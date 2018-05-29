using System.Linq;
using CAFU.Core.Domain.Translator;
using CAFU.Flow.Data.Entity;
using CAFU.Flow.Domain.Model;
using CAFU.Flow.Utility;

namespace CAFU.Flow.Domain.Translator
{
    public class FlowModelTranslator : IModelTranslator<FlowEntity, FlowModel>
    {
        public FlowModel Translate(FlowEntity entity)
        {
            return new FlowModel(
                start: entity.Start,
                end: entity.End,
                intervalTime: entity.IntervalTime,
                intervalDivationTime: entity.IntervalDiviationTime,
                units: entity.Units,
                lotteryWeightArray: new WeightedArray<int>(
                    entity.LotteryWeights.ToDictionary(it => it.Id, it => it.Weight)
                )
            );
        }
    }
}