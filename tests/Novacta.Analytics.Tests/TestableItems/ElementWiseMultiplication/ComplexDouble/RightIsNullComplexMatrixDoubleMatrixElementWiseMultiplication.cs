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
    /// <seealso cref="TestableComplexMatrixDoubleMatrixElementWiseMultiplication{ArgumentNullException}" />
    class RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication :
        TestableComplexMatrixDoubleMatrixElementWiseMultiplication<ArgumentNullException>
    {
        public RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication"/> class.</returns>
        public static RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication Get()
        {
            return new RightIsNullComplexMatrixDoubleMatrixElementWiseMultiplication();
        }
    }
}
