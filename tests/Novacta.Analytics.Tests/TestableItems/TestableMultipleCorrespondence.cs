// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a tested
    /// <see cref="Analytics.MultipleCorrespondence"/> instance. 
    /// </summary>
    class TestableMultipleCorrespondence
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableMultipleCorrespondence"/>
        /// class.</summary>
        /// <param name="multipleCorrespondence">
        /// The multiple correspondence to test.</param>
        /// <param name="individuals">The expected individuals.</param>
        /// <param name="categories">The expected categories.</param>
        public TestableMultipleCorrespondence(
            MultipleCorrespondence multipleCorrespondence,
            TestablePrincipalProjections individuals,
            TestablePrincipalProjections categories
            )
        {
            this.MultipleCorrespondence = multipleCorrespondence;
            this.Individuals = individuals;
            this.Categories = categories;
        }

        /// <summary>Gets the multiple correspondence to test.</summary>
        /// <value>The correspondence to test.</value>
        public MultipleCorrespondence MultipleCorrespondence { get; }

        /// <summary>Gets a testable version of the individual
        /// principal projections.</summary>
        /// <value>The testable individual principal projections.</value>
        public TestablePrincipalProjections Individuals { get; }

        /// <summary>Gets a testable version of the category
        /// principal projections.</summary>
        /// <value>The testable category principal projections.</value>
        public TestablePrincipalProjections Categories { get; }
    }
}
