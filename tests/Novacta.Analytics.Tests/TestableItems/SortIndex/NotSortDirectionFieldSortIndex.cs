﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.SortIndex
{
    /// <summary>
    /// Represents a sort index operation whose sort direction
    /// parameter is not a field of <see cref="SortDirection"/>.
    /// </summary>
    class NotSortDirectionFieldSortIndex :
        SortIndexOperation<ArgumentException>
    {
        protected NotSortDirectionFieldSortIndex() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_NOT_FIELD_OF_SORT_DIRECTION" }),
                    paramName: "sortDirection"),
                data: TestableDoubleMatrix32.Get(),
                sortDirection: (SortDirection)(-1))
        {
        }

        /// <summary>
        /// Gets an instance of 
        /// the <see cref="NotSortDirectionFieldSortIndex"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="NotSortDirectionFieldSortIndex"/> class.</returns>
        public static NotSortDirectionFieldSortIndex Get()
        {
            return new NotSortDirectionFieldSortIndex();
        }
    }
}