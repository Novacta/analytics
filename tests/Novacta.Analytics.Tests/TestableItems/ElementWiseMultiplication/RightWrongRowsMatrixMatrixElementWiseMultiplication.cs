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
    /// 1  4  7          <para /> 
    /// 2  5  8          <para /> 
    /// 3  6  9          <para /> 
    ///
    /// r   =                  <para /> 
    /// -5.0  -3.0  -1.0       <para /> 
    /// -4.0  -2.0  -0.0       <para /> 
    ///
    /// DoubleMatrix.ElementWiseMultiply(l, r)  => <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixElementWiseMultiplication{DoubleMatrixState}" />
    class RightWrongRowsMatrixMatrixElementWiseMultiplication :
        TestableMatrixMatrixElementWiseMultiplication<ArgumentException>
    {
        RightWrongRowsMatrixMatrixElementWiseMultiplication() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_ELEMENT_WISE_ALL_DIMS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableDoubleMatrix14.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongRowsMatrixMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongRowsMatrixMatrixElementWiseMultiplication"/> class.</returns>
        public static RightWrongRowsMatrixMatrixElementWiseMultiplication Get()
        {
            return new RightWrongRowsMatrixMatrixElementWiseMultiplication();
        }
    }
}
