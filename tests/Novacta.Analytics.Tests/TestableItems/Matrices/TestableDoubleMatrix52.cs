// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1     26    51    76 <para /> 
    /// 2     27    52    77 <para /> 
    /// 3     28    53    78 <para /> 
    /// 4     29    54    79 <para /> 
    /// 5     30    55    80 <para /> 
    /// 6     31    56    81 <para /> 
    /// 7     32    57    82 <para /> 
    /// 8     33    58    83 <para /> 
    /// 9     34    59    84 <para />
    /// 10    35    60    85 <para />
    /// 11    36    61    86 <para />
    /// 12    37    62    87 <para />
    /// 13    38    63    88 <para />
    /// 14    39    64    89 <para />
    /// 15    40    65    90 <para />
    /// 16    41    66    91 <para />
    /// 17    42    67    92 <para />
    /// 18    43    68    93 <para />
    /// 19    44    69    94 <para />
    /// 20    45    70    95 <para />
    /// 21    46    71    96 <para />
    /// 22    47    72    97 <para />
    /// 23    48    73    98 <para />
    /// 24    49    74    99 <para />
    /// 25    50    75    100<para />
    /// </summary>
    class TestableDoubleMatrix52 : TestableDoubleMatrix
    {
        static double[] GetAsColumnMajorDenseArray()
        {
            var asColumnMajorDenseArray = new double[100];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = i + 1;
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix52" /> class.
        /// </summary>
        TestableDoubleMatrix52() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 25,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 3,
                lowerBandwidth: 24)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix52"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix52"/> class.</returns>
        public static TestableDoubleMatrix52 Get()
        {
            return new TestableDoubleMatrix52();
        }
    }
}
