// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents the results of the execution of a
    /// Cross-Entropy program represented by a 
    /// <see cref="SystemPerformanceOptimizer"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Cross-Entropy program represented by a
    /// <see cref="SystemPerformanceOptimizer"/> 
    /// object can be executed 
    /// by calling its method
    /// <see cref="SystemPerformanceOptimizer.Optimize(
    /// SystemPerformanceOptimizationContext, double, int)"/>, which
    /// returns an instance of class 
    /// <see cref="SystemPerformanceOptimizationResults"/>.
    /// </para>    
    /// <para>
    /// The argument that
    /// optimizes the performance function of the context in 
    /// which the program has been executed is returned
    /// by property <see cref="OptimalState"/>, while the 
    /// value taken on at it by the performance function
    /// can be inspected via property
    /// <see cref="OptimalPerformance"/>.
    /// </para>
    /// <para>
    /// Property <see cref="CrossEntropyResults.HasConverged"/>
    /// returns <c>true</c> only if the program stops
    /// before having executed the maximum allowed number 
    /// of iterations, as specified by 
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MaximumNumberOfIterations"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="SystemPerformanceOptimizer"/>
    public class SystemPerformanceOptimizationResults 
        : CrossEntropyResults
    {
        /// <summary>
        /// Gets the value taken on by the performance
        /// when evaluated at the <see cref="OptimalState"/>
        /// of a <see cref="SystemPerformanceOptimizationContext"/>.
        /// </summary>
        /// <value>
        /// The value taken on by the performance
        /// of a <see cref="SystemPerformanceOptimizationContext"/>
        /// when evaluated at the <see cref="OptimalState"/>.
        /// </value>
        public double OptimalPerformance { get; internal set; }

        /// <summary>
        /// Gets a value representing the argument that
        /// optimizes the performance function of a
        /// <see cref="SystemPerformanceOptimizationContext"/>.
        /// </summary>
        /// <value>
        /// The argument that
        /// optimizes the performance function of a
        /// <see cref="SystemPerformanceOptimizationContext"/>.
        /// </value>
        public DoubleMatrix OptimalState { get; internal set; }
    }
}
