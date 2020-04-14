// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a Cross-Entropy program designed to provide the estimation 
    /// of rare event probabilities.
    /// </summary>
    /// <remarks>
    /// <para><b>The Cross-Entropy method for rare event simulation</b></para>
    /// <para>
    /// The current implementation of 
    /// the <see cref="RareEventProbabilityEstimator"/> class
    /// is based on the main Cross-Entropy program for rare event simulation 
    /// proposed by Rubinstein and Kroese 
    /// (Algorithm 2.3.1)<cite>rubinstein-kroese-2004</cite>.
    /// </para>    
    /// <para id='Estimator.1'>
    /// A Cross-Entropy estimator is designed to evaluate the probability 
    /// of rare events regarding the performance of complex systems.
    /// It is assumed that such probability must be evaluated 
    /// with respect to a specific density function, member of a parametric 
    /// family  
    /// <latex mode="display">\mathcal{P}=\{f_v\left(x\right)|v\in \mathcal{V}\},</latex>
    /// where <latex mode='inline'>x</latex> is a vector representing 
    /// a possible state of the system, 
    /// and <latex mode='inline'>\mathcal{V}</latex> is the set of allowable 
    /// values for parameter <latex mode='inline'>v</latex>. 
    /// Let <latex mode='inline'>u\in \mathcal{V}</latex> be the specific 
    /// parameter value under which the probability must be evaluated. 
    /// This is referred to as the <em>nominal parameter</em>. 
    /// </para>    
    /// <para>
    /// A given event is 
    /// eligible for being targeted by 
    /// a <see cref="RareEventProbabilityEstimator"/> 
    /// if it can be defined as 
    /// <latex mode="display">A_L\!\left(\lambda\right)=\{x\in\mathcal{X}|\lambda\leq H\!\left(x\right)\}</latex> 
    /// or 
    /// <latex mode="display">A_U\!\left(\lambda\right)=\{x\in\mathcal{X}|H\!\left(x\right)\leq\lambda\},</latex> 
    /// where <latex mode='inline'>\mathcal{X}</latex> is the state space 
    /// of the system,  
    /// <latex mode='inline'>H\!\left(x\right)</latex> is the function 
    /// returning the system performance at state  
    /// <latex mode='inline'>x</latex> and <latex mode='inline'>\lambda</latex> is 
    /// a performance value, which is referred to as 
    /// the <em>threshold level</em> of the event under study, such that the set is 
    /// rare under <latex mode='inline'>f_u</latex>. 
    /// </para>
    /// <para>
    /// The target probability can thus be represented as 
    /// <latex mode="display">{\Pr}_u[A\left(\lambda\right)]=\int I_{A\left(\lambda\right)}\!\left(x\right)f_u\!\left(x\right)dx,</latex> 
    /// where  <latex mode='inline'>A\left(\lambda\right)</latex> is 
    /// <latex mode='inline'>A_U\!\left(\lambda\right)</latex> or 
    /// <latex mode='inline'>A_L\!\left(\lambda\right)</latex>, and 
    /// <latex mode='inline'>I_{A\left(\lambda\right)}</latex> is the indicator function 
    /// of <latex mode='inline'>A\left(\lambda\right)</latex>.
    /// In principle, one can base its estimation  
    /// on a sample 
    /// <latex mode='inline'>X_{u,0},\dots,X_{u,N-1}</latex>
    /// drawn from 
    /// <latex mode='inline'>f_u</latex> by evaluating the crude Monte 
    /// Carlo estimator 
    /// <latex mode="display">\frac{1}{N}\sum_{i=0}^{N-1}I_{A\left(\lambda\right)}\!\left(X_{u,i}\right).</latex>
    /// However, if 
    /// <latex mode='inline'>A\left(\lambda\right)</latex> is rare under 
    /// <latex mode='inline'>f_u</latex>, a large number of indicator 
    /// functions will evaluate to zero, leading to inefficiencies.
    /// To overcome such difficulty, the Cross-Entropy method suggests 
    /// to exploit a likelihood ratio estimator: instead of sampling from 
    /// <latex mode='inline'>f_u</latex>, the method select another 
    /// density in <latex mode='inline'>\mathcal{P}</latex> by identifying a 
    /// parameter <latex mode='inline'>v_{*}\in \mathcal{V}</latex> 
    /// that guarantees the most accurate estimate of the target probability 
    /// for a specified simulation effort, or sample size 
    /// <latex mode='inline'>N</latex>. Given a sample 
    /// <latex mode='inline'>X_{v_{*},0},\dots,X_{v_{*},N-1}</latex>
    /// from <latex mode='inline'>f_{v_{*}}</latex>, the 
    /// probability is thus estimated as 
    /// <latex mode="display">\frac{1}{N}\sum_{i=0}^{N-1}I_{A\left(\lambda\right)}\!\left(X_{v_{*},i}\right)\mathrm{LR}_{u,v_{*}}\!\left(X_{v_{*},i}\right),</latex>
    /// where 
    /// <latex mode="display">\mathrm{LR}_{u,v_{*}}\!\left(x\right)=\frac{f_u\!\left(x\right)}{f_{v_{*}}\!\left(x\right)}</latex>
    /// is the <em>likelihood ratio</em> of 
    /// <latex mode='inline'>f_u</latex> to 
    /// <latex mode='inline'>f_{v_{*}}</latex> evaluated at 
    /// state <latex mode='inline'>x</latex>. 
    /// </para>
    /// <para>
    /// The idea behind the Cross-Entropy method is to select 
    /// <latex mode='inline'>v_{*}</latex> by taking into account 
    /// the estimator based on the ratio 
    /// <latex mode="display">\frac{f_u\!\left(x\right)}{f_{u|A\left(\lambda\right)}\!\left(x\right)},</latex>
    /// where 
    /// <latex mode="display">f_{u|A\left(\lambda\right)}\!\left(x\right)=\frac{I_{A\left(\lambda\right)}\!\left(x\right)f_u\!\left(x\right)}{{\Pr}_u[A\left(\lambda\right)]}</latex>
    /// is the density of <latex mode='inline'>X</latex> 
    /// conditional on <latex mode='inline'>A\left(\lambda\right)</latex>. 
    /// Such estimator is unusable, since it depends on the target probability.   
    /// However, it is ideally optimal because its variance is 
    /// equal to zero. 
    /// For this reason, it is proposed to select the 
    /// parameter <latex mode='inline'>v_{*}</latex> such that the 
    /// corresponding density <latex mode='inline'>f_{v_{*}}</latex> is 
    /// the closest to the optimal one, 
    /// the 
    /// difference between two densities, say 
    /// <latex mode='inline'>\phi</latex> and 
    /// <latex mode='inline'>\varphi</latex>, being measured as a 
    /// Kullback–Leibler divergence:
    /// <latex mode="display">D\left(\phi,\varphi\right)=\int \phi\left(x\right) \ln \phi\left(x\right) dx - \int \phi\left(x\right) \ln \varphi\left(x\right) dx.</latex>
    /// The selection is thus obtained by 
    /// minimizing the divergence between the generic density in 
    /// <latex mode='inline'>\mathcal{P}</latex>, say 
    /// <latex mode='inline'>f_v,\hspace{2pt}v\in\mathcal{V}</latex>, and 
    /// the ideal density <latex mode='inline'>f_{u|A\left(\lambda\right)}</latex>:
    /// <latex mode="display">\min_{v\in\mathcal{V}} D\left(f_{u|A\left(\lambda\right)},f_v\right).</latex>
    /// </para>
    /// <para>
    /// Such minimization corresponds to the maximization 
    /// <latex mode="display">\max_{v\in\mathcal{V}} \mathrm{E}_{u} [ I_{A\left(\lambda\right)}\!\left(X\right) \ln f_{v}\!\left(X\right) ],</latex>
    /// where <latex mode='inline'>\mathrm{E}_{u}</latex> is the expectation operator 
    /// under <latex mode='inline'>X\sim f_{u}</latex>, or, equivalently, to 
    /// <latex mode="display">\max_{v\in\mathcal{V}} \mathrm{E}_{w} [ I_{A\left(\lambda\right)}\!\left(X\right) \mathrm{LR}_{u,w}\!\left(X\right) \ln f_{v}\left(X\right) ]</latex>
    /// for every parameter <latex mode='inline'>w\in \mathcal{V}</latex>. 
    /// This implies that, given a 
    /// sample <latex mode='inline'>X_{w,0},\dots,X_{w,N}</latex> 
    /// from <latex mode='inline'>f_w</latex>, a solution to such 
    /// optimization can be estimated through the solution to the 
    /// program:
    /// <latex mode="display">\max_{v\in\mathcal{V}} g\left(v,w,\lambda\right),</latex>
    /// where
    /// <latex mode="display">g\left(v,w,\lambda\right)= \frac{1}{N}\sum_{i\in \mathcal{S}\left(w,\lambda\right)} \mathrm{LR}_{u,w}\!\left(X_{w,i}\right) \ln f_{v}\left(X_{w,i}\right)</latex>
    /// and 
    /// <latex mode="display">\mathcal{S}\left(w,\lambda\right)=\{i\in\{0,\dots,N-1\} | X_{w,i}\in A\left(\lambda\right)\}.</latex>
    /// In this context, <latex mode='inline'>w</latex> 
    /// is said the sampling <em>reference</em> parameter, while 
    /// the elements in set 
    /// <latex mode='inline'>\mathcal{S}\left(w,\lambda\right)</latex> 
    /// are referred to as the <em>elite</em> sample positions, and the 
    /// corresponding sample points 
    /// <latex mode='inline'>X_{w,i}</latex>, for 
    /// <latex mode='inline'>i \in \mathcal{S}\left(w,\lambda\right)</latex>, 
    /// as the <em>elite</em> sampled states. 
    /// The interest in such an approach is due to the possibility 
    /// of choosing a parameter <latex mode='inline'>w</latex> 
    /// under which the event <latex mode='inline'>A\left(\lambda\right)</latex> 
    /// is less rare under density <latex mode='inline'>f_w</latex> 
    /// than under density <latex mode='inline'>f_u</latex>. 
    /// In this way, in order to obtain a good estimation, 
    /// a smaller simulation effort would be required.
    /// </para>
    /// <para>
    /// The selection of the optimal <em>reference</em> parameter is   
    /// an iterative multi step procedure, in which, at each 
    /// iteration <latex mode='inline'>t</latex>, a new level 
    /// <latex mode='inline'>\lambda_t</latex> and a new <em>reference</em> 
    /// parameter <latex mode='inline'>w_t \in \mathcal{V}</latex> are 
    /// identified. The goal being to obtain, after a number of iterations 
    /// <latex mode='inline'>T</latex>, 
    /// a reference parameter very close to the optimal one and 
    /// set <latex mode='inline'>v_{*}=w_{T}</latex>.
    /// The algorithm has two main steps.
    /// </para>
    /// <para><em>Sampling step</em></para>
    /// <para>
    /// The first one, the <em>sampling step</em>, is responsible for generating 
    /// diverse candidate states of the system. Let 
    /// <latex mode='inline'>w_{t-1}</latex> be the <em>reference</em> parameter at 
    /// iteration <latex mode='inline'>t</latex>, 
    /// with <latex mode='inline'>w_{0}=u</latex>, and let 
    /// <latex mode='inline'>X_{t,0},\dots,X_{t,N-1}</latex> be the sample 
    /// drawn from <latex mode='inline'>f_{w_{t-1}}</latex>. 
    /// </para>
    /// <para><em>Updating step</em></para>
    /// <para>
    /// To avoid the difficulties 
    /// due to the rareness of 
    /// <latex mode='inline'>A\left(\lambda\right)</latex> under 
    /// <latex mode='inline'>u</latex>, the sample drawn in the 
    /// sampling step is not exploited 
    /// to estimate <latex mode='inline'>\Pr_u[A\left(\lambda\right)]</latex>. 
    /// Instead, an iteration specific target event is defined in terms 
    /// of a new iteration level <latex mode='inline'>\lambda_t</latex>, 
    /// which is set as follows.
    /// </para>
    /// The sample performances, 
    /// <latex mode='inline'>H\left(X_{t,0}\right),\dots,H\left(X_{t,N-1}\right)</latex>, 
    /// are computed and sorted in increasing order, say obtaining the 
    /// following ordering:
    /// <latex mode="display">
    /// h_{t,\left(0\right)}\leq\dots\leq h_{t,\left(N-1\right)}.
    /// </latex>
    /// <para>
    /// Let <latex mode='inline'>X_{t,\left(i\right)}</latex> be the sampled 
    /// state such that 
    /// <latex mode='inline'>h_{t,\left(i\right)}=H\left(X_{t,\left(i\right)}\right)</latex>, 
    /// <latex mode='inline'>i=0,\dots,N-1</latex>, and consider 
    /// a constant, referred to as the <em>rarity</em> of the program, 
    /// say <latex mode='inline'>\rho</latex>, set to a value 
    /// in the interval <latex mode='inline'>\left[.01,.1\right]</latex>. 
    /// </para>
    /// <para>
    /// If <latex mode='inline'>A\left(\lambda\right)=A_L\!\left(\lambda\right),</latex> 
    /// then the points 
    /// <latex mode='inline'>X_{t,\left(\eta_u\right)},\dots,X_{t,\left(N-1\right)}</latex> 
    /// occupying a position greater than or equal 
    /// to <latex mode='inline'>\eta_u=\lceil\left(1-\rho\right)N\rceil</latex> are 
    /// the <em>elite</em> sampled states, with 
    /// <latex mode='inline'>\lceil x \rceil</latex> being the ceiling function. 
    /// In this case, the current iteration is said to reach a performance 
    /// <em>level</em> equal to <latex mode='inline'>\lambda_t=h_{t,\left(\eta_u\right)}</latex>.
    /// </para>
    /// <para>
    /// Otherwise, if 
    /// <latex mode='inline'>A\left(\lambda\right)=A_U\!\left(\lambda\right),</latex>  
    /// then <em>elite</em> sampled states can be defined as those points 
    /// occupying a position less than or equal 
    /// to <latex mode='inline'>\eta_l=\lfloor\left(\rho\right)N\rfloor</latex>, 
    /// where <latex mode='inline'>\lfloor x \rfloor</latex> is the floor function. 
    /// Such points are thus
    /// <latex mode='inline'>X_{t,\left(0\right)},\dots,X_{t,\left(\eta_l\right)}</latex>,
    /// and the current iteration is said to reach a performance 
    /// <em>level</em> equal to <latex mode='inline'>\lambda_t=h_{t,\left(\eta_l\right)}</latex>.
    /// </para>
    /// <para>
    /// Once that the <em>elite</em> 
    /// states have been identified, the joint 
    /// distribution from which the states will be sampled in the next iteration 
    /// is updated by estimating its parameters using the <em>elite</em> states only. 
    /// </para>
    /// <para>
    /// The iteration parameter <latex mode='inline'>w_t</latex> is chosen 
    /// to be the optimal one for estimating 
    /// <latex mode='inline'>{\Pr}_{w_{t-1}}[A\left(\lambda_t\right)]}</latex> 
    /// by solving the program 
    /// <latex mode="display">w_t=\arg\max_{v\in\mathcal{V}} g\left(v,w_{t-1},\lambda_t\right).</latex>
    /// </para>
    /// <para>
    /// In this way, new <em>reference</em> parameters 
    /// are selected so that the target event 
    /// becomes more
    /// and more probable than under the <em>nominal</em> distribution of interest. 
    /// As discussed in the remarks about the <see cref="CrossEntropyProgram"/> 
    /// class, these operations correspond to the <em>updating step</em> of a 
    /// Cross-Entropy program. 
    /// </para>
    /// <para>
    /// The algorithm stops iterating when the iteration <em>level</em> 
    /// reaches the event <em>level</em>, i.e. when 
    /// <latex mode='inline'>\lambda_t\geq\lambda</latex> if 
    /// <latex mode='inline'>A\left(\lambda\right)=A_L\!\left(\lambda\right)</latex>; 
    /// otherwise, when <latex mode='inline'>\lambda_t\leq\lambda</latex>.
    /// </para>
    /// <para><b>Executing a Cross-Entropy estimator</b></para>
    /// <para id='Run'>
    /// Class <see cref="RareEventProbabilityEstimator"/> follows 
    /// the <em>Template Method</em> pattern<cite>gamma-etal-1995</cite> by 
    /// defining in an 
    /// operation the structure of a Cross-Entropy algorithm aimed to  
    /// estimate the probability of a rare event. 
    /// <see cref="Estimate">Estimate</see> is 
    /// the template method, in which the invariant parts of a Cross-Entropy 
    /// estimator are implemented once, 
    /// deferring the implementation of behaviors that can vary to 
    /// a <see cref="RareEventProbabilityEstimationContext"/> instance,
    /// that method <see cref="Estimate">Estimate</see> receives as 
    /// a parameter.
    /// </para>
    /// <para>
    /// <see cref="RareEventProbabilityEstimationContext"/> inherits 
    /// or defines abstract or virtual methods 
    /// which represent some <em>primitive operations</em> 
    /// of the template method, i.e. those varying steps that its 
    /// subclasses will 
    /// override to define the concrete 
    /// behavior of the algorithm. 
    /// See the documentation of class 
    /// <see cref="RareEventProbabilityEstimationContext"/> for
    /// examples of how to implement a context for rare event
    /// simulations.
    /// </para>
    /// </remarks>
    /// <seealso cref="CrossEntropyProgram" />
    public sealed class RareEventProbabilityEstimator : CrossEntropyProgram
    {
        /// <summary>
        /// Runs this Cross-Entropy program designed to estimate 
        /// the probability of the specified rare event context.
        /// </summary>
        /// <param name="context">The context in which the rare event 
        /// probability must be estimated.</param>
        /// <param name="rarity">The rarity applied by the Cross-Entropy method.</param>
        /// <param name="sampleSize">The size of the samples drawn during the
        /// sampling step of the Cross-Entropy method.</param>
        /// <param name="estimationSampleSize">The size of the sample drawn to 
        /// estimate the rare event probability.</param>
        /// <returns>The results of the Cross-Entropy estimator.</returns>
        /// <remarks>
        /// <para>
        /// For a thorough description of the method and an example 
        /// of how to use it, 
        /// see the remarks 
        /// about the
        /// <see cref="RareEventProbabilityEstimationContext"/> class.
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
        /// <paramref name="rarity"/> is not less than 1.<br/>
        /// -or-<br/>
        /// <paramref name="estimationSampleSize"/> is not positive.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", 
            "CA1062:Validate arguments of public methods", 
            Justification = "Input validation delegated to CrossEntropyProgram.Run.")]
        public RareEventProbabilityEstimationResults Estimate(
            RareEventProbabilityEstimationContext context,
            double rarity,
            int sampleSize,
            int estimationSampleSize)
        {
            if (estimationSampleSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(estimationSampleSize),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var baseResults = base.Run(
                context,
                sampleSize,
                rarity);

            var results = new RareEventProbabilityEstimationResults
            {
                Levels = baseResults.Levels,
                Parameters = baseResults.Parameters,
                HasConverged = true
            };

            DoubleMatrix optimalParameter = results.Parameters.Last.Value;
            DoubleMatrix finalSample = this.Sample(
                context, estimationSampleSize, optimalParameter);

            var finalPerformances = this.EvaluatePerformances(context, finalSample);

            Predicate<double> match;
            if (context.EliteSampleDefinition == EliteSampleDefinition.LowerThanLevel)
            {
                match = (p) => { return p <= context.ThresholdLevel; };
            }
            else
            {
                match = (p) => { return p >= context.ThresholdLevel; };
            }

            var excessIndexes = finalPerformances.FindWhile(match);

            var ratios = DoubleMatrix.Dense(1, estimationSampleSize);

            var nominalParameter = context.InitialParameter;

            for (int i = 0; i < estimationSampleSize; i++)
            {
                var samplePoint = finalSample[i, ":"];
                ratios[i] = context.GetLikelihoodRatio(
                    samplePoint,
                    nominalParameter,
                    optimalParameter);
            }

            double rareEventProbability =
                Stat.Sum(ratios.Vec(excessIndexes)) / Convert.ToDouble(estimationSampleSize);

            results.RareEventProbability = rareEventProbability;

            return results;
        }
    }
}