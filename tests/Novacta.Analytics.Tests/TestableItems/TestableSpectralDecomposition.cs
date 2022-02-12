// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of 
    /// <see cref="Advanced.SpectralDecomposition"/> methods,
    /// to be tested 
    /// with <see cref="Tools.SpectralDecompositionTest"/>.
    /// </summary>
    public class TestableSpectralDecomposition<TTestableMatrix, TMatrix>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableSpectralDecomposition{TTestableMatrix, TMatrix}"/>
        /// class.</summary>
        /// <param name="matrix">The matrix whose spectral decomposition must be tested.</param>
        /// <param name="values">The expected eigenvalues.</param>
        /// <param name="vectorsIfLower">The expected eigenvectors if the lower triangular part
        /// of <paramref name="matrix"/> is exploited.</param>
        /// <param name="vectorsIfUpper">The expected eigenvectors if the upper triangular part
        /// of <paramref name="matrix"/> is exploited.</param>
        public TestableSpectralDecomposition(
            TTestableMatrix matrix,
            DoubleMatrix values,
            TMatrix vectorsIfLower,
            TMatrix vectorsIfUpper
            )
        {
            this.TestableMatrix = matrix;
            this.Values = values;
            this.VectorsIfLower = vectorsIfLower;
            this.VectorsIfUpper = vectorsIfUpper;
        }

        /// <summary>Gets the testable matrix to test.</summary>
        /// <value>The testable matrix to test.</value>
        public TTestableMatrix TestableMatrix { get; }

        /// <summary>Gets the expected eigenvalues.</summary>
        /// <value>The expected eigenvalues.</value>
        public DoubleMatrix Values { get; }

        /// <summary>Gets the expected eigenvectors if the tested matrix
        /// contains the lower triangular part of the matrix to be decomposed.</summary>
        /// <value>The expected eigenvectors if the tested matrix
        /// contains the lower triangular part of the matrix to be decomposed.</value>
        public TMatrix VectorsIfLower { get; }

        /// <summary>Gets the expected eigenvectors if the tested matrix
        /// contains the upper triangular part of the matrix to be decomposed.</summary>
        /// <value>The expected eigenvectors if the tested matrix
        /// contains the upper triangular part of the matrix to be decomposed.</value>
        public TMatrix VectorsIfUpper { get; }
    }
}
