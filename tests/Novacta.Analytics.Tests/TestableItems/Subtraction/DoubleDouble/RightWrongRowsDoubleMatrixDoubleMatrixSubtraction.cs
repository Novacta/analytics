// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
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
    /// l - r  =>           <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixSubtraction{DoubleMatrixState}" />
    class RightWrongRowsDoubleMatrixDoubleMatrixSubtraction :
        TestableDoubleMatrixDoubleMatrixSubtraction<ArgumentException>
    {
        RightWrongRowsDoubleMatrixDoubleMatrixSubtraction() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_SUBTRACT_ALL_DIMS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableDoubleMatrix14.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongRowsDoubleMatrixDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongRowsDoubleMatrixDoubleMatrixSubtraction"/> class.</returns>
        public static RightWrongRowsDoubleMatrixDoubleMatrixSubtraction Get()
        {
            return new RightWrongRowsDoubleMatrixDoubleMatrixSubtraction();
        }
    }
}
