// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable operation sorting
    /// entries of the specified operand.
    /// </summary>
    /// <typeparam name="TData">The type of the data operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableSortingOperation<TData, TExpected>
    {
        /// <summary>
        /// Gets the expected state of the operation result, if any;
        /// otherwise the expected exception.
        /// </summary>
        /// <value>The expected behavior of the operation.</value>
        public TExpected Expected { get; protected set; }

        /// <summary>
        /// Gets the operand 
        /// of the operation.
        /// </summary>
        /// <value>The operand.</value>
        public TData Data { get; protected set; }

        /// <summary>
        /// Gets the constant to specify if to sort in ascending
        /// or descending order.
        /// </summary>
        /// <value>The constant.</value>
        public SortDirection SortDirection { get; protected set; }
    }

    /// <summary>
    /// Represents a testable operation which sorts
    /// all items in a matrix.
    /// </summary>
    class SortOperation<TExpected> :
        TestableSortingOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="SortOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="sortDirection">The sort direction.</param>
        protected SortOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    SortDirection sortDirection)
        {
            this.Expected = expected;
            this.Data = data;
            this.SortDirection = sortDirection;
            this.DataWritableOps = new Func<DoubleMatrix, SortDirection, DoubleMatrix>[1] { Stat.Sort };

            this.DataReadOnlyOps = new Func<ReadOnlyDoubleMatrix, SortDirection, DoubleMatrix>[1] { Stat.Sort };
        }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, SortDirection, DoubleMatrix>[]
        DataWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, SortDirection, DoubleMatrix>[]
        DataReadOnlyOps
        { get; private set; }
    }

    /// <summary>
    /// Represents a testable operation which sorts
    /// all items in a matrix and similarly arranges
    /// the corresponding linear indexes.
    /// </summary>
    class SortIndexOperation<TExpected> :
        TestableSortingOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="SortIndexOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="sortDirection">The sort direction.</param>
        protected SortIndexOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    SortDirection sortDirection)
        {
            this.Expected = expected;
            this.Data = data;
            this.SortDirection = sortDirection;
            this.DataWritableOps = new Func<DoubleMatrix, SortDirection, SortIndexResults>[1] { Stat.SortIndex };
            this.DataReadOnlyOps = new Func<ReadOnlyDoubleMatrix, SortDirection, SortIndexResults>[1] { Stat.SortIndex };
        }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, SortDirection, SortIndexResults>[]
        DataWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, SortDirection, SortIndexResults>[]
        DataReadOnlyOps
        { get; private set; }
    }
}