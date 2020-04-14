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
    /// <seealso cref="TestableMatrixMatrixElementWiseMultiplication{ArgumentNullException}" />
    class RightIsNullMatrixMatrixElementWiseMultiplication :
        TestableMatrixMatrixElementWiseMultiplication<ArgumentNullException>
    {
        public RightIsNullMatrixMatrixElementWiseMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullMatrixMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullMatrixMatrixElementWiseMultiplication"/> class.</returns>
        public static RightIsNullMatrixMatrixElementWiseMultiplication Get()
        {
            return new RightIsNullMatrixMatrixElementWiseMultiplication();
        }
    }
}
