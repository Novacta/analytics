using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Provides the abstract base class for algorithms implementing  
    /// the Cross-Entropy method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances of the <see cref="CrossEntropyProgram"/> class 
    /// represents algorithms based upon the Cross-Entropy 
    /// method<cite>rubinstein-kroese-2004</cite>, useful to analyze
    /// the performance of complex systems under extreme conditions.
    /// For example, a Cross-Entropy program can be implemented to 
    /// estimate the probability that the system will perform extremely 
    /// well, or to determine what state of the system corresponds to 
    /// a minimal performance.
    /// </para>
    /// <para>
    /// The Cross-Entropy method raises in the context of rare event probability 
    /// estimation, but it is also
    /// closely related to the optimization of continuous or discrete functions. 
    /// In fact, optimizing a function can be seen as the 
    /// problem of estimating the probability of the event represented by the 
    /// function assuming a value over a given extreme level, an event that 
    /// can be typically interpreted as rare.
    /// A <see cref="CrossEntropyProgram"/> can thus be 
    /// designed to solve optimization or estimation problems, provided
    /// that such problems can be defined in terms of performance excesses 
    /// over an exceptionally high or low level.
    /// </para>
    /// <para><b>The Cross-Entropy method</b></para>
    /// <para>
    /// The Cross-Entropy method iteratively executes two main steps.
    /// </para>
    /// <para><em>Sampling step</em></para>
    /// <para>
    /// The first one, the <em>sampling step</em>, is responsible for generating 
    /// diverse candidate states of the system. In this step, 
    /// states are interpreted as points of a sample drawn from a 
    /// parametric statistical distribution, whose definition depends 
    /// upon the problem under study.
    /// </para>
    /// <para><em>Updating step</em></para>
    /// <para>
    /// The <em>updating step</em> aims to modify the distribution from which the 
    /// samples will be obtained in the next iteration, in order to 
    /// improve the probability of sampling relevant states, 
    /// i.e. those states corresponding to the performance excesses of 
    /// interest.
    /// Let us represent the sampled states in the current iteration 
    /// as <latex mode='inline'>X_0,\dots,X_{N-1}</latex>, 
    /// where <latex mode='inline'>N</latex> is the sample size. 
    /// The corresponding performances, 
    /// <latex mode='inline'>H\left(X_0\right),\dots,H\left(X_{N-1}\right)</latex>, 
    /// are computed and sorted in increasing order, say obtaining the 
    /// following ordering:
    /// <latex mode="display">
    /// h_{\left(0\right)}\leq\dots\leq h_{\left(N-1\right)}.
    /// </latex>
    /// In this way, 
    /// the states which guarantee the highest performances can be identified 
    /// as those whose performances occupy the last positions in the ordering. 
    /// </para>
    /// <para>
    /// The <em>updating step</em> relies on the concept of <em>elite</em> 
    /// sample points. Depending on 
    /// the excesses being defined over a high or a low level, they can be
    /// outlined differently as follows. 
    /// If the problem under study is defined in terms of performance excesses 
    /// over an exceptionally high level, 
    /// the Cross-Entropy method refers to the sample points whose 
    /// performances occupy the last positions in the ordering as 
    /// the <em>elite</em> sample points; otherwise, the <em>elite</em> points 
    /// will be those whose performances occupy the first positions.    
    /// </para>
    /// <para>
    /// A way to define them exactly 
    /// is as follows. Let <latex mode='inline'>X_{\left(i\right)}</latex> be the sampled 
    /// state such that 
    /// <latex mode='inline'>h_{\left(i\right)}=H\left(X_{\left(i\right)}\right)</latex>, 
    /// <latex mode='inline'>i=0,\dots,N-1</latex>, and consider 
    /// a constant, referred to as the <em>rarity</em> of the program, 
    /// say <latex mode='inline'>\rho</latex>, usually set to a value 
    /// in the interval <latex mode='inline'>\left[.01,.1\right]</latex>. 
    /// </para>
    /// <para>
    /// If the <em>elite</em> points refer to the highest performances, 
    /// then the points 
    /// <latex mode='inline'>X_{\left(\eta_u\right)},\dots,X_{\left(N-1\right)}</latex> 
    /// occupying a position greater than or equal 
    /// to <latex mode='inline'>\eta_u=\lceil\left(1-\rho\right)N\rceil</latex> are 
    /// defined as elite ones, with 
    /// <latex mode='inline'>\lceil x \rceil</latex> being the ceiling function. 
    /// In this case, the current iteration is said to reach a performance 
    /// <em>level</em> equal to <latex mode='inline'>h_{\left(\eta_u\right)}</latex>.
    /// </para>
    /// <para>
    /// If the <em>elite</em> points refer to the lowest performances, 
    /// then they can be defined as those points occupying a position less than or equal 
    /// to <latex mode='inline'>\eta_l=\lfloor\left(\rho\right)N\rfloor</latex>, 
    /// where <latex mode='inline'>\lfloor x \rfloor</latex> is the floor function. 
    /// Such points are thus
    /// <latex mode='inline'>X_{\left(0\right)},\dots,X_{\left(\eta_l\right)}</latex>,
    /// and the current iteration will reach a performance 
    /// <em>level</em> equal to <latex mode='inline'>h_{\left(\eta_l\right)}</latex>.
    /// </para>
    /// <para>
    /// Once that the elite 
    /// states have been identified, the joint 
    /// distribution from which the states will be sampled in the next iteration 
    /// is updated by estimating its parameters using the elite states only. 
    /// Since they correspond to the performance excesses of interest, this 
    /// updating mechanism guarantees that states having relevant performances 
    /// will be included in subsequent samples with greater probabilities.
    /// </para>
    /// <para><b>Executing a Cross-Entropy program</b></para>
    /// <para id='Run.1'>
    /// Class <see cref="CrossEntropyProgram"/> follows 
    /// the <em>Template Method</em> pattern<cite>gamma-etal-1995</cite> by 
    /// defining in an 
    /// operation the structure of a Cross-Entropy algorithm. 
    /// <see cref="Run(CrossEntropyContext, int, double)">Run</see>  
    /// is the template method, in which the invariant parts of a Cross-Entropy 
    /// program are implemented once. 
    /// The behaviors that can vary are deferred to a 
    /// <see cref="CrossEntropyContext"/> object, which is passed as a parameter
    /// to the template method.
    /// </para>
    /// <para>
    /// Methods <see cref="EvaluatePerformances(CrossEntropyContext, DoubleMatrix)">
    /// EvaluatePerformances</see> and 
    /// <see cref="Sample(CrossEntropyContext, int, DoubleMatrix)">Sample</see>
    /// can execute their tasks both sequentially and concurrently.
    /// You can control their activities by setting properties
    /// <see cref="PerformanceEvaluationParallelOptions"/> and
    /// <see cref="SampleGenerationParallelOptions"/>, respectively.
    /// By default, the number of concurrently running operations
    /// is unlimited.
    /// </para>
    /// <para><b>Specialized Cross-Entropy programs</b></para>
    /// <para>
    /// It is possible to apply the Cross-Entropy method 
    /// by exploiting a <see cref="CrossEntropyProgram"/> instance and
    /// deriving directly from class <see cref="CrossEntropyContext"/> to 
    /// define the context of interest. 
    /// However, it is recommended to execute the specialized programs 
    /// represented by <see cref="SystemPerformanceOptimizer"/> instances 
    /// when addressing combinatorial and multi-extremal optimization problems,
    /// or by <see cref="RareEventProbabilityEstimator"/> ones if 
    /// the estimation of rare event probabilities is required.
    /// These programs have corresponding specialized contexts,
    /// <see cref="SystemPerformanceOptimizationContext"/> and 
    /// <see cref="RareEventProbabilityEstimationContext"/> respectively,
    /// from which one can derive to define the problem under study.
    /// </para>
    /// </remarks>
    /// <seealso cref="RareEventProbabilityEstimator"/>
    /// <seealso cref="SystemPerformanceOptimizer"/>
    public abstract class CrossEntropyProgram
    {
        #region Parallel infrastructure

        // This infrastructure to support executing both sequential and parallel
        // versions of the CE algorithm.

        private readonly Lazy<ConcurrentDictionary<int, RandomNumberGenerator>>
            randomNumberGeneratorPool =
                new();

        private ParallelOptions performanceEvaluationParallelOptions =
            new()
            { MaxDegreeOfParallelism = -1 };

        private ParallelOptions sampleGenerationParallelOptions =
            new()
            { MaxDegreeOfParallelism = -1 };

        /// <summary>
        /// Gets or sets options that configure the operation of the 
        /// <see cref="Parallel"/> class while computing
        /// performance evaluations.
        /// </summary>
        /// <value>The performance evaluation parallel options.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public ParallelOptions PerformanceEvaluationParallelOptions
        {
            get
            {
                return this.performanceEvaluationParallelOptions;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                this.performanceEvaluationParallelOptions = value;
            }
        }

        /// <summary>
        /// Gets or sets options that configure the operation of the 
        /// <see cref="Parallel"/> class while generating
        /// sample points.
        /// </summary>
        /// <value>The sample generation parallel options.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public ParallelOptions SampleGenerationParallelOptions
        {
            get
            {
                return this.sampleGenerationParallelOptions;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                this.sampleGenerationParallelOptions = value;
            }
        }

        private static RandomNumberGenerator OnThreadLocalSampling(
            CrossEntropyContext context,
            double[] destinationArray,
            Tuple<int, int> destinationRange,
            RandomNumberGenerator randomNumberGenerator,
            DoubleMatrix parameter,
            int sampleSize)
        {
            context.PartialSample(                
                destinationArray,
                destinationRange,
                randomNumberGenerator,
                parameter,
                sampleSize);

            return randomNumberGenerator;
        }

        #endregion

        #region Template method

        /// <summary>
        /// Runs this Cross-Entropy program in the specified context.
        /// </summary>
        /// <param name="context">
        /// The context in which the program must be executed.
        /// </param>
        /// <param name="rarity">
        /// The rarity applied by the Cross-Entropy method.
        /// </param>
        /// <param name="sampleSize">
        /// The size of the samples drawn during the
        /// sampling step of the Cross-Entropy method.
        /// </param>
        /// <returns>
        /// The results of the Cross-Entropy program.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="CrossEntropyProgram" 
        /// path="para[@id='Run']"/>
        /// <para>
        /// For a thorough description of the method, see the remarks 
        /// about the <see cref="CrossEntropyProgram"/> class.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="rarity"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="rarity"/> is not less than 1.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="EliteSampleDefinition"/> of <paramref name="context"/>
        /// is <see cref="EliteSampleDefinition.HigherThanLevel"/> and applying
        /// <see cref="Math.Ceiling(double)"/> to expression
        /// <c><paramref name="sampleSize"/>*(1.0 -<paramref name="rarity"/>)</c>
        /// is not lower than <paramref name="sampleSize"/>.<br/>
        /// -or-<br/>
        /// The <see cref="EliteSampleDefinition"/> of <paramref name="context"/>
        /// is <see cref="EliteSampleDefinition.LowerThanLevel"/> and applying
        /// <see cref="Math.Ceiling(double)"/> to expression
        /// <c><paramref name="sampleSize"/>*(<paramref name="rarity"/>)</c>
        /// is not lower than <paramref name="sampleSize"/>.
        /// </exception>
        protected CrossEntropyResults Run(
            CrossEntropyContext context,
            int sampleSize,
            double rarity)
        {
            #region Input validation

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (sampleSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(sampleSize),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if ((rarity <= 0.0) || (1.0 <= rarity))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(rarity),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"), 
                        0.0, 
                        1.0));
            }

            var eliteSampleDefinition = context.EliteSampleDefinition;

            switch (eliteSampleDefinition)
            {
                case EliteSampleDefinition.HigherThanLevel:
                    {
                        if (Convert.ToInt32(
                                Math.Ceiling(sampleSize * (1 - rarity)))>=sampleSize)
                        {
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    ImplementationServices.GetResourceString(
                                        "STR_EXCEPT_CEM_TOO_LOW_RARITY"),
                                    nameof(sampleSize)),
                                nameof(rarity));
                        }
                        break;
                    }   
                case EliteSampleDefinition.LowerThanLevel:
                    {
                        if (Convert.ToInt32(
                                Math.Ceiling(sampleSize * rarity))>=sampleSize)
                        {
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    ImplementationServices.GetResourceString(
                                        "STR_EXCEPT_CEM_TOO_HIGH_RARITY"),
                                    nameof(sampleSize)),
                                nameof(rarity));
                        }
                        break; 
                    }
            }

            #endregion


            var parameters = new LinkedList<DoubleMatrix>();
            parameters.AddFirst(context.InitialParameter);

            DoubleMatrix sample, performances, parameter;

            var levels = new LinkedList<double>();

            bool continueExecution = true;
            int iteration = 1;

            while (continueExecution)
            {
                parameter = parameters.Last.Value;

                if (context.TraceExecution)
                {
                    Trace.WriteLine(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Iteration: {0}", 
                            iteration));
                }

                // Draw a sample
                sample = this.Sample(
                    context,
                    sampleSize,
                    parameter);

                // Compute the sample performances
                performances = this.EvaluatePerformances(
                    context,
                    sample);

                // Update the level and return the elite sample
                levels.AddLast(
                    context.UpdateLevel(
                        performances,
                        sample,
                        eliteSampleDefinition,
                        rarity,
                        out DoubleMatrix eliteSample));

                // Update the parameter
                parameters.AddLast(
                    context.UpdateParameter(
                        parameters,
                        eliteSample));

                // Additional operations 
                context.OnExecutedIteration(
                    iteration,
                    sample,
                    levels,
                    parameters);

                // Check the stop criterion
                continueExecution = !context.StopExecution(
                    iteration,
                    levels,
                    parameters);

                if (context.TraceExecution)
                {
                    Trace.WriteLine(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Level: {0}", 
                            levels.Last.Value));
                    Trace.WriteLine("Parameter:");
                    Trace.WriteLine(parameter);
                }

                ++iteration;

            }  // end of while

            var results = new CrossEntropyResults
            {
                Levels = levels,
                Parameters = parameters
            };

            return results;
        }

        #endregion

        #region Primitive operations having dependencies on specific problems

        /// <summary>
        /// Evaluates the performance of the points in the sample drawn 
        /// in the current iteration.
        /// </summary>
        /// <param name="sample">
        /// The sample drawn in the current iteration.
        /// </param>
        /// <param name="context">
        /// The context in which the method must be executed.
        /// </param>
        /// <returns>The matrix containing the performances of 
        /// the points in the current sample.</returns>
        /// <remarks>
        /// <para id='PerformanceEvalution.1'>
        /// Method <see cref="EvaluatePerformances(
        /// CrossEntropyContext, DoubleMatrix)"/> 
        /// takes the matrix returned 
        /// by <see cref="Sample(
        /// CrossEntropyContext, int, DoubleMatrix)">Sample</see> as
        /// its second parameter, the <em>sample matrix</em>, 
        /// while its first parameter is 
        /// the context which defines the performance
        /// of a specified state, referred to as 
        /// the <em>performance function</em>. 
        /// </para>
        /// <para id='PerformanceEvalution.2'>
        /// Since the sampled points are represented as the rows of 
        /// the <em>sample matrix</em>, 
        /// it is expected that the <em>performance function</em> 
        /// will accept such rows as valid representations of 
        /// a system's state.
        /// </para>
        /// <para>
        /// The performances of the points are returned as 
        /// a column vector.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="sample"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="sample"/> has a <see cref="DoubleMatrix.NumberOfColumns"/>
        /// not matching the <see cref="CrossEntropyContext.StateDimension"/> of
        /// <paramref name="context"/>.
        /// </exception>
        protected DoubleMatrix EvaluatePerformances(
            CrossEntropyContext context,
            DoubleMatrix sample)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (sample is null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (sample.NumberOfColumns != context.StateDimension)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEP_SAMPLE_IS_CONTEXT_INCOMPATIBLE"),
                    nameof(sample));
            }

            Func<DoubleMatrix, double> performanceFunction = context.Performance;

            int sampleSize = sample.NumberOfRows;
            DoubleMatrix performances = DoubleMatrix.Dense(sampleSize, 1);
            double[] performancesArray = performances.GetStorage();

            if (1 == this.performanceEvaluationParallelOptions.MaxDegreeOfParallelism)
            {
                // SEQUENTIAL ----------------------------------------------------------

                for (int i = 0; i < sampleSize; i++)
                {
                    performancesArray[i] = performanceFunction(sample[i, ":"]);
                }
            }
            else
            {
                // PARALLEL  -----------------------------------------------------------

                var partitioner = Partitioner.Create(0, sampleSize);

                Parallel.ForEach(
                    partitioner,
                    this.performanceEvaluationParallelOptions,
                    (range) =>
                    {
                        for (int i = range.Item1; i < range.Item2; i++)
                            performancesArray[i] = performanceFunction(sample[i, ":"]);
                    }
                );
            }

            return performances;
        }

        /// <summary>
        /// Draws a sample having the specified size from a distribution 
        /// defined in the given context and characterized by the 
        /// designated parameter.
        /// </summary>
        /// <param name="context">
        /// The context in which the method must be executed.
        /// </param>
        /// <param name="parameter">
        /// The sampling parameter.
        /// </param>
        /// <param name="sampleSize">
        /// The sample size.
        /// </param>
        /// <returns>
        /// The matrix containing the sample data.
        /// </returns>
        /// <remarks>
        /// <para id='Sample.1'>
        /// This method executes the <i>sampling step</i> of a
        /// Cross-Entropy program.
        /// </para>
        /// <para id='Sample.2'>
        /// Method <see cref="Sample(
        /// CrossEntropyContext, int, DoubleMatrix)">Sample</see> 
        /// returns a <see cref="DoubleMatrix"/> whose
        /// rows represent the generated sample points, while its 
        /// <see cref="DoubleMatrix.NumberOfColumns"/> is the 
        /// <see cref="CrossEntropyContext.StateDimension"/> of the 
        /// points in the specified <paramref name="context"/>. 
        /// This is equivalent to require that a state of the system 
        /// under study must be representable by a row vector.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="parameter"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The dimensions of <paramref name="parameter"/> do not match
        /// those of the <see cref="CrossEntropyContext.InitialParameter"/> of 
        /// <paramref name="context"/>.
        /// </exception>
        protected DoubleMatrix Sample(
            CrossEntropyContext context,
            int sampleSize,
            DoubleMatrix parameter)
        {
            #region Input validation

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (sampleSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(sampleSize),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (parameter.NumberOfRows != context.InitialParameter.NumberOfRows
                ||
                parameter.NumberOfColumns != context.InitialParameter.NumberOfColumns)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEP_PARAMETER_IS_CONTEXT_INCOMPATIBLE"),
                    nameof(parameter));
            }

            #endregion

            int sampleSizeDimension = context.StateDimension;
            DoubleMatrix sample = DoubleMatrix.Dense(sampleSize, sampleSizeDimension);
            var destinationArray = sample.GetStorage();

            if (1 != this.sampleGenerationParallelOptions.MaxDegreeOfParallelism)
            {
                /* Assign a distinct random number generator
                   to whatever thread was scheduled by the ThreadPool.
                   In this way, generators are recycled together with their 
                   corresponding threads. */

                var partitioner = Partitioner.Create(0, sampleSize);

                Parallel.ForEach(
                    source: partitioner,
                    parallelOptions: this.sampleGenerationParallelOptions,
                    localInit: () =>
                    {
                        return this.randomNumberGeneratorPool.Value.GetOrAdd(
                            Environment.CurrentManagedThreadId,
                            (threadId) =>
                            {
                                var localRandomNumberGenerator =
                                    RandomNumberGenerator.CreateNextMT2203(7777777);
                                return localRandomNumberGenerator;
                            });
                    },
                    body: (range, state, localRandomNumberGenerator) =>
                    {
                        return CrossEntropyProgram.OnThreadLocalSampling(
                            context: context,
                            destinationArray: destinationArray,
                            destinationRange: range,
                            randomNumberGenerator: localRandomNumberGenerator,
                            parameter: parameter,
                            sampleSize: sampleSize);
                    },
                    localFinally: (localRandomNumberGenerator) => { });
            }
            else
            {
                CrossEntropyProgram.OnThreadLocalSampling(
                    context: context,
                    destinationArray: destinationArray,
                    destinationRange: new Tuple<int, int>(0, sampleSize),
                    randomNumberGenerator: RandomNumberGenerator.CreateSFMT19937(7777777),
                    parameter: parameter,
                    sampleSize: sampleSize);
            }

            return sample;
        }

        #endregion
    }
}