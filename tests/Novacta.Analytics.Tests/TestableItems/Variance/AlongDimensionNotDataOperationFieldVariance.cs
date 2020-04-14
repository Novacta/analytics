// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operation
    /// parameter is not a field of <see cref="DataOperation"/>.
    /// </summary>
    class AlongDimensionNotDataOperationFieldVariance :
        AlongDimensionVariance<ArgumentException>
    {
        protected AlongDimensionNotDataOperationFieldVariance(
            bool adjustForBias) :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION" }),
                    paramName: "dataOperation"),
                data: TestableDoubleMatrix32.Get(),
                adjustForBias: adjustForBias,
                dataOperation: (DataOperation)(-1))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionNotDataOperationFieldVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionNotDataOperationFieldVariance"/> class.</returns>
        public static AlongDimensionNotDataOperationFieldVariance Get(
            bool adjustForBias)
        {
            return new AlongDimensionNotDataOperationFieldVariance(
                adjustForBias);
        }
    }
}