// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="Analytics.PrincipalProjections"/> instance 
    /// to be tested 
    /// with <see cref="Tools.PrincipalProjectionsTest"/>.
    /// </summary>
    class TestablePrincipalProjections
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestablePrincipalProjections"/>
        /// class.</summary>
        /// <param name="principalProjections">
        /// The principal projections to test.</param>
        /// <param name="activeCloud">The expected active cloud.</param>
        /// <param name="coordinates">The expected principal coordinates.</param>
        /// <param name="variances">The expected principal variances.</param>
        /// <param name="directions">The expected principal directions.</param>
        /// <param name="contributions">The expected contributions.</param>
        /// <param name="representationQualities">The expected representation qualities.</param>
        /// <param name="regressionCoefficients">The expected regression coefficients.</param>
        /// <param name="correlations">The expected correlations.</param>
        public TestablePrincipalProjections(
            PrincipalProjections principalProjections,
            Cloud activeCloud,
            DoubleMatrix coordinates,
            DoubleMatrix variances,
            DoubleMatrix directions,
            DoubleMatrix contributions,
            DoubleMatrix representationQualities,
            DoubleMatrix regressionCoefficients = null,
            DoubleMatrix correlations = null
            )
        {
            this.PrincipalProjections = principalProjections;
            this.ActiveCloud = activeCloud;
            this.Coordinates = coordinates;
            this.Variances = variances;
            this.Directions = directions;
            this.Contributions = contributions;
            this.RepresentationQualities = representationQualities;
            this.RegressionCoefficients = regressionCoefficients;
            this.Correlations = correlations;
        }

        /// <summary>Gets the principal projections to test.</summary>
        /// <value>The principal projections to test.</value>
        public PrincipalProjections PrincipalProjections { get; }

        /// <summary>Gets the expected active cloud.</summary>
        /// <value>The expected active cloud.</value>
        public Cloud ActiveCloud { get; }

        /// <summary>Gets the expected principal coordinates.</summary>
        /// <value>The expected principal coordinates.</value>
        public DoubleMatrix Coordinates { get; }

        /// <summary>Gets the expected principal variances.</summary>
        /// <value>The expected principal variances.</value>
        public DoubleMatrix Variances { get; }

        /// <summary>Gets the expected principal directions.</summary>
        /// <value>The expected principal directions.</value>
        public DoubleMatrix Directions { get; }

        /// <summary>Gets the expected contributions.</summary>
        /// <value>The expected contributions.</value>
        public DoubleMatrix Contributions { get; }

        /// <summary>Gets the expected representation qualities.</summary>
        /// <value>The representation qualities.</value>
        public DoubleMatrix RepresentationQualities { get; }

        /// <summary>Gets the expected regression coefficients.</summary>
        /// <value>The expected regression coefficients.</value>
        public DoubleMatrix RegressionCoefficients { get; }

        /// <summary>Gets the expected correlations.</summary>
        /// <value>The expected correlations.</value>
        public DoubleMatrix Correlations { get; }
    }
}
