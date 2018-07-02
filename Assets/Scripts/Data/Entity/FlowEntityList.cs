using System;
using System.Collections.Generic;
using CAFU.Generics.Data.Entity;

namespace CAFU.Flow.Data.Entity
{
    [Serializable]
    public class FlowEntityList : ScriptableObjectGenericEntityList<FlowEntity>
    {
        public string Key;
    }

    [Serializable]
    public class FlowEntity : IGenericEntity
    {
        /// <summary>
        /// Start time of flow. unit is seconds.
        /// </summary>
        public float Start;

        /// <summary>
        /// End time of flow. unit is seconds.
        /// </summary>
        public float End;

        /// <summary>
        /// Interval time of flow emitting items. unit is seconds.
        /// </summary>
        public float IntervalTime;

        /// <summary>
        /// Random diviation time of interval time. unit is seconds.
        /// </summary>
        public float IntervalDiviationTime;

        /// <summary>
        /// The number of emitting items in each interval.
        /// </summary>
        public int Units;

        /// <summary>
        /// Lotting items by specified weights.
        /// e.g. [{id: 1, weight: 9}, {id: 2, weight: 1}] then items of id:1 would be spawn with 90%.
        /// </summary>
        public List<WeightEntity> LotteryWeights;
    }
}