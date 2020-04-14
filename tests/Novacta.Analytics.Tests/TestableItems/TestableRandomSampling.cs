// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="RandomSampling"/> instance to be tested 
    /// with <see cref="Tools.RandomSamplingTest"/>.
    /// </summary>
    class TestableRandomSampling
    {
        readonly RandomSampling randomSampling;
        readonly int popilationSize;
        readonly int sampleSize;
        readonly DoubleMatrix inclusionProbabilities;
        readonly double goodnessOfFitCriticalValue;

        /// <summary>Initializes a new instance of the
        /// <see cref="TestableRandomSampling"/>
        /// class.</summary>
        /// <param name="randomSampling">
        /// The random sampling to test.</param>
        /// <param name="populationSize">The expected population size.</param>
        /// <param name="sampleSize">The expected sample size.</param>
        /// <param name="inclusionProbabilities">
        /// The expected inclusion probabilities.
        /// </param>
        /// <param name="goodnessOfFitCriticalValue">
        /// A quantile of the chi-squared distribution with a number of
        /// degrees of freedom equal to <see cref="PopulationSize"/> 
        /// minus <c>1</c>. To serve as the critical value for the Pearson's 
        /// chi-squared test whose null hypothesis assume that the 
        /// the <paramref name="inclusionProbabilities"/> hold true
        /// when samples are drawn via the <paramref name="randomSampling"/>
        /// instance.
        /// </param>
        /// <seealso href="https://en.wikipedia.org/wiki/Pearson%27s_chi-squared_test"/>
        public TestableRandomSampling(
            RandomSampling randomSampling,
            int populationSize,
            int sampleSize,
            DoubleMatrix inclusionProbabilities,
            double goodnessOfFitCriticalValue)
        {
            this.randomSampling = randomSampling;
            this.popilationSize = populationSize;
            this.sampleSize = sampleSize;
            this.inclusionProbabilities = inclusionProbabilities;
            this.goodnessOfFitCriticalValue = goodnessOfFitCriticalValue;
        }

        /// <summary>Gets the random sampling to test.</summary>
        /// <value>The random sampling to test.</value>
        public RandomSampling RandomSampling
        { get => this.randomSampling; }

        /// <summary>Gets the expected population size.</summary>
        /// <value>The expected population size.</value>
        public double PopulationSize { get => this.popilationSize; }

        /// <summary>Gets the expected sample size.</summary>
        /// <value>The expected sample size.</value>
        public double SampleSize { get => this.sampleSize; }

        /// <summary>Gets the expected inclusion probabilities.</summary>
        /// <value>The expected inclusion probabilities.</value>
        public DoubleMatrix InclusionProbabilities
        { get => this.inclusionProbabilities; }

        /// <summary>Gets a critical value for the Pearson's 
        /// chi-squared test about the 
        /// expected inclusion probabilities.</summary>
        /// <value>
        /// A quantile of the chi-squared distribution with a number of
        /// degrees of freedom equal to <see cref="PopulationSize"/> 
        /// minus <c>1</c>.
        /// </value>
        /// <seealso href="https://en.wikipedia.org/wiki/Pearson%27s_chi-squared_test"/>
        public double GoodnessOfFitCriticalValue
        { get => this.goodnessOfFitCriticalValue; }
    }
}
