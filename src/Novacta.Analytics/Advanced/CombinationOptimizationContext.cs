// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a Cross-Entropy context supporting
    /// the optimization of objective functions whose
    /// arguments are the combinations having the specified
    /// size available from a given set of items.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Class <see cref="CombinationOptimizationContext"/> derives from
    /// <see cref="SystemPerformanceOptimizationContext"/>, and defines
    /// a Cross-Entropy context able to solve combinatorial optimization
    /// problems regarding the selection of fixed size subsets from
    /// a collection of items.
    /// </para>
    /// <para id='Optimize.3'>
    /// Class <see cref="SystemPerformanceOptimizationContext"/> thoroughly 
    /// defines a system whose performance must be optimized. 
    /// Class <see cref="CombinationOptimizationContext"/> specializes 
    /// that system by assuming that its performance,
    /// say <latex>H</latex>, is defined on the set of combinations, 
    /// having the specified size, available from 
    /// a given collection of items.
    /// </para>
    /// <para>
    /// The system's state-space 
    /// <latex>\mathcal{X}</latex>, i.e. the domain of 
    /// <latex>H</latex>, can thus be represented as the Cartesian 
    /// product of 
    /// <latex>n</latex> copies of the set 
    /// <latex>\{0,1\}</latex>, where <latex>n</latex> is the 
    /// number of available items. An argument
    /// <latex>\round{x_0,\dots,x_{n-1}}</latex> defines a combination 
    /// by signaling that the <latex>j</latex>-th item is included in
    /// the combination
    /// if <latex>x_j=1</latex>, otherwise setting <latex>x_j=0</latex>.
    /// </para>
    /// <para>
    /// If the combinations under study have fixed size equal to
    /// <latex>k</latex>, then each argument will have exactly
    /// <latex>k</latex> nonzero entries.
    /// </para>
    /// <para>
    /// A Cross-Entropy optimizer is designed to identify the 
    /// optimal arguments at which the performance function of a
    /// complex system reaches
    /// its minimum or maximum value.
    /// To get the optimal state, the system's state-space 
    /// <latex>\mathcal{X}</latex>, i.e. the domain of 
    /// <latex>H</latex>, is traversed iteratively 
    /// by sampling, at each iteration, from 
    /// a specific density function, member of a parametric 
    /// family  
    /// <latex mode="display">
    /// \mathcal{P}=\{f_v\left(x\right)|v\in \mathcal{V}\},
    /// </latex>
    /// where <latex mode='inline'>x \in \mathcal{X}</latex> is 
    /// a possible argument of <latex>H</latex>, 
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
    /// <b>Implementing a context for optimizing on combinations</b>
    /// </para>
    /// <para>
    /// The Cross-Entropy method 
    /// provides an iterative multi step procedure. In the context
    /// of combinatorial optimization, at each 
    /// iteration <latex mode='inline'>t</latex> a <i>sampling step</i>
    /// is executed in order to generate diverse candidate arguments of 
    /// the objective function, sampled from a distribution 
    /// characterized by the <i>reference parameter</i> of the iteration,
    /// say <latex>w_{t-1} \in \mathcal{V}</latex>. 
    /// Such sample is thus exploited in the <i>updating step</i> in which 
    /// a new <i>reference</i> 
    /// parameter <latex mode='inline'>w_t \in \mathcal{V}</latex> is 
    /// identified to modify the distribution from which the samples 
    /// will be obtained in the next iteration: such modification is
    /// executed in order to improve 
    /// the probability of sampling relevant arguments, i.e. those 
    /// arguments corresponding to the function values of interest
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
    /// <para>These steps have been implemented as follows.</para>
    /// <para><b><i>Sampling step</i></b></para>
    /// <para>
    /// In a <see cref="CombinationOptimizationContext"/>, the parametric 
    /// family <latex>\mathcal{P}</latex> is outlined as follows.
    /// Each component <latex>x_j</latex> of an argument
    /// <latex>\round{x_0,\dots,x_{n-1}}</latex> of <latex>H</latex>
    /// is attached to a independent
    /// Bernoulli distribution having parameter <latex>p_j</latex>,
    /// and <latex>f_v</latex> is defined as the distribution of the sum 
    /// of the corresponding Bernoulli trials, conditional to having 
    /// exactly <latex>k</latex> successes.
    /// The Cross-Entropy sampling parameter <latex>v</latex>
    /// can thus be  
    /// represented as the <latex>1 \times n</latex> vector
    /// <latex>\mx{p_0,\cdots,p_{n-1}}</latex>.
    /// </para>
    /// <para>
    /// The parametric space <latex>\mathcal{V}</latex> should 
    /// include a parameter under which all possible states must have 
    /// a real chance of being selected: this parameter
    /// is specified as the initial <i>reference</i> parameter
    /// <latex>w_0</latex>.
    /// A <see cref="CombinationOptimizationContext"/> defines 
    /// <latex>w_0</latex> as a constant vector whose entries are all
    /// equal to <latex>0.5</latex>. 
    /// </para>    
    /// <para><b><i>Updating step</i></b></para>
    /// <para  id='Updating.1'>
    /// At iteration <latex>t</latex>, let us represent the sample drawn 
    /// as <latex>X_{t,0},\dots,X_{t,N-1}</latex>, where <latex>N</latex> is the 
    /// Cross-Entropy sample size, and the <latex>i</latex>-th sample point
    /// is the sequence <latex>X_{t,i}=\round{X_{t,i,0},\dots,X_{t,i,n-1}}</latex>.
    /// The parameter's 
    /// updating formula is, 
    /// for <latex mode='inline'>j=0,\dots,n-1</latex>,
    /// <latex mode="display">
    /// w_{t,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)\,X_{t,i,j}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)},
    /// </latex>
    /// where <latex mode='inline'>A_t</latex>
    /// is the elite sample in this context, i.e. the set of sample
    /// points having the lowest performances observed during the <latex>t</latex>-th
    /// iteration, if minimizing, the highest ones, otherwise, while
    /// <latex>I_{A_t}</latex> is its indicator function.
    /// </para>
    /// <para><i>Applying a smoothing scheme to updated parameters</i></para>
    /// <para id='Smoothing.1'>
    /// In a <see cref="CombinationOptimizationContext"/>, 
    /// the sampling parameter 
    /// is smoothed applying the following formula
    /// (See Rubinstein and Kroese, 
    /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>):
    /// <latex mode="display">
    /// w_{t,j} \leftarrow \alpha\,w_{t,j} + \round{1 - \alpha} w_{t-1,j},
    /// </latex>
    /// where <latex>0 &lt; \alpha &lt; 1</latex>.
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
    /// Function <latex>\OA</latex> is defined by overriding method 
    /// <see cref="GetOptimalState(
    /// DoubleMatrix)"/> 
    /// that should return <latex>\OA\round{w}</latex>
    /// given a specific reference parameter <latex>w</latex>.
    /// </para>
    /// <para>
    /// Given the optimal parameter (the parameter corresponding to the 
    /// last iteration <latex>T</latex> executed by the algorithm before 
    /// stopping),
    /// <latex mode='display'>
    /// w_T = \mx{
    ///  p_{T,0} \cdots p_{T,n-1} 
    /// },
    /// </latex>
    /// the argument at which the searched extremum is considered 
    /// as reached according to the Cross-Entropy method will be
    /// returned as follows.
    /// The probabilities <latex>p_{T,j}</latex> are sorted in increasing order, 
    /// say obtaining the 
    /// following ordering:
    /// <latex mode="display">
    /// p_{T,\left(0\right)}\leq\dots\leq p_{T,\left(n-1\right)},
    /// </latex>
    /// and the corresponding sequence of indexes
    /// <latex>i_{T,0},\dots,i_{T,n-1}</latex>
    /// such that
    /// <latex mode="display">
    /// p_{T,i_j} = p_{T,\left(j\right)} \quad j=0,\dots,n-1,
    /// </latex>
    /// is taken into account by defining the set 
    /// <latex>B_T = \{i_{T,n-k},\dots,i_{T,n-1}\}</latex>
    /// and returning
    /// <latex mode='display'>
    /// \OA\round{w_T} = \mx{x_{T,0} \cdots x_{T,n-1}},
    /// </latex>
    /// where 
    /// <latex>x_{T,j}</latex> is one if <latex>j \in B_T</latex>;
    /// zero otherwise.
    /// This is equivalent to include in the optimal combination
    /// those items having the greatest <latex>k</latex> probabilities in 
    /// parameter <latex>w_T</latex>.
    /// </para>
    /// <para><b><i>Stopping criterion</i></b></para>
    /// <para id='Stop.1'>
    /// A <see cref="CombinationOptimizationContext"/> never stops before
    /// executing a number of iterations less than 
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MinimumNumberOfIterations"/>, and always stops
    /// if such number is greater than or equal to
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MaximumNumberOfIterations"/>. 
    /// </para>
    /// <para id='Stop.2'>
    /// For intermediate iterations, method <see cref=
    /// "StopAtIntermediateIteration(
    /// int, LinkedList{double}, LinkedList{DoubleMatrix})"/> is
    /// called to check if a Cross-Entropy program executing in this
    /// context should stop or not.
    /// </para>
    /// <para id='Stop.3'>
    /// In a <see cref="CombinationOptimizationContext"/>, the method 
    /// analyzes the currently updated reference parameter, 
    /// say <latex>w_t=\mx{p_{t,0} \cdots p_{t,n-1}}</latex> as follows. 
    /// If condition
    /// <latex mode='display'>
    /// B_t = B_{t-l}, \quad l=1,\dots,m, 
    /// </latex>
    /// can be verified,
    /// the method returns <c>true</c>; otherwise <c>false</c> is returned.
    /// Equivalently, the algorithm converges if the indexes of 
    /// the largest <latex>k</latex> probabilities coincide
    /// <latex>m</latex> times in a row of iterations.
    /// </para>
    /// <para><b>Instantiating a context for optimizing on combinations</b></para>
    /// <para>
    /// At instantiation, the constructor of 
    /// a <see cref="CombinationOptimizationContext"/> object
    /// will receive information about the optimization under study by
    /// means of parameters representing the objective function
    /// <latex>H</latex>, the combination constants
    /// <latex>n</latex> and
    /// <latex>k</latex>, the extremes of the allowed range of
    /// intermediate iterations,
    /// <latex>m</latex> and <latex>M</latex>, and a constant stating 
    /// if the optimization goal is a maximization or a minimization.
    /// In addition, the smoothing parameter <latex>\alpha</latex>
    /// is also 
    /// passed to the constructor.
    /// </para>
    /// <para>
    /// After construction, <latex>m</latex>
    /// and <latex>M</latex>
    /// can be inspected, respectively, via properties
    /// <see cref="SystemPerformanceOptimizationContext.MinimumNumberOfIterations"/> and
    /// <see cref="SystemPerformanceOptimizationContext.MaximumNumberOfIterations"/>. 
    /// The smoothing coefficient <latex>\alpha</latex> is also
    /// available via property
    /// <see cref="ProbabilitySmoothingCoefficient"/>.
    /// Combination constants <latex>n</latex> and <latex>k</latex> are returned by 
    /// <see cref="CrossEntropyContext.StateDimension"/> and
    /// <see cref="CombinationDimension"/>, respectively.
    /// In addition, 
    /// property 
    /// <see cref="SystemPerformanceOptimizationContext.OptimizationGoal"/> 
    /// signals that the performance function
    /// must be maximized if it 
    /// evaluates to the constant <see cref="OptimizationGoal.Maximization"/>, or 
    /// that a minimization is requested
    /// if it evaluates to
    /// the constant <see cref="OptimizationGoal.Minimization"/>.
    /// </para>
    /// <para>
    /// To evaluate the objective function <latex>H</latex> at a
    /// specific argument, one can call method
    /// <see cref="CrossEntropyContext.Performance(DoubleMatrix)"/>
    /// passing the argument as a parameter.
    /// It is expected that the objective function 
    /// will accept a row 
    /// vector having binary entries as a valid representation of an argument.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, an existing partition of twelve items is explained
    /// by selecting two features out of the seven ones available in 
    /// an artificial data set regarding the items under study.
    /// </para>
    /// <para>
    /// The selection criterion is defined as the maximization
    /// of the Dunn Index in the domain of selectable sub data sets.
    /// </para>
    /// <para>
    /// <code title="Selecting features from a data set to explain a given partition
    /// by Dunn Index maximization."
    /// source="..\Novacta.Analytics.CodeExamples\Advanced\CombinationOptimizationContextExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// </example>
    /// <seealso href="https://en.wikipedia.org/wiki/Combination"/>
    public sealed class CombinationOptimizationContext
        : SystemPerformanceOptimizationContext
    {
        #region State

        private readonly LinkedList<SortedSet<int>> largestProbabilityPositions =
            new();

        private readonly Func<DoubleMatrix, double> objectiveFunction;

        /// <summary>
        /// Gets the dimension of a combination represented by a 
        /// system's state
        /// when 
        /// a <see cref="CrossEntropyProgram"/> executes in this
        /// context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the number of items to be taken at a time
        /// in the combinations under study.
        /// </para>
        /// </remarks>
        /// <value>The combination dimension.</value>
        public int CombinationDimension { get; }

        /// <summary>
        /// Gets the coefficient that defines the smoothing scheme 
        /// for the probabilities of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </summary>
        /// <value>
        /// The coefficient to apply when smoothing the
        /// probabilities of the Cross-Entropy parameters.
        /// </value>
        public double ProbabilitySmoothingCoefficient { get; }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CombinationOptimizationContext" /> class 
        /// aimed to optimize the specified 
        /// objective function,
        /// with the given optimization goal, 
        /// range of iterations, and probability smoothing coefficient.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The function to be optimized.
        /// </param>
        /// <param name="stateDimension">
        /// The number of available items.
        /// </param>
        /// <param name="combinationDimension">
        /// The number of items to be taken at a time.
        /// </param>
        /// <param name="optimizationGoal">
        /// A constant to specify if the function
        /// must be minimized or maximized.
        /// </param>
        /// <param name="probabilitySmoothingCoefficient">
        /// A coefficient to define the smoothing scheme for the
        /// probabilities of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </param>
        /// <param name="minimumNumberOfIterations">
        /// The minimum number of iterations 
        /// required by this context.
        /// </param>
        /// <param name="maximumNumberOfIterations">
        /// The maximum number of iterations 
        /// allowed by this context.
        /// </param>
        /// <remarks>
        /// <para>
        /// It is assumed that the <paramref name="objectiveFunction"/> 
        /// will accept row 
        /// vectors as valid representations of an argument.
        /// </para>
        /// <para id='Performance.1'>
        /// Any argument represents a combination
        /// of <paramref name="stateDimension"/>, say
        /// <latex>n</latex>, items
        /// taken <paramref name="combinationDimension"/>, say <latex>k</latex>,
        /// at a time.
        /// An argument, 
        /// say <latex>\round{x_0,\dots,x_{n-1}}</latex>, must have its
        /// <latex>j</latex>-th entry <latex>x_j</latex> equal to <latex>1</latex> 
        /// if the corresponding item is included in
        /// the combination; <latex>0</latex> otherwise.
        /// </para>
        /// <para>
        /// As discussed by Rubinstein and Kroese, 
        /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>,
        /// typical values for <paramref name="probabilitySmoothingCoefficient"/>
        /// are between .7 and 1 (excluded).
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="optimizationGoal"/> is not a field of
        /// <see cref="OptimizationGoal"/>.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is greater than 
        /// <paramref name="maximumNumberOfIterations"/>.
        /// -or-<br/>
        /// <paramref name="combinationDimension"/> is not less than 
        /// <paramref name="stateDimension"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="probabilitySmoothingCoefficient"/> is not
        /// in the open interval between 0 and 1.<br/>
        /// -or-<br/>
        /// <paramref name="stateDimension"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="combinationDimension"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is
        /// not positive.<br/>
        /// -or-<br/>
        /// <paramref name="maximumNumberOfIterations"/> is
        /// not positive.
        /// </exception>
        public CombinationOptimizationContext(
            Func<DoubleMatrix, double> objectiveFunction,
            int stateDimension,
            int combinationDimension,
            double probabilitySmoothingCoefficient,
            OptimizationGoal optimizationGoal,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations)
            : base(
                  stateDimension: stateDimension,
                  initialParameter: GetInitialParameter(stateDimension),
                  optimizationGoal: optimizationGoal,
                  minimumNumberOfIterations: minimumNumberOfIterations,
                  maximumNumberOfIterations: maximumNumberOfIterations)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (
                (probabilitySmoothingCoefficient <= 0.0)
                ||
                (1.0 <= probabilitySmoothingCoefficient)
               )
            {
                throw new ArgumentOutOfRangeException(
                    nameof(probabilitySmoothingCoefficient),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                        "0.0", 
                        "1.0"));
            }

            if (combinationDimension < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(combinationDimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (stateDimension <= combinationDimension)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER"),
                        nameof(combinationDimension),
                        nameof(stateDimension)),
                    nameof(combinationDimension)
                    );
            }

            #endregion

            this.CombinationDimension = combinationDimension;
            this.ProbabilitySmoothingCoefficient = probabilitySmoothingCoefficient;
            this.objectiveFunction = objectiveFunction;
        }

        private static DoubleMatrix GetInitialParameter(
            int stateDimension)
        {
            if (stateDimension <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(stateDimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var initialParameter = DoubleMatrix.Dense(
                1,
                stateDimension,
                .5);

            return initialParameter;
        }

        #endregion

        #region CrossEntropyContext

        /// <summary>
        /// Computes the objective function at a specified argument
        /// as the performance defined in this context.
        /// </summary>
        /// <param name="state">
        /// The argument at which the objective function 
        /// must be evaluated.
        /// </param>
        /// <returns>
        /// The value of the objective function at the 
        /// specified argument.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method has been overridden to define the 
        /// performance of a state 
        /// as the value of the objective function 
        /// at <paramref name="state"/>.
        /// It is expected that the objective function will accept a row 
        /// vector as a valid representation of an argument.
        /// </para>
        /// <para id='Performance.1'>
        /// Any argument represents a combination
        /// of <see cref="CrossEntropyContext.StateDimension"/>, say
        /// <latex>n</latex>, items
        /// taken <see cref="CombinationDimension"/>, say <latex>k</latex>,
        /// at a time.
        /// An argument is expected to be a row vector, 
        /// say <latex>\round{x_0,\dots,x_{n-1}}</latex>, whose
        /// <latex>j</latex>-th entry <latex>x_j</latex> is <latex>1</latex> 
        /// if the corresponding item is included in
        /// the combination; <latex>0</latex> otherwise.
        /// </para>
        /// </remarks>
        protected internal override double Performance(DoubleMatrix state)
        {
            return this.objectiveFunction(state);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method has been overridden to support
        /// the optimization of functions on combinations.
        /// </para>
        /// </remarks>
        protected internal override void PartialSample(
            double[] destinationArray,
            Tuple<int, int> sampleSubsetRange,
            RandomNumberGenerator randomNumberGenerator,
            DoubleMatrix parameter,
            int sampleSize)
        {
            var randomBipartition =
                UnequalProbabilityRandomSampling.
                    FromBernoulliProbabilities(
                        parameter,
                        this.CombinationDimension);
            randomBipartition.RandomNumberGenerator = randomNumberGenerator;

            for (int i = sampleSubsetRange.Item1; i < sampleSubsetRange.Item2; i++)
            {
                var point = randomBipartition.NextIndexCollection();
                for (int j = 0; j < point.Count; j++)
                {
                    destinationArray[i + sampleSize * point[j]] = 1.0;
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method has been overridden to support
        /// the optimization of functions whose arguments
        /// are combinations.
        /// </para>
        /// </remarks>
        protected internal override DoubleMatrix UpdateParameter(
            LinkedList<DoubleMatrix> parameters,
            DoubleMatrix eliteSample)
        {
            var newParameter =
                Stat.Mean(
                    data: eliteSample,
                    dataOperation: DataOperation.OnColumns);

            // No parameter's entries are allowed to be equal to 0 or 1.
            // Before returning, such entries should be modified.
            // Otherwise,
            // UnequalProbabilityRandomSampling.FromBernoulliProbabilities
            // will throw an exception.

            var newParameterArray = newParameter.GetStorage();
            for (int i = 0; i < newParameterArray.Length; i++)
            {
                double value = newParameterArray[i];
                if (value == 0.0)
                {
                    newParameterArray[i] = 1e-9;
                }
                else if (value == 1.0)
                {
                    newParameterArray[i] = .999999999;
                }
            }

            return newParameter;
        }

        /// <inheritdoc/>
        protected internal override void OnExecutedIteration(
            int iteration,
            DoubleMatrix sample,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            var parameter = parameters.Last.Value;
            var sortedIndexes = Stat.SortIndex(
                parameter,
                SortDirection.Ascending).SortedIndexes;

            var n = this.StateDimension;
            var k = this.CombinationDimension;

            var set = new SortedSet<int>();
            for (int j = n - k; j < n; j++)
            {
                set.Add(sortedIndexes[j]);
            }
            this.largestProbabilityPositions.AddLast(set);

            // Calling the base class OnExecutedIteration method.
            base.OnExecutedIteration(
                iteration,
                sample,
                levels,
                parameters);
        }

        #endregion

        #region SystemPerformanceOptimizationContext

        /// <summary>
        /// Gets the argument that optimizes the objective function
        /// in this context, according to the specified 
        /// Cross-Entropy sampling parameter.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The optimal argument must be a valid input for the 
        /// <see cref="CrossEntropyContext.Performance(DoubleMatrix)">
        /// objective (performance) function</see> 
        /// defined by this context.
        /// </para>
        /// </remarks>
        /// <param name="parameter">
        /// A sampling parameter 
        /// exploited by a <see cref="SystemPerformanceOptimizer"/> during its 
        /// execution.
        /// </param>
        /// <returns>
        /// The argument that, according to the specified sampling parameter,
        /// is guessed as the optimal one of
        /// the objective function
        /// defined by this context.
        /// </returns>
        protected internal override DoubleMatrix GetOptimalState(DoubleMatrix parameter)
        {
            int n = this.StateDimension;
            var optimalState = DoubleMatrix.Dense(1, n);

            var sortedIndexes = Stat.SortIndex(
                parameter,
                SortDirection.Ascending).SortedIndexes;

            var k = this.CombinationDimension;
            for (int j = n - k; j < n; j++)
            {
                optimalState[sortedIndexes[j]] = 1.0;
            }

            return optimalState;
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
        /// <inheritdoc cref="CombinationOptimizationContext" 
        /// path="para[@id='Smoothing.1']"/>
        /// <para>
        /// The applied value of <latex>\alpha</latex> is set at 
        /// instantiation and can be returned
        /// by property <see cref="ProbabilitySmoothingCoefficient"/>.
        /// </para>
        /// </remarks>
        protected internal override void SmoothParameter(
            LinkedList<DoubleMatrix> parameters)
        {
            double iteration = Convert.ToDouble(parameters.Count);
            if (iteration > 1)
            {
                DoubleMatrix currentParameter = parameters.Last.Value;
                DoubleMatrix previousParameter = parameters.Last.Previous.Value;

                double alpha = this.ProbabilitySmoothingCoefficient;
                parameters.Last.Value =
                    alpha * currentParameter + (1.0 - alpha) * previousParameter;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// At intermediate iterations, the algorithm will stop under the
        /// following conditions.
        /// </para>
        /// <inheritdoc cref="CombinationOptimizationContext" 
        /// path="para[@id='Stop.1']"/>
        /// <inheritdoc cref="CombinationOptimizationContext" 
        /// path="para[@id='Stop.2']"/>
        /// <para id='Stop.3'>
        /// In a <see cref="CombinationOptimizationContext"/>, the method 
        /// analyzes the currently updated reference parameter, 
        /// say <latex>w_t=\mx{p_{t,0} \cdots p_{t,n-1}}</latex> as follows. 
        /// Let <latex>m</latex> be 
        /// <see cref="SystemPerformanceOptimizationContext.MinimumNumberOfIterations"/>, 
        /// and defines <latex>B_t</latex> as the set of indexes
        /// in <latex>w_t</latex> corresponding to its <latex>k</latex> largest probabilities,
        /// where <latex>k</latex> is <see cref="CombinationDimension"/>. 
        /// If condition
        /// <latex mode='display'>
        /// B_t = B_{t-l}, \quad l=1,\dots,m, 
        /// </latex>
        /// can be verified,
        /// the method returns <c>true</c>; otherwise <c>false</c> is returned.
        /// The algorithm thus converges if the indexes of 
        /// the largest <latex>k</latex> probabilities coincide
        /// <latex>m</latex> times in a row of iterations.
        /// </para>
        /// </remarks>
        protected internal override bool StopAtIntermediateIteration(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            bool stopExecution = true;

            var lastPositions =
                this.largestProbabilityPositions.Last.Value;

            SortedSet<int> currentPreviousPositions;

            var currentPreviousNode =
                this.largestProbabilityPositions.Last.Previous;

            for (int i = 0; i < this.MinimumNumberOfIterations; i++)
            {
                currentPreviousPositions =
                    currentPreviousNode.Value;

                if (!lastPositions.SetEquals(currentPreviousPositions))
                {
                    stopExecution = false;
                    break;
                }
                currentPreviousNode = currentPreviousNode.Previous;
            }

            return stopExecution;
        }

        #endregion
    }
}
