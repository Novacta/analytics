// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable summary operation returning 
    /// minimum or maximum of the specified operand.
    /// </summary>
    /// <typeparam name="TData">The type of the data operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableExtremumOperation<TData, TExpected>
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
    }

    /// <summary>
    /// Represents a testable operation which returns
    /// extrema of all items in a matrix.
    /// </summary>
    class OnAllExtremumOperation<TExpected> :
        TestableExtremumOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="OnAllExtremumOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected OnAllExtremumOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    Func<DoubleMatrix, IndexValuePair>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, IndexValuePair>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.DataWritableOps = dataWritableOps;
            this.DataReadOnlyOps = dataReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, IndexValuePair>[]
        DataWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, IndexValuePair>[]
        DataReadOnlyOps
        { get; private set; }
    }

    /// <summary>
    /// Represents a testable operation which returns extrema
    /// along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionExtremumOperation<TExpected> :
        TestableExtremumOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionExtremumOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="dataOperation">
        /// A value signaling if the operation must be executed 
        /// on the rows or the columns of the <paramref name="data"/>
        /// operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected AlongDimensionExtremumOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DataOperation dataOperation,
                    Func<DoubleMatrix, DataOperation, IndexValuePair[]>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, DataOperation, IndexValuePair[]>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.DataOperation = dataOperation;
            this.DataWritableOps = dataWritableOps;
            this.DataReadOnlyOps = dataReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets a value signaling along what dimension
        /// the items are summarized.
        /// </summary>
        /// <value>The operators.</value>
        public DataOperation DataOperation
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, DataOperation, IndexValuePair[]>[]
        DataWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DataOperation, IndexValuePair[]>[]
        DataReadOnlyOps
        { get; private set; }
    }
}
