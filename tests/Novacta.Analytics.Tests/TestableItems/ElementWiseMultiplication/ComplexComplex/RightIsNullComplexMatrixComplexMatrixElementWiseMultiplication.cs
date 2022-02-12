// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Represents an element-wise multiplication between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixElementWiseMultiplication{ArgumentNullException}" />
    class RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication :
        TestableComplexMatrixComplexMatrixElementWiseMultiplication<ArgumentNullException>
    {
        public RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication"/> class.</returns>
        public static RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication Get()
        {
            return new RightIsNullComplexMatrixComplexMatrixElementWiseMultiplication();
        }
    }
}
