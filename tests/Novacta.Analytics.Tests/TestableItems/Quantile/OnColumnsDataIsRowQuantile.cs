﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose data operand 
    /// has one row.
    /// </summary>
    class OnColumnsDataIsRowQuantile :
        AlongDimensionQuantile<DoubleMatrixState[]>
    {
        protected OnColumnsDataIsRowQuantile() :
            base(
                expected: [
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix21.Get().AsDense[0] ],
                        numberOfRows: 1,
                        numberOfColumns: 1),                    
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix21.Get().AsDense[1] ],
                        numberOfRows: 1,
                        numberOfColumns: 1),
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix21.Get().AsDense[2] ],
                        numberOfRows: 1,
                        numberOfColumns: 1)
                ],
                data: TestableDoubleMatrix21.Get(),
                probabilities: DoubleMatrix.Identity(1),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowQuantile"/> class.</returns>
        public static OnColumnsDataIsRowQuantile Get()
        {
            return new OnColumnsDataIsRowQuantile();
        }
    }
}