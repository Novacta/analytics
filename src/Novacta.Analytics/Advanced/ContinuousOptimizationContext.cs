// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a Cross-Entropy context in which  
    /// a continuous function is minimized or maximized.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Class <see cref="ContinuousOptimizationContext"/> derives from
    /// <see cref="SystemPerformanceOptimizationContext"/>, and defines
    /// a Cross-Entropy context able to solve continuous optimization
    /// problems.
    /// </para>
    /// <para>
    /// Such a context is exploited by the methods exposed by the static
    /// class <see cref="ContinuousOptimization"/>, where the parameters
    /// of the algorithm are set to default values. For advanced 
    /// scenarios, a context with specialized values can be
    /// instantiated and hence passed as a parameter to method 
    /// <see cref="SystemPerformanceOptimizer.Optimize(
    /// SystemPerformanceOptimizationContext, double, int)"/>. 
    /// </para>
    /// <para id='Optimize.3'>
    /// Class <see cref="SystemPerformanceOptimizationContext"/> thoroughly 
    /// defines a system whose performance must be optimized. 
    /// Class <see cref="ContinuousOptimizationContext"/> specializes 
    /// that system by assuming the its performance is a continuous
    /// function, say <latex>H</latex>. 
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
    /// where <latex mode='inline'>x \in \mathcal{X}</latex> is a 
    /// vector representing 
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
    /// <b>Implementing a context for continuous optimization</b>
    /// </para>
    /// <para>
    /// The Cross-Entropy method 
    /// provides an iterative multi step procedure. In the context
    /// of continuous optimization, at each 
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
    /// <para>These steps must be implemented as follows.</para>
    /// <para><b><i>Sampling step</i></b></para>
    /// <para>
    /// In a <see cref="ContinuousOptimizationContext"/>, the parametric 
    /// family <latex>\mathcal{P}</latex> is defined as follows.
    /// Each component <latex>x_j</latex> of an argument
    /// <latex>\round{x_0,\dots,x_{n-1}}</latex> of <latex>H</latex>
    /// is attached to a 
    /// Gaussian distribution, say <latex>f_{\mu_j,\sigma_j}</latex>,
    /// and 
    /// the <i>j</i>-th entry 
    /// of an argument is sampled from <latex>f_{\mu_j,\sigma_j}</latex>, 
    /// independently from the other,
    /// where <latex>n</latex> is the dimension of 
    /// <latex>\mathcal{X}</latex>. 
    /// A Cross-Entropy sampling parameter 
    /// can thus be  
    /// represented as a <latex>2 \times n</latex> matrix
    /// <latex>v</latex>,
    /// whose first row stores the means of the Gaussian 
    /// distributions, while the second row contains their standard
    /// deviations, so that <latex>v_{0,j} = \mu_j</latex> and
    /// <latex>v_{1,j} = \sigma_j</latex>.
    /// </para>
    /// <para>
    /// The parametric space <latex>\mathcal{V}</latex> should 
    /// include a parameter under which all possible states must have 
    /// a real chance of being selected: this parameter
    /// must be specified as the initial <i>reference</i> parameter
    /// <latex>w_0</latex>.
    /// A <see cref="ContinuousOptimizationContext"/> defines 
    /// <latex>w_0</latex> as follows. The first row of 
    /// <latex>w_0</latex> is set to an initial guess argument
    /// of <latex>H</latex>, say 
    /// <latex>\tilde{x} \equiv \round{\tilde{x}_0,\dots,\tilde{x}_{n-1}}</latex>,
    /// so that 
    /// <latex mode='display'>
    /// w_{0,0,j}=\tilde{x}_j,\quad j=0,\dots,n-1.
    /// </latex>
    /// The entries in the second row of <latex>w_0</latex> are instead
    /// all set constantly equal to an initial, constant standard 
    /// deviation, say <latex>\sigma_0</latex>:
    /// <latex mode='display'>
    /// w_{0,1,j}=\sigma_0,\quad j=0,\dots,n-1.
    /// </latex>
    /// </para>    
    /// <para><b><i>Updating step</i></b></para>
    /// <para  id='Updating.1'>
    /// At iteration <latex>t</latex>, let us represent the sample drawn 
    /// as <latex>X_{t,0},\dots,X_{N}</latex>, where <latex>N</latex> is the 
    /// Cross-Entropy sample size, and the <latex>i</latex>-th sample point
    /// is the sequence <latex>X_{t,i}=\round{X_{t,i,0},\dots,X_{t,i,n-1}}</latex>.
    /// The parameter's 
    /// updating formula is, 
    /// <latex mode="display">
    /// w_{t,0,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)\,X_{t,i,j}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)},
    /// </latex>
    /// where <latex>A_t</latex>
    /// is the elite sample in this context, i.e. the set of sample
    /// points having the lowest performances observed during the <latex>t</latex>-th
    /// iteration, if minimizing, the highest ones, otherwise, while
    /// <latex>I_{A_t}</latex> is its indicator function.
    /// The parameter's second row, that containing the standard deviations, 
    /// is instead updated as follows:
    /// <latex mode="display">
    /// w_{t,1,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)\round{X_{t,i,j} - w_{t,0,j}}^2}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)}.
    /// </latex>
    /// </para>
    /// <para><i>Applying a smoothing scheme to updated parameters</i></para>
    /// <para id='Smoothing.1'>
    /// In a <see cref="ContinuousOptimizationContext"/>, 
    /// the sampling parameter 
    /// is smoothed applying the following formulas
    /// (See Rubinstein and Kroese, 
    /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>):
    /// <latex mode="display">
    /// w_{t,i,j} \leftarrow \alpha_i\,w_{t,i,j} + \round{1 - \alpha_i} w_{t-1,i,j},
    /// </latex>
    /// where <latex>\alpha_0 = \alpha</latex>, 
    ///  and
    /// <latex mode="display">
    /// \alpha_1 = \beta \round{1 - \round{1 - \frac{1}{t}}^q},
    /// </latex>
    /// with <latex>0 &lt; \alpha,\beta &lt; 1</latex> 
    /// and <latex>q</latex> a positive integer.
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
    /// Given the optimal parameter (the parameter corresponding to the last iteration 
    /// <latex>T</latex> executed by the algorithm before stopping),
    /// <latex mode='display'>
    /// w_T = \mx{
    ///  \mu_{T,0} \cdots \mu_{T,n-1} \\
    ///  \sigma_{T,0} \cdots \sigma_{T,n-1}
    /// },
    /// </latex>
    /// the argument at which the searched extremum is considered 
    /// as reached according to the Cross-Entropy method will be
    /// returned as 
    /// <latex mode='display'>
    /// \OA\round{w_T} = \mx{\mu_{T,0} \cdots \mu_{T,n-1}}.
    /// </latex>
    /// </para>
    /// <para><b><i>Stopping criterion</i></b></para>
    /// <para id='Stop.1'>
    /// A <see cref="ContinuousOptimizationContext"/> never stops before
    /// executing a number of iterations less than 
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MinimumNumberOfIterations"/>, and always stops
    /// if such number is greater than or equal to
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MaximumNumberOfIterations"/>. 
    /// </para>
    /// <para id='Stop.2'>
    /// For intermediate iterations, method 
    /// <see cref="StopAtIntermediateIteration(
    /// int, LinkedList{double}, LinkedList{DoubleMatrix})"/> is
    /// called to check if a Cross-Entropy program executing in this
    /// context should stop or not.
    /// </para>
    /// <para id='Stop.3'>
    /// In a <see cref="ContinuousOptimizationContext"/>, the method 
    /// controls if, in the currently updated reference parameter, 
    /// say <latex>w_t</latex>, all
    /// the standard deviations are less than a specified
    /// termination tolerance, say <latex>\epsilon</latex>, testing that
    /// <latex mode='display'>
    /// w_{t,1,j} &lt; \epsilon, \quad j=0,\dots,n-1, 
    /// </latex>
    /// and, if so,
    /// return <b>true</b>; otherwise returns <b>false</b>.
    /// </para>
    /// <para><b>Instantiating a context for continuous optimization</b></para>
    /// <para>
    /// At instantiation, the constructor of 
    /// a <see cref="ContinuousOptimizationContext"/> object
    /// will receive information about the optimization under study by
    /// means of parameters representing the objective function
    /// <latex>H</latex>, 
    /// <latex>\tilde{x}</latex>, 
    /// <latex>\sigma_0</latex>, 
    /// <latex>m</latex>, <latex>M</latex>, and a constant stating 
    /// if the optimization goal is a maximization or a minimization.
    /// In addition, the smoothing parameters <latex>\alpha</latex>,
    /// <latex>\beta</latex>, and <latex>q</latex> are also 
    /// passed to the constructor.
    /// </para>
    /// <para>
    /// After construction, property 
    /// <see cref="InitialArgument"/> represents 
    /// <latex mode='inline'>\tilde{x}</latex>, while <latex>m</latex>
    /// and <latex>M</latex>
    /// can be inspected, respectively, via properties
    /// <see cref="SystemPerformanceOptimizationContext.MinimumNumberOfIterations"/> and
    /// <see cref="SystemPerformanceOptimizationContext.MaximumNumberOfIterations"/>. 
    /// The smoothing coefficients <latex>\alpha</latex>,
    /// <latex>\beta</latex>, and the exponent <latex>q</latex> are also
    /// available via properties
    /// <see cref="MeanSmoothingCoefficient"/>,
    /// <see cref="StandardDeviationSmoothingCoefficient"/> and
    /// <see cref="StandardDeviationSmoothingExponent"/>,
    /// respectively.
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
    /// vector as a valid representation of an argument.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, function
    /// <latex mode='display'>
    /// f\round{x}=\exp\round{-\round{x-2}^2} 
    /// + \round{.8} \exp\round{-\round{x+2}^2} 
    /// </latex>
    /// is maximized.
    /// </para>
    /// <para>
    /// The search for the maximizer starts from
    /// the initial argument <latex>-6</latex>.
    /// </para>
    /// <para>
    /// <code title="Maximizing a function on the real line."
    /// source="..\Novacta.Analytics.CodeExamples\Advanced\ContinuousOptimizationContextExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// </example>
    ///<see cref="SystemPerformanceOptimizationContext"/>
    public sealed class ContinuousOptimizationContext
    : SystemPerformanceOptimizationContext
    {
        #region State

        private readonly Func<DoubleMatrix, double> objectiveFunction;

        /// <summary>
        /// Gets the minimal value which, if greater than all the 
        /// standard deviations of a Cross-Entropy parameter, 
        /// signals that the optimization must be considered as 
        /// converged at intermediate iterations.
        /// </summary>
        /// <value>
        /// The tolerance on the standard deviations of a 
        /// Cross-Entropy parameter to apply while testing if the
        /// optimization has converged at intermediate iterations.
        /// </value>
        public double TerminationTolerance { get; }

        /// <summary>
        /// Gets the coefficient that defines the smoothing scheme 
        /// for the means of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </summary>
        /// <value>
        /// The coefficient to apply when smoothing the
        /// means of the Cross-Entropy parameters.
        /// </value>
        public double MeanSmoothingCoefficient { get; }

        /// <summary>
        /// Gets the coefficient that defines the base smoothing 
        /// scheme for the standard deviations of the Cross-Entropy 
        /// parameters exploited by this context.
        /// </summary>
        /// <value>
        /// The coefficient that defines the base smoothing scheme 
        /// for the standard deviations of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </value>
        public double StandardDeviationSmoothingCoefficient { get; }

        /// <summary>
        /// Gets the exponent that defines the dynamic smoothing scheme 
        /// for the standard deviations of the Cross-Entropy 
        /// parameters exploited by this context.
        /// </summary>
        /// <value>
        /// The exponent that defines the dynamic smoothing scheme 
        /// for the standard deviations of the Cross-Entropy 
        /// parameters exploited by this context.
        /// </value>
        public int StandardDeviationSmoothingExponent { get; }

        /// <summary>
        /// Gets the value assigned to each standard deviation in
        /// the initial Cross-Entropy parameter
        /// exploited by this context.
        /// </summary>
        /// <value>
        /// The value assigned to each standard deviation in
        /// the initial Cross-Entropy parameter
        /// exploited by this context.
        /// </value>
        public double InitialStandardDeviation { get; }

        /// <summary>
        /// The initial argument of the function which 
        /// the search for the optimal one starts with.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The initial argument represents the means of the 
        /// Cross-Entropy parameter
        /// initially exploited to sample arguments 
        /// of the function while searching
        /// for the optimal one.
        /// </para>
        /// </remarks>
        /// <value>
        /// The initial argument of the function which 
        /// the search for the optimal one starts with.
        /// </value>
        public DoubleMatrix InitialArgument { get; }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ContinuousOptimizationContext" /> class 
        /// aimed to optimize the specified 
        /// continuous function,
        /// with the given initial 
        /// guess argument, optimization goal, 
        /// range of iterations, and smoothing coefficients.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The function to be optimized.
        /// </param>
        /// <param name="optimizationGoal">
        /// A constant to specify if the function
        /// must be minimized or maximized.
        /// </param>
        /// <param name="initialArgument">
        /// The means of the Cross-Entropy parameter
        /// initially exploited to sample arguments 
        /// of the function while searching
        /// for the optimal one.
        /// </param>
        /// <param name="meanSmoothingCoefficient">
        /// A coefficient to define the smoothing scheme for the
        /// means of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </param>
        /// <param name="standardDeviationSmoothingCoefficient">
        /// The coefficient that defines the base smoothing scheme for the
        /// standard deviations of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </param>
        /// <param name="standardDeviationSmoothingExponent">
        /// The exponent that defines the dynamic smoothing scheme 
        /// for the standard deviations of the Cross-Entropy 
        /// parameters exploited by this context.
        /// </param>
        /// <param name="initialStandardDeviation">
        /// The value assigned to each standard deviation in
        /// the initial Cross-Entropy parameter
        /// exploited by this context.
        /// </param>
        /// <param name="terminationTolerance">
        /// The minimal value which, if greater than all the standard 
        /// deviations of a Cross-Entropy parameter, signals that
        /// the optimization must be considered as converged
        /// at intermediate iterations.
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
        /// As a consequence, <paramref name="initialArgument"/> is 
        /// expected to be a row vector.
        /// </para>
        /// <para>
        /// As discussed by Rubinstein and Kroese, 
        /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>,
        /// typical values for <paramref name="meanSmoothingCoefficient"/>
        /// are between .7 and 1 (excluded), while
        /// <paramref name="standardDeviationSmoothingCoefficient"/> should
        /// be between .8 and 1 (excluded), with
        /// <paramref name="standardDeviationSmoothingExponent"/>
        /// between 5 and 10.
        /// </para>
        /// <para>
        /// Also, it is expected that 
        /// <paramref name="initialStandardDeviation"/> is a big enough
        /// number, such as 100.0.
        /// In this way, during the first execution of the sampling step,
        /// each argument will have a good likelihood of being drawn.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="optimizationGoal"/> is not a field of
        /// <see cref="OptimizationGoal"/>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is not a row
        /// vector.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is greater than 
        /// <paramref name="maximumNumberOfIterations"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="meanSmoothingCoefficient"/> is not
        /// in the open interval between 0 and 1.<br/>
        /// -or-<br/>
        /// <paramref name="standardDeviationSmoothingCoefficient"/> is
        /// not in the open interval between 0 and 1.<br/>
        /// -or-<br/>
        /// <paramref name="standardDeviationSmoothingExponent"/> is
        /// not positive.<br/>
        /// -or-<br/>
        /// <paramref name="initialStandardDeviation"/> is not
        /// positive.<br/>
        /// -or-<br/>
        /// <paramref name="terminationTolerance"/> is not 
        /// positive.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is
        /// not positive.<br/>
        /// -or-<br/>
        /// <paramref name="maximumNumberOfIterations"/> is
        /// not positive.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", 
            "CA1062:Validate arguments of public methods", 
            Justification = 
                "Input validation partially delegated to GetStateDimension.")]
        public ContinuousOptimizationContext(
            Func<DoubleMatrix, double> objectiveFunction,
            DoubleMatrix initialArgument,
            double meanSmoothingCoefficient,
            double standardDeviationSmoothingCoefficient,
            int standardDeviationSmoothingExponent,
            double initialStandardDeviation,
            double terminationTolerance,
            OptimizationGoal optimizationGoal,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations)
            : base(
                stateDimension: GetStateDimension(initialArgument),
                optimizationGoal: optimizationGoal,
                initialParameter: GetInitialParameter(
                    initialArgument,
                    initialStandardDeviation),
                minimumNumberOfIterations: minimumNumberOfIterations,
                maximumNumberOfIterations: maximumNumberOfIterations)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (terminationTolerance <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(terminationTolerance),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (
                (meanSmoothingCoefficient <= 0.0)
                ||
                (1.0 <= meanSmoothingCoefficient)
               )
            {
                throw new ArgumentOutOfRangeException(
                    nameof(meanSmoothingCoefficient),
                    string.Format(
                        CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", 
                            "1.0"));
            }

            if (
                (standardDeviationSmoothingCoefficient <= 0.0)
                ||
                (1.0 <= standardDeviationSmoothingCoefficient)
               )
            {
                throw new ArgumentOutOfRangeException(
                    nameof(standardDeviationSmoothingCoefficient),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                        "0.0", 
                        "1.0"));
            }

            if (standardDeviationSmoothingExponent < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(standardDeviationSmoothingExponent),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            this.InitialArgument = initialArgument;
            this.MeanSmoothingCoefficient = meanSmoothingCoefficient;
            this.StandardDeviationSmoothingCoefficient =
                standardDeviationSmoothingCoefficient;
            this.StandardDeviationSmoothingExponent =
                standardDeviationSmoothingExponent;
            this.InitialStandardDeviation = initialStandardDeviation;

            this.objectiveFunction = objectiveFunction;
            this.TerminationTolerance = terminationTolerance;
        }

        private static int GetStateDimension(DoubleMatrix initialArgument)
        {
            if (initialArgument is null)
            {
                throw new ArgumentNullException(nameof(initialArgument));
            }

            if (!initialArgument.IsRowVector)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    nameof(initialArgument));
            }

            return initialArgument.Count;
        }

        private static DoubleMatrix GetInitialParameter(
            DoubleMatrix initialArgument,
            double initialStandardDeviation)
        {
            if (initialStandardDeviation <= 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(initialStandardDeviation),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var initialParameter = DoubleMatrix.Dense(
                2,
                initialArgument.Count);

            initialParameter[0, ":"] = initialArgument;
            initialParameter[1, ":"] += initialStandardDeviation;

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
        /// </remarks>
        protected internal override double Performance(DoubleMatrix state)
        {
            return this.objectiveFunction(state);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method has been overridden to support
        /// the optimization of functions whose arguments
        /// are continuous variables.
        /// </para>
        /// </remarks>
        protected internal override void PartialSample(
            double[] destinationArray,
            Tuple<int, int> sampleSubsetRange,
            RandomNumberGenerator randomNumberGenerator,
            DoubleMatrix parameter,
            int sampleSize)
        {
            // Must be Item1 included, Item2 excluded
            int subSampleSize =
                sampleSubsetRange.Item2 - sampleSubsetRange.Item1;

            for (int j = 0; j < this.StateDimension; j++)
            {
                var distribution = new
                    GaussianDistribution(
                        mu: parameter[0, j],
                        sigma: parameter[1, j])
                {
                    RandomNumberGenerator = randomNumberGenerator
                };

                distribution.Sample(
                     subSampleSize,
                     destinationArray, 
                     j * sampleSize + sampleSubsetRange.Item1);
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method has been implemented to support
        /// the optimization of functions whose arguments
        /// are continuous variables.
        /// </para>
        /// </remarks>
        protected internal override DoubleMatrix UpdateParameter(
            LinkedList<DoubleMatrix> parameters,
            DoubleMatrix eliteSample)
        {
            var newParameter = DoubleMatrix.Dense(2, this.StateDimension);
            newParameter[0, ":"] =
                Stat.Mean(
                    data: eliteSample,
                    dataOperation: DataOperation.OnColumns);
            newParameter[1, ":"] =
                Stat.StandardDeviation(
                    data: eliteSample,
                    dataOperation: DataOperation.OnColumns,
                    adjustForBias: false);

            return newParameter;
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
        protected internal override DoubleMatrix GetOptimalState(
            DoubleMatrix parameter)
        {
            // The optimal state is the vector of
            // means.
            return parameter[0, ":"];
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
        /// <inheritdoc cref="ContinuousOptimizationContext" 
        /// path="para[@id='Smoothing.1']"/>
        /// </remarks>
        protected internal override void SmoothParameter(
            LinkedList<DoubleMatrix> parameters)
        {
            double iteration = Convert.ToDouble(parameters.Count);
            if (iteration > 1)
            {
                DoubleMatrix currentParameter = parameters.Last.Value;
                DoubleMatrix previousParameter = parameters.Last.Previous.Value;

                // Smoothing means
                double meanAlpha = .7;
                var previousMeans = previousParameter[0, ":"];
                var currentMeans = currentParameter[0, ":"];
                currentParameter[0, ":"] =
                    meanAlpha * currentMeans + (1.0 - meanAlpha) * previousMeans;

                // Smoothing standard deviations
                var previousStdDevs = previousParameter[1, ":"];
                var currentStdDevs = currentParameter[1, ":"];
                double q = 6.0;
                double beta = .9;
                double stdDevAlpha = beta * (1.0 - Math.Pow(1.0 - 1.0 / iteration, q));

                currentParameter[1, ":"] =
                    stdDevAlpha * currentStdDevs + (1.0 - stdDevAlpha) * previousStdDevs;
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// At intermediate iterations, the algorithm will stop 
        /// if each standard deviation in the updated 
        /// sampling reference
        /// parameter will be less than <see cref="TerminationTolerance"/>.
        /// </para>
        /// </remarks>
        protected internal override bool StopAtIntermediateIteration(
            int iteration,
            LinkedList<double> levels,
            LinkedList<DoubleMatrix> parameters)
        {
            var stdDevs = parameters.Last.Value[1, ":"];
            for (int j = 0; j < stdDevs.Count; j++)
            {
                if (stdDevs[j] >= this.TerminationTolerance)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}