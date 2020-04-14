// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 0  2  2    <para /> 
    /// 2  0  NaN  <para />
    /// </summary>
    class TestableDoubleMatrix37 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix37" /> class.
        /// </summary>
        TestableDoubleMatrix37() : base(
                asColumnMajorDenseArray: new double[6] { 0, 2, 2, 0, 2, Double.NaN },
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 1)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix37"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix37"/> class.</returns>
        public static TestableDoubleMatrix37 Get()
        {
            return new TestableDoubleMatrix37();
        }
    }
}
