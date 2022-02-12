// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (1,1)  (4,4)  (7,7)          <para /> 
    /// (2,2)  (5,5)  (8,8)          <para /> 
    /// (3,3)  (6,6)  (9,9)          <para /> 
    ///
    /// r   =                  <para /> 
    /// (-5,-5)  (-3,-3)  (-1,-1)       <para /> 
    /// (-4,-4)  (-2,-2)  (-0,-0)       <para /> 
    ///
    /// ComplexMatrix.ElementWiseMultiply(l, r)  => <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixElementWiseMultiplication{ComplexMatrixState}" />
    class RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication :
        TestableComplexMatrixComplexMatrixElementWiseMultiplication<ArgumentException>
    {
        RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_ELEMENT_WISE_ALL_DIMS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableComplexMatrix14.Get(),
                right: TestableComplexMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication"/> class.</returns>
        public static RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication Get()
        {
            return new RightWrongRowsComplexMatrixComplexMatrixElementWiseMultiplication();
        }
    }
}
