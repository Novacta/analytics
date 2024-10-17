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
    /// arguments are collections of <see cref="CategoricalEntailment"/>
    /// instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Class <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> 
    /// derives from <see cref="SystemPerformanceOptimizationContext"/>, 
    /// and defines a Cross-Entropy context able to solve optimization
    /// problems regarding the selection of an ensemble of categorical 
    /// entailments, i.e. objects defining collections of premises, 
    /// about a given set of feature 
    /// variables, that imply a specific response category, with the
    /// conclusion entailed by the premises with an eventually 
    /// partial truth value, ranging between completely false to 
    /// completely true.
    /// </para>
    /// <para>
    /// Categorical entailments can be exploited when
    /// items from a given 
    /// feature space <latex>\FS</latex> must be classified 
    /// into a set <latex>\RD</latex> of labels. 
    /// If <latex>L</latex> feature categorical variables
    /// are taken into account, and if 
    /// variable <latex>l\in\{0,\dots,L-1\}</latex> has 
    /// finite domain <latex>\FD_l</latex>, then <latex>\FS</latex> can
    /// be represented as the 
    /// Cartesian product <latex>\FD_0\times\dots\times\FD_{L-1}</latex>.
    /// A categorical entailment is a 
    /// triple <latex>\round{\EP,\EC,\ET}</latex>, 
    /// where <latex>\EP</latex> is a proper subset of <latex>\FS</latex>
    /// representing the entailment premises 
    /// (<latex>\,\EP=\EP_0\times\dots\times\EP_{L-1}</latex>, 
    /// with <latex>\emptyset\neq\EP_l\subset\FD_l\,</latex>), 
    /// <latex>\EC\in\RD</latex> is the concluded response category,
    /// and <latex>\ET</latex> is the entailment truth value. 
    /// </para>
    /// <para id='Optimize.3'>
    /// Class <see cref="SystemPerformanceOptimizationContext"/> thoroughly 
    /// defines a system whose performance must be optimized. 
    /// Class <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> specializes 
    /// that system by assuming that its performance,
    /// say <latex>H</latex>, 
    /// is defined on a collection of possible entailments
    /// about specific features and response variables in a given
    /// categorical data set.
    /// </para>
    /// <para>
    /// Let <latex>J</latex> be the number of entailments to be selected.
    /// For <latex>l=0,\dots,L-1</latex>, 
    /// let <latex>n_l</latex> be the
    /// number of categories in the domain of the 
    /// <latex>l</latex>-th feature, say
    /// <latex>\FD_l=\set{\FC_{l,0},\dots,\FC_{l,n_l-1}}</latex>, and
    /// let <latex>n_{\RD}</latex> be the number of  
    /// categories in the response domain, 
    /// say <latex>\RD=\set{\RC_0,\dots,\RC_{n_{\RD}-1}}</latex>.
    /// An entailment <latex>\EI_j=\round{\EP_j,\EC_j,\ET_j}</latex> can be 
    /// represented by a partitioned 
    /// row vector, say 
    /// <latex>x_j = \mx{x_{j,0} &amp; \cdots &amp; 
    /// x_{j,L-1} &amp; x_{j,\RD} &amp; \ET_j}</latex>, whose blocks are
    /// defined as follows.
    /// For <latex>l=0,\dots,L-1</latex>, 
    /// <latex>x_{j,l} = \mx{x_{j,l,0} &amp; \cdots &amp; x_{j,l,n_{l}-1}}</latex>,
    /// with <latex>x_{j,l,c}</latex> being unity if the <latex>c</latex>-th 
    /// category of <latex>\FD_l</latex> is included in the corresponding 
    /// premise <latex>\EP_j</latex>, 
    /// zero otherwise, or, using indicator functions,
    /// <latex mode='display'>
    /// x_{j,l,c} = I_{\EP_{j,l}}\round{\FC_{l,c}},\hspace{12pt}  c=0,\dots,n_l-1,
    /// </latex>    
    /// while block <latex>x_{j,\RD}</latex> is a binary
    /// vector <latex>\mx{x_{j,\RD,0} &amp; \cdots &amp; 
    /// x_{j,\RD,n_{\RD}-1}}</latex> in 
    /// which, using indicator functions, 
    /// <latex mode='display'>
    /// x_{j,\RD,c} = I_{\set{\EC_j}}\round{\RC_c},\hspace{12pt} c=0,\dots,n_{\RD}-1,
    /// </latex>
    /// i.e., there is only one entry equal 
    /// to unity corresponding to the feature category concluded by the
    /// entailment.
    /// </para>
    /// <para>
    /// An argument for the Cross-Entropy program hence admits the
    /// partitioned form 
    /// <latex>\mx{x_0 &amp; \cdots &amp; x_{J-1}}</latex>, having
    /// dimensions <latex>1 \times J\round{n+n_{\RD}+1}</latex>,
    /// with <latex>n=n_0 + \dots + n_{L-1}</latex> being 
    /// the overall number of available feature categories.
    /// Given a <see cref="DoubleMatrix"/> instance representing an
    /// argument, the collection 
    /// of <see cref="CategoricalEntailment"/> instances it 
    /// represents can be inspected by calling
    /// method <see cref="GetCategoricalEntailmentEnsembleClassifier(
    /// DoubleMatrix, List{CategoricalVariable}, CategoricalVariable)"/>.
    /// </para>
    /// <para>
    /// The system's state-space 
    /// <latex>\mathcal{X}</latex>, i.e. the domain of 
    /// <latex>H</latex>, can thus be represented as 
    /// the Cartesian product of 
    /// <latex>J</latex> copies of the set 
    /// <latex>\ES=\set{0,1}^{n+n_{\RD}} \times [0,1]</latex>,
    /// with the last closed interval 
    /// representing the range of available truth values.
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
    /// <b>Implementing a context for optimizing on categorical entailments</b>
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
    /// In a <see cref="CategoricalEntailmentEnsembleOptimizationContext"/>, 
    /// the parametric 
    /// family <latex>\mathcal{P}</latex> is outlined as follows.
    /// Each component <latex>x_j</latex> of an argument
    /// <latex>\round{x_0,\dots,x_{J-1}}</latex> of <latex>H</latex>
    /// is attached to a parameter <latex>p_j</latex>, and
    /// the Cross-Entropy sampling parameter <latex>p</latex> is a partitioned 
    /// row vector whose parts are 
    /// those <latex>J</latex> blocks, each one governing the sampling of 
    /// a different entailment <latex>\EI_j=\round{\EP_j,\EC_j,\ET_j}</latex> 
    /// as follows. 
    /// Firstly, a finite discrete 
    /// distribution <latex>p_{j,\RD}=\mx{p_{j,\RD,0} &amp; 
    /// \dots &amp; p_{j,\RD,n_{\RD}-1}}</latex> is 
    /// defined on the 
    /// label domain <latex>\RD</latex> and sampled to obtain 
    /// label <latex>\EC_j</latex>. 
    /// Secondly, the entailment premise <latex>\EP_j</latex> must be sampled too. 
    /// This is equivalent to select at random, for each input 
    /// attribute <latex>l</latex>, 
    /// a subset <latex>\EP_{j,l}</latex> of domain <latex>\FD_j</latex>, and hence 
    /// define <latex>\EP_j=\EP_{j,0}\times\dots\times \EP_{j,L-1}</latex>: 
    /// to obtain such result,
    /// a Bernoulli distribution, say <latex>p_{j,l,c}</latex>, is assigned to 
    /// each category <latex>\FC_{l,c}</latex> 
    /// in the attribute domain <latex>\FD_l</latex>: 
    /// sampling <latex>x_{j,l,c}</latex> from <latex>p_{j,l,c}</latex> 
    /// enable us to set <latex>\FC_{l,c}\in \EP_{j,l}</latex> if and only 
    /// if <latex>x_{j,l,c}=1</latex>.
    /// For <latex>j=0,\dots,J-1</latex>, <latex>p_j</latex> is thus
    /// defined as
    /// <latex>p_j= \mx{p_{j,0} &amp; \cdots &amp; p_{j,L-1} &amp; p_{j,\RD}}</latex>, 
    /// where,  
    /// for <latex>l=0,\dots,L-1</latex>, one has
    /// <latex>p_{j,l} = \mx{p_{j,l,0} &amp; \cdots &amp; p_{j,l,n_{l}-1}}</latex>.
    /// </para>
    /// <para>
    /// Finally, the truth value <latex>\ET_j</latex> is set equal to unity if
    /// partial truth values are not allowed; otherwise, one has
    /// <latex mode='display'>
    /// \ET_j = 1 + \sum_{c=0}^{n_{\RD}-1} \log_{n_{\RD}}\round{p_{j,\RD,c}},
    /// </latex>
    /// so that higher truth values will be assigned 
    /// to entailments whose corresponding response distributions are less 
    /// heterogeneous.
    /// </para>
    /// <para>
    /// As a consequence of the previous discussion, the Cross-Entropy 
    /// sampling parameter <latex>v</latex>
    /// can be represented as vector
    /// <latex>\mx{p_0,\cdots,p_{J-1}}</latex>.
    /// </para>
    /// <para>
    /// The parametric space <latex>\mathcal{V}</latex> should 
    /// include a parameter under which all possible states must have 
    /// a real chance of being selected: this parameter
    /// is specified as the initial <i>reference</i> parameter
    /// <latex>w_0</latex>.
    /// A <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> defines 
    /// <latex>w_0</latex> as a vector whose entries 
    /// <latex>p_{j,l,c}</latex>,
    /// corresponding to feature 
    /// categories, are all set equal to <latex>0.5</latex>, while entries
    /// <latex>p_{j,\RD,c}</latex>,
    /// corresponding to response categories, are all set equal to 
    /// <latex>1/n_{\RD}</latex>.
    /// </para>    
    /// <para><b><i>Updating step</i></b></para>
    /// <para  id='Updating.1'>
    /// At iteration <latex>t</latex>, let us represent the sample drawn 
    /// as <latex>X_{t,0},\dots,X_{N-1}</latex>, where <latex>N</latex> is the 
    /// Cross-Entropy sample size, and the <latex>i</latex>-th sample point
    /// is the sequence <latex>X_{t,i}=\round{X_{t,i,0},\dots,X_{t,i,J-1}}</latex>.
    /// The parameter's 
    /// updating formula is, 
    /// for <latex mode='inline'>l=0,\dots,L-1</latex> and
    /// <latex mode='inline'>c=0,\dots,n_l-1</latex>,
    /// <latex mode="display">
    /// p_{t,j,l,c} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\round{X_{t,i}}\,X_{t,i,l,c}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\round{X_{t,i}}},
    /// </latex>
    /// where <latex mode='inline'>A_t</latex>
    /// is the elite sample in this context, i.e. the set of sample
    /// points having the lowest performances observed during the <latex>t</latex>-th
    /// iteration, if minimizing, the highest ones, otherwise, while
    /// <latex>I_{A_t}</latex> is its indicator function.
    /// </para>
    /// <para>
    /// Analogously, one has, for <latex>c=0,\dots,n_{\RD}-1</latex>,
    /// <latex mode="display">
    /// p_{t,j,\RD,c} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\round{X_{t,i}}\,X_{t,i,\RD,c}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\round{X_{t,i}}}.
    /// </latex>
    /// </para>
    /// <para><i>Applying a smoothing scheme to updated parameters</i></para>
    /// <para id='Smoothing.1'>
    /// In a <see cref="CategoricalEntailmentEnsembleOptimizationContext"/>, 
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
    ///  p_{T,0} &amp; \cdots &amp; p_{T,J-1} 
    /// },
    /// </latex>
    /// the argument <latex>x_T\equiv\mx{x_{T,0} \cdots x_{T,J-1}}=
    /// \OA\round{w_T}</latex> at which 
    /// the searched extremum is considered 
    /// as reached according to the Cross-Entropy method will be
    /// returned as follows.
    /// </para>
    /// <para>
    /// For <latex>j=0,\dots,J-1</latex>, one has
    /// <latex>x_{T,j}=\mx{x_{T,j,0} &amp; \cdots &amp; x_{T,j,L-1} &amp; 
    /// x_{T,j,\RD} &amp; \ET_{T,j}}</latex>, where
    /// for <latex>l=0,\dots,L-1</latex>, 
    /// <latex>x_{T,j,l} = \mx{x_{T,j,l,0} &amp; \cdots &amp; x_{T,j,l,n_{l}-1}}</latex>, 
    /// with <latex>x_{T,j,l,c}</latex> equal to unity 
    /// if <latex>p_{T,j,l,c} &gt; .5</latex>, zero otherwise.
    /// Block <latex>x_{T,j,\RD}</latex> is a binary vector 
    /// <latex>\mx{x_{T,j,\RD,0} &amp; \cdots &amp; 
    /// x_{T,j,\RD,n_{\RD}-1}}</latex>, in 
    /// which there is only one entry equal 
    /// to unity, taken at random among those corresponding to 
    /// probabilities <latex>p_{T,j,\RD,c}</latex> equal to 
    /// <latex mode='display'>
    /// \max\,\set{p_{T,j,\RD,g} : g=0,\dots,n_{\RD}-1}}.
    /// </latex>
    /// Finally, <latex>\ET_{T,j}</latex> is unity if partial 
    /// truth values are not allowed; otherwise, it is set
    /// equal to <latex>1 + \sum_{c=0}^{n_{\RD}-1} 
    /// \log_{n_{\RD}}\round{p_{T,j,\RD,c}}.
    /// </latex>
    /// </para>
    /// <para><b><i>Stopping criterion</i></b></para>
    /// <para id='Stop.1'>
    /// A <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> never 
    /// stops before executing a number of iterations less than 
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MinimumNumberOfIterations"/>, and always stops
    /// if such number is greater than or equal to
    /// <see cref="SystemPerformanceOptimizationContext
    /// .MaximumNumberOfIterations"/>. 
    /// </para>
    /// <para id='Stop.2'>
    /// For intermediate iterations, default method <see cref=
    /// "SystemPerformanceOptimizationContext.StopAtIntermediateIteration(
    /// int, LinkedList{double}, LinkedList{DoubleMatrix})"/> is
    /// called to check if a Cross-Entropy program executing in this
    /// context should stop or not.
    /// </para>
    /// <para><b>Instantiating a context for optimizing on entailments</b></para>
    /// <para>
    /// At instantiation, the constructor of 
    /// a <see cref="CategoricalEntailmentEnsembleOptimizationContext"/> object
    /// will receive information about the optimization under study by
    /// means of parameters representing the objective function
    /// <latex>H</latex>, the number of categories in the feature and
    /// response variables, 
    /// <latex>n_0,\dots,n_{L-1}</latex> and
    /// <latex>n_{\RD}</latex> respectively, 
    /// the number of entailments to be searched <latex>J</latex>, 
    /// the extremes of the allowed range of
    /// intermediate iterations,
    /// <latex>m</latex> and <latex>M</latex>, and a constant stating 
    /// if the optimization goal is a maximization or a minimization.
    /// In addition, the smoothing parameter <latex>\alpha</latex>
    /// and a boolean constant signaling if entailment partial truth
    /// values should be allowed
    /// are also 
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
    /// Count constants <latex>J</latex>,
    /// <latex>\round{n_0,\dots,n_{L-1}}</latex> and <latex>n_{\RD}</latex> are 
    /// returned by 
    /// <see cref="NumberOfCategoricalEntailments"/>,
    /// <see cref="FeatureCategoryCounts"/>, and
    /// <see cref="NumberOfResponseCategories"/>, respectively.
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
    /// vector representing a valid representation of an argument.
    /// </para>
    /// </remarks>
    public sealed class CategoricalEntailmentEnsembleOptimizationContext
        : SystemPerformanceOptimizationContext
    {
        #region State

        /// <summary>
        /// Gets the collection of category counts for the 
        /// features on which are defined the premises of the 
        /// categorical entailments searched by this context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let us assume that the
        /// feature space <latex>\FS</latex> has been defined
        /// by taking into account 
        /// <latex>L</latex> feature variables, and that 
        /// variable <latex>l\in\{0,\dots,L-1\}</latex> has 
        /// finite domain <latex>\FD_l</latex>. 
        /// Then <latex>\FS</latex> can
        /// be represented as the 
        /// Cartesian product <latex>\FD_0\times\dots\times\FD_{L-1}</latex>,
        /// and the <latex>l</latex>-th entry 
        /// in <see cref="FeatureCategoryCounts"/> will 
        /// store the cardinality of <latex>\FD_l</latex>.
        /// </para>
        /// </remarks>
        /// <value>
        /// The collection of category counts for the 
        /// features on which are defined the premises of the 
        /// categorical entailments searched by this context.
        /// </value>
        public List<int> FeatureCategoryCounts { get; }

        /// <summary>
        /// Gets the number of categories in the response variable.
        /// </summary>
        /// <value>
        /// The number of categories in the response variable.
        /// </value>
        public int NumberOfResponseCategories { get; }

        /// <summary>
        /// Gets a value indicating whether that the truth value of 
        /// a sampled categorical entailment must be equal to the 
        /// homogeneity of the probability distribution from which 
        /// its conclusion has been drawn. 
        /// Otherwise, the truth value is always set to unity.
        /// </summary>
        /// <value>
        /// <c>true</c> if the truth value of 
        /// a sampled categorical entailment must be equal to the 
        /// homogeneity of the probability distribution from which 
        /// its conclusion has been drawn. 
        /// <c>false</c>, if the truth value must be always set to unity.
        /// </value>
        public bool AllowEntailmentPartialTruthValues { get; }

        /// <summary>
        /// Gets the overall number of feature categories.
        /// </summary>
        /// <value>
        /// The sum of all entries in <see cref="FeatureCategoryCounts"/>.
        /// </value>
        public int OverallNumberOfFeatureCategories { get; }

        /// <summary>
        /// Gets the number of categorical entailments.
        /// </summary>
        /// <value>The number of categorical entailments.</value>
        public int NumberOfCategoricalEntailments { get; }

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

        #region Stop conditions

        /// <summary>
        /// A list of arrays whose j-th entry stores the response code
        /// consented by the j-th entailment at a specified iteration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The length of each array equals 
        /// the <see cref="NumberOfCategoricalEntailments"/>.
        /// </para>
        /// </remarks>
        private readonly LinkedList<double[]> entailmentConcludedResponseCodes;

        /// <summary>
        /// A list of jagged arrays whose [j][i]-th entry stores the set of
        /// codes for the i-th feature contributing to the definition of 
        /// the j-th entailment at a specified iteration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The dimension of each jagged array equals the
        /// <see cref="NumberOfCategoricalEntailments"/>.
        /// </para>
        /// <para>
        /// The dimension of each element in the array is the number of
        /// available features.
        /// </para>
        /// </remarks>
        private readonly LinkedList<SortedSet<int>[][]> entailmentInPremiseFeaturePositions;

        #endregion

        /// <summary>
        /// The function representing the performance of the system.
        /// </summary>
        private readonly Func<DoubleMatrix, double> objectiveFunction;

        /// <summary>
        /// A random number generator aimed to resolve at random 
        /// ties among response probabilities equal to the 
        /// maximal one in <see cref="GetOptimalState(DoubleMatrix)"/>.
        /// </summary>
        private readonly RandomNumberGenerator randomNumberGenerator =
            RandomNumberGenerator.CreateMT19937(777777);

        /// <summary>
        /// The response codes as a matrix.
        /// </summary>
        private readonly DoubleMatrix responseCategoryCodes;

        /// <summary>
        /// Gets the <see cref="CategoricalEntailmentEnsembleClassifier"/> instance
        /// represented by an element of the state-space defined by this context,
        /// having the specified feature and response variables.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The argument <paramref name="state"/> must be a valid input 
        /// for the <see cref="CrossEntropyContext.Performance(DoubleMatrix)">
        /// objective (performance) function</see> 
        /// defined by this context.
        /// </para>
        /// </remarks>
        /// <param name="state">
        /// A matrix representing an element of the state-space        
        /// defined by this context.
        /// </param>
        /// <param name="featureVariables">
        /// The list of feature categorical variables about which 
        /// the represented entailments define 
        /// their <see cref="CategoricalEntailment.FeaturePremises"/>.
        /// </param>
        /// <param name="responseVariable">
        /// The categorical variable from which 
        /// the represented entailments define 
        /// their <see cref="CategoricalEntailment.ResponseConclusion"/> values.
        /// </param>
        /// <returns>
        /// The <see cref="CategoricalEntailmentEnsembleClassifier"/> instance
        /// whose categorical entailments are represented 
        /// by <paramref name="state"/>,
        /// having the specified feature and response variables.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="state"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariables"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariable"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="state"/> has not the  
        /// <see cref="DoubleMatrix.Count"/> expected given 
        /// the <see cref="NumberOfCategoricalEntailments"/> of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="featureVariables"/> has not the same  
        /// <see cref="ICollection{T}.Count"/> of the
        /// <see cref="FeatureCategoryCounts"/> of this instance.<br/>
        /// -or-<br/>
        /// Some variable in <paramref name="featureVariables"/> has not
        /// the <see cref="ICollection{T}.Count"/> expected given the 
        /// the <see cref="FeatureCategoryCounts"/> of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="responseVariable"/> has not
        /// the <see cref="ICollection{T}.Count"/> expected given the 
        /// the <see cref="NumberOfResponseCategories"/> of this instance.
        /// </exception>
        public CategoricalEntailmentEnsembleClassifier 
            GetCategoricalEntailmentEnsembleClassifier(
                DoubleMatrix state,
                List<CategoricalVariable> featureVariables,
                CategoricalVariable responseVariable)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(state);

            int numberOfCategoricalEntailments =
                this.NumberOfCategoricalEntailments;

            int overallNumberOfCategories =
                this.OverallNumberOfFeatureCategories +
                this.NumberOfResponseCategories;

            int entailmentRepresentationLength =
                overallNumberOfCategories + 1;

            if (state.Count != entailmentRepresentationLength * numberOfCategoricalEntailments)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_INVALID_STATE_COUNT"),
                    nameof(state));
            }

            ArgumentNullException.ThrowIfNull(featureVariables);

            if (featureVariables.Count != this.FeatureCategoryCounts.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_MUST_HAVE_SAME_FEATURES_COUNT"),
                    nameof(featureVariables));
            }

            for (int f = 0; f < featureVariables.Count; f++)
            {
                if (featureVariables[f].NumberOfCategories != this.FeatureCategoryCounts[f])
                {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CEE_INVALID_FEATURE_CATEGORIES_COUNT"),
                        nameof(featureVariables));
                }
            }

            ArgumentNullException.ThrowIfNull(responseVariable);

            if (responseVariable.NumberOfCategories != this.NumberOfResponseCategories)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CEE_INVALID_RESPONSE_CATEGORIES_COUNT"),
                    nameof(responseVariable));
            }

            #endregion

            var classifier = new CategoricalEntailmentEnsembleClassifier(
                featureVariables: featureVariables,
                responseVariable: responseVariable);

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                int entailmentRepresentationIndex = e * entailmentRepresentationLength;

                classifier.Add(new CategoricalEntailment(
                    entailmentRepresentation: state[
                        0, IndexCollection.Range(
                            entailmentRepresentationIndex,
                            entailmentRepresentationIndex + overallNumberOfCategories)],
                    featureVariables: featureVariables,
                    responseVariable: responseVariable));
            }

            return classifier;
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CategoricalEntailmentEnsembleOptimizationContext" /> class 
        /// aimed to train an ensemble of categorical entailments by optimizing the 
        /// specified objective function,
        /// with the given range of iterations, and 
        /// probability smoothing coefficient.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The function to be optimized.
        /// </param>
        /// <param name="featureCategoryCounts">
        /// A list whose length equals the number of features on which 
        /// are based the premises of the categorical entailments to be searched. 
        /// At a given position, the list stores the number of categories in
        /// the corresponding feature variable.
        /// </param>
        /// <param name="numberOfResponseCategories">
        /// The number of categories in
        /// the feature variable about which is defined the conclusions
        /// of the categorical entailments to be searched.
        /// </param>
        /// <param name="numberOfCategoricalEntailments">
        /// The number of categorical entailments to be selected.
        /// </param>
        /// <param name="allowEntailmentPartialTruthValues">
        /// If set to <c>true</c> signals that the truth value of a sampled
        /// categorical entailment must be equal to the homogeneity 
        /// of the probability distribution from which its conclusion has been
        /// drawn. Otherwise, the truth value is unity.
        /// </param>
        /// <param name="probabilitySmoothingCoefficient">
        /// A coefficient to define the smoothing scheme for the
        /// probabilities of the Cross-Entropy parameters 
        /// exploited by this context.
        /// </param>
        /// <param name="optimizationGoal">
        /// A constant to specify if the function
        /// must be minimized or maximized.
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
        /// <para>
        /// As discussed by Rubinstein and Kroese, 
        /// Remark 5.2, p. 189<cite>rubinstein-kroese-2004</cite>,
        /// typical values for <paramref name="probabilitySmoothingCoefficient"/>
        /// are between .7 and 1 (excluded).
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="featureCategoryCounts"/> is <b>null</b>.<br/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="optimizationGoal"/> is not a field of
        /// <see cref="OptimizationGoal"/>.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is greater than 
        /// <paramref name="maximumNumberOfIterations"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="probabilitySmoothingCoefficient"/> is not
        /// in the open interval between 0 and 1.<br/>
        /// -or-<br/>
        /// <paramref name="featureCategoryCounts"/> is empty.<br/>
        /// -or-<br/>
        /// <paramref name="featureCategoryCounts"/> has negative or zero entries.<br/>
        /// -or-<br/>
        /// <paramref name="numberOfCategoricalEntailments"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="minimumNumberOfIterations"/> is
        /// not positive.<br/>
        /// -or-<br/>
        /// <paramref name="maximumNumberOfIterations"/> is
        /// not positive.
        /// </exception>
        public CategoricalEntailmentEnsembleOptimizationContext(
            Func<DoubleMatrix, double> objectiveFunction,
            List<int> featureCategoryCounts,
            int numberOfResponseCategories,
            int numberOfCategoricalEntailments,
            bool allowEntailmentPartialTruthValues,
            double probabilitySmoothingCoefficient,
            OptimizationGoal optimizationGoal,
            int minimumNumberOfIterations,
            int maximumNumberOfIterations)
            : base(
                  stateDimension: GetStateDimension(
                      featureCategoryCounts,
                      numberOfResponseCategories,
                      numberOfCategoricalEntailments,
                      out DoubleMatrix initialParameter,
                      out int overallNumberOfFeatureCategories),
                  initialParameter: initialParameter,
                  optimizationGoal: optimizationGoal,
                  minimumNumberOfIterations: minimumNumberOfIterations,
                  maximumNumberOfIterations: maximumNumberOfIterations)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(objectiveFunction);

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
                            "0.0", "1.0"));
            }

            #endregion

            this.objectiveFunction = objectiveFunction;
            this.FeatureCategoryCounts = new List<int>(featureCategoryCounts);
            this.NumberOfResponseCategories = numberOfResponseCategories;
            this.OverallNumberOfFeatureCategories = overallNumberOfFeatureCategories;
            this.responseCategoryCodes = DoubleMatrix.Dense(1, numberOfResponseCategories);

            for (int i = 0; i < this.responseCategoryCodes.Count; i++)
            {
                this.responseCategoryCodes[i] = i;
            }

            this.entailmentInPremiseFeaturePositions = new LinkedList<SortedSet<int>[][]>();
            this.entailmentConcludedResponseCodes = new LinkedList<double[]>();
            this.AllowEntailmentPartialTruthValues = allowEntailmentPartialTruthValues;

            this.NumberOfCategoricalEntailments = numberOfCategoricalEntailments;
            this.ProbabilitySmoothingCoefficient = probabilitySmoothingCoefficient;
        }

        /// <summary>
        /// Gets the state dimension for this context. Also computes
        /// its initial parameter and the overall number of feature
        /// categories.
        /// </summary>
        private static int GetStateDimension(
            List<int> featureCategoryCounts,
            int numberOfResponseCategories,
            int numberOfCategoricalEntailments,
            out DoubleMatrix initialParameter,
            out int overallNumberOfFeatureCategories)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(featureCategoryCounts);

            if (featureCategoryCounts.Count == 0)
            {
                throw new ArgumentException(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY"),
                nameof(featureCategoryCounts));
            }

            if (numberOfResponseCategories < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfResponseCategories),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (numberOfCategoricalEntailments < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfCategoricalEntailments),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            int stateDimension = 0;
            for (int j = 0; j < featureCategoryCounts.Count; j++)
            {
                if (featureCategoryCounts[j] <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(featureCategoryCounts),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE"));
                }
                stateDimension += featureCategoryCounts[j];
            }
            overallNumberOfFeatureCategories = stateDimension;

            stateDimension += numberOfResponseCategories + 1;

            stateDimension *= numberOfCategoricalEntailments;

            int overallNumberOfCategories =
                overallNumberOfFeatureCategories + numberOfResponseCategories;

            initialParameter = DoubleMatrix.Dense(
                1,
                overallNumberOfCategories * numberOfCategoricalEntailments);

            double initialResponseProbability = 1.0 / numberOfResponseCategories;

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                int j = e * overallNumberOfCategories;
                int f;
                for (f = 0; f < overallNumberOfFeatureCategories; f++)
                {
                    initialParameter[j + f] = .5;
                }
                int leadingIndex = j + f;
                for (int r = 0; r < numberOfResponseCategories; r++)
                {
                    initialParameter[leadingIndex + r] = initialResponseProbability;
                }
            }

            return stateDimension;
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
        /// This method is overridden so that the performance of a state 
        /// is defined as the value of the objective function 
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
        /// This method has been implemented to support
        /// the optimization of functions on ensembles of
        /// categorical entailments.
        /// </para>
        /// </remarks>
        protected internal override DoubleMatrix UpdateParameter(
            LinkedList<DoubleMatrix> parameters,
            DoubleMatrix eliteSample)
        {
            int overallNumberOfFeatureCategories = this.OverallNumberOfFeatureCategories;
            int numberOfResponseCategories = this.NumberOfResponseCategories;
            int overallNumberOfCategories = overallNumberOfFeatureCategories + numberOfResponseCategories;

            int numberOfCategoricalEntailments = this.NumberOfCategoricalEntailments;

            int entailmentParameterLength = overallNumberOfCategories;
            int entailmentRepresentationLength = 1 + overallNumberOfCategories;

            DoubleMatrix newParameter =
                DoubleMatrix.Dense(1,
                    entailmentParameterLength * numberOfCategoricalEntailments);

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                int entailmentParameterIndex = e * entailmentParameterLength;
                int entailmentRepresentationIndex = e * entailmentRepresentationLength;

                newParameter[0, IndexCollection.Range(
                    entailmentParameterIndex,
                    entailmentParameterIndex + entailmentParameterLength - 1)] =
                    Stat.Mean(
                        data: eliteSample[":", IndexCollection.Range(
                            entailmentRepresentationIndex,
                            entailmentRepresentationIndex + entailmentParameterLength - 1)],
                        dataOperation: DataOperation.OnColumns);
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

            int overallNumberOfFeatureCategories = this.OverallNumberOfFeatureCategories;
            int numberOfResponseCategories = this.NumberOfResponseCategories;
            int overallNumberOfCategories = overallNumberOfFeatureCategories + numberOfResponseCategories;
            int numberOfCategoricalEntailments = this.NumberOfCategoricalEntailments;

            var lastInPremiseFeaturePositions = new SortedSet<int>[numberOfCategoricalEntailments][];
            double[] lastConcludedResponseCodes = new double[numberOfCategoricalEntailments];

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                lastInPremiseFeaturePositions[e] =
                    new SortedSet<int>[this.FeatureCategoryCounts.Count];

                int entailmentParameterIndex = e * overallNumberOfCategories;

                for (int f = 0; f < this.FeatureCategoryCounts.Count; f++)
                {
                    lastInPremiseFeaturePositions[e][f] = [];
                    for (int c = 0; c < this.FeatureCategoryCounts[f]; c++, entailmentParameterIndex++)
                    {
                        if (parameter[entailmentParameterIndex] > .5)
                            lastInPremiseFeaturePositions[e][f].Add(c);
                    }
                }

                int maxResponseProbabilityIndex = 0;
                double maxResponseProbability = Double.NegativeInfinity;
                for (int r = 0; r < numberOfResponseCategories; r++)
                {
                    int entailmenteResponseParameterIndex = r + entailmentParameterIndex;
                    if (parameter[entailmenteResponseParameterIndex] > maxResponseProbability)
                    {
                        maxResponseProbability = parameter[entailmenteResponseParameterIndex];
                        maxResponseProbabilityIndex = r;
                    }
                }
                lastConcludedResponseCodes[e] =
                    this.responseCategoryCodes[maxResponseProbabilityIndex];
            }

            this.entailmentInPremiseFeaturePositions.AddLast(lastInPremiseFeaturePositions);

            this.entailmentConcludedResponseCodes.AddLast(lastConcludedResponseCodes);

            // Calling the base class OnExecutedIteration method.
            base.OnExecutedIteration(
                iteration,
                sample,
                levels,
                parameters);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// This method has been implemented to support
        /// the optimization of functions on ensembles of categorical entailments.
        /// </para>
        /// </remarks>
        protected internal override void PartialSample(
            double[] destinationArray,
            Tuple<int, int> sampleSubsetRange,
            RandomNumberGenerator randomNumberGenerator,
            DoubleMatrix parameter,
            int sampleSize)
        {
            var featureCategoryDistribution = BernoulliDistribution.Balanced();
            featureCategoryDistribution.RandomNumberGenerator = randomNumberGenerator;

            int localSampleSize = sampleSubsetRange.Item2 - sampleSubsetRange.Item1;

            int leadingDimension = sampleSize; // Overall sample size

            int overallNumberOfFeatureCategories = this.OverallNumberOfFeatureCategories;
            int numberOfResponseCategories = this.NumberOfResponseCategories;

            var responseCategoryCodes =
                this.responseCategoryCodes;

            int entailmentParameterLength =
                overallNumberOfFeatureCategories + numberOfResponseCategories;

            int entailmentRepresentationLength = 1 + entailmentParameterLength;

            for (int e = 0; e < this.NumberOfCategoricalEntailments; e++)
            {
                int entailmentRepresentationIndex = e * entailmentRepresentationLength;
                int entailmentParameterIndex = e * entailmentParameterLength;

                // Sample the premises
                for (int f = 0;
                     f < overallNumberOfFeatureCategories;
                     f++, entailmentRepresentationIndex++, entailmentParameterIndex++)
                {
                    featureCategoryDistribution.SuccessProbability =
                        parameter[0, entailmentParameterIndex];
                    featureCategoryDistribution.Sample(
                        localSampleSize,
                        destinationArray,
                        entailmentRepresentationIndex * leadingDimension + sampleSubsetRange.Item1);
                }

                // Sample the conclusion
                var responseProbabilities = parameter[0,
                    IndexCollection.Range(
                        entailmentParameterIndex,
                        entailmentParameterIndex + numberOfResponseCategories - 1)];
                var responseDistribution =
                    new FiniteDiscreteDistribution(
                        responseCategoryCodes,
                        responseProbabilities,
                        fromPublicAPI: false)
                    {
                        RandomNumberGenerator = randomNumberGenerator
                    };

                var responseSample = responseDistribution.Sample(localSampleSize);

                double truthValue = 1.0;
                for (int r = 0; r < numberOfResponseCategories; r++)
                {
                    var responseCategoryIndexes = responseSample.Find(responseCategoryCodes[r]);
                    if (responseCategoryIndexes is not null)
                    {
                        for (int i = 0; i < responseCategoryIndexes.Count; i++)
                        {
                            destinationArray[(entailmentRepresentationIndex + r) * leadingDimension
                                + sampleSubsetRange.Item1 + responseCategoryIndexes[i]] = 1.0;
                        }
                    }
                    if (this.AllowEntailmentPartialTruthValues)
                    {
                        var p = responseProbabilities[r];
                        if (p > 0.0)
                        {
                            truthValue += p * Math.Log(p, numberOfResponseCategories);
                        }
                    }
                }

                // Set the truth value
                int truthValueLeadingIndex =
                    (entailmentRepresentationIndex + numberOfResponseCategories) * leadingDimension;
                for (int i = sampleSubsetRange.Item1; i < sampleSubsetRange.Item2; i++)
                {
                    destinationArray[truthValueLeadingIndex + i] = truthValue;
                }
            }
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
            int overallNumberOfFeatureCategories = this.OverallNumberOfFeatureCategories;
            int numberOfResponseCategories = this.NumberOfResponseCategories;
            int overallNumberOfCategories = overallNumberOfFeatureCategories + numberOfResponseCategories;

            int numberOfCategoricalEntailments = this.NumberOfCategoricalEntailments;

            int entailmentParameterLength = overallNumberOfCategories;
            int entailmentRepresentationLength = 1 + overallNumberOfCategories;

            DoubleMatrix optimalState =
                DoubleMatrix.Dense(1, this.StateDimension);

            for (int e = 0; e < numberOfCategoricalEntailments; e++)
            {
                int entailmentRepresentationIndex = e * entailmentRepresentationLength;
                int entailmentParameterIndex = e * entailmentParameterLength;

                // Optimal premises
                for (int f = 0;
                    f < overallNumberOfFeatureCategories;
                    f++, entailmentRepresentationIndex++, entailmentParameterIndex++)
                {
                    if (parameter[entailmentParameterIndex] > .5)
                        optimalState[entailmentRepresentationIndex] = 1.0;
                }

                // Optimal response
                var responseIndexes =
                    IndexCollection.Range(
                        entailmentParameterIndex,
                        entailmentParameterIndex + numberOfResponseCategories - 1);

                var responseProbabilities = parameter.Vec(responseIndexes);

                double maximumResponseProbability = Stat.Max(responseProbabilities).value;

                var maximumProbabilityIndexes =
                    responseProbabilities.Find(maximumResponseProbability);

                int numberOfMaximumProbabilityIndexes = maximumProbabilityIndexes.Count;
                if (numberOfMaximumProbabilityIndexes == 1)
                {
                    optimalState[
                        entailmentRepresentationIndex + maximumProbabilityIndexes[0]] = 1.0;
                }
                else
                {
                    // Pick a position corresponding to a maximum probability at random
                    int randomMaximumProbabilityIndex = Convert.ToInt32(
                        Math.Floor(numberOfMaximumProbabilityIndexes *
                            this.randomNumberGenerator.DefaultUniform()));
                    optimalState[
                        entailmentRepresentationIndex +
                        maximumProbabilityIndexes[randomMaximumProbabilityIndex]] = 1.0;
                }

                // Truth value
                double truthValue = 1.0;
                if (this.AllowEntailmentPartialTruthValues)
                {
                    for (int r = 0; r < responseProbabilities.Count; r++)
                    {
                        var p = responseProbabilities[r];

                        if (p > 0.0)
                        {
                            truthValue += p * Math.Log(p, numberOfResponseCategories);
                        }
                    }
                }

                optimalState[
                    entailmentRepresentationIndex + numberOfResponseCategories] = truthValue;
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
        /// <inheritdoc cref="CategoricalEntailmentEnsembleOptimizationContext" 
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
                DoubleMatrix previousParameter = parameters.Last.Previous.Value;

                double alpha = this.ProbabilitySmoothingCoefficient;
                parameters.Last.Value =
                    alpha * parameters.Last.Value + (1.0 - alpha) * previousParameter;
            }
        }

#endregion
    }
}
