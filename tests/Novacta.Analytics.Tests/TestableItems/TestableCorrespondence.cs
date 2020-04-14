// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a tested
    /// <see cref="Correspondence"/> instance. 
    /// </summary>
    class TestableCorrespondence
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableCorrespondence"/>
        /// class.</summary>
        /// <param name="correspondence">The correspondence to test.</param>
        /// <param name="rowProfiles">The expected row profiles.</param>
        /// <param name="columnProfiles">The expected column profiles.</param>
        public TestableCorrespondence(
            Correspondence correspondence,
            TestablePrincipalProjections rowProfiles,
            TestablePrincipalProjections columnProfiles
            )
        {
            this.Correspondence = correspondence;
            this.RowProfiles = rowProfiles;
            this.ColumnProfiles = columnProfiles;
        }

        /// <summary>Gets the correspondence to test.</summary>
        /// <value>The correspondence to test.</value>
        public Correspondence Correspondence { get; }

        /// <summary>Gets a testable version of the row
        /// principal projections.</summary>
        /// <value>The testable row principal projections.</value>
        public TestablePrincipalProjections RowProfiles { get; }

        /// <summary>Gets a testable version of the row
        /// principal projections.</summary>
        /// <value>The testable row principal projections.</value>
        public TestablePrincipalProjections ColumnProfiles { get; }
    }
}
