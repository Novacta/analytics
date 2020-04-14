// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a Cross-Entropy program designed to provide 
    /// the optimization of a system's performance.
    /// </summary>
    /// <remarks>
    /// <para><b>The Cross-Entropy method for combinatorial and 
    /// multi-extremal optimization</b></para>
    /// <para>
    /// The current implementation of 
    /// the <see cref="SystemPerformanceOptimizer"/> class
    /// is based on the main Cross-Entropy program for optimization 
    /// proposed by Rubinstein and Kroese 
    /// (Algorithm 4.2.1)<cite>rubinstein-kroese-2004</cite>.
    /// </para>    
    /// <para>
    /// The Cross-Entropy method raises as a solution to the
    /// problem of estimating the probability of rare events 
    /// regarding the performance of complex systems. However,
    /// soon the method was recognized 
    /// as closely related to the optimization 
    /// of continuous or discrete functions. In fact, optimizing 
    /// a function 
    /// is a task that can be accomplished by exploiting the 
    /// Cross-Entropy machinery aimed to estimate
    /// the probability of rare events, since the event
    /// represented by a function assuming a value over 
    /// a given extreme threshold can be typically interpreted as rare.
    /// </para>
    /// <para>
    /// The Cross-Entropy algorithm for optimization is an 
    /// adapted version of that for rare event simulation. As the latter,
    /// it is an iterative 
    /// multi step procedure, having two main steps. In the <i>sampling</i>
    /// step, the state-space of the system (the domain 
    /// <latex>\mathcal{X}</latex> of the
    /// performance function <latex>H\round{x}</latex>, 
    /// <latex>x \in \mathcal{X}</latex>) is traversed looking 
    /// for good candidate 
    /// points at
    /// which global performance
    /// extrema can be attained. In the subsequent <i>updating</i> step,
    /// the distribution from which the states are sampled is modified
    /// in order to increase, in the next iterations, the probability
    /// of sampling near the true optimizer,
    /// say <latex>x_{*}</latex>.
    /// </para>
    /// <para><i>Sampling step</i></para>
    /// <para>
    /// At each iteration <latex>t</latex>, a <i>sampling step</i> is
    /// executed to generate states of the system which are 
    /// interpreted as candidate arguments of the performance
    /// function. It is assumed that such sample is drawn
    /// from a specific density or probability function, 
    /// member of a parametric family  
    /// <latex mode="display">
    /// \mathcal{P}=\{f_v\left(x\right)|v\in \mathcal{V}\},
    /// </latex>
    /// where <latex mode='inline'>x</latex> is a vector representing 
    /// a possible state of the system, 
    /// and <latex mode='inline'>\mathcal{V}</latex> is the set of allowable 
    /// values for parameter <latex mode='inline'>v</latex>. 
    /// The parameter exploited at iteration <latex mode='inline'>t</latex>
    /// is referred to as the <i>reference</i> parameter at 
    /// <latex mode='inline'>t</latex> and indicated as
    /// <latex mode='inline'>w_{t-1}</latex>.
    /// </para>
    /// <para>
    /// The parametric family must be
    /// carefully chosen. In particular, the first reference 
    /// parameter, <latex>w_0</latex>, must be
    /// such that no relevant subsets of the 
    /// state-space  <latex>\mathcal{X}</latex> will  
    /// receive a too low probability: when starting the search
    /// for an optimizer, all possible states must have a real chance
    /// of being selected. If there is no a-priori knowledge
    /// about the position of the optimizer inside the state-space,
    /// it would be very useful if the parametric family could
    /// include a parameter such that the corresponding distribution
    /// were - at least approximately - uniform on the 
    /// system's state-space.
    /// That parameter would be the perfect candidate 
    /// for <latex>w_0</latex>.
    /// </para>
    /// <para>
    /// Furthermore, it is expected that, given a reference 
    /// parameter <latex>w</latex>,
    /// a corresponding reasonable value
    /// could be guessed for the optimal argument, say 
    /// <latex>\OA\round{w}</latex>. In this way, 
    /// by modifying appropriately  
    /// the reference parameter, one can 
    /// suppose to better foresee the optimal argument.
    /// Finally,
    /// given the optimal reference parameter, say 
    /// <latex>w_T</latex> (the parameter corresponding to the 
    /// last iteration <latex>T</latex> executed
    /// by the algorithm before stopping),
    /// it will be possible to select
    /// via 
    /// <latex>\OA\round{w_T}</latex> the best argument 
    /// at which the searched extremum can be considered 
    /// as reached according to the
    /// Cross-Entropy method. 
    /// </para>
    /// <para><i>Updating step</i></para>
    /// <para>
    /// Given the sample <latex>X_{t,0},\dots,X_{t,N-1}</latex>
    /// drawn at iteration <latex>t</latex> from distribution
    /// <latex>f_{w_{t-1}</latex>,
    /// the main idea behind the Cross-Entropy 
    /// <i>updating</i> step is that it must be designed to improve
    /// the concentration of the probability mass near 
    /// <latex>x_{*}</latex>. To obtain such result, the updating 
    /// procedure is based on the sampled
    /// points whose performances are more relevant given
    /// the optimization goal of the algorithm: if one is 
    /// maximizing the performance function, then the updating
    /// step relies on the states having the highest
    /// observed performances; otherwise, the update of the
    /// reference parameter is supported by those
    /// sampled states corresponding to the lowest
    /// performances.
    /// </para>
    /// The relevant points are known as <i>elite</i> sample
    /// points and can be defined as follows.
    /// The sample performances, 
    /// <latex>
    /// H\left(X_{t,0}\right),\dots,H\left(X_{t,N-1}\right)</latex>, 
    /// are computed and sorted in increasing order, say obtaining the 
    /// following ordering:
    /// <latex mode="display">
    /// h_{t,\left(0\right)}\leq\dots\leq h_{t,\left(N-1\right)}.
    /// </latex>
    /// <para>
    /// Let <latex>X_{t,\left(i\right)}</latex> be the sampled 
    /// state such that 
    /// <latex>
    /// h_{t,\left(i\right)}=H\left(X_{t,\left(i\right)}\right)</latex>, 
    /// <latex>i=0,\dots,N-1</latex>, and consider 
    /// a constant, referred to as the <em>rarity</em> of the program, 
    /// say <latex>\rho</latex>, set to a value 
    /// in the interval <latex>\left[.01,.1\right]</latex>. 
    /// </para>
    /// <para>
    /// If the optimization goal is a maximization, 
    /// then the points 
    /// <latex>X_{t,\left(\eta_u\right)},\dots,X_{t,\left(N-1\right)}</latex> 
    /// occupying a position greater than or equal 
    /// to <latex>\eta_u=\lceil\left(1-\rho\right)N\rceil</latex> are 
    /// the <em>elite</em> sampled states, with 
    /// <latex>\lceil x \rceil</latex> being the ceiling function. 
    /// In this case, the current iteration is said to reach a performance 
    /// <i>level</i> equal to <latex>\lambda_t=h_{t,\left(\eta_u\right)}</latex>.
    /// </para>
    /// <para>
    /// Otherwise, if the optimization goal is a minimization, 
    /// then the <i>elite</i> sampled states can be defined as those points 
    /// occupying a position less than or equal 
    /// to <latex>\eta_l=\lfloor\left(\rho\right)N\rfloor</latex>, 
    /// where <latex>\lfloor x \rfloor</latex> is the floor function. 
    /// Such points are thus
    /// <latex>X_{t,\left(0\right)},\dots,X_{t,\left(\eta_l\right)}</latex>,
    /// and the current iteration is said to reach a performance 
    /// <i>level</i> equal to <latex>\lambda_t=h_{t,\left(\eta_l\right)}</latex>.
    /// </para>
    /// <para>
    /// The joint 
    /// distribution from which the states will be sampled in the 
    /// next iteration is updated exploiting 
    /// the <i>elite</i> states only: in this way, given that the 
    /// performances of
    /// the <i>elite</i> points are likely to be similar to that
    /// at <latex>x_{*}</latex>, it can be expected that such
    /// choice will push <latex>\OA\round{w_t}</latex> towards
    /// <latex>x_{*}</latex>.
    /// </para>
    /// <para>
    /// In addition, to achieve the convergence of the algorithm,
    /// the mass also needs to accumulate near <latex>\OA\round{w_t}</latex>.
    /// Such result can be obtained by estimating via the Cross-Entropy
    /// method the probability of 
    /// a rare event: in case of maximization, the event of interest
    /// being
    /// <latex mode='display'>
    /// A_L\!\left(\lambda_t\right)
    /// \equiv \{x\in\mathcal{X}|\lambda_t\leq H\!\left(x\right)\},</latex>
    /// or, if <latex>H</latex> must be minimized, 
    /// <latex mode="display">
    /// A_U\!\left(\lambda_t\right)
    /// \equiv\{x\in\mathcal{X}|H\!\left(x\right)\leq\lambda_t\}.</latex> 
    /// </para>
    /// <para>
    /// The target probability can thus be represented as 
    /// <latex mode="display">
    /// {\Pr}_{w_{t-1}}[A\left(\lambda_t\right)]=
    /// \int I_{A\left(\lambda_t\right)}\!\left(x\right)f_{w_{t-1}}\!\left(x\right)dx,
    /// </latex> 
    /// where  <latex>A\left(\lambda\right)</latex> is 
    /// <latex>A_U\!\left(\lambda\right)</latex> or 
    /// <latex>A_L\!\left(\lambda\right)</latex>, and 
    /// <latex>I_{A\left(\lambda\right)}</latex> is the
    /// indicator function 
    /// of <latex mode='inline'>A\left(\lambda\right)</latex>.
    /// </para>
    /// <para>
    /// The Cross-Entropy method selects another 
    /// density in <latex>\mathcal{P}</latex> by identifying a 
    /// parameter <latex>w_{t}\in \mathcal{V}</latex> 
    /// that guarantees the most accurate estimate of the target probability 
    /// for a specified simulation effort, or sample size 
    /// <latex mode='inline'>N</latex>, by taking into account 
    /// the Likelihood estimator based on the ratio 
    /// <latex mode="display">
    /// \mathrm{LR}\!\left(x\right)=
    /// \frac{f_{w_{t-1}}\!\left(x\right)}
    /// {f_{w_{t-1}|A\left(\lambda_t\right)}\!\left(x\right)},
    /// </latex>
    /// where 
    /// <latex mode="display">
    /// f_{w_{t-1}|A\left(\lambda_t\right)}\!\left(x\right)=
    /// \frac{I_{A\left(\lambda_t\right)}\!\left(x\right)f_{w_{t-1}}\!
    /// \left(x\right)}{{\Pr}_{w_{t-1}}[A\left(\lambda_t\right)]}
    /// </latex>
    /// is the density of <latex mode='inline'>X</latex> 
    /// conditional on <latex mode='inline'>A\left(\lambda_t\right)</latex>.
    /// </para> 
    /// <para>
    /// Such estimator is unusable, since it depends on the target probability.   
    /// However, it is ideally optimal because its variance is 
    /// equal to zero. 
    /// For this reason, it is proposed to select the 
    /// parameter <latex mode='inline'>w_{t}</latex> such that the 
    /// corresponding density <latex mode='inline'>f_{w_{t}}</latex> is 
    /// the closest to the optimal one, 
    /// the 
    /// difference between two densities, say 
    /// <latex mode='inline'>\phi</latex> and 
    /// <latex mode='inline'>\varphi</latex>, being measured as a 
    /// Kullback–Leibler divergence:
    /// <latex mode="display">
    /// D\left(\phi,\varphi\right)=
    /// \int \phi\left(x\right) \ln \phi\left(x\right) dx 
    /// - \int \phi\left(x\right) \ln \varphi\left(x\right) dx.
    /// </latex>
    /// The selection is thus obtained by 
    /// minimizing the divergence between the generic density in 
    /// <latex mode='inline'>\mathcal{P}</latex>, say 
    /// <latex mode='inline'>f_v,\hspace{2pt}v\in\mathcal{V}</latex>, and 
    /// the ideal 
    /// density <latex mode='inline'>f_{w_{t-1}|A\left(\lambda_t\right)}</latex>:
    /// <latex mode="display">
    /// \min_{v\in\mathcal{V}} D\left(f_{w_{t-1}|A\left(\lambda_t\right)},f_v\right).
    /// </latex>
    /// </para>
    /// <para>
    /// Such minimization corresponds to the maximization 
    /// <latex mode="display">
    /// \max_{v\in\mathcal{V}} \mathrm{E}_{w_{t-1}} [ 
    /// I_{A\left(\lambda_t\right)}\!\left(X\right) \ln f_{v}\!\left(X\right) ],
    /// </latex>
    /// where <latex mode='inline'>\mathrm{E}_{w_{t-1}}</latex> is the 
    /// expectation operator 
    /// under <latex mode='inline'>X\sim f_{w_{t-1}}</latex>. 
    /// This implies that, given a 
    /// sample <latex mode='inline'>X_{t,0},\dots,X_{t,\round{N-1}}</latex> 
    /// from <latex mode='inline'>f_{w_{t-1}}</latex>, 
    /// a solution to such 
    /// optimization can be estimated through the solution to the 
    /// program:
    /// <latex mode="display">
    /// \max_{v\in\mathcal{V}} g\left(v,w_{t-1},\lambda_t\right),
    /// </latex>
    /// where
    /// <latex mode="display">
    /// g\left(v,w_{t-1},\lambda_t\right)= 
    /// \frac{1}{N}\sum_{i\in \mathcal{S}\left(w_{t-1},\lambda_t\right)}
    /// \ln f_{v}\left(X_{t,i}\right)
    /// </latex>
    /// and 
    /// <latex mode="display">
    /// \mathcal{S}\left(w_{t-1},\lambda_t\right)=\{i\in\{0,\dots,N-1\} 
    /// | X_{t,i}\in A\left(\lambda_t\right)\}.
    /// </latex>
    /// The interest in such an approach is due to the possibility 
    /// of choosing the new parameter <latex>w_{t}</latex> 
    /// under which the event <latex>A\left(\lambda_t\right)</latex> 
    /// becomes less rare under density <latex>f_{w_{t}}</latex> 
    /// than under density <latex>f_{w_{t-1}}</latex>:
    /// the mass concentration
    /// becomes higher.
    /// In this way, by decreasing the rareness of
    /// <latex>A\left(\lambda_t\right)</latex>,
    /// a smaller simulation effort would be required
    /// in order to obtain a good estimation,
    /// since a higher mass concentration is attained.
    /// The iteration parameter 
    /// <latex>w_t</latex> is thus chosen 
    /// to be the optimal one in <latex>\mathcal{V}</latex>
    /// for estimating 
    /// <latex>{\Pr}_{w_{t-1}}[A\left(\lambda_t\right)]}</latex>: 
    /// <latex mode="display">
    /// w_t=\arg\max_{v\in\mathcal{V}} g\left(v,w_{t-1},\lambda_t\right),</latex>
    /// and <latex>w_t</latex> can be interpreted as the
    /// Maximum Likelihood estimate of <latex>w_{t-1}</latex> based
    /// only on the <i>elite</i> sample points.
    /// </para>
    /// <para>
    /// By default, the algorithm stops iterating when 
    /// the iteration <i>level</i> 
    /// does not change for a predefined number of iterations,
    /// or the number of executed iterations reaches a 
    /// specified maximum.
    /// </para>
    /// <para><b>Executing a Cross-Entropy optimizer</b></para>
    /// <para id='Optimize'>
    /// Class <see cref="SystemPerformanceOptimizer"/> follows 
    /// the <em>Template Method</em> pattern<cite>gamma-etal-1995</cite> by 
    /// defining in an 
    /// operation the structure of a Cross-Entropy algorithm aimed to  
    /// optimize the performance of a system. 
    /// <see cref="Optimize">Optimize</see> is 
    /// the template method, in which the invariant parts of a Cross-Entropy 
    /// optimizer are implemented once, 
    /// deferring the implementation of behaviors that can vary to 
    /// a <see cref="SystemPerformanceOptimizationContext"/> instance,
    /// that method <see cref="Optimize">Optimize</see> receives as 
    /// a parameter.
    /// </para>
    /// <para>
    /// <see cref="SystemPerformanceOptimizationContext"/> inherits 
    /// or defines abstract or virtual methods 
    /// which represent some <em>primitive operations</em> 
    /// of the template method, i.e. those varying steps that its 
    /// subclasses will 
    /// override to define the concrete 
    /// behavior of the algorithm. 
    /// See the documentation of class 
    /// <see cref="SystemPerformanceOptimizationContext"/> for
    /// examples of how to implement a context for system's 
    /// performance optimization.
    /// </para>
    /// </remarks>
    ///<seealso cref="CrossEntropyProgram"/>
    public sealed class SystemPerformanceOptimizer
        : CrossEntropyProgram
    {
        /// <summary>
        /// Runs a Cross-Entropy program designed to optimize
        /// a system's performance in the
        /// specified context.
        /// </summary>
        /// <param name="context">The Cross-Entropy context 
        /// representing the optimization problem to solve.</param>
        /// <param name="rarity">
        /// The rarity applied by the Cross-Entropy method.
        /// </param>
        /// <param name="sampleSize">
        /// The size of the samples drawn during the
        /// sampling step of the Cross-Entropy method.
        /// </param>
        /// <remarks>
        /// <para>
        /// For a thorough description of the method and an example 
        /// of how to use it, 
        /// see the remarks 
        /// about the <see cref="SystemPerformanceOptimizer"/> class.
        /// </para>    
        /// </remarks>
        /// <returns>The results of the Cross-Entropy optimizer.</returns>
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Input validation delegated to CrossEntropyProgram.Run.")]
        public SystemPerformanceOptimizationResults Optimize(
            SystemPerformanceOptimizationContext context,
            double rarity,
            int sampleSize)
        {
            var baseResults = base.Run(
                context,
                sampleSize,
                rarity);

            var results = new SystemPerformanceOptimizationResults
            {
                Levels = baseResults.Levels,
                Parameters = baseResults.Parameters
            };

            results.HasConverged = results.Levels.Count < context.MaximumNumberOfIterations;

            DoubleMatrix optimalParameter = results.Parameters.Last.Value;
            results.OptimalState = context.GetOptimalState(optimalParameter);

            results.OptimalPerformance = context.Performance(results.OptimalState);

            return results;
        }
    }
}
