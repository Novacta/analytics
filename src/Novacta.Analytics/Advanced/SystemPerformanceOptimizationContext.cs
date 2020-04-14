// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a Cross-Entropy context in which the 
    /// optimization of the performance of a system
    /// is obtained by exploiting 
    /// a <see cref="SystemPerformanceOptimizer"/> instance.
    /// </summary>
    /// <remarks>
    /// <para id='Optimize.2'>
    /// A <see cref="SystemPerformanceOptimizer"/> is executed by calling its 
    /// <see cref="SystemPerformanceOptimizer.Optimize(
    /// SystemPerformanceOptimizationContext, double, int)">Optimize</see> 
    /// method. This is a 
    /// template method, in the sense that it defines the 
    /// invariant parts of a Cross-Entropy program for system's performance
    /// optimization.
    /// Such method relies on an instance of 
    /// class <see cref="SystemPerformanceOptimizationContext"/>, which is 
    /// passed as a parameter to it and specifies the 
    /// <em>primitive operations</em> 
    /// of the template method, i.e. 
    /// those varying steps of the algorithm that depends 
    /// on the problem under study.
    /// </para>
    /// <para id='Optimize.3'>
    /// Class <see cref="SystemPerformanceOptimizationContext"/> thoroughly 
    /// defines the system whose performance must be optimized, and supplies the 
    /// primitive operations as abstract or virtual methods, that its 
    /// subclasses will override to provide the concrete 
    /// behavior of the optimizer.
    /// </para>
    /// <para>
    /// A Cross-Entropy optimizer is designed to identify the 
    /// optimal arguments at which the performance function of a
    /// complex system reaches
    /// its minimum or maximum value.
    /// To get the optimal state, the system's state-space 
    /// <latex>\mathcal{X}</latex> is traversed iteratively 
    /// by sampling, at each iteration, from 
    /// a specific density function, member of a parametric 
    /// family  
    /// <latex mode="display">
    /// \mathcal{P}=\{f_v\left(x\right)|v\in \mathcal{V}\},
    /// </latex>
    /// where <latex mode='inline'>x \in \mathcal{X}</latex> is a 
    /// vector representing 
    /// a possible state of the system, 
    /// and <latex mode='inline'>\mathcal{V}</latex> is the set of 
    /// allowable values for parameter <latex mode='inline'>v</latex>.
    /// The parameter exploited at a given iteration <latex>t</latex>
    /// is referred to
    /// as the <i>reference</i> parameter of such iteration and indicated
    /// as <latex>w_{t-1}</latex>.
    /// A minimum number
    /// of iterations, say <latex>m</latex>, must be executed, while a 
    /// number of them up to a maximum, say <latex>M</latex>, is allowed.
    /// </para>
    /// <para>
    /// The parametric space <latex>\mathcal{V}</latex> should 
    /// include a parameter under which all possible states must have 
    /// a real chance of being selected: this parameter
    /// must be specified as the initial <i>reference</i> parameter
    /// <latex>w_0</latex>.
    /// </para>    
    /// <para>
    /// A Cross-Entropy optimizer can solve one of the 
    /// following optimization programs:
    /// <latex mode='display'>
    /// \max_{x\in\mathcal{X}} H\round{x}
    /// \qquad\text{or}\qquad
    /// \min_{x\in\mathcal{X}} H\round{x},
    /// </latex>
    /// where <latex mode='inline'>\mathcal{X}</latex> is the state space 
    /// of the system and 
    /// <latex mode='inline'>H\!\left(x\right)</latex> is the function 
    /// returning the system performance at state  
    /// <latex mode='inline'>x</latex>.
    /// </para>
    /// <para>
    /// At instantiation, the constructor of 
    /// a <see cref="SystemPerformanceOptimizationContext"/> object
    /// will receive information about the optimization under study by
    /// means of parameters representing <latex>w_0</latex>,
    /// <latex>m</latex>, <latex>M</latex>, and a constant stating 
    /// if the optimization goal is a maximization or a minimization.
    /// </para>
    /// <para>
    /// After construction, property 
    /// <see cref="CrossEntropyContext.InitialParameter"/> represents 
    /// <latex mode='inline'>w_0</latex>, while <latex>m</latex>
    /// and <latex>M</latex>
    /// can be inspected, respectively, via properties
    /// <see cref="MinimumNumberOfIterations"/> and
    /// <see cref="MaximumNumberOfIterations"/>. Lastly, 
    /// property 
    /// <see cref="OptimizationGoal"/> 
    /// signals that the performance function
    /// must be maximized if it 
    /// evaluates to the constant <see cref="OptimizationGoal.Maximization"/>, or 
    /// that a minimization is requested
    /// if it evaluates to
    /// the constant <see cref="OptimizationGoal.Minimization"/>.
    /// </para>
    /// <para>
    /// <b>Implementing a context for combinatorial 
    /// and multi-extremal optimization</b>
    /// </para>
    /// <para>
    /// The Cross-Entropy method 
    /// provides an iterative multi step procedure. At each 
    /// iteration <latex mode='inline'>t</latex>, the <i>sampling step</i>
    /// is executed in order to generate diverse candidate states of 
    /// the system, sampled from a distribution 
    /// characterized by the <i>reference parameter</i> of the iteration,
    /// say <latex>w_{t-1} \in \mathcal{V}</latex>. 
    /// Such sample is thus exploited in the <i>updating step</i> as follows.
    /// Firstly, an extreme level 
    /// <latex>\lambda_t</latex> is computed to define 
    /// the <i>elite</i> sampled points, i.e. those points
    /// having the lowest performances (less than <latex>\lambda_t</latex>)
    /// in case of a minimization,
    /// or the highest ones in case of a maximization (greater than
    /// <latex>\lambda_t</latex>).
    /// Secondly, a new <i>reference</i> 
    /// parameter <latex mode='inline'>w_t \in \mathcal{V}</latex> is 
    /// identified to modify the distribution from which the samples 
    /// will be obtained in the next iteration: such modification is
    /// based on the <i>elite</i> sample points only, in order to improve 
    /// the probability of sampling relevant states, i.e. those 
    /// states corresponding to the performance excesses of interest
    /// (See the documentation of 
    /// class <see cref="CrossEntropyProgram"/> for a 
    /// thorough discussion of the Cross-Entropy method). 
    /// </para>
    /// <para>
    /// When the Cross-Entropy method is applied in an optimization
    /// context, a final <i>optimizing step</i> is executed, in which  
    /// the argument corresponding to the searched extremum  
    /// is effectively identified.
    /// </para>
    /// <para>These steps must be implemented as follows.</para>
    /// <para><b><i>Sampling step</i></b></para>
    /// <para id='Sample.1'>
    /// Since class <see cref="SystemPerformanceOptimizationContext"/> derives 
    /// from <see cref="CrossEntropyContext"/>,  
    /// property <see cref="CrossEntropyContext.StateDimension"/> 
    /// will return the dimension of the points in the Cross-Entropy samples.  
    /// If a state <latex mode='inline'>x</latex> of the system 
    /// can be represented as a vector of length 
    /// <latex mode='inline'>D</latex>, as in 
    /// <latex mode='inline'>x=\left(x_{0},\dots,x_{D-1}\right)</latex>, then 
    /// <latex mode='inline'>D</latex> should be returned.
    /// In addition, method <see cref="CrossEntropyContext.PartialSample(double[], 
    /// Tuple{int, int}, 
    /// RandomNumberGenerator, DoubleMatrix, int)"/> 
    /// must be overriden to determine how to draw the sample locally 
    /// to a given thread when processing in parallel the sampling step.
    /// </para>
    /// <para><b><i>Updating step</i></b></para>
    /// <para>
    /// Class <see cref="SystemPerformanceOptimizationContext"/> overrides 
    /// for you the methods 
    /// required for ordering the system performances of the states sampled 
    /// in the previous step, for updating the 
    /// iteration levels and identifying the corresponding elite sample points.
    /// However, to complete the implementation of the <i>updating step</i>, 
    /// function <latex mode='inline'>H</latex> must be defined via 
    /// method <see cref="CrossEntropyContext.Performance(DoubleMatrix)"/>,
    /// and method <see cref="CrossEntropyContext.UpdateParameter(
    /// LinkedList{DoubleMatrix}, DoubleMatrix)"/> 
    /// also needs to be overridden. 
    /// Given <latex mode='inline'>\lambda_t</latex> and 
    /// <latex mode='inline'>w_{t-1}</latex>, this method is expected to return 
    /// the solution to the following program:
    /// <latex mode="display">
    /// \max_{v\in\mathcal{V}} \frac{1}{N}\sum_{i\in \mathcal{S}\left(w_{t-1},\lambda_t\right)} 
    /// \ln f_{v}\left(X_{t,i}\right),
    /// </latex>
    /// where 
    /// <latex>\mathcal{S}\left(w_{t-1},\lambda_t\right)</latex> is the set 
    /// of elite sample positions (row indexes of the matrix returned 
    /// by method <see cref="CrossEntropyProgram.Sample(
    /// CrossEntropyContext, int, DoubleMatrix)">Sample</see>, with 
    /// <latex>X_{t,i}</latex> being the <latex>i</latex>-th row of such matrix
    /// and <latex>N</latex> being its number of rows.
    /// Method <see cref="CrossEntropyContext.UpdateParameter(
    /// LinkedList{DoubleMatrix}, DoubleMatrix)">UpdateParameter</see> 
    /// receives two parameters. The first is a <see cref="LinkedList{T}"/> 
    /// of <see cref="DoubleMatrix"/> instances, 
    /// representing the <em>reference</em> parameters applied in previous 
    /// iterations. The instance 
    /// returned by property <see cref="LinkedList{T}.Last"/> corresponds to 
    /// parameter <latex mode='inline'>w_{t-1}</latex>. The second method parameter is 
    /// a <see cref="DoubleMatrix"/> instance whose rows represent the elite points
    /// sampled in the current iteration.
    /// </para>
    /// <para><b><i>Optimizing step</i></b></para>
    /// <para>
    /// The optimizing step is executed after that the underlying 
    /// Cross-Entropy program 
    /// has converged. 
    /// In a specified context, it is expected that, 
    /// given a reference parameter 
    /// <latex>w</latex>, a corresponding reasonable value could be 
    /// guessed for the optimizing argument of <latex>H</latex>, 
    /// say <latex>\OA\round{w}</latex>, with <latex>\OA</latex> a function
    /// from <latex>\mathcal{V}</latex> to <latex>\mathcal{X}</latex>. 
    /// Function <latex>\OA</latex> must be defined by overriding method 
    /// <see cref="GetOptimalState(
    /// DoubleMatrix)"/> 
    /// that should return <latex>\OA\round{w}</latex>
    /// given a specific reference parameter <latex>w</latex>.
    /// </para>
    /// <para>
    /// Given the optimal parameter <latex>w_T</latex>, 
    /// (the parameter corresponding to the last iteration 
    /// <latex>T</latex> executed by the algorithm before stopping), 
    /// the argument at which the searched extremum is considered 
    /// as reached according to the Cross-Entropy method will be
    /// returned as <latex>\OA\round{w_T}</latex>.
    /// </para>
    /// <para><b><i>Additional overridable methods</i></b></para>
    /// <para><i>Applying a smoothing scheme to parameters</i></para>
    /// <para id='Smoothing.1'>
    /// Especially when a sampling parameter consists of probabilities, a
    /// <see cref="SystemPerformanceOptimizer"/> could converge to a 
    /// wrong solution
    /// if such parameter is updated without applying a smoothing scheme,
    /// so preventing the probabilities to reach 
    /// <latex>0</latex> or <latex>1</latex> values
    /// too quickly, in the early iterations of the program.
    /// </para>
    /// <para id='Smoothing.2'>
    /// Let <latex>w_t</latex> the reference parameter 
    /// exploited in the last iteration, 
    /// and let <latex>w_{t-1}</latex> the previous one.
    /// By default, this method 
    /// applies the following smoothing scheme:
    /// <latex mode='display'>
    /// w_t \leftarrow \alpha w_t + \round{1 - \alpha} w_{t-1},
    /// </latex>
    /// where <latex>\alpha = .7</latex>.
    /// </para>
    /// <para>
    /// You can override 
    /// method <see cref="SmoothParameter(LinkedList{DoubleMatrix})"/>
    /// in order to apply a smoothing scheme specific to
    /// a given context.
    /// </para>
    /// <para><i>Stopping criterion</i></para>
    /// <para id='Stop.1'>
    /// An iteration is defined as intermediate
    /// for a <see cref="SystemPerformanceOptimizationContext"/> if it is 
    /// greater than <see cref="MinimumNumberOfIterations"/>, and
    /// less than <see cref="MaximumNumberOfIterations"/>.
    /// </para>
    /// <para id='Stop.2'>
    /// For intermediate iterations, method <see cref=
    /// "StopAtIntermediateIteration(
    /// int, LinkedList{double}, LinkedList{DoubleMatrix})"/> is
    /// called to check if a Cross-Entropy program executing in this
    /// context should stop or not.
    /// </para>
    /// <para id='Stop.3'>
    /// By default, the method controls if, in the last 
    /// <see cref="MinimumNumberOfIterations"/> iterations, the 
    /// levels achieved by the program remain constant, and, if so,
    /// return <b>true</b>; otherwise returns <b>false</b>.
    /// </para>
    /// <para>
    /// You can override <see cref="StopAtIntermediateIteration(int, 
    /// LinkedList{double}, LinkedList{DoubleMatrix})">
    /// StopAtIntermediateIteration</see> in order to apply a specific 
    /// stopping criterion for intermediate iterations.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// The following example is based on Section 5.1 
    /// proposed by Rubinstein and Kroese 
    /// (Section 2.2.1)<cite>rubinstein-kroese-2004</cite>.
    /// </para>
    /// <para>
    /// A Cross-Entropy optimizer is exploited 
    /// to minimize the bi-dimensional Rosenbrock function,
    /// showed in the following figure.
    ///</para>
    /// <para>
    /// <image>
    ///   <width>100%</width>
    ///   <alt>Bi-dimensional Rosenbrock function</alt>
    ///   <src>OptimizationExample.png</src>
    /// </image>
    /// </para>
    /// <para>
    /// The global minimum at <latex>\round{1,1,0}</latex>
    /// is indicated by a red dot.
    /// </para>
    /// <para>
    /// In order to solve such problem, we define a Cross-Entropy context 
    /// as follows. 
    /// Since the function under study has two arguments, we analyze a
    /// system whose generic state
    /// <latex mode='inline'>x</latex> can be represented as a 
    /// vector of length 2:
    /// <latex mode="display">
    /// x=\left(x_0,x_1\right),
    /// </latex>    
    /// while the Rosenbrock function is interpreted as the performance 
    /// of the system, i.e. 
    /// <latex mode="display">H\left(x_0,x_1\right)=
    /// 100 \round{x_1 - x_0^2}^2 + \round{x_0 - 1}^2.</latex>
    /// </para>
    /// <para>
    /// The sampling step is accomplished as follows.
    /// Each component <latex>x_j</latex> of a state
    /// <latex>\round{x_0,x_1}</latex> is attached to a 
    /// Gaussian distribution, say <latex>f_{\mu_j,\sigma_j}</latex>,
    /// and 
    /// the <i>j</i>-th entry 
    /// of a state is sampled from <latex>f_{\mu_j,\sigma_j}</latex>, 
    /// independently from the other. 
    /// The Cross-Entropy sampling parameter can thus be 
    /// represented as a <latex>2 \times 2</latex> matrix <latex>w</latex>,
    /// whose first row stores the means of the Gaussian 
    /// distributions, while the second row contains their standard
    /// deviations, so that <latex>w_{0,j} = \mu_j</latex> and
    /// <latex>w_{1,j} = \sigma_j</latex>.
    /// </para>
    /// <para>
    /// The initial parameter sets (arbitrarily) the Gaussian 
    /// means to <latex>-1</latex>.
    /// More importantly, the standard deviations are set equal
    /// to <latex>10000</latex>: in this way, during the first
    /// execution of the sampling
    /// step each argument will have a good likelihood 
    /// of being drawn.
    /// </para>
    /// <para>
    /// At iteration <latex mode='inline'>t</latex>, the parameter's first 
    /// row updating formula is, 
    /// for <latex mode='inline'>j=0,1</latex>,
    /// <latex mode="display">
    /// w_{t,0,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)\,X_{t,i,j}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)},
    /// </latex>
    /// where <latex mode='inline'>A_t \equiv A_{U}\!\left(\lambda_t\right)</latex>
    /// is the set of elite sample in this context, i.e. the set of sample
    /// points having the lowest performances observed during the <latex>t</latex>-th
    /// iteration.
    /// The parameter's second row, that containing the standard deviations, 
    /// is instead updated as follows:
    /// <latex mode="display">
    /// w_{t,1,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)\round{X_{t,i,j} - w_{t,0,j}}^2}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)},
    /// </latex>
    /// </para>
    /// <para>
    /// The so-updated parameter is thus smoothed applying the following formulas
    /// (See Rubinstein and Kroese, 
    /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>.):
    /// <latex mode="display">
    /// w_{t,i,j} \leftarrow \alpha_i\,w_{t,i,j} + \round{1 - \alpha_i} w_{t-1,i,j},
    /// </latex>
    /// where <latex>\alpha_0 = .7</latex>, and
    /// <latex mode="display">
    /// \alpha_1 = \beta \round{1 - \round{1 - \frac{1}{t}}^q},
    /// </latex>
    /// with <latex>\beta = .9</latex> and <latex>q = 6</latex>.
    /// </para>
    /// <para>
    /// The algorithm will stop if each standard deviation in a Cross-Entropy
    /// parameter will be less than <latex>.05</latex>.
    /// </para>
    /// <para>
    /// <code title="Minimizing the bi-dimensional Rosenbrock function.
    /// "
    /// source="..\Novacta.Analytics.CodeExamples\Advanced\CrossEntropyOptimizerExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    ///</example>
    ///<see cref="CrossEntropyContext"/>
    public abstract class SystemPerformanceOptimizationContext 
        : CrossEntropyContext
    {
        #region State

        /// <summary>
        /// Gets a constant specifying if the performance function 
        /// in this context must be minimized or maximized.
        /// </summary>
        /// <value>
        /// <see cref="OptimizationGoal.Maximization"/> if the 
        /// performance function must be maximized; otherwise,
        /// <see cref="OptimizationGoal.Minimization"/>.  
        /// </value>
        public OptimizationGoal OptimizationGoal { get; }

        /// <summary>
        /// Gets the minimum number of iterations 
        /// required by this context.
        /// </summary>
        /// <value>
        /// The minimum number of iterations 
        /// required by this context. 
        /// </value>
        public int MinimumNumberOfIterations { get; }

        /// <summary>
        /// Gets the maximum number of iterations 
        /// allowed by this context. 
        /// </summary>
        /// <value>
        /// The maximum number of iterations 
        /// allowed by this context. 
        /// </value>
        public int MaximumNumberOfIterations { get; }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SystemPerformanceOptimizationContext" /> class 
        /// having the specified state dimension, initial parameter, 
        /// optimization goal, and range of iterations.
        /// </summary>
        /// <param name="stateDimension">
        /// The dimension of a vector representing the state
        /// of the system whose performance must be optimized.
        /// </param>
        /// <param name="optimizationGoal">
        /// A constant to specify if the performance function
        /// must be minimized or maximized.
        /// </param>
        /// <param name="initialParameter">
        /// The parameter initially exploited to sample arguments 
        /// of the performance function while searching
        /// for the optimal one.
        /// </param>
        /// <param name="minimumNumberOfIterations">
        /// The minimum number of iterations 
        /// required by this context.
        /// </param>
        /// <param name="maximumNumberOfIterations">
        /// The maximum number of iterations 
        /// allowed by this context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initialParameter"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="optimizationGoal"/> is not a field of
        /// <see cref="Advanced.OptimizationGoal"/>.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is greater than 
        /// <paramref name="maximumNumberOfIterations"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minimumNumberOfIterations"/> is
        /// not positive.<br/>
        /// -or-<br/>
        /// <paramref name="maximumNumberOfIterations"/> is
        /// not positive.
        /// </exception>
        protected SystemPerformanceOptimizationContext(
            int stateDimension,
            DoubleMatrix initialParameter,
            OptimizationGoal optimizationGoal,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations)
            : base(stateDimension, initialParameter)
        {
            if ((optimizationGoal != OptimizationGoal.Maximization)
                &&
                (optimizationGoal != OptimizationGoal.Minimization)
                )
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL"),
                    nameof(optimizationGoal));
            }

            if (minimumNumberOfIterations < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(minimumNumberOfIterations),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (maximumNumberOfIterations < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maximumNumberOfIterations),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (maximumNumberOfIterations < minimumNumberOfIterations)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        nameof(maximumNumberOfIterations),
                        nameof(minimumNumberOfIterations)),
                    nameof(maximumNumberOfIterations));
            }

            this.OptimizationGoal = optimizationGoal;
            this.MinimumNumberOfIterations = minimumNumberOfIterations;
            this.MaximumNumberOfIterations = maximumNumberOfIterations;
        }

        #endregion

        #region CrossEntropyContext

        ///<inheritdoc/>
        protected internal override EliteSampleDefinition EliteSampleDefinition
        {
            get
            {
                if (this.OptimizationGoal
                    ==
                    OptimizationGoal.Minimization)
                    return EliteSampleDefinition.LowerThanLevel;
                else
                    return EliteSampleDefinition.HigherThanLevel;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// In the <see cref="SystemPerformanceOptimizationContext"/> 
        /// class, this method is implemented as follows.
        /// </para>
        /// <para>
        /// An <paramref name="iteration"/> is defined as intermediate
        /// for a <see cref="SystemPerformanceOptimizationContext"/> if it is 
        /// greater than <see cref="MinimumNumberOfIterations"/>, and
        /// less than <see cref="MaximumNumberOfIterations"/>.
        /// </para>
        /// <para>
        /// A <see cref="SystemPerformanceOptimizationContext"/> never stops if
        /// <paramref name="iteration"/> is less than or equal to 
        /// <see cref="MinimumNumberOfIterations"/>, and always stops
        /// if <paramref name="iteration"/> is greater than or equal to
        /// <see cref="MaximumNumberOfIterations"/>. 
        /// </para>
        /// <para>
        /// For intermediate iterations, overridable method <see cref=
        /// "StopAtIntermediateIteration(
        /// int, LinkedList{double}, LinkedList{DoubleMatrix})"/> is
        /// called to check if a Cross-Entropy program executing in this
        /// context should stop or not after completing an
        /// intermediate iteration.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="levels"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="parameters"/> is <b>null</b>.
        /// </exception>
        protected internal sealed override bool StopExecution(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            if (levels is null)
            {
                throw new ArgumentNullException(nameof(levels));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            bool stopExecution;

            if (iteration == this.MaximumNumberOfIterations)
            {
                stopExecution = true;
            }
            else
            {
                if (this.MinimumNumberOfIterations < iteration)
                {
                    stopExecution = this.StopAtIntermediateIteration(
                        iteration,
                        levels,
                        parameters);
                }
                else
                {
                    stopExecution = false;
                }
            }

            return stopExecution;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="performances"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="sample"/> is <b>null</b>.
        /// </exception>
        protected internal override sealed double UpdateLevel(
            DoubleMatrix performances,
            DoubleMatrix sample,
            EliteSampleDefinition eliteSampleDefinition,
            double rarity,
            out DoubleMatrix eliteSample)
        {
            if (performances is null)
            {
                throw new ArgumentNullException(nameof(performances));
            }

            if (sample is null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            var performanceArray = performances.GetStorage();
            SortHelper.Sort(
                performanceArray,
                SortDirection.Ascending,
                out int[] indexTable);

            if (this.TraceExecution)
            {
                Trace.WriteLine(
                    "Sample points ordered by performance:");
                var sampleInfo = DoubleMatrix.Dense(
                    numberOfRows: sample.NumberOfRows,
                    numberOfColumns: this.StateDimension + 1);
                sampleInfo.SetColumnName(0, "Performance");
                for (int j = 1; j < sampleInfo.NumberOfColumns; j++)
                {
                    sampleInfo.SetColumnName(j, "S" + j);
                }
                sampleInfo[":", 0] = performances;
                sampleInfo[":", IndexCollection.Range(1, this.StateDimension)]
                    = sample[
                        IndexCollection.FromArray(indexTable, false), ":"];

                Trace.WriteLine(sampleInfo);
            }

            int eliteFirstIndex = 0;
            int eliteLastIndex = 0;
            int sampleSize = sample.NumberOfRows;

            double level = Double.NaN;

            // Compute the relevant sample percentile (the level) 
            // and achieved performance
            switch (eliteSampleDefinition)
            {
                case EliteSampleDefinition.HigherThanLevel:
                    eliteFirstIndex = Convert.ToInt32(
                        Math.Ceiling(sampleSize * (1 - rarity)));
                    eliteLastIndex = sampleSize - 1;
                    level = performanceArray[eliteFirstIndex];
                    break;
                case EliteSampleDefinition.LowerThanLevel:
                    eliteFirstIndex = 0;
                    eliteLastIndex = Convert.ToInt32(
                        Math.Floor(sampleSize * rarity));
                    level = performanceArray[eliteLastIndex];
                    break;
            }

            // Update the reference parameter
            var eliteSamplePositions =
                IndexCollection.Range(eliteFirstIndex, eliteLastIndex);

            if (this.TraceExecution)
            {
                Trace.WriteLine(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Elite positions: {0} - {1}.",
                        eliteFirstIndex, 
                        eliteLastIndex));
            }

            var sortedIndexes = 
                IndexCollection.FromArray(indexTable, false);

            eliteSample = sample[sortedIndexes[eliteSamplePositions], ":"];

            return level;
        }

        #endregion

        /// <summary>
        /// Specifies conditions 
        /// under which 
        /// a <see cref="SystemPerformanceOptimizer"/> executing in 
        /// this context should be considered as terminated after
        /// completing an intermediate iteration.
        /// </summary>
        /// <param name="iteration">
        /// The current iteration identifier.
        /// </param>
        /// <param name="levels">
        /// The performance levels achieved in previous iterations 
        /// by a <see cref="SystemPerformanceOptimizer"/>  
        /// executing in this context.
        /// </param>
        /// <param name="parameters">
        /// The sampling parameters applied in previous iterations  
        /// by a <see cref="SystemPerformanceOptimizer"/> executing 
        /// in this context.
        /// </param>
        /// <returns>
        /// <b>true</b> if the optimization program must be stopped; 
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="SystemPerformanceOptimizationContext" 
        /// path="para[@id='Stop.1']"/>
        /// <inheritdoc cref="SystemPerformanceOptimizationContext" 
        /// path="para[@id='Stop.2']"/>
        /// <inheritdoc cref="SystemPerformanceOptimizationContext" 
        /// path="para[@id='Stop.3']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="levels"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="parameters"/> is <b>null</b>.
        /// </exception>
        protected internal virtual bool StopAtIntermediateIteration(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            if (levels is null)
            {
                throw new ArgumentNullException(nameof(levels));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            bool stopExecution = true;

            double lastLevel = levels.Last.Value;
            double currentPreviousLevel;
            LinkedListNode<double> currentPreviousNode = levels.Last.Previous;

            for (int i = 0; i < this.MinimumNumberOfIterations; i++)
            {
                currentPreviousLevel = currentPreviousNode.Value;
                if (currentPreviousLevel != lastLevel)
                {
                    stopExecution = false;
                    break;
                }
                currentPreviousNode = currentPreviousNode.Previous;
            }

            return stopExecution;
        }

        /// <summary>
        /// Provides the smoothing of the updated sampling parameter
        /// of a <see cref="SystemPerformanceOptimizer"/>  
        /// executing in this context.        
        /// </summary>
        /// <param name="parameters">
        /// The sampling parameters applied in previous iterations  
        /// by a <see cref="SystemPerformanceOptimizer"/>  
        /// executing in this context. 
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="SystemPerformanceOptimizationContext" 
        /// path="para[@id='Smoothing.1']"/>
        /// <inheritdoc cref="SystemPerformanceOptimizationContext" 
        /// path="para[@id='Smoothing.2']"/>
        /// <para>
        /// Method <see cref="SmoothParameter(
        /// LinkedList{DoubleMatrix})">SmoothParameter</see> is 
        /// called by <see cref="OnExecutedIteration(int, DoubleMatrix, 
        /// LinkedList{double}, LinkedList{DoubleMatrix})"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a new context class is derived
        /// from <see cref="SystemPerformanceOptimizationContext"/>
        /// that needs to override 
        /// method <see cref="SmoothParameter(LinkedList{DoubleMatrix})"/>.
        /// </para>
        /// <code title="Overriding method SmoothParameter" language="cs">
        /// <![CDATA[
        /// class DerivedContext : SystemPerformanceOptimizationContext
        /// {
        ///     public override void SmoothParameter(
        ///         LinkedList<DoubleMatrix> parameters)
        ///     {
        ///         Console.WriteLine("In SmoothParameter.");
        ///
        ///         double alpha = .7;
        ///
        ///         if (parameters.Count > 1)
        ///         {
        ///             DoubleMatrix currentParameter = parameters.Last.Value;
        ///             DoubleMatrix previousParameter = parameters.Last.Previous.Value;
        ///
        ///             parameters.RemoveLast();
        ///             parameters.AddLast(
        ///                 alpha * currentParameter + (1.0 - alpha) * previousParameter);
        ///         }
        ///     }
        ///     
        ///     // Additional code here. 
        /// }]]>
        /// </code>        
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parameters"/> is <b>null</b>.
        /// </exception>
        protected internal virtual void SmoothParameter(
            LinkedList<DoubleMatrix> parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            double alpha = .7;

            if (parameters.Count > 1)
            {
                DoubleMatrix currentParameter = parameters.Last.Value;
                DoubleMatrix previousParameter = parameters.Last.Previous.Value;

                parameters.Last.Value =
                    alpha * currentParameter + (1.0 - alpha) * previousParameter;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// In the <see cref="SystemPerformanceOptimizationContext"/> 
        /// class, this method calls
        /// <see cref="SmoothParameter(LinkedList{DoubleMatrix})"/>
        /// to provide a smoothing scheme for Cross-Entropy
        /// parameters.
        /// </para>
        /// <note type='caution'>
        /// When overriding this method, please remember to call
        /// its base implementation, or no smoothing scheme will be 
        /// applied to Cross-Entropy parameters.
        /// </note>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a new context class is derived
        /// from <see cref="SystemPerformanceOptimizationContext"/>
        /// that needs to override method <see cref="OnExecutedIteration(
        /// int, DoubleMatrix, LinkedList{double}, LinkedList{DoubleMatrix})"/>.
        /// </para>
        /// <code title="Overriding method OnExecutedIteration" language="cs">
        /// <![CDATA[
        /// class DerivedContext : SystemPerformanceOptimizationContext
        /// {
        ///     public override void OnExecutedIteration(
        ///         int iteration,
        ///         DoubleMatrix sample,
        ///         LinkedList<double> levels,
        ///         LinkedList<DoubleMatrix> parameters)
        ///     {
        ///         Console.WriteLine("Iteration: {0}", iteration);
        ///
        ///         // Calling the base class OnExecutedIteration method.
        ///         base.OnExecutedIteration(
        ///             iteration,
        ///             sample,
        ///             levels,
        ///             parameters);
        ///     }
        ///
        ///     // Additional code here. 
        /// }]]>
        /// </code>
        /// </example>
        protected internal override void OnExecutedIteration(
            int iteration,
            DoubleMatrix sample,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            this.SmoothParameter(parameters);
            base.OnExecutedIteration(iteration, sample, levels, parameters);
        }

        /// <summary>
        /// Gets the argument that optimizes the performance function
        /// in this context, according to the specified 
        /// Cross-Entropy sampling parameter.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The optimal state must be a valid input for the 
        /// <see cref="CrossEntropyContext.Performance(DoubleMatrix)">
        /// performance function</see> 
        /// of the system defined by this context.
        /// </para>
        /// </remarks>
        /// <param name="parameter">
        /// A sampling parameter 
        /// exploited by a <see cref="SystemPerformanceOptimizer"/> during its 
        /// execution.
        /// </param>
        /// <returns>
        /// The state that, according to the specified sampling parameter,
        /// is guessed as the optimal argument of
        /// the performance function
        /// defined by this context.
        /// </returns>
        protected internal abstract DoubleMatrix GetOptimalState(
            DoubleMatrix parameter);
    }
}
