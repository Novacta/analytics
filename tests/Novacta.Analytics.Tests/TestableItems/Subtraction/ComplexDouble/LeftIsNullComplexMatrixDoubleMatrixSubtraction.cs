// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a subtraction operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixSubtraction{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleMatrixSubtraction :
        TestableComplexMatrixDoubleMatrixSubtraction<ArgumentNullException>
    {
        LeftIsNullComplexMatrixDoubleMatrixSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullMatrixMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullMatrixMatrixSubtraction"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleMatrixSubtraction Get()
        {
            return new LeftIsNullComplexMatrixDoubleMatrixSubtraction();
        }
    }
}
