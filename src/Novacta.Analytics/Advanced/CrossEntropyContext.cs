// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// A Cross-Entropy context. 
    /// This object is used to implement
    /// the varying steps of
    /// a <see cref="CrossEntropyProgram"/>.
    /// </summary>
    /// <remarks>
    /// <para id='Run.2'>
    /// A <see cref="CrossEntropyProgram"/> is executed by calling its 
    /// <see cref="CrossEntropyProgram.Run(CrossEntropyContext, 
    /// int, double)">Run</see> method. This is a 
    /// template method, in the sense that it defines the 
    /// invariant parts of a Cross-Entropy program.
    /// Such method relies on an instance of 
    /// class <see cref="CrossEntropyContext"/>, which is 
    /// passed as a parameter to it and specifies the 
    /// <em>primitive operations</em> 
    /// of the template method, i.e. 
    /// those varying steps of the algorithm that depends 
    /// on the problem under study.
    /// </para>
    /// <para id='Run.3'>
    /// Class <see cref="CrossEntropyContext"/> defines the 
    /// primitive operations as abstract methods, that its 
    /// subclasses will override to provide the concrete 
    /// behavior of the Cross-Entropy program.
    /// </para>
    /// <para>
    /// A Cross-Entropy program deals with the performance of a system,
    /// and it is responsibility of a <see cref="CrossEntropyContext"/> instance
    /// to define the state-space of the system, the performance function,
    /// how a program executed in such context must update, at each iteration,
    /// its levels and parameters, how the state-space must be sampled while
    /// searching for states having relevant performances, and at what conditions
    /// the program must stop iterating.
    /// </para>
    /// <para><b>Implementing a Cross-Entropy context</b></para>
    /// <para>
    /// The following abstract or virtual methods should be overridden to 
    /// define the context of interest in which 
    /// a <see cref="CrossEntropyProgram"/> must be executed.
    /// </para>
    /// <para><b><i>Sampling step</i></b></para>
    /// <para>
    /// Class <see cref="CrossEntropyProgram"/> can run in parallel 
    /// the generation of the points in the sampling step of a 
    /// Cross-Entropy algorithm. 
    /// The points are represented by the rows of the matrix returned by 
    /// method <see cref="CrossEntropyProgram.Sample"/>. 
    /// The matrix has a number of rows equal to the overall sample size, 
    /// and a number of
    /// columns equal to the <see cref="StateDimension"/>
    /// of the system under study. 
    /// </para>
    /// <para>
    /// The parameter from which samples are drawn while executing
    /// the first iteration of a Cross-Entropy program in this context
    /// is given by property <see cref="InitialParameter"/>.
    /// </para>
    /// <para>
    /// To enable parallel sampling procedures, the collection of row 
    /// indexes of such matrix are partitioned, and each part is assigned to 
    /// a thread in a specific pool for processing.
    /// Method <see cref="PartialSample(double[], 
    /// Tuple{int, int}, 
    /// RandomNumberGenerator, DoubleMatrix, int)">SubSample</see> 
    /// is called when sampling locally to a given thread in the pool, and 
    /// draws the specified range of sample points.
    /// </para>
    /// <para>
    /// When called, 
    /// it is thus expected 
    /// that the method will fill the rows of the sample matrix corresponding 
    /// to the indexes inside the given range.
    /// </para>
    /// <para><b><i>Updating step</i></b></para>
    /// <para>
    /// The <i>updating step</i> is implemented by overriding three methods, 
    /// as follows.
    /// </para>
    /// <para><i>Performance evaluation</i></para>
    /// <para id='PerformanceEvaluation'>
    /// The performance of a state can be defined by overriding 
    /// method <see cref="Performance(DoubleMatrix)"/>.
    /// It is expected that the performance function will accept a row 
    /// vector as a valid representation of a system's state.
    /// </para>
    /// <para><i>Level update</i></para>
    /// <para id='LevelUpdate'>
    /// Method 
    /// <see cref="UpdateLevel(DoubleMatrix, DoubleMatrix, 
    /// EliteSampleDefinition, double, out DoubleMatrix)">UpdateLevel</see> 
    /// is intended to be responsible for sorting the performances, 
    /// compute the corresponding performance <em>level</em> for 
    /// the current iteration and
    /// return the elite sample. As a consequence, a context should also
    /// give its definition of <i>elite sample points</i>, by overriding
    /// property <see cref="EliteSampleDefinition"/>.
    /// </para>
    /// <para><i>Parameter update</i></para>
    /// <para id='ParameterUpdate'>
    /// Method <see cref="UpdateParameter(
    /// LinkedList{DoubleMatrix}, 
    /// DoubleMatrix)">UpdateParameter</see> is
    /// expected to update the parameter of the random mechanism
    /// attending the generation of the sample in the next iteration
    /// of the program.
    /// </para>
    /// <para><b><i>Optional tasks needed at iteration completion</i></b></para>
    /// <para id='OnExecutedIteration'>
    /// Virtual method <see cref="OnExecutedIteration(
    /// int, DoubleMatrix, LinkedList{double}, 
    /// LinkedList{DoubleMatrix})">OnExecutedIteration</see> is
    /// is not a mandatory step of a Cross-Entropy program.
    /// It is executed after completion of the updating step, 
    /// and by default does nothing.
    /// Users can override it to add functionality to each Cross-Entropy iteration.
    /// </para>
    /// <para><b><i>Stopping criterion</i></b></para>
    /// <para id='StoppingCriterion'>
    /// Each iteration executed by the <see cref="CrossEntropyProgram.Run(
    /// CrossEntropyContext,  
    /// int, double)">Run</see> method
    /// of a <see cref="CrossEntropyProgram"/> ends with a 
    /// call to method <see cref="StopExecution(
    /// int, LinkedList{double}, 
    /// LinkedList{DoubleMatrix})">StopExecution</see>, and 
    /// the program stops iterating if it returns
    /// <b>true</b>. 
    /// By overriding the method in a derived context class, it determines
    /// at what conditions the Cross-Entropy program should stop 
    /// at iteration completion.
    /// </para>    
    /// <para><b>Specialized contexts</b></para>
    /// <para>
    /// Contexts specialized for the estimation of rare event probabilities
    /// and the optimization of combinatorial or multi-extremal problems
    /// are given by classes 
    /// <see cref="RareEventProbabilityEstimationContext"/> and
    /// <see cref="SystemPerformanceOptimizationContext"/>,
    /// respectively. 
    /// It is recommended to directly derive from
    /// such classes in order to define the problem of interest, since
    /// they override for you 
    /// methods <see cref="UpdateLevel(DoubleMatrix, DoubleMatrix, 
    /// EliteSampleDefinition, double, 
    /// out DoubleMatrix)">UpdateLevel</see>,
    /// <see cref="StopExecution(int, LinkedList{double}, 
    /// LinkedList{DoubleMatrix})">StopExecution</see>, and
    /// property <see cref="EliteSampleDefinition"/>.
    /// Then, the problems can be solved by executing the 
    /// corresponding specialized programs, 
    /// represented by <see cref="RareEventProbabilityEstimator"/>  
    /// or <see cref="SystemPerformanceOptimizer"/> instances. 
    /// </para>
    /// </remarks>
    /// <seealso cref="SystemPerformanceOptimizationContext"/>
    /// <seealso cref="RareEventProbabilityEstimationContext"/>
    public abstract class CrossEntropyContext
    {
        #region State

        /// <summary>
        /// Gets the parameter initially exploited to sample from 
        /// the state-space of the system defined by this context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the parameter characterizing the distribution
        /// from which sample points are drawn when a
        /// <see cref="CrossEntropyProgram"/> executes its
        /// first iteration in this context.
        /// </para>
        /// </remarks>
        /// <value>
        /// The parameter from which samples are drawn while executing
        /// the first iteration of a Cross-Entropy program in this context.
        /// </value>
        public DoubleMatrix InitialParameter { get; }

        /// <summary>
        /// Gets or sets the dimension of a vector representing a 
        /// system's state
        /// when 
        /// a <see cref="CrossEntropyProgram"/> executes in this
        /// context.
        /// </summary>
        /// <value>The state dimension.</value>
        public int StateDimension { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the 
        /// execution of this context must be traced.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If set to <b>true</b>, information about the execution
        /// of a <see cref="CrossEntropyProgram"/> in this context 
        /// is provided as output of methods defined in the
        /// <see cref="Trace"/> class.
        /// </para>
        /// </remarks>
        /// <value>
        /// <b>true</b> if the context must be instrumented;
        /// otherwise, <b>false</b>.
        /// </value>
        public bool TraceExecution { get; set; }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CrossEntropyContext" /> class 
        /// having the specified state dimension.
        /// </summary>
        /// <param name="stateDimension">The state dimension.</param>
        /// <param name="initialParameter">
        /// The parameter from which samples are drawn while executing
        /// the first iteration of a Cross-Entropy program in this context.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="stateDimension"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initialParameter"/> is <b>null</b>.
        /// </exception>
        protected CrossEntropyContext(
            int stateDimension,
            DoubleMatrix initialParameter)
        {
            if (stateDimension < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(stateDimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (initialParameter is null)
            {
                throw new ArgumentNullException(nameof(initialParameter));
            }

            this.StateDimension = stateDimension;
            this.InitialParameter = initialParameter;
        }

        #endregion

        #region Deferred to subclasses

        /// <summary>
        /// Gets the elite sample definition for this context.
        /// </summary>
        /// <value>
        /// <see cref="EliteSampleDefinition.HigherThanLevel"/>
        /// if the elite sample points of this 
        /// context correspond
        /// to the highest performances of the system under study;
        /// <see cref="EliteSampleDefinition.LowerThanLevel"/> if
        /// they correspond to the lowest ones.
        /// </value>
        protected internal abstract EliteSampleDefinition EliteSampleDefinition { get; }

        /// <summary>
        /// Called after completion of each iteration of 
        /// a <see cref="CrossEntropyProgram"/> executing in this
        /// context.
        /// </summary>
        /// <param name="iteration">
        /// The current iteration identifier.</param>
        /// <param name="sample">
        /// The current sample.</param>
        /// <param name="levels">
        /// The performance levels achieved by the program 
        /// in previous iterations.
        /// </param>
        /// <param name="parameters">
        /// The sampling parameters applied by the program in  
        /// previous iterations.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyContext" 
        /// path="para[@id='OnExecutedIteration']"/>
        /// </remarks>
        protected internal virtual void OnExecutedIteration(
            int iteration,
            DoubleMatrix sample,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
        }

        /// <summary>
        /// Defines the performance of a specified state
        /// in this context.
        /// </summary>
        /// <param name="state">
        /// The state whose performance must be evaluated.
        /// </param>
        /// <returns>
        /// The performance of the specified state.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyContext" 
        /// path="para[@id='PerformanceEvaluation']"/>
        /// </remarks>
        protected internal abstract double Performance(DoubleMatrix state);

        /// <summary>
        /// Draws the specified subset of a sample from a distribution 
        /// characterized by the given parameter, using the stated
        /// random number generator. Used when executing the sampling
        /// step of a  <see cref="CrossEntropyProgram"/> running
        /// in this context.
        /// </summary>
        /// <param name="destinationArray">
        /// The destination array that 
        /// receives the overall sampled data.
        /// </param>
        /// <param name="sampleSubsetRange">
        /// The range specifying the subset of sampled points to draw.
        /// </param>
        /// <param name="randomNumberGenerator">
        /// The random number generator exploited to sample.
        /// </param>
        /// <param name="parameter">
        /// The parameter characterizing the probability distribution
        /// from which the sample subset must be drawn.
        /// </param>
        /// <param name="sampleSize">
        /// The overall sample size.
        /// </param>
        /// <remarks>
        /// <para>
        /// Class <see cref="CrossEntropyProgram"/> can run in parallel 
        /// the generation of the points in the sampling step of the 
        /// Cross-Entropy program. 
        /// Such generations can be carried out efficiently in parallel, 
        /// since the sampled points are assumed to be statistically 
        /// independent. 
        /// The points are represented by the rows of the matrix returned by 
        /// method <see cref="CrossEntropyProgram.Sample"/>. 
        /// The matrix has <paramref name="sampleSize"/> rows and a number of
        /// columns equal to the <see cref="StateDimension"/>
        /// of the system under study. 
        /// Parameter <paramref name="destinationArray"/> must be interpreted as
        /// a <see cref="StorageOrder.ColumnMajor"/> ordered, 
        /// <see cref="StorageScheme.Dense"/> representation
        /// of such matrix.
        /// </para>
        /// <para>
        /// To enable parallel sampling procedures, the collection of row 
        /// indexes of such matrix are partitioned, and each part is assigned to 
        /// a thread in a specific pool for processing.
        /// Method <see cref="PartialSample(double[], 
        /// Tuple{int, int}, 
        /// RandomNumberGenerator, DoubleMatrix, int)">SubSample</see> 
        /// is called when sampling locally to a given thread in the pool, and 
        /// receives the range of points to be sampled in the current thread through
        /// parameter <paramref name="sampleSubsetRange"/>.
        /// Each item in the range must thus be a valid row index for the
        /// matrix returned 
        /// by <see cref="CrossEntropyProgram.Sample">Sample</see>, 
        /// which implies that it must be nonnegative and less than the size
        /// of the sample, <paramref name="sampleSize"/>. 
        /// </para>
        /// <para>
        /// Ranges are represented as <see cref="Tuple{T1, T2}"/> instances, 
        /// and must be 
        /// interpreted as <see cref="Tuple{T1, T2}.Item1"/> included, 
        /// <see cref="Tuple{T1, T2}.Item2"/> excluded ranges. When called, 
        /// it is expected 
        /// that the method will fill the rows of the sample matrix corresponding 
        /// to the indexes inside the range.
        /// More thoroughly, let <latex>i_1</latex> be
        /// <see cref="Tuple{T1, T2}.Item1"/>
        /// and <latex>i_2</latex> be <see cref="Tuple{T1, T2}.Item2"/>, and let 
        /// <latex>k</latex> and <latex>n</latex> represent, respectively,
        /// <see cref="StateDimension"/> and <paramref name="sampleSize"/>. Then,
        /// for <latex>i_1 \leq i &lt; i_2</latex> and
        /// <latex>0 \leq j &lt; k</latex>, this method
        /// is expected to fill the  
        /// <latex>\round{(j)\,n + i}</latex>-th position
        /// of <paramref name="destinationArray"/> with the 
        /// <latex>j</latex>-th component of the <latex>i</latex>-th sampled 
        /// point.
        /// </para>
        /// <para>
        /// The method also receive a random number generator which is local 
        /// to the current 
        /// thread. You should not use generators other than this in the body 
        /// of this method.
        /// In fact, a distinct random number generator is assigned to whatever 
        /// thread was scheduled 
        /// to execute <see cref="PartialSample(double[], Tuple{int, int}, 
        /// RandomNumberGenerator, 
        /// DoubleMatrix, int)">SubSample</see>. 
        /// Such generators are recycled together with their 
        /// corresponding threads,
        /// and are statistically mutually independent.
        /// </para>
        /// </remarks>
        protected internal abstract void PartialSample(
            double[] destinationArray,
            Tuple<int, int> sampleSubsetRange,
            RandomNumberGenerator randomNumberGenerator,
            DoubleMatrix parameter,
            int sampleSize);

        /// <summary>
        /// Updates the performance level for the current iteration 
        /// of a  <see cref="CrossEntropyProgram"/> executing in
        /// this context
        /// and determines the corresponding elite sample.
        /// </summary>
        /// <param name="performances">
        /// The performances of the 
        /// points in the current sample.
        /// </param>
        /// <param name="sample">
        /// The current sample.
        /// </param>
        /// <param name="eliteSampleDefinition">
        /// The elite sample mode.
        /// </param>
        /// <param name="rarity">
        /// The rarity applied to define the elite sample.
        /// </param>
        /// <param name="eliteSample">
        /// The elite sample.
        /// </param>
        /// <returns>
        /// The performance level for the current iteration.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyContext" 
        /// path="para[@id='LevelUpdate']"/>
        /// </remarks>
        protected internal abstract double UpdateLevel(
            DoubleMatrix performances,
            DoubleMatrix sample,
            EliteSampleDefinition eliteSampleDefinition,
            double rarity,
            out DoubleMatrix eliteSample);

        /// <summary>
        /// Specifies conditions 
        /// under which 
        /// a <see cref="CrossEntropyProgram"/> executing in this 
        /// context should be considered 
        /// as terminated.
        /// </summary>
        /// <param name="iteration">The current iteration identifier.
        /// </param>
        /// <param name="levels">
        /// The performance levels achieved by the program 
        /// in previous iterations.
        /// </param>
        /// <param name="parameters">
        /// The sampling parameters applied by the program in  
        /// previous iterations.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyContext" 
        /// path="para[@id='StoppingCriterion']"/>
        /// </remarks>
        /// <returns>
        /// <b>true</b> to stop the program; <b>false</b> otherwise.
        /// </returns>
        protected internal abstract bool StopExecution(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters);

        /// <summary>
        /// Updates the 
        /// sampling parameter attending the generation 
        /// of the sample in the next iteration of a 
        ///  <see cref="CrossEntropyProgram"/> executing in 
        ///  this context.
        /// </summary>
        /// <param name="parameters">
        /// The sampling parameters applied by the program in  
        /// previous iterations.
        /// </param>
        /// <param name="eliteSample">
        /// The elite sample drawn in the current iteration.</param>
        /// <returns>
        /// The sampling parameter attending the generation 
        /// of the sample in the next iteration.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyContext" 
        /// path="para[@id='ParameterUpdate']"/>
        /// </remarks>
        protected internal abstract DoubleMatrix UpdateParameter(
            LinkedList<DoubleMatrix> parameters,
            DoubleMatrix eliteSample);

        #endregion
    }
}
