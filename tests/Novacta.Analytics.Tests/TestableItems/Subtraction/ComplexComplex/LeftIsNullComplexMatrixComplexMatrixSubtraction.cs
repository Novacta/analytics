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
    /// <seealso cref="TestableComplexMatrixComplexMatrixSubtraction{ArgumentNullException}" />
    class LeftIsNullComplexMatrixComplexMatrixSubtraction :
        TestableComplexMatrixComplexMatrixSubtraction<ArgumentNullException>
    {
        LeftIsNullComplexMatrixComplexMatrixSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableComplexMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullMatrixMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullMatrixMatrixSubtraction"/> class.</returns>
        public static LeftIsNullComplexMatrixComplexMatrixSubtraction Get()
        {
            return new LeftIsNullComplexMatrixComplexMatrixSubtraction();
        }
    }
}
