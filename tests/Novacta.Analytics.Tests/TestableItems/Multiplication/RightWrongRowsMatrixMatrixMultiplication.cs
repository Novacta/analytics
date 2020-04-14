// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 1  4  7          <para /> 
    /// 2  5  8          <para /> 
    /// 3  6  9          <para /> 
    ///
    /// r   =           <para /> 
    /// 1.0  0.0        <para /> 
    /// 0.0  0.0        <para /> 
    ///
    /// l * r  =>           <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixMultiplication{DoubleMatrixState}" />
    class RightWrongRowsMatrixMatrixMultiplication :
        TestableMatrixMatrixMultiplication<ArgumentException>
    {
        RightWrongRowsMatrixMatrixMultiplication() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_MULTIPLY_INNER_DIMS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableDoubleMatrix14.Get(),
                right: TestableDoubleMatrix09.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongRowsMatrixMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongRowsMatrixMatrixMultiplication"/> class.</returns>
        public static RightWrongRowsMatrixMatrixMultiplication Get()
        {
            return new RightWrongRowsMatrixMatrixMultiplication();
        }
    }
}
