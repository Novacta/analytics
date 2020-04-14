// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose data operation
    /// parameter is not a field of <see cref="DataOperation"/>.
    /// </summary>
    class AlongDimensionNotDataOperationFieldQuantile :
        AlongDimensionQuantile<ArgumentException>
    {
        protected AlongDimensionNotDataOperationFieldQuantile() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION" }),
                    paramName: "dataOperation"),
                data: TestableDoubleMatrix32.Get(),
                probabilities: DoubleMatrix.Identity(2),
                dataOperation: (DataOperation)(-1))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionNotDataOperationFieldQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionNotDataOperationFieldQuantile"/> class.</returns>
        public static AlongDimensionNotDataOperationFieldQuantile Get()
        {
            return new AlongDimensionNotDataOperationFieldQuantile();
        }
    }
}