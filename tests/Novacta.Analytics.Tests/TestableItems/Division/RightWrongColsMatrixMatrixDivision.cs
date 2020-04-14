// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
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
    /// l / r  =>           <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixDivision{DoubleMatrixState}" />
    class RightWrongColsMatrixMatrixDivision :
        TestableMatrixMatrixDivision<ArgumentException>
    {
        RightWrongColsMatrixMatrixDivision() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_DIVIDE_COLUMNS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix09.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongColsMatrixMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongColsMatrixMatrixDivision"/> class.</returns>
        public static RightWrongColsMatrixMatrixDivision Get()
        {
            return new RightWrongColsMatrixMatrixDivision();
        }
    }
}
