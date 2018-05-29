using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExtraCollection;
using ExtraLinq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CAFU.Flow.Utility
{
    /// <summary>
    /// Weighted Array
    /// </summary>
    /// <typeparam name="TKey">TKey specify weight related value</typeparam>
    public class WeightedArray<TKey>
    {
        private List<KeyValuePair<TKey, int>> data;

        private List<int> comulativeWeights;

        private int totalWeights;

        public WeightedArray(IDictionary<TKey, int> dictionary)
        {
            this.Set(dictionary);
        }

        public void Set(IDictionary<TKey, int> dictionary)
        {
            if (dictionary.IsNullOrEmpty())
            {
                this.data = new List<KeyValuePair<TKey, int>>();
                this.comulativeWeights = new List<int>();
                this.totalWeights = 0;
            }
            else
            {
                this.data = dictionary.ToList();
                this.comulativeWeights = this.data.Select(it => it.Value).Scan((sum, it) => sum + it).ToList();
                this.totalWeights = this.comulativeWeights[this.data.Count - 1];
            }
        }

        /// <summary>
        /// Random Selection by using lottery probability culculated by weights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidProgramException"></exception>
        public TKey Lot()
        {
            if (this.data.IsNullOrEmpty())
            {
                throw new InvalidDataException("Cannot lot from data. Data is empty.");
            }

            var indexWeight = Mathf.CeilToInt(this.GetRandomValue() * this.totalWeights);

            for (var i = 0; i < comulativeWeights.Count; i++)
            {
                if (indexWeight <= this.comulativeWeights[i])
                {
                    return this.data[i].Key;
                }
            }

            throw new InvalidProgramException("Weight calculation is invalid. Cannot find lottery weight.");
        }

        protected virtual float GetRandomValue()
        {
            return Random.value;
        }
    }
}