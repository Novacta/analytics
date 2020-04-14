// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents the results of the execution of a
    /// <see cref="CrossEntropyProgram"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="CrossEntropyProgram"/> can be executed 
    /// by calling its method
    /// <see cref="CrossEntropyProgram.Run(
    /// CrossEntropyContext, int, double)"/>, which
    /// returns an instance of class <see cref="CrossEntropyResults"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="CrossEntropyProgram"/>
    public class CrossEntropyResults
    {
        internal CrossEntropyResults()
        { }

        /// <summary>
        /// Gets the performance levels achieved by the program 
        /// in its iterations.
        /// </summary>
        /// <value>
        /// The list of performance levels achieved by the
        /// executed Cross-Entropy program.
        /// </value>
        public LinkedList<double> Levels { get; internal set; }

        /// <summary>
        /// Gets the sampling parameters applied by the program in  
        /// its iterations.
        /// </summary>
        /// <value>
        /// The list of sampling parameters exploited by the
        /// executed Cross-Entropy program.
        /// </value>
        public LinkedList<DoubleMatrix> Parameters { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the executed
        /// Cross-Entropy program has converged.
        /// </summary>
        /// <value>
        /// <c>true</c> if the Cross-Entropy program has converged;
        /// otherwise, <c>false</c>.
        /// </value>
        public bool HasConverged { get; internal set; }
    }
}
