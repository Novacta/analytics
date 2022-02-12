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
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// r   =                  <para /> 
    /// 1  0       <para /> 
    /// 0  0       <para /> 
    ///
    /// DoubleMatrix.ElementWiseMultiply(l, r)  => <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixElementWiseMultiplication{DoubleMatrixState}" />
    class RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication :
        TestableDoubleMatrixDoubleMatrixElementWiseMultiplication<ArgumentException>
    {
        RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_ELEMENT_WISE_ALL_DIMS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix09.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication"/> class.</returns>
        public static RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication Get()
        {
            return new RightWrongColsDoubleMatrixDoubleMatrixElementWiseMultiplication();
        }
    }
}
