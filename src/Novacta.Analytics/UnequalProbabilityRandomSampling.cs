// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to draw samples from a finite population
    /// whose units have unequal probabilities
    /// of being inserted in a sample.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The current implementation of 
    /// the <see cref="UnequalProbabilityRandomSampling"/> class
    /// is based on the sampling scheme proposed by Chen et al.
    /// (Procedure 1)<cite>chen-etal-1994</cite>.
    /// </para>
    /// <para><b>Instantiation</b></para>
    /// <para id='fromInclusion'>
    /// Method <see cref="FromInclusionProbabilities(DoubleMatrix)"/>
    /// creates <see cref="UnequalProbabilityRandomSampling"/> instances 
    /// by specifying, for each population unit, its probability
    /// of being included in a sample. In this case, the sample size
    /// is defined as the sum of such probabilities.
    /// </para>
    /// <para id='fromBernoulli'>
    /// Method <see cref="FromBernoulliProbabilities(DoubleMatrix, int)"/>
    /// creates instances by assigning to each unit
    /// an independent Bernoulli 
    /// random variable and sampling from the distribution of the sum 
    /// of the corresponding Bernoulli trials conditional to having 
    /// exactly <see cref="SampleSize"/> successes.
    /// </para>
    /// </remarks>
    /// <seealso cref="RandomSampling" />
    public class UnequalProbabilityRandomSampling : RandomSampling
    {
        #region State

        /// <summary>
        /// The inclusion probabilities.
        /// </summary>
        private readonly DoubleMatrix inclusionProbabilities;

        /// <summary>
        /// The weights corresponding to the inclusion probabilities.
        /// </summary>
        private readonly DoubleMatrix weigths;


        /// <summary>
        /// The inclusion probabilities divided by the sample size.
        /// </summary>
        private readonly SortedList<int, double> p1_S;

        /// <summary>
        /// The population size.
        /// </summary>
        private readonly int populationSize;

        /// <summary>
        /// The sample size.
        /// </summary>
        private readonly int sampleSize;

        /// <inheritdoc/>
        public override int PopulationSize
        {
            get
            {
                return this.populationSize;
            }
        }

        /// <inheritdoc/>
        public override int SampleSize
        {
            get
            {
                return this.sampleSize;
            }
        }

        /// <inheritdoc/>
        public override ReadOnlyDoubleMatrix InclusionProbabilities
        {
            get { return this.inclusionProbabilities.AsReadOnly(); }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="UnequalProbabilityRandomSampling" /> class
        /// to draw samples from a finite population where each
        /// unit has the specified inclusion probability and weight.
        /// </summary>
        /// <param name="inclusionProbabilities">The inclusion
        /// probabilities of the population units.</param>
        /// <param name="weights">The weights of the population
        /// units.</param>
        /// <param name="sampleSize">The size of the sample.</param>
        private UnequalProbabilityRandomSampling(
            DoubleMatrix inclusionProbabilities,
            DoubleMatrix weights,
            int sampleSize)
        {
            int N = inclusionProbabilities.Count;
            this.inclusionProbabilities = inclusionProbabilities;
            this.p1_S = new SortedList<int, double>(N);
            for (int i = 0; i < N; i++)
            {
                this.p1_S.Add(
                    key: i,
                    value: inclusionProbabilities[i] / sampleSize);
            }
            this.weigths = weights;
            this.sampleSize = sampleSize;
            this.populationSize = weights.Count;
        }

        /// <summary>
        /// Computes the R function at the specified index, collection, 
        /// and weights.
        /// </summary>
        /// <param name="k">The index.</param>
        /// <param name="c">The collection.</param>
        /// <param name="w">The weights.</param>
        /// <returns>
        /// The value of the R function at the specified arguments.</returns>
        /// <remarks>
        /// <para>
        /// This method computes the <c>R</c> function 
        /// introduced in Theorem 3 of Chen et al. (1994)
        /// <cite>chen-etal-1994</cite>.
        /// The collection <paramref name="c"/> is a subset of 
        /// S={0,...,N-1}, where N is the count of <paramref name="w"/>.
        /// </para>
        /// </remarks>
        /// <return>
        /// A matrix whose (i, 0) entry contains R(i, c), for i = 0,...,k,
        /// while entry (i, j + 1) is R(i, c\{c[j]}) for j = 0,...,|c| - 1.
        /// </return>
        private static DoubleMatrix R(
            int k,
            IList<int> c,
            DoubleMatrix w)
        {
            int m = c.Count;

            // t[i, j] = T(i+1, c\{c[j - 1]}) for j = 1,...,|c|.
            // t[i, 0] = T(i+1, c).
            DoubleMatrix t = DoubleMatrix.Dense(k, m + 1);

            // r[i, j + 1] = R(i, c\{c[j]}) for j = 0,...,|c| - 1.
            // r[i, 0] = R(i, c).
            var r = DoubleMatrix.Dense(k + 1, m + 1);

            for (int i = 0; i < k; i++)
            {
                int i_plus_1 = i + 1;
                for (int j = 0; j < m; j++)
                {
                    t[i, 0] += Math.Pow(w[c[j]], i_plus_1);
                    r[0, j] = 1.0;
                }
                double t_i_0 = t[i, 0];
                for (int j = 1; j < m + 1; j++)
                {
                    t[i, j] = t_i_0 - Math.Pow(w[c[j - 1]], i_plus_1);
                }
            }
            r[0, m] = 1.0;

            for (int j = 0; j < m + 1; j++)
            {
                for (int index = 1; index < k + 1; index++)
                {
                    bool negate = false;
                    for (int i = 1; i < index + 1; i++, negate = !negate)
                    {
                        double value = t[i - 1, j] * r[index - i, j] / index;

                        r[index, j] += negate ? -value : value;
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// Computes the R function at the specified index and weights, 
        /// while the collection matches the entire population.
        /// </summary>
        /// <param name="k">The index.</param>
        /// <param name="w">The weights.</param>
        /// <returns>
        /// The value of the R function at the specified arguments.</returns>
        /// <remarks>
        /// <para>
        /// This method specializes the <c>R</c> function 
        /// introduced in Theorem 3 of Chen et al. (1994)
        /// <cite>chen-etal-1994</cite> to the case in which
        /// it must be computed at the population S={0,...,N-1},
        /// where <c>N</c> is the <see cref="DoubleMatrix.Count"/>
        /// of <see cref="weigths"/>.
        /// </para>
        /// </remarks>
        /// <return>
        /// A matrix whose (i,0) entry contains R(i, S), for i = 0,...,k,
        /// while entry (i,j) is R(i, S\{j - 1}) for j = 1,...,N.
        /// </return>
        private static DoubleMatrix R(
            int k,
            DoubleMatrix w)
        {
            int N = w.Count;

            // t[i, j] = T(i+1, S\{j - 1}) for j = 1,...,N.
            // t[i, 0] = T(i+1, S).
            DoubleMatrix t = DoubleMatrix.Dense(k, N + 1);

            // r[i, j] = R(i, S\{j - 1}) for j = 1,...,N.
            // r[i, 0] = R(i, S).
            var r = DoubleMatrix.Dense(k + 1, N + 1);

            for (int i = 0; i < k; i++)
            {
                int i_plus_1 = i + 1;
                for (int j = 0; j < N; j++)
                {
                    t[i, 0] += Math.Pow(w[j], i_plus_1);
                    r[0, j] = 1.0;
                }
                double t_i_0 = t[i, 0];
                for (int j = 1; j < N + 1; j++)
                {
                    t[i, j] = t_i_0 - Math.Pow(w[j - 1], i_plus_1);
                }
            }
            r[0, N] = 1.0;

            for (int j = 0; j < N + 1; j++)
            {
                for (int index = 1; index < k + 1; index++)
                {
                    bool negate = false;
                    for (int i = 1; i < index + 1; i++, negate = !negate)
                    {
                        double value = t[i - 1, j] * r[index - i, j] / index;

                        r[index, j] += negate ? -value : value;
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="UnequalProbabilityRandomSampling"/> class
        /// by specifying the Bernoulli probabilities assigned to
        /// the population units and the sample size.
        /// </summary>
        /// <param name="bernoulliProbabilities">
        /// The Bernoulli probabilities assigned to the population units.</param>
        /// <param name="sampleSize">The size of the samples to draw.</param>
        /// <returns>The <see cref="UnequalProbabilityRandomSampling"/> instance
        /// having the specified characteristics.</returns>
        /// <remarks>
        /// <inheritdoc cref="UnequalProbabilityRandomSampling" 
        /// path="para[@id='fromBernoulli']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="bernoulliProbabilities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bernoulliProbabilities"/> has 
        /// <see cref="DoubleMatrix.Count"/> equal to <c>1</c>.<br/>
        /// -or-<br/>
        /// <paramref name="bernoulliProbabilities"/> contains at 
        /// least an entry 
        /// not belonging to the open interval <c>]0, 1[</c>. 
        /// -or-<br/> 
        /// <paramref name="sampleSize"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="sampleSize"/> is not less than 
        /// the <see cref="DoubleMatrix.Count"/> of
        /// <paramref name="bernoulliProbabilities"/>.
        /// </exception>
        public static UnequalProbabilityRandomSampling
            FromBernoulliProbabilities(
                DoubleMatrix bernoulliProbabilities,
                int sampleSize)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(bernoulliProbabilities);

            int populationSize = bernoulliProbabilities.Count;

            if (populationSize <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bernoulliProbabilities),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                        "1"));
            }

            var found = bernoulliProbabilities.FindWhile(pr => pr <= 0.0 || pr >= 1.0);
            if (null != found)
            {
                throw new ArgumentOutOfRangeException(nameof(bernoulliProbabilities),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1"));
            }

            if (sampleSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleSize),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (populationSize <= sampleSize)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_VALUE"),
                        "the count of parameter bernoulliProbabilities"),
                    nameof(sampleSize));
            }

            #endregion

            int n = sampleSize;
            int N = populationSize;
            int N_minus_1 = N - 1;
            int n_minus_1 = n - 1;
            var weights = DoubleMatrix.Dense(N, 1);
            var weightsArray = weights.GetStorage();
            for (int j = 0; j < weightsArray.Length; j++)
            {
                double p_j = bernoulliProbabilities[j];
                weightsArray[j] = p_j / (1.0 - p_j);
            }

            var inclusionProbabilities = DoubleMatrix.Dense(N, 1);

            // S = {0,...,N-1}.
            // r[i, j] = R(i, S\{j - 1}) for j = 1,...,N.
            // r[i, 0] = R(i, S).
            var r = R(n, weights);
            var R_n_S = r[n, 0];// R(n, weights);

            for (int j = 0; j < N; j++)
            {
                inclusionProbabilities[j] = weightsArray[j] * r[n_minus_1, j + 1] / R_n_S;
            }

            return new UnequalProbabilityRandomSampling(
                inclusionProbabilities,
                weights,
                sampleSize);
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="UnequalProbabilityRandomSampling"/> class
        /// by specifying the inclusion probabilities assigned to
        /// the population units.
        /// </summary>
        /// <param name="inclusionProbabilities">
        /// The inclusion probabilities assigned to the population units.</param>
        /// <returns>The <see cref="UnequalProbabilityRandomSampling"/> instance
        /// having the specified characteristics.</returns>
        /// <remarks>
        /// <inheritdoc cref="UnequalProbabilityRandomSampling" 
        /// path="para[@id='fromInclusion']"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="inclusionProbabilities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="inclusionProbabilities"/> has 
        /// <see cref="DoubleMatrix.Count"/> equal to <c>1</c>.<br/>
        /// -or-<br/>
        /// <paramref name="inclusionProbabilities"/> contains entries
        /// whose sum differs from an integer more than .001 in
        /// absolute value.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="inclusionProbabilities"/> contains at 
        /// least an entry 
        /// not belonging to the open interval <c>]0, 1[</c>.
        /// </exception>        
        public static UnequalProbabilityRandomSampling FromInclusionProbabilities(
            DoubleMatrix inclusionProbabilities)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(inclusionProbabilities);

            int populationSize = inclusionProbabilities.Count;

            if (populationSize <= 1)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"), "1"),
                    nameof(inclusionProbabilities));
            }

            var found = inclusionProbabilities.FindWhile(p => p <= 0.0 || p >= 1.0);
            if (null != found)
            {
                throw new ArgumentOutOfRangeException(nameof(inclusionProbabilities),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_OPEN_INTERVAL"),
                        "0", "1"));
            }

            double sum = Stat.Sum(inclusionProbabilities);
            double roundedSum = Math.Round(sum);
            if (Math.Abs(roundedSum - sum) > .001)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_INTEGER"),
                    nameof(inclusionProbabilities));
            }

            int sampleSize = Convert.ToInt32(roundedSum);

            #endregion

            double delta = 1e-6;
            var weights = GetWeights(
                inclusionProbabilities,
                sampleSize,
                populationSize,
                delta);

            return new UnequalProbabilityRandomSampling(
                inclusionProbabilities,
                weights,
                sampleSize);
        }

        /// <summary>
        /// Gets the weights corresponding to the specified inclusion
        /// probabilities.
        /// </summary>
        /// <param name="inclusionProbabilities">
        /// The inclusion probabilities.</param>
        /// <param name="sampleSize">The size of the sample.</param>
        /// <param name="populationSize">The size of the population.</param>
        /// <param name="delta">The delta set for convergence.</param>
        /// <returns>The weights of the population units.</returns>
        /// <remarks><para>
        /// This method applies Theorem 2 of Chen et al. (1994)
        /// <cite>chen-etal-1994</cite>.
        /// </para>
        /// <para>
        /// Convergence is reached if
        /// <latex mode="display">
        /// \delta&gt;\max_{i\in \{N-1\}^{c}} \abs{w_i^{(t+1)}-w_i^{(t)}},
        /// </latex>
        /// where <latex mode="inline">\delta</latex> is
        /// <paramref name="delta" /> and
        /// <latex mode="inline">N</latex> is the <see cref="PopulationSize" />.
        /// </para></remarks>
        private static DoubleMatrix GetWeights(
            DoubleMatrix inclusionProbabilities,
            int sampleSize,
            int populationSize,
            double delta)
        {
            var sortedInfo = Stat.SortIndex(
                inclusionProbabilities,
                SortDirection.Ascending);

            var sortedIndexes = sortedInfo.SortedIndexes;
            var sortedPi = sortedInfo.SortedData;
            bool continueExecution = true;
            var currentWeights = sortedPi;
            while (continueExecution)
            {
                currentWeights = GetNextWeights(
                    currentWeights,
                    sortedPi,
                    sampleSize,
                    populationSize,
                    delta,
                    ref continueExecution);
            }

            return currentWeights.Vec(sortedIndexes);
        }

        /// <summary>
        /// Gets the next weights given the current ones.
        /// </summary>
        /// <param name="currentWeights">The current weights.</param>
        /// <param name="inclusionProbabilities">
        /// The inclusion probabilities.</param>
        /// <param name="sampleSize">The size of the sample.</param>
        /// <param name="populationSize">The size of the population.</param>
        /// <param name="delta">The delta set for convergence.</param>
        /// <param name="continueExecution">if set to <c>true</c>,
        /// the algorithm continues its execution searching for
        /// converging weights.</param>
        /// <returns>The next weights.</returns>
        /// <remarks><para>
        /// This method implements equation (7) of Chen et al. (1994)
        /// <cite>chen-etal-1994</cite>.
        /// </para>
        /// <para>
        /// Convergence is reached if
        /// <latex mode="display">
        /// \delta&gt;\max_{i\in \{N-1\}^{c}} \abs{w_i^{(t+1)}-w_i^{(t)}},
        /// </latex>
        /// where <latex mode="inline">\delta</latex> is 
        /// <paramref name="delta" /> and
        /// <latex mode="inline">N</latex> is the <see cref="PopulationSize" />.
        /// </para></remarks>
        private static DoubleMatrix GetNextWeights(
            DoubleMatrix currentWeights,
            DoubleMatrix inclusionProbabilities,
            int sampleSize,
            int populationSize,
            double delta,
            ref bool continueExecution)
        {
            int n_minus_1 = -1 + sampleSize;
            int N = populationSize;
            int N_minus_1 = -1 + N;
            int N_minus_2 = -2 + N;
            var pi = inclusionProbabilities;
            DoubleMatrix nextWeights = DoubleMatrix.Dense(N, 1);
            nextWeights[N_minus_1] = currentWeights[N_minus_1];

            // S = {0,...,N-1}.
            // r[i, j] = R(i, S\{j - 1}) for j = 1,...,N.
            // r[i, 0] = R(i, S).
            var r = R(n_minus_1, currentWeights);

            double R_n_minus_1_N_minus_1_c = r[n_minus_1, N];
            int stoppingCriterion = 0;
            for (int j = N_minus_2; j >= 0; j--)
            {
                nextWeights[j] = pi[j] * R_n_minus_1_N_minus_1_c /
                    r[n_minus_1, j + 1];
                if (Math.Abs(nextWeights[j] - currentWeights[j]) < delta)
                {
                    stoppingCriterion++;
                }
            }
            if (stoppingCriterion == (N_minus_1))
            {
                continueExecution = false;
            }

            return nextWeights;
        }

        #endregion

        #region Sample

        private int Sample(SortedList<int, double> p1)
        {
            var u = this.RandomNumberGenerator.DefaultUniform();
            double cumProb = 0.0;
            int sampleUnit = -1;
            for (int j = 0; j < p1.Count; j++)
            {
                cumProb += p1.Values[j];
                if (u <= cumProb)
                {
                    sampleUnit = p1.Keys[j];
                    break;
                }
            }

            return sampleUnit;
        }


        /// <inheritdoc/>
        public override DoubleMatrix NextDoubleMatrix()
        {
            var sample = DoubleMatrix.Dense(1, this.PopulationSize);
            double[] samplePositions =sample.GetStorage();
            
            int n = this.sampleSize;
            var w = this.weigths;

            // k == 1
            var p1 = new SortedList<int, double>(this.p1_S);
            int i_k = this.Sample(p1);
            samplePositions[i_k] = 1.0;
            p1.Remove(i_k);

            // k > 1
            for (int k = 2; k <= n; k++)
            {
                int n_minus_k = n - k;
                int n_minus_k_plus_1 = n_minus_k + 1;

                // r[i, j + 1] = R(i, c\{c[j]}) for j = 0,...,|c| - 1.
                // r[i, 0] = R(i, c).
                DoubleMatrix r = R(n_minus_k_plus_1, p1.Keys, w);
                double denominator = n_minus_k_plus_1 * r[n_minus_k_plus_1, 0];

                for (int h = 0; h < p1.Count; h++)
                {
                    var j = p1.Keys[h];

                    double p_j =
                        (w[j] * r[n_minus_k, 1 + h])
                        /
                        (denominator);

                    p1[j] = p_j;
                }

                i_k = this.Sample(p1);
                samplePositions[i_k] = 1.0;
                p1.Remove(i_k);
            }

            return sample;
        }

        /// <inheritdoc/>
        public override IndexCollection NextIndexCollection()
        {
            int n = this.sampleSize;
            int[] A_k = new int[n];
            var w = this.weigths;

            // k == 1
            var p1 = new SortedList<int, double>(this.p1_S);
            int i_k = this.Sample(p1);
            A_k[0] = i_k;
            p1.Remove(i_k);

            // k > 1
            for (int k = 2; k <= n; k++)
            {
                int k_minus_1 = k - 1;
                int n_minus_k = n - k;
                int n_minus_k_plus_1 = n_minus_k + 1;

                // r[i, j + 1] = R(i, c\{c[j]}) for j = 0,...,|c| - 1.
                // r[i, 0] = R(i, c).
                DoubleMatrix r = R(n_minus_k_plus_1, p1.Keys, w);
                double denominator = n_minus_k_plus_1 * r[n_minus_k_plus_1, 0];

                for (int h = 0; h < p1.Count; h++)
                {
                    var j = p1.Keys[h];

                    double p_j = 
                        (w[j] * r[n_minus_k, 1 + h]) 
                        /
                        (denominator);

                    p1[j] = p_j;
                }

                i_k = this.Sample(p1);
                A_k[k_minus_1] = i_k;
                p1.Remove(i_k);
            }

            return new IndexCollection(A_k, false);
        }

        #endregion
    }
}
