// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable summary operation.
    /// </summary>
    /// <typeparam name="TData">The type of the data operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableSummaryOperation<TData, TExpected>
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

    #region Not adjustable for bias

    /// <summary>
    /// Represents a testable, not adjustable for bias operation which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllNotAdjustableForBiasSummaryOperation<TExpected> :
        TestableSummaryOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="NotAdjustableForBiasOnAllSummaryOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected OnAllNotAdjustableForBiasSummaryOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    Func<DoubleMatrix, double>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, double>[]
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
        public Func<DoubleMatrix, double>[]
        DataWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, double>[]
        DataReadOnlyOps
        { get; private set; }
    }

    /// <summary>
    /// Represents a testable, not adjustable for bias summary operation which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionNotAdjustableForBiasSummaryOperation<TExpected> :
        TestableSummaryOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionNotAdjustableForBiasSummaryOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="dataOperation">
        /// A value signaling if the operation must be executed 
        /// on the rows or the columns of the <paramref name="data"/>
        /// operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected AlongDimensionNotAdjustableForBiasSummaryOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DataOperation dataOperation,
                    Func<DoubleMatrix, DataOperation, DoubleMatrix>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, DataOperation, DoubleMatrix>[]
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
        public Func<DoubleMatrix, DataOperation, DoubleMatrix>[]
        DataWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DataOperation, DoubleMatrix>[]
        DataReadOnlyOps
        { get; private set; }
    }

    #endregion

    #region Adjustable for bias ops

    /// <summary>
    /// Represents a testable, adjustable for bias summary operation 
    /// that is adjustable for bias.
    /// </summary>
    /// <typeparam name="TData">The type of the data operand.</typeparam>
    /// <typeparam name="TExpected">
    /// The type of the expected result or exception.</typeparam>
    class TestableAdjustableForBiasSummaryOperation<TData, TExpected>
        : TestableSummaryOperation<TData, TExpected>
    {
        /// <summary>
        /// Gets a value signaling if the operation is adjusted for bias.
        /// </summary>
        /// <value>The value.</value>
        public bool AdjustForBias { get; protected set; }
    }

    /// <summary>
    /// Represents a testable, adjustable for bias operation which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllAdjustableForBiasSummaryOperation<TExpected> :
        TestableAdjustableForBiasSummaryOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="OnAllAdjustableForBiasSummaryOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="adjustForBias">A value to signal if the operation is 
        /// adjusted for bias.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected OnAllAdjustableForBiasSummaryOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    bool adjustForBias,
                    Func<DoubleMatrix, bool, double>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, bool, double>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.AdjustForBias = adjustForBias;
            this.DataWritableOps = dataWritableOps;
            this.DataReadOnlyOps = dataReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, bool, double>[]
        DataWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, bool, double>[]
        DataReadOnlyOps
        { get; private set; }
    }

    /// <summary>
    /// Represents a testable, adjustable for bias summary operation which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionAdjustableForBiasSummaryOperation<TExpected> :
        TestableAdjustableForBiasSummaryOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionAdjustableForBiasSummaryOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="adjustForBias">A value to signal if the operation is 
        /// adjusted for bias.</param>
        /// <param name="dataOperation">
        /// A value signaling if the operation must be executed 
        /// on the rows or the columns of the <paramref name="data"/>
        /// operand.</param>
        /// <param name="dataWritableOps">The ops having writable data.</param>
        /// <param name="dataReadOnlyOps">The ops having read only data.</param>
        protected AlongDimensionAdjustableForBiasSummaryOperation(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    bool adjustForBias,
                    DataOperation dataOperation,
                    Func<DoubleMatrix, bool, DataOperation, DoubleMatrix>[]
                        dataWritableOps,
                    Func<ReadOnlyDoubleMatrix, bool, DataOperation, DoubleMatrix>[]
                        dataReadOnlyOps)
        {
            this.Expected = expected;
            this.Data = data;
            this.AdjustForBias = adjustForBias;
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
        public Func<DoubleMatrix, bool, DataOperation, DoubleMatrix>[]
        DataWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators acting on data of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, bool, DataOperation, DoubleMatrix>[]
        DataReadOnlyOps
        { get; private set; }
    }

    #endregion
}
