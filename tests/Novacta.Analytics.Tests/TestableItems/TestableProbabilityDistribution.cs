// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="ProbabilityDistribution"/> instance to be tested 
    /// with <see cref="Tools.ProbabilityDistributionTest"/>.
    /// </summary>
    class TestableProbabilityDistribution
    {
        readonly ProbabilityDistribution probabilityDistribution;
        readonly double mean;
        readonly double variance;
        readonly Dictionary<TestableDoubleMatrix, DoubleMatrix> pdfPartialGraph;
        readonly Dictionary<TestableDoubleMatrix, DoubleMatrix> cdfPartialGraph;
        readonly bool canInvertCdf;
        readonly Dictionary<TestableDoubleMatrix, DoubleMatrix> inverseCdfPartialGraph;

        /// <summary>Initializes a new instance of the
        /// <see cref="TestableProbabilityDistribution"/>
        /// class.</summary>
        /// <param name="probabilityDistribution">
        /// The probability distribution to test.</param>
        /// <param name="mean">The expected mean.</param>
        /// <param name="variance">The expected variance.</param>
        /// <param name="pdfPartialGraph">
        /// A dictionary in which keys represent PDF arguments, and the 
        /// corresponding values contain the expected images of such arguments.
        /// </param>
        /// <param name="cdfPartialGraph">
        /// A dictionary in which keys represent CDF arguments, and the 
        /// corresponding values contain the expected images of such arguments.
        /// </param>
        /// <param name="canInvertCdf"><c>true</c> if the target distribution 
        /// can invert the CDF; otherwise <c>false</c>.</param>
        /// <param name="inverseCdfPartialGraph">
        /// A dictionary in which keys represent inverse CDF arguments, and 
        /// the corresponding values contain the expected images of such 
        /// arguments.
        /// </param>
        public TestableProbabilityDistribution(
            ProbabilityDistribution probabilityDistribution,
            double mean,
            double variance,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> pdfPartialGraph,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> cdfPartialGraph,
            bool canInvertCdf = false,
            Dictionary<TestableDoubleMatrix, DoubleMatrix> inverseCdfPartialGraph = null)
        {
            this.probabilityDistribution = probabilityDistribution;
            this.mean = mean;
            this.variance = variance;
            this.pdfPartialGraph = pdfPartialGraph;
            this.cdfPartialGraph = cdfPartialGraph;
            this.canInvertCdf = canInvertCdf;
            this.inverseCdfPartialGraph = inverseCdfPartialGraph;
        }

        /// <summary>Gets the distribution to test.</summary>
        /// <value>The distribution to test.</value>
        public ProbabilityDistribution Distribution
        { get => this.probabilityDistribution; }

        /// <summary>Gets the expected mean.</summary>
        /// <value>The expected mean.</value>
        public double Mean { get => this.mean; }
        
        /// <summary>Gets the expected variance.</summary>
        /// <value>The expected variance.</value>
        public double Variance { get => this.variance; }

        /// <summary>Gets an expected PDF partial graph.</summary>
        /// <value>A dictionary in which keys represent inverse PDF arguments, and 
        /// the corresponding values contain the expected images of such 
        /// arguments.</value>
        public Dictionary<TestableDoubleMatrix, DoubleMatrix> PdfPartialGraph
        { get => this.pdfPartialGraph; }

        /// <summary>Gets an expected CDF partial graph.</summary>
        /// <value>A dictionary in which keys represent CDF arguments, and 
        /// the corresponding values contain the expected images of such 
        /// arguments.</value>
        public Dictionary<TestableDoubleMatrix, DoubleMatrix> CdfPartialGraph
        { get => this.cdfPartialGraph; }
        
        /// <summary>Gets a value indicating whether this instance is expected 
        /// to invert its CDF.</summary>
        /// <value>
        /// <c>true</c> if this instance is expected to invert its CDF; 
        /// otherwise, <c>false</c>.</value>
        public bool CanInvertCdf { get => this.canInvertCdf; }

        /// <summary>Gets an expected inverse CDF partial graph.</summary>
        /// <value>A dictionary in which keys represent inverse CDF arguments, and 
        /// the corresponding values contain the expected images of such 
        /// arguments.</value>
        public Dictionary<TestableDoubleMatrix, DoubleMatrix> InverseCdfPartialGraph
        { get => this.inverseCdfPartialGraph; }
    }
}
