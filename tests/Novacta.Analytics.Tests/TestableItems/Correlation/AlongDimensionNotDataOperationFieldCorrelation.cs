﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Correlation
{
    /// <summary>
    /// Represents a covariance operation whose data operation
    /// parameter is not a field of <see cref="DataOperation"/>.
    /// </summary>
    class AlongDimensionNotDataOperationFieldCorrelation :
        AlongDimensionCorrelation<ArgumentException>
    {
        protected AlongDimensionNotDataOperationFieldCorrelation() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION" }),
                    paramName: "dataOperation"),
                data: TestableDoubleMatrix32.Get(),
                dataOperation: (DataOperation)(-1))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionNotDataOperationFieldCorrelation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionNotDataOperationFieldCorrelation"/> class.</returns>
        public static AlongDimensionNotDataOperationFieldCorrelation Get()
        {
            return new AlongDimensionNotDataOperationFieldCorrelation();
        }
    }
}