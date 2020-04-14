// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Represents a range of numerical blocks.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="NumericalBlockRange"/> instances are exploited when 
    /// searching for an optimal multi-interval splitting strategy of 
    /// the range of a numerical variable. Each range is represented by 
    /// a sequence of consecutive <see cref="NumericalBlock"/>
    /// instances. So, when a given split is inspected for optimality, 
    /// it can be represented as a pair 
    /// of <see cref="NumericalBlockRange"/> instances.
    /// </para>
    /// </remarks>
    /// <seealso cref="CategoricalDataSet.CategorizeByEntropyMinimization(
    /// System.IO.TextReader, char, IndexCollection, bool, int, 
    /// System.IFormatProvider)"/>
    internal class NumericalBlockRange
    {

        #region State

        public int FirstIndex { get; }

        public int LastIndex { get; }

        public int NumberOfObservedClasses { get; }

        public double Entropy { get; }

        public int NumberOfInstances { get; }

        #endregion

        public NumericalBlockRange(
            int firstIndex, 
            int lastIndex, 
            List<NumericalBlock> blocks)
        {
            this.FirstIndex = firstIndex;
            this.LastIndex = lastIndex;

            // Compute the range frequency distribution
            int numberOfClasses = blocks[0].TargetFrequencyDistribution.Count;
            var rangeFrequencyDistribution = 
                new Dictionary<double, int>(numberOfClasses);

            foreach (var key in blocks[0].TargetFrequencyDistribution.Keys) {
                int frequency = 0;
                for (int i = firstIndex; i <= lastIndex; i++) {
                    var blockFrequencyDistribution = 
                        blocks[i].TargetFrequencyDistribution;
                    frequency += blockFrequencyDistribution[key];
                }
                rangeFrequencyDistribution.Add(key, frequency);
            }

            // Compute the range size
            int n = rangeFrequencyDistribution.Values.Sum();
            this.NumberOfInstances = n;

            // Compute the range entropy and number of observed classes
            double rangeEntropy = 0.0;
            int numberOfDistinctClasses = 0;

            foreach (var frequency in rangeFrequencyDistribution.Values) {
                if (0.0 != frequency) {
                    rangeEntropy -= frequency * Log(frequency, 2.0);
                    numberOfDistinctClasses++;
                }
            }
            rangeEntropy /= n;
            rangeEntropy += Log(n, 2.0);
            this.Entropy = rangeEntropy;
            this.NumberOfObservedClasses = numberOfDistinctClasses;
        }
    }
}
