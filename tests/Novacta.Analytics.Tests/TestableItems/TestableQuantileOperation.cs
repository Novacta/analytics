// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable summary operation returning 
    /// quantiles of the specified operand.
    /// </summary>
    /// <typeparam name="TData">The type of the data operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableQuantileOperation<TData, TExpected>
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
        /// Gets the probabilities whose quantiles 
        /// need to be computed by the operation.
        /// </summary>
        /// <value>The operand.</value>
        public DoubleMatrix Probabilities { get; protected set; }
    }

    /// <summary>
    /// Represents a testable operation which returns
    /// quantiles of all items in a matrix.
    /// </summary>
    class OnAllQuantileOperation<TExpected> :
        TestableQuantileOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="OnAllQuantileOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="probabilities">The probabilities.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected OnAllQuantileOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DoubleMatrix probabilities,
                    Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.Probabilities = probabilities;
            this.DataWritableOps = dataWritableOps;
            this.DataReadOnlyOps = dataReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[]
        DataWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[]
        DataReadOnlyOps
        { get; private set; }
    }

    /// <summary>
    /// Represents a testable operation which returns quantiles
    /// along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionQuantileOperation<TExpected> :
        TestableQuantileOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="AlongDimensionQuantileOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="probabilities">The probabilities.</param>
        /// <param name="dataOperation">A value signaling if the operation must be executed
        /// on the rows or the columns of the <paramref name="data" />
        /// operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected AlongDimensionQuantileOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DoubleMatrix probabilities,
                    DataOperation dataOperation,
                    Func<DoubleMatrix, DoubleMatrix, DataOperation, DoubleMatrix[]>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, DoubleMatrix, DataOperation, DoubleMatrix[]>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.Probabilities = probabilities;
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
        public Func<DoubleMatrix, DoubleMatrix, DataOperation, DoubleMatrix[]>[]
        DataWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DoubleMatrix, DataOperation, DoubleMatrix[]>[]
        DataReadOnlyOps
        { get; private set; }
    }
}