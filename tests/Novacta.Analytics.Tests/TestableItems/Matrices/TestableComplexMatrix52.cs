// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1 ,1 )    (26,26)    (51,51)    (76,76) <para /> 
    /// (2 ,2 )    (27,27)    (52,52)    (77,77) <para /> 
    /// (3 ,3 )    (28,28)    (53,53)    (78,78) <para /> 
    /// (4 ,4 )    (29,29)    (54,54)    (79,79) <para /> 
    /// (5 ,5 )    (30,30)    (55,55)    (80,80) <para /> 
    /// (6 ,6 )    (31,31)    (56,56)    (81,81) <para /> 
    /// (7 ,7 )    (32,32)    (57,57)    (82,82) <para /> 
    /// (8 ,8 )    (33,33)    (58,58)    (83,83) <para /> 
    /// (9 ,9 )    (34,34)    (59,59)    (84,84) <para />
    /// (10,10)    (35,35)    (60,60)    (85,85) <para />
    /// (11,11)    (36,36)    (61,61)    (86,86) <para />
    /// (12,12)    (37,37)    (62,62)    (87,87) <para />
    /// (13,13)    (38,38)    (63,63)    (88,88) <para />
    /// (14,14)    (39,39)    (64,64)    (89,89) <para />
    /// (15,15)    (40,40)    (65,65)    (90,90) <para />
    /// (16,16)    (41,41)    (66,66)    (91,91) <para />
    /// (17,17)    (42,42)    (67,67)    (92,92) <para />
    /// (18,18)    (43,43)    (68,68)    (93,93) <para />
    /// (19,19)    (44,44)    (69,69)    (94,94) <para />
    /// (20,20)    (45,45)    (70,70)    (95,95) <para />
    /// (21,21)    (46,46)    (71,71)    (96,96) <para />
    /// (22,22)    (47,47)    (72,72)    (97,97) <para />
    /// (23,23)    (48,48)    (73,73)    (98,98) <para />
    /// (24,24)    (49,49)    (74,74)    (99,99) <para />
    /// (25,25)    (50,50)    (75,75)    (100,100)<para />
    /// </summary>
    class TestableComplexMatrix52 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var asColumnMajorDenseArray = new Complex[100];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(i + 1, i + 1);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix52" /> class.
        /// </summary>
        TestableComplexMatrix52() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 25,
                numberOfColumns: 4,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 3,
                lowerBandwidth: 24)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix52"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix52"/> class.</returns>
        public static TestableComplexMatrix52 Get()
        {
            return new TestableComplexMatrix52();
        }
    }
}
