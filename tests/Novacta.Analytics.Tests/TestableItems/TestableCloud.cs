// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="Advanced.Cloud"/> instance 
    /// to be tested 
    /// with <see cref="Tools.CloudTest"/>.
    /// </summary>
    class TestableCloud
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableCloud"/>
        /// class.</summary>
        /// <param name="cloud">The cloud to test.</param>
        /// <param name="coordinates">The expected coordinates.</param>
        /// <param name="weights">The expected weights.</param>
        /// <param name="basis">The expected basis.</param>
        /// <param name="mean">The expected mean.</param>
        /// <param name="variance">The expected variance.</param>
        /// <param name="covariance">The expected covariances.</param>
        /// <param name="centred">The expected centered coordinates.</param>
        /// <param name="standardized">The expected standardized coordinates.</param>
        /// <param name="rebased">The expected re-based coordinates.</param>
        public TestableCloud(
            Cloud cloud,
            DoubleMatrix coordinates,
            DoubleMatrix weights,
            Basis basis,
            DoubleMatrix mean,
            double variance,
            DoubleMatrix covariance,
            DoubleMatrix centred,
            DoubleMatrix standardized,
            Dictionary<Basis, DoubleMatrix> rebased
            )
        {
            this.Cloud = cloud;
            this.Coordinates = coordinates;
            this.Weights = weights;
            this.Basis = basis;
            this.Mean = mean;
            this.Variance = variance;
            this.Covariance = covariance;
            this.Centred = centred;
            this.Standardized = standardized;
            this.Rebased = rebased;
        }

        /// <summary>Gets the cloud to test.</summary>
        /// <value>The cloud to test.</value>
        public Cloud Cloud { get; }

        /// <summary>Gets the expected principal coordinates.</summary>
        /// <value>The expected principal coordinates.</value>
        public DoubleMatrix Coordinates { get; }

        /// <summary>Gets the expected weights.</summary>
        /// <value>The expected weights.</value>
        public DoubleMatrix Weights { get; }

        /// <summary>Gets the expected basis.</summary>
        /// <value>The expected basis.</value>
        public Basis Basis { get; }

        /// <summary>Gets the expected mean.</summary>
        /// <value>The expected Mean.</value>
        public DoubleMatrix Mean { get; }

        /// <summary>Gets the expected variance.</summary>
        /// <value>The expected variance.</value>
        public double Variance { get; }

        /// <summary>Gets the expected covariances.</summary>
        /// <value>The expected covariances.</value>
        public DoubleMatrix Covariance { get; }

        /// <summary>Gets the expected centered coordinates.</summary>
        /// <value>The expected centered coordinates.</value>
        public DoubleMatrix Centred { get; }

        /// <summary>Gets the expected standardized coordinates.</summary>
        /// <value>The expected standardized coordinates.</value>
        public DoubleMatrix Standardized { get; }

        /// <summary>Gets the expected re-based coordinates.</summary>
        /// <value>The expected re-based coordinates.</value>
        public Dictionary<Basis, DoubleMatrix> Rebased { get; }
    }
}
