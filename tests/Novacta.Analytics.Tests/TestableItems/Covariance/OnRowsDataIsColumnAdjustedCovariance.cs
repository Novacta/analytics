// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Covariance
{
    /// <summary>
    /// Represents a covariance operation whose data operand 
    /// has not enough columns to enable adjusting for bias.
    /// </summary>
    class OnRowsDataIsColumnAdjustedCovariance : AlongDimensionCovariance<ArgumentException>
    {
        protected OnRowsDataIsColumnAdjustedCovariance() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: true,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnAdjustedCovariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnAdjustedCovariance"/> class.</returns>
        public static OnRowsDataIsColumnAdjustedCovariance Get()
        {
            return new OnRowsDataIsColumnAdjustedCovariance();
        }
    }
}