// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Sampling
{
    /// <summary>
    /// Provides methods to test implementations of a
    /// random sampling where the population and the sample sizes are,
    /// respectively, <c>9</c> and <c>4</c>, and the inclusion 
    /// probabilities are:
    /// <para /> 
    /// 0.0602933142691422 <para /> 
    /// 0.130083877944215 <para /> 
    /// 0.211283730466511 <para /> 
    /// 0.305874041451126 <para /> 
    /// 0.415127817757125 <para /> 
    /// 0.537249360967418 <para /> 
    /// 0.661978741628504 <para /> 
    /// 0.782520969486053 <para /> 
    /// 0.895588146029904 <para /> 
    /// </summary>
    class TestableUnequalProbabilityRandomSampling00 : TestableRandomSampling
    {
        const int populationSize = 9;
        const int sampleSize = 4;
        static readonly DoubleMatrix inclusionProbabilities =
            DoubleMatrix.Dense(populationSize, 1,
                [
                    0.0602933142691422,
                    0.130083877944215,
                    0.211283730466511,
                    0.305874041451126,
                    0.415127817757125,
                    0.537249360967418,
                    0.661978741628504,
                    0.782520969486053,
                    0.895588146029904]);
        // The quantile of order .9 for
        // the chi-squared distribution having 9-1
        // degrees of freedom is 13.36157
        // (as from R function qchisq(.9, 8))
        const double goodnessOfFitCriticalValue = 13.36157;
        static readonly DoubleMatrix bernoulliProbabilities =
            DoubleMatrix.Dense(populationSize, 1,
                [.1, .2, .3, .4, .5, .6, .7, .8, .9]);

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableUnequalProbabilityRandomSampling00" /> class.
        /// </summary>
        TestableUnequalProbabilityRandomSampling00() : base(
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
        /// <see cref="TestableUnequalProbabilityRandomSampling00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableUnequalProbabilityRandomSampling00"/> class.</returns>
        public static TestableUnequalProbabilityRandomSampling00 Get()
        {
            return new TestableUnequalProbabilityRandomSampling00();
        }
    }
}
