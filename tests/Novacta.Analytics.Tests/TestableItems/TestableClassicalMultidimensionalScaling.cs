// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of 
    /// <see cref="ClassicalMultidimensionalScaling"/> methods,
    /// to be tested 
    /// with <see cref="Tools.ClassicalMultidimensionalScalingTest"/>.
    /// </summary>
    public class TestableClassicalMultidimensionalScaling<TTestableMatrix>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableClassicalMultidimensionalScaling{TTestableMatrix}"/>
        /// class.</summary>
        /// <param name="proximities">
        /// The matrix whose MDS must be tested.
        /// </param>
        /// <param name="configurationDimension">
        /// The dimension of the configuration.
        /// </param>
        /// <param name="configuration">
        /// The expected configuration.
        /// </param>
        /// <param name="goodnessOfFit">
        /// The expected goodness of fit.
        /// </param>
        public TestableClassicalMultidimensionalScaling(
            TTestableMatrix proximities,
            int? configurationDimension,
            DoubleMatrix configuration,
            double goodnessOfFit
            )
        {
            this.TestableProximityMatrix = proximities;
            this.ConfigurationDimension = configurationDimension;
            this.Configuration = configuration;
            this.GoodnessOfFit = goodnessOfFit;
        }

        /// <summary>Gets the testable matrix to test.</summary>
        /// <value>The testable matrix to test.</value>
        public TTestableMatrix TestableProximityMatrix { get; }

        /// <summary>Gets the dimension of the configuration.</summary>
        public int? ConfigurationDimension { get; }

        /// <summary>Gets the expected configuration.</summary>
        /// <value>The expected configuration.</value>
        public DoubleMatrix Configuration { get; }

        /// <summary>Gets the expected goodness of fit.</summary>
        /// <value>The expected goodness of fit.</value>
        public double GoodnessOfFit { get; }
    }
}