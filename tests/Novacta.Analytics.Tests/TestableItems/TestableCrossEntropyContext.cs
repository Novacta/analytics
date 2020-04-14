// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents the expected behavior of a 
    /// <see cref="TestableCrossEntropyContext"/> instance.
    /// </summary>
    /// <typeparam name="TCrossEntropyContext">
    /// The type of the Cross-Entropy context.
    /// </typeparam>
    class TestableCrossEntropyContext<TCrossEntropyContext>
        where TCrossEntropyContext : CrossEntropyContext
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="TestableCrossEntropyContext"/>
        /// class.</summary>
        /// <param name="stateDimension">The expected state dimension.</param>
        /// <param name="eliteSampleDefinition">The expected elite sample definition.</param>
        /// <param name="traceExecution">The expected value about tracing context execution.</param>
        public TestableCrossEntropyContext(
            TCrossEntropyContext context,
            int stateDimension,
            EliteSampleDefinition eliteSampleDefinition,
            bool traceExecution
            )
        {
            this.Context = context;
            this.StateDimension = stateDimension;
            this.EliteSampleDefinition = eliteSampleDefinition;
            this.TraceExecution = traceExecution;
        }

        /// <summary>Gets or sets the context to test.</summary>
        /// <value>The context to test.</value>
        public TCrossEntropyContext Context { get; }

        /// <summary>Gets the expected state dimension.</summary>
        /// <value>The expected state dimension.</value>
        public int StateDimension { get; }

        /// <summary>Gets the expected elite sample definition.</summary>
        /// <value>The expected elite sample definition.</value>
        public EliteSampleDefinition EliteSampleDefinition { get; }

        /// <summary>Gets the expected value about tracing context execution.</summary>
        /// <value>The expected value about tracing context execution.</value>
        public bool TraceExecution { get; }
    }
}
