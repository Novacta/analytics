// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents the results of the execution of a
    /// Cross-Entropy program represented by a 
    /// <see cref="RareEventProbabilityEstimator"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Cross-Entropy program represented by a
    /// <see cref="RareEventProbabilityEstimator"/> 
    /// object can be executed 
    /// by calling its method
    /// <see cref="RareEventProbabilityEstimator.Estimate(
    /// RareEventProbabilityEstimationContext, double, int, int)"/>, which
    /// returns an instance of class 
    /// <see cref="RareEventProbabilityEstimationResults"/>.
    /// </para>    
    /// <para>
    /// The estimated probability of the rare event under study
    /// can be analyzed by inspecting property
    /// <see cref="RareEventProbability"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="RareEventProbabilityEstimator"/>
    public class RareEventProbabilityEstimationResults
        : CrossEntropyResults
    {
        /// <summary>
        /// Gets a value representing the estimated probability
        /// of the rare event under study.
        /// </summary>
        /// <value>
        /// The estimated probability
        /// of the rare event under study.
        /// </value>
        public double RareEventProbability { get; internal set; }
    }
}
