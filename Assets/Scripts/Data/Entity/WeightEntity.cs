using System;

namespace CAFU.Flow.Data.Entity
{
    [Serializable]
    public class WeightEntity
    {
        /// <summary>
        /// Entity related Id.
        /// </summary>
        public int Id;

        /// <summary>
        /// Weight shows ratio of the specified entity. Lottery probability = weight/sum(weight)
        /// </summary>
        public int Weight;
    }
}