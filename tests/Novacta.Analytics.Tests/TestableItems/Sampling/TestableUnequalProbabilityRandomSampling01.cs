// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Sampling
{
    /// <summary>
    /// Provides methods to test implementations of a
    /// random sampling where the population and the sample sizes are,
    /// respectively, <c>9</c> and <c>4</c>, and the inclusion 
    /// probabilities are constant and equal to 4/9.
    /// </summary>
    class TestableUnequalProbabilityRandomSampling01 : TestableRandomSampling
    {
        const int populationSize = 9;
        const int sampleSize = 4;
        static readonly DoubleMatrix inclusionProbabilities =
            DoubleMatrix.Dense(populationSize, 1,
                (double)sampleSize / (double) populationSize);
        // The quantile of order .9 for
        // the chi-squared distribution having 9-1
        // degrees of freedom is 13.36157
        // (as from R function qchisq(.9, 8))
        const double goodnessOfFitCriticalValue = 13.36157;
        static readonly DoubleMatrix bernoulliProbabilities =
            DoubleMatrix.Dense(populationSize, 1, .5);

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableUnequalProbabilityRandomSampling01" /> class.
        /// </summary>
        TestableUnequalProbabilityRandomSampling01() : base(
            randomSampling:
                UnequalProbabilityRandomSampling.FromBernoulliProbabilities(
                    bernoulliProbabilities: bernoulliProbabilities,
                    sampleSize: sampleSize),
            populationSize: populationSize,
            sampleSize: sampleSize,
            inclusionProbabilities: inclusionProbabilities,
            goodnessOfFitCriticalValue: goodnessOfFitCriticalValue)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableUnequalProbabilityRandomSampling01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableUnequalProbabilityRandomSampling01"/> class.</returns>
        public static TestableUnequalProbabilityRandomSampling01 Get()
        {
            return new TestableUnequalProbabilityRandomSampling01();
        }
    }
}
