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
    /// estimation of a rare event probability 
    /// is obtained by exploiting 
    /// a <see cref="RareEventProbabilityEstimator"/> instance.
    /// </summary>
    /// <remarks>
    /// <para id='Estimate.2'>
    /// A <see cref="RareEventProbabilityEstimator"/> is executed by calling its 
    /// <see cref="RareEventProbabilityEstimator.Estimate(
    /// RareEventProbabilityEstimationContext, double, int, int)">Estimate</see> 
    /// method. This is a 
    /// template method, in the sense that it defines the 
    /// invariant parts of a Cross-Entropy program for rare event simulation.
    /// Such method relies on an instance of 
    /// class <see cref="RareEventProbabilityEstimationContext"/>, which is 
    /// passed as a parameter to it and specifies the 
    /// <em>primitive operations</em> 
    /// of the template method, i.e. 
    /// those varying steps of the algorithm that depends 
    /// on the problem under study.
    /// </para>
    /// <para id='Estimate.3'>
    /// Class <see cref="RareEventProbabilityEstimationContext"/> thoroughly 
    /// defines the rare event of interest, and supplies the 
    /// primitive operations as abstract methods, that its 
    /// subclasses will override to provide the concrete 
    /// behavior of the estimator.
    /// </para>
    /// <para>
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
    /// </para>    
    /// <para>
    /// Let <latex mode='inline'>\lambda</latex> be
    /// an extreme performance value, referred to as the 
    /// <i>threshold level</i>, and consider  
    /// the event <latex mode='inline'>E\left(\lambda\right)</latex> defined as 
    /// <latex mode="display">
    /// E_L\!\left(\lambda\right)=\{x\in\mathcal{X}|\lambda\leq H\!\left(x\right)\}</latex> 
    /// or 
    /// <latex mode="display">
    /// E_U\!\left(\lambda\right)=\{x\in\mathcal{X}|H\!\left(x\right)\leq\lambda\},</latex> 
    /// where <latex mode='inline'>\mathcal{X}</latex> is the state space 
    /// of the system and 
    /// <latex mode='inline'>H\!\left(x\right)</latex> is the function 
    /// returning the system performance at state  
    /// <latex mode='inline'>x</latex>. Let us assume that 
    /// the probability of an event 
    /// having form <latex>E_L</latex> or <latex>E_U</latex> must 
    /// be evaluated under the specific parameter 
    /// value <latex mode='inline'>u\in \mathcal{V}</latex>, 
    /// the so called <i>nominal parameter</i>: such 
    /// probabilities can be evaluated by a Cross-Entropy 
    /// estimator.
    /// </para>
    /// <para>
    /// At instantiation, the constructor of 
    /// a <see cref="RareEventProbabilityEstimationContext"/> object
    /// will receive information about the rare event under study by
    /// means of parameters representing <latex>\lambda</latex>,
    /// <latex>u</latex>, and a constant stating if the event has 
    /// the form <latex>E_L</latex> or <latex>E_U</latex>.
    /// </para>
    /// <para>
    /// After construction, property 
    /// <see cref="ThresholdLevel"/> represents 
    /// <latex mode='inline'>\lambda</latex>, while <latex>u</latex>
    /// can be inspected via property <see cref="CrossEntropyContext.InitialParameter"/>. Lastly, 
    /// property 
    /// <see cref="RareEventPerformanceBoundedness"/> 
    /// signals that the 
    /// event has the form <latex mode='inline'>E_L\!\left(\lambda\right)</latex> if it 
    /// evaluates to the constant <see cref="RareEventPerformanceBoundedness.Lower"/>, or 
    /// that it has the form <latex mode='inline'>E_U\!\left(\lambda\right)</latex> 
    /// if it evaluates to
    /// the constant <see cref="RareEventPerformanceBoundedness.Upper"/>.
    /// </para>
    /// <para><b>Implementing a context for rare event simulation</b></para>
    /// <para>
    /// The Cross-Entropy method 
    /// provides an iterative multi step procedure, where, at each 
    /// iteration <latex mode='inline'>t</latex>, the <i>sampling step</i>
    /// is executed in order to generate diverse candidate states of 
    /// the system, sampled from a distribution 
    /// characterized by the <i>reference parameter</i> of the iteration,
    /// say <latex>w_{t-1} \in \mathcal{V}</latex>. 
    /// Such sample is thus exploited in the <i>updating step</i>,
    /// in which a new level 
    /// <latex mode='inline'>\lambda_t</latex> and a new <em>reference</em> 
    /// parameter <latex mode='inline'>w_t \in \mathcal{V}</latex> are 
    /// identified to modify the distribution from which the samples 
    /// will be obtained in the next iteration, in order to improve 
    /// the probability of sampling relevant states, i.e. those 
    /// states corresponding to the performance excesses of interest
    /// (See the documentation of class <see cref="CrossEntropyProgram"/> for a 
    /// thorough discussion of the Cross-Entropy method). 
    /// </para>
    /// <para>
    /// When the Cross-Entropy method is applied in an rare event 
    /// context, the <i>estimating step</i> is executed, in which the rare event 
    /// probability is effectively estimated.
    /// </para>
    /// <para>These steps must be implemented as follows.</para>
    /// <para><b><i>Sampling step</i></b></para>
    /// <para id='Sample.1'>
    /// Since class <see cref="RareEventProbabilityEstimationContext"/> derives 
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
    /// Class <see cref="RareEventProbabilityEstimationContext"/> overrides 
    /// for you the methods 
    /// required for ordering the system performances of the states sampled 
    /// in the previous step, and for updating the 
    /// iteration levels.
    /// However, to complete the implementation of the <i>updating step</i>, 
    /// function <latex mode='inline'>H</latex> must be defined via 
    /// method <see cref="CrossEntropyContext.Performance(DoubleMatrix)"/>,
    /// and method <see cref="CrossEntropyContext.UpdateParameter(
    /// LinkedList{DoubleMatrix}, DoubleMatrix)"/> 
    /// also needs to be overridden. 
    /// Given <latex mode='inline'>\lambda_t</latex> and 
    /// <latex mode='inline'>w_{t-1}</latex>, the method is expected to return 
    /// the solution to the following program:
    /// <latex mode="display">
    /// \max_{v\in\mathcal{V}} \frac{1}{N}\sum_{i\in \mathcal{S}\left(w_{t-1},\lambda_t\right)} 
    /// \ln f_{v}\left(X_{t,i}\right),
    /// </latex>
    /// where 
    /// <latex mode='display'>
    /// \mathrm{LR}_{u,w_{t-1}}\round{x} = \frac{f_u\round{x}}{f_{w_{t-1}}\round{x}}
    /// </latex>
    /// is the Likelihood ratio of <latex>f_u</latex> to <latex>f_{w_{t-1}}</latex>
    /// evaluated at <latex>x</latex>,
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
    /// <para><b><i>Estimating step</i></b></para>
    /// <para>
    /// The estimating step is executed after that the underlying 
    /// Cross-Entropy program 
    /// has converged. Given the optimal 
    /// parameter <latex mode='inline'>w_{T}</latex>, 
    /// a Likelihood estimator based on the ratio 
    /// <latex mode='inline'>\mathrm{LR}_{u,w_{T}}</latex> is exploited by 
    /// drawing a sample from density 
    /// <latex mode='inline'>f_{w_{T}}</latex>.  
    /// Method 
    /// <see cref="GetLikelihoodRatio(
    /// DoubleMatrix, DoubleMatrix, DoubleMatrix)"/> 
    /// must be overridden in order to evaluate the ratio 
    /// at a specific sample point 
    /// for a given pair of <i>nominal</i> and <i>reference</i> parameters.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// The following example is based on the Rare-Event Simulation Example 
    /// proposed by Rubinstein and Kroese 
    /// (Section 2.2.1)<cite>rubinstein-kroese-2004</cite>.
    /// </para>
    /// <para>
    /// A Cross-Entropy estimator is exploited 
    /// to analyze, under extreme conditions, the length 
    /// <latex mode='inline'>l</latex> of the shortest path from 
    /// node <latex mode='inline'>\alpha</latex> to 
    /// node <latex mode='inline'>\omega</latex> in the undirected graph 
    /// depicted in the following figure.
    ///</para>
    /// <para>
    /// <image>
    ///   <width>100%</width>
    ///   <alt>Shortest path from A to B</alt>
    ///   <src>RareEventExample.png</src>
    /// </image>
    /// </para>
    /// <para>
    /// There are 5 edges in the graph. Their lengths 
    /// are 
    /// represented as independent and exponentially distributed random 
    /// variables <latex mode='inline'>X_0,\dots,X_4</latex>, with 
    /// variable <latex mode='inline'>X_i</latex> 
    /// having a specific mean <latex mode='inline'>u_i</latex>, 
    /// for <latex mode='inline'>i=0,\dots,4</latex>, forming 
    /// the following <em>nominal</em> parameter:  
    /// <latex mode="display">u\equiv\left(u_0,\dots,u_4\right)=
    /// \left(0.25,0.4,0.1,0.3,0.2\right).</latex> 
    /// </para>
    /// <para>
    /// In order to represent a possible state 
    /// <latex mode='inline'>x</latex> of such system, we hence need a 
    /// vector of length 5:
    /// <latex mode="display">
    /// x=\left(x_0,\dots,x_4\right),
    /// </latex>    
    /// and the performance of the system is the total length 
    /// <latex mode='inline'>l</latex> of the 
    /// shortest path given <latex mode='inline'>x</latex>, i.e. 
    /// <latex mode="display">H\left(x_0,\dots,x_4\right)=
    /// \min\{x_0+x_3,x_0+x_2+x_4,x_1+x_4,x_1+x_2+x_3\}.</latex>
    /// </para>
    /// <para>
    /// The likelihood ratio of 
    /// <latex mode='inline'>f_u</latex> to the generic density 
    /// <latex mode='inline'>f_w</latex> is 
    /// <latex mode="display">
    /// \mathrm{LR}_{u,w}\left(x_0,\dots,x_4\right)=\exp\left(-\sum_{j=0}^{4}
    /// x_j\left(\frac{1}{u_j}-\frac{1}{w_j}\right)\right)\prod_{j=0}^{4}\frac{w_j}{u_j}.
    /// </latex>
    /// </para>
    /// <para>
    /// At iteration <latex mode='inline'>t</latex>, the parameter updating formula is, 
    /// for <latex mode='inline'>j=0,\dots,4</latex>,
    /// <latex mode="display">
    /// w_{t,j} = \frac{\sum_{i=0}^{N-1}I_{A_t}\!\left(X_{t,i}\right)
    /// \mathrm{LR}_{u,w_{t-1}}\!\left(X_{t,i}\right)X_{t,i,j}}
    /// {\sum_{i=0}^{N-1}I_{A_t}\left(X_{t,i}\right)
    /// \mathrm{LR}_{u,w_{t-1}}\left(X_{t,i}\right)},
    /// </latex>
    /// where <latex mode='inline'>A_t \equiv A_{L}\!\left(\lambda_t\right)</latex>.
    /// </para>
    /// <para>
    /// In this example, we want to estimate the probability of observing 
    /// a shortest path greater than or equal to a <em>level</em> 
    /// set as <latex mode='inline'>\lambda=2.0</latex>. 
    /// This is equivalent to define the target event as 
    /// <latex mode='inline'>A_{L}\left(2.0\right)=\{x\in\mathcal{X}|H\left(x\right)\geq 2.0\}</latex>. 
    /// </para>
    /// <para>
    /// <code title="Estimating the probability of an extreme shortest path in a network.
    /// "
    /// source="..\Novacta.Analytics.CodeExamples\Advanced\CrossEntropyEstimatorExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    ///</example>
    ///<seealso cref="CrossEntropyContext"/>
    public abstract class RareEventProbabilityEstimationContext
        : CrossEntropyContext
    {
        #region State

        /// <summary>
        /// Gets a constant specifying if the rare event is a 
        /// set of states whose performances are lower or upper bounded 
        /// by the <see cref="ThresholdLevel"/> of this instance.
        /// </summary>
        /// <value>
        /// <see cref="RareEventPerformanceBoundedness.Lower"/> if the 
        /// performances of interest are lower bounded; otherwise,
        /// <see cref="RareEventPerformanceBoundedness.Upper"/>.  
        /// </value>
        public RareEventPerformanceBoundedness RareEventPerformanceBoundedness { get; }

        /// <summary>
        /// Get the threshold level applied to bound the 
        /// performances characterizing the rare event
        /// taken into account by this context.
        /// </summary>
        /// <value>
        /// The threshold level applied to bound the 
        /// state performances under study.
        /// </value>
        public double ThresholdLevel { get; }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="RareEventProbabilityEstimationContext" /> class 
        /// having the specified state dimension, threshold level,
        /// and boundedness.
        /// </summary>
        /// <param name="thresholdLevel">
        /// The performance level under or over which a state 
        /// is considered as included in the rare event under study.
        /// </param>
        /// <param name="stateDimension">
        /// The dimension of a vector representing the state
        /// of the system in which the rare event can happen.
        /// </param>
        /// <param name="rareEventPerformanceBoundedness">
        /// A constant to specify if the rare event is a set 
        /// of states whose 
        /// performances are lower or upper bounded 
        /// by the <paramref name="thresholdLevel"/>. 
        /// </param>
        /// <param name="initialParameter">
        /// The nominal parameter under which 
        /// the event probability must be evaluated.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initialParameter"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="rareEventPerformanceBoundedness"/> is not a field of
        /// <see cref="Advanced.RareEventPerformanceBoundedness"/>.
        /// </exception>
        protected RareEventProbabilityEstimationContext(
            int stateDimension,
            DoubleMatrix initialParameter,
            double thresholdLevel,
            RareEventPerformanceBoundedness rareEventPerformanceBoundedness
            )
            : base(stateDimension, initialParameter)
        {
            if ((rareEventPerformanceBoundedness != RareEventPerformanceBoundedness.Upper)
                &&
                (rareEventPerformanceBoundedness != RareEventPerformanceBoundedness.Lower)
               )
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_NOT_FIELD_OF_RARE_EVENT_PERFORMANCE_BOUNDEDNESS"),
                    nameof(rareEventPerformanceBoundedness));
            }

            this.RareEventPerformanceBoundedness = rareEventPerformanceBoundedness;
            this.ThresholdLevel = thresholdLevel;
        }

        #endregion

        #region CrossEntropyContext

        ///<inheritdoc/>
        protected internal override EliteSampleDefinition EliteSampleDefinition
        {
            get
            {
                if (this.RareEventPerformanceBoundedness
                    ==
                    RareEventPerformanceBoundedness.Upper)
                    return EliteSampleDefinition.LowerThanLevel;
                else
                    return EliteSampleDefinition.HigherThanLevel;
            }
        }

        /// <inheritdoc/>
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

            double lastLevel = levels.Last.Value;

            bool stopExecution;

            if (this.EliteSampleDefinition == EliteSampleDefinition.HigherThanLevel)
            {
                stopExecution = lastLevel >= this.ThresholdLevel;
            }
            else
            {
                stopExecution = lastLevel <= this.ThresholdLevel;
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

            double thresholdLevel = this.ThresholdLevel;

            // Compute the relevant sample percentile (the level) 
            // and achieved performance
            switch (eliteSampleDefinition)
            {
                case EliteSampleDefinition.HigherThanLevel:
                    eliteFirstIndex = Convert.ToInt32(
                        Math.Ceiling(sampleSize * (1 - rarity)));
                    eliteLastIndex = sampleSize - 1;

                    level = performanceArray[eliteFirstIndex];
                    if (level > thresholdLevel)
                    {
                        level = thresholdLevel;
                    }
                    break;
                case EliteSampleDefinition.LowerThanLevel:
                    eliteFirstIndex = 0;
                    eliteLastIndex = Convert.ToInt32(
                        Math.Ceiling(sampleSize * rarity));

                    level = performanceArray[eliteLastIndex];
                    if (level < thresholdLevel)
                    {
                        level = thresholdLevel;
                    }
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
        /// Gets the likelihood ratio of a nominal density to 
        /// a given reference one, 
        /// evaluated at the specified sample point.
        /// </summary>
        /// <param name="samplePoint">The sample point.</param>
        /// <param name="nominalParameter">The nominal parameter.</param>
        /// <param name="referenceParameter">The reference parameter.</param>
        /// <returns>The Likelihood ratio.</returns>
        /// <remarks>
        /// <inheritdoc cref="RareEventProbabilityEstimator" 
        /// path="para[@id='Estimator.1']"/>
        /// <para>
        /// Method <see cref="GetLikelihoodRatio(DoubleMatrix, 
        /// DoubleMatrix, DoubleMatrix)">GetLikelihoodRatio</see>
        /// is intended to return the value
        /// <latex mode="display">\mathrm{LR}_{u,w}\!\left(x\right)=
        /// \frac{f_u\!\left(x\right)}{f_{w}\!\left(x\right)}</latex>
        /// i.e., the <em>likelihood ratio</em> of 
        /// <latex mode='inline'>f_u</latex> to 
        /// <latex mode='inline'>f_{v_{*}}</latex> evaluated at 
        /// state <latex mode='inline'>x</latex>, where <latex>u</latex> 
        /// is the <paramref name="nominalParameter"/> while <latex>w</latex>
        /// is the <paramref name="referenceParameter"/>.
        /// </para>
        /// </remarks>
        protected internal abstract double GetLikelihoodRatio(
            DoubleMatrix samplePoint,
            DoubleMatrix nominalParameter,
            DoubleMatrix referenceParameter);
    }
}
