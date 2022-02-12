// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a subtraction between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixSubtraction{ArgumentNullException}" />
    class RightIsNullComplexMatrixComplexMatrixSubtraction :
        TestableComplexMatrixComplexMatrixSubtraction<ArgumentNullException>
    {
        public RightIsNullComplexMatrixComplexMatrixSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixComplexMatrixSubtraction"/> class.</returns>
        public static RightIsNullComplexMatrixComplexMatrixSubtraction Get()
        {
            return new RightIsNullComplexMatrixComplexMatrixSubtraction();
        }
    }
}
