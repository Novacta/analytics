// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to randomly select samples from a finite population
    /// where all samples of a given size share the same probability
    /// to be selected.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The current implementation of the <see cref="SimpleRandomSampling"/> class
    /// is based on the Donald E. Knuth's Algorithm S 
    /// (Selection sampling technique, p. 142)<cite>knuth-2-1997</cite>.
    /// </para>
    /// </remarks>
    /// <seealso cref="RandomSampling" />
    public class SimpleRandomSampling : RandomSampling
    {
        /// <summary>
        /// The number of elements belonging to the population.
        /// </summary>
        private readonly int populationSize;

        /// <summary>
        /// The number of elements belonging to a sample.
        /// </summary>
        private readonly int sampleSize;

        /// <summary>
        /// The inclusion probabilities of the population units.
        /// </summary>
        private readonly DoubleMatrix inclusionProbabilities;

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="SimpleRandomSampling"/> class.
        /// </summary>
        /// <param name="populationSize">The size of the sampled population.</param>
        /// <param name="sampleSize">The size of the samples to draw.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="populationSize"/> is less than <c>2</c>.<br/>
        /// -or-<br/> 
        /// <paramref name="sampleSize"/> is
        /// not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="sampleSize"/> is not less
        /// than <paramref name="populationSize"/>.
        /// </exception>
        public SimpleRandomSampling(int populationSize, int sampleSize)
        {
            if (populationSize <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(populationSize),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1"));
            }

            if (sampleSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleSize),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (populationSize <= sampleSize)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE"),
                        "the population size"),
                    nameof(sampleSize));
            }

            this.sampleSize = sampleSize;
            this.populationSize = populationSize;
            this.inclusionProbabilities = DoubleMatrix.Dense(
                populationSize,
                1,
                (double)this.sampleSize / (double)this.populationSize);
        }

        /// <inheritdoc/>
        public override int PopulationSize
        {
            get
            {
                return this.populationSize;
            }
        }

        /// <inheritdoc/>
        public override int SampleSize
        {
            get
            {
                return this.sampleSize;
            }
        }

        /// <inheritdoc/>
        public override ReadOnlyDoubleMatrix InclusionProbabilities
        {
            get { return this.inclusionProbabilities.AsReadOnly(); }
        }


        /// <inheritdoc/>
        public override DoubleMatrix NextDoubleMatrix()
        {
            int populationSize = this.PopulationSize;
            var sample = DoubleMatrix.Dense(1, populationSize);
            double[] samplePositions = sample.GetStorage();

            double u;
            int numberOfSampledUnits = 0;
            int numberOfInspectedUnits = 0;
            for (int i = 0; i < samplePositions.Length; i++)
            {
                u = this.RandomNumberGenerator.DefaultUniform();

                if ((populationSize - numberOfInspectedUnits) * u
                    >= (this.sampleSize - numberOfSampledUnits))
                {
                    numberOfInspectedUnits++;
                }
                else
                {
                    samplePositions[i] = 1.0;
                    numberOfSampledUnits++;
                    numberOfInspectedUnits++;
                    if (numberOfSampledUnits == this.sampleSize)
                    {
                        break;
                    }
                }
            }

            return sample;
        }

        /// <inheritdoc/>
        public override IndexCollection NextIndexCollection()
        {
            double populationSize = this.populationSize;
            int sampleSize = this.sampleSize;
            double u;
            double numberOfSampledUnits = 0;
            int numberOfInspectedUnits = 0;
            List<int> sampleList = new List<int>(sampleSize);
            for (int i = 0; i < populationSize; i++)
            {
                u = this.RandomNumberGenerator.DefaultUniform();

                if ((populationSize - numberOfInspectedUnits) * u
                    >= (sampleSize - numberOfSampledUnits))
                {
                    numberOfInspectedUnits++;
                }
                else
                {
                    sampleList.Add(i);
                    numberOfSampledUnits++;
                    numberOfInspectedUnits++;
                    if (numberOfSampledUnits == sampleSize)
                    {
                        break;
                    }
                }
            }

            return new IndexCollection(sampleList.ToArray(), false);
        }
    }
}
