// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of 
    /// <see cref="NonMetricMultidimensionalScaling"/> methods,
    /// to be tested 
    /// with <see cref="Tools.NonMetricMultidimensionalScalingTest"/>.
    /// </summary>
    public class TestableNonMetricMultidimensionalScaling<TTestableMatrix>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableNonMetricMultidimensionalScaling{TTestableMatrix}"/>
        /// class.</summary>
        /// <param name="dissimilarities">
        /// The matrix whose MDS must be tested.
        /// </param>
        /// <param name="configurationDimension">
        /// The dimension of the configuration.
        /// </param>
        /// <param name="minkowskiDistanceOrder">
        /// The Minkowski distance order.
        /// </param>
        /// <param name="maximumNumberOfIterations">
        /// The maximum number of iterations.
        /// </param>
        /// <param name="terminationTolerance">
        /// The termination tolerance.
        /// </param>
        /// <param name="configuration">
        /// The expected configuration.
        /// </param>
        /// <param name="stress">
        /// The expected stress.
        /// </param>
        /// <param name="hasConverged">
        /// A value indicating whether the MDS algorithm has converged.
        /// </param>
        public TestableNonMetricMultidimensionalScaling(
            TTestableMatrix dissimilarities,
            int? configurationDimension,
            int minkowskiDistanceOrder,
            int maximumNumberOfIterations,
            double terminationTolerance,
            DoubleMatrix configuration,
            double stress,
            bool hasConverged
            )
        {
            this.TestableDissimilarityMatrix = dissimilarities;
            this.ConfigurationDimension = configurationDimension;
            this.MinkowskiDistanceOrder = minkowskiDistanceOrder;
            this.MaximumNumberOfIterations = maximumNumberOfIterations;
            this.TerminationTolerance = terminationTolerance;
            this.Configuration = configuration;
            this.Stress = stress;
            this.HasConverged = hasConverged;
        }

        /// <summary>Gets the testable matrix to test.</summary>
        /// <value>The testable matrix to test.</value>
        public TTestableMatrix TestableDissimilarityMatrix { get; }

        /// <summary>Gets the dimension of the configuration.</summary>
        /// <value>The dimension of the configuration.</value>
        public int? ConfigurationDimension { get; }

        /// <summary>Gets the Minkowski distance order.</summary>
        /// <value>The maximum Minkowski distance order.</value>
        public int MinkowskiDistanceOrder { get; }

        /// <summary>Gets the maximum number of iterations.</summary>
        /// <value>The maximum number of iterations.</value>
        public int MaximumNumberOfIterations { get; }

        /// <summary>Gets the termination tolerance.</summary>
        /// <value>The termination tolerance.</value>
        public double TerminationTolerance { get; }

        /// <summary>Gets the expected configuration.</summary>
        /// <value>The expected configuration.</value>
        public DoubleMatrix Configuration { get; }

        /// <summary>Gets the expected stress.</summary>
        /// <value>The expected stress.</value>
        public double Stress { get; }

        /// <summary>Gets the expected convergence.</summary>
        /// <value>The expected convergence.</value>
        public bool HasConverged { get; }

    }
}