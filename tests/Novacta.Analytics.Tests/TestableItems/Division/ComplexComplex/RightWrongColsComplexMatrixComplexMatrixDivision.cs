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
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =                  <para /> 
    /// 1  0       <para /> 
    /// 0  0       <para /> 
    ///
    /// l / r  =>           <para />
    /// throw new ArgumentException ...    <para /> 
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightWrongColsComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ArgumentException>
    {
        RightWrongColsComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_MAT_DIVIDE_COLUMNS_MUST_MATCH" }),
                    paramName: "right"),
                left: TestableComplexMatrix16.Get(),
                right: TestableComplexMatrix09.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightWrongColsComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightWrongColsComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightWrongColsComplexMatrixComplexMatrixDivision Get()
        {
            return new RightWrongColsComplexMatrixComplexMatrixDivision();
        }
    }
}
