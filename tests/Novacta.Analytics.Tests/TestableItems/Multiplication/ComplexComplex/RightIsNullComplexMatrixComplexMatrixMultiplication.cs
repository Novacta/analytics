// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixMultiplication{ArgumentNullException}" />
    class RightIsNullComplexMatrixComplexMatrixMultiplication :
        TestableComplexMatrixComplexMatrixMultiplication<ArgumentNullException>
    {
        public RightIsNullComplexMatrixComplexMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixComplexMatrixMultiplication"/> class.</returns>
        public static RightIsNullComplexMatrixComplexMatrixMultiplication Get()
        {
            return new RightIsNullComplexMatrixComplexMatrixMultiplication();
        }
    }
}
