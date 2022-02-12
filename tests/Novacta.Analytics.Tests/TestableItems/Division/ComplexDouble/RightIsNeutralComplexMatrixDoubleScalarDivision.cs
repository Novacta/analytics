// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =     <para /> 
    /// 1.0       <para /> 
    ///
    /// l / r  =        <para />
    /// (0,0)  (2,2)  (4,4)    <para /> 
    ///	(1,1)  (3,3)  (5,5)    <para />       
    ///	</summary>
    class RightIsNeutralComplexMatrixDoubleScalarDivision :
        TestableComplexMatrixDoubleScalarDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                0, 1, 2, 3, 4, 5
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsNeutralComplexMatrixDoubleScalarDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: 1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNeutralComplexMatrixDoubleScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNeutralComplexMatrixDoubleScalarDivision"/> class.</returns>
        public static RightIsNeutralComplexMatrixDoubleScalarDivision Get()
        {
            return new RightIsNeutralComplexMatrixDoubleScalarDivision();
        }
    }
}
