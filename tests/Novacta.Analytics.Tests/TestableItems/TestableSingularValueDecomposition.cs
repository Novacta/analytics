// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of 
    /// <see cref="Advanced.SingularValueDecomposition"/> methods,
    /// to be tested 
    /// with <see cref="Tools.SingularValueDecompositionTest"/>.
    /// </summary>
    public class TestableSingularValueDecomposition<TTestableMatrix, TMatrix>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableSingularValueDecomposition{TTestableMatrix, TMatrix}"/>
        /// class.</summary>
        /// <param name="matrix">The matrix whose singular value decomposition must be tested.</param>
        /// <param name="values">The expected singular values.</param>
        /// <param name="leftVectors">The expected left singular vectors.</param>
        /// <param name="conjugateTransposedRightVectors">The expected conjugate transposed
        /// right vectors.</param>
        public TestableSingularValueDecomposition(
            TTestableMatrix matrix,
            DoubleMatrix values,
            TMatrix leftVectors,
            TMatrix conjugateTransposedRightVectors
            )
        {
            this.TestableMatrix = matrix;
            this.Values = values;
            this.LeftVectors = leftVectors;
            this.ConjugateTransposedRightVectors = conjugateTransposedRightVectors;
        }

        /// <summary>Gets the testable matrix to test.</summary>
        /// <value>The testable matrix to test.</value>
        public TTestableMatrix TestableMatrix { get; }

        /// <summary>Gets the expected singular values.</summary>
        /// <value>The expected singular values.</value>
        public DoubleMatrix Values { get; }

        /// <summary>Gets the expected left singular vectors.</summary>
        /// <value>The expected left singular vectors.</value>
        public TMatrix LeftVectors { get; }

        /// <summary>Gets the expected conjugate transposed right 
        /// singular vectors.</summary>
        /// <value>The expected conjugate transposed right singular 
        /// vectors.</value>
        public TMatrix ConjugateTransposedRightVectors { get; }
    }
}
