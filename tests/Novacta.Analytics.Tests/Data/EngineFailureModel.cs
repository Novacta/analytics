// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.Data
{
    /// <summary>
    /// Exposes data and Maximum Likelihood methods related to 
    /// a model relating failure times 
    /// to corrosion levels in a set of engines.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The model is discussed in Chapter 2 of
    /// 'An Introduction to Statistical Modeling of Extreme Values', by
    /// Stuart Coles.
    /// </para>
    /// <para>
    /// The failure time <latex>T</latex> is modeled as exponentially
    /// distributed as follows:
    /// <latex mode='display'>
    /// f_T\round{t}=\lambda \exp\round{-\lambda\,t},\quad t>0, \quad \lambda = a\,w^b,
    /// </latex>
    /// where <latex>w</latex> is a corrosion level, while <latex>a,\,b</latex>
    /// are parameters of the model.
    /// </para>
    /// </remarks>
    /// <seealso href="https://link.springer.com/book/10.1007/978-1-4471-3675-0"/>
    public static class EngineFailureModel
    {
        #region Data and estimated parameters

        /// <summary>
        /// A matrix representing failure times (first column)
        /// and related corrosion levels (second column).
        /// </summary>
        internal static DoubleMatrix Data = DoubleMatrix.Dense(32, 2, 
            [
                // Failure times
                5.23123756299729160000,
                0.88374116211433573000,
                0.24582451897111149000,
                3.73750804619983110000,
                1.19354868264895300000,
                0.74444900919339752000,
                0.00033167198762031530,
                2.21263305849936210000,
                0.09988934086693641900,
                0.15701307574048620000,
                0.59387648740079346000,
                0.54507631246757449000,
                2.78271317329455000000,
                0.95551184186107296000,
                0.12054848100654020000,
                0.38856808810386700000,
                0.14556138879926550000,
                0.39274632383387242000,
                0.23401253435247291000,
                0.61334011609957173000,
                0.35972613452214880000,
                0.02001332508547953900,
                0.08535049758900932500,
                0.83787770770598524000,
                1.49180968728027400000,
                0.08067041681600099800,
                1.21000099575594900000,
                0.79851811661140137000,
                0.45019236749052499000,
                0.60979204243305885000,
                0.30816877416295257000,
                0.08961976704041332800,                
                // Corrosion levels
                0.02856560982763766900,
                0.11644553393125540000,
                0.32556412275880581000,
                0.36187570309266448000,
                0.77289500040933490000,
                1.07671243371442000000,
                1.40806602779775790000,
                1.53019431279972200000,
                1.56819202704355100000,
                1.64420582354068800000,
                1.64440864231437400000,
                1.66461208835244200000,
                1.69701453531161000000,
                1.74957353621721290000,
                1.78876443300396200000,
                1.87775873113423610000,
                1.88814442139118910000,
                2.02600740827619990000,
                2.05149662913754580000,
                2.19011591048911210000,
                2.36558147706091400000,
                2.39948192844167400000,
                2.56172240013256580000,
                2.56528501631692010000,
                2.62078743427991780000,
                2.71643983433023100000,
                2.92964335065334990000,
                3.33795519545674280000,
                3.40658882167190310000,
                3.86109929298981980000,
                4.16830997914075760000,
                4.17895696591585790000
                ]);

        /// <summary>
        /// Gets the estimated model parameters.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A vector having length 2 whose entries represent
        /// parameters <latex>a,\,b</latex>, respectively.
        /// </para>
        /// </remarks>
        internal static DoubleMatrix EstimatedParameters
            = DoubleMatrix.Dense(1, 2,
                [
                    // a
                    1.133,
                    // b
                    .479
                ]);

        #endregion


        /// <summary>
        /// Computes the log-likelihood of the model.
        /// </summary>
        /// <param name="argument">
        /// The model parameters, i.e. the argument of the log-likelihood function. 
        /// A vector whose entries are <latex>a</latex> and <latex>b</latex>.
        /// </param>
        /// <param name="parameter">
        /// The Log-likelihood parameter. A 32-by-2 matrix, 
        /// whose first column contains failure times, while the second one 
        /// contains the corrosion levels.
        /// </param>
        /// <returns>
        /// The log-likelihood value at the specified argument and parameter.
        /// </returns>
        public static double LogLikelihood(
            DoubleMatrix argument, 
            DoubleMatrix parameter)
        {
            var a = argument[0];

            if (a <= 0)
                return Double.MinValue;

            var b = argument[1];

            int n = parameter.NumberOfRows;
            double logLikelihood = n * Math.Log(a);
            double sum_log_w = 0.0, sum_w_to_b_by_t = 0.0, w_i, t_i;

            for (int i = 0; i < n; i++)
            {
                t_i = parameter[i, 0];
                w_i = parameter[i, 1];
                sum_log_w += Math.Log(w_i);
                sum_w_to_b_by_t += Math.Pow(w_i, b) * t_i;
            }

            logLikelihood += b * sum_log_w - a * sum_w_to_b_by_t;

            return logLikelihood;
        }

        /// <summary>
        /// Computes the log-likelihood gradient of the model.
        /// </summary>
        /// <param name="argument">
        /// The model parameters, i.e. the argument of the log-likelihood function. 
        /// A vector whose entries are <latex>a</latex> and <latex>b</latex>.
        /// </param>
        /// <param name="parameter">
        /// The Log-likelihood parameter. A 32-by-2 matrix, 
        /// whose first column contains failure times, while the second one 
        /// contains the corrosion levels.
        /// </param>
        /// <returns>
        /// The log-likelihood gradient at the specified argument and parameter.
        /// </returns>
        public static DoubleMatrix LogLikelihoodGradient(
            DoubleMatrix argument, 
            DoubleMatrix parameter)
        {
            var a = argument[0];
            var b = argument[1];

            int n = parameter.NumberOfRows;
            double w_to_b_by_t, log_w, sum_log_w = 0.0, sum_w_to_b_by_t = 0.0,
                sum_w_to_b_by_t_by_log_w = 0.0;
            double w_i, t_i;
            for (int i = 0; i < n; i++)
            {
                t_i = parameter[i, 0];
                w_i = parameter[i, 1];
                log_w = Math.Log(w_i);
                sum_log_w += log_w;
                w_to_b_by_t = Math.Pow(w_i, b) * t_i;
                sum_w_to_b_by_t += w_to_b_by_t;
                sum_w_to_b_by_t_by_log_w += w_to_b_by_t * log_w;
            }
            int k = argument.Count;
            DoubleMatrix gradient = DoubleMatrix.Dense(k, 1);
            gradient[0] = n / a - sum_w_to_b_by_t;
            gradient[1] = sum_log_w - a * sum_w_to_b_by_t_by_log_w;

            return gradient;
        }

        /// <summary>
        /// Computes the log-likelihood Hessian matrix of the model.
        /// </summary>
        /// <param name="argument">
        /// The model parameters, i.e. the argument of the log-likelihood function. 
        /// A vector whose entries are <latex>a</latex> and <latex>b</latex>.
        /// </param>
        /// <param name="parameter">
        /// The Log-likelihood parameter. A 32-by-2 matrix, 
        /// whose first column contains failure times, while the second one 
        /// contains the corrosion levels.
        /// </param>
        /// <returns>
        /// The log-likelihood Hessian matrix at the specified argument and parameter.
        /// </returns>
        public static DoubleMatrix LogLikelihoodHessian(
            DoubleMatrix argument, 
            DoubleMatrix parameter)
        {
            var a = argument[0];
            var b = argument[1];

            int n = parameter.NumberOfRows;
            double w_to_b_by_t, log_w, sum_w_to_b_by_t = 0.0,
                sum_w_to_b_by_t_by_log_w = 0.0, 
                sum_w_to_b_by_t_by_squared_log_w = 0.0;

            double w_i, t_i;
            for (int i = 0; i < n; i++)
            {
                t_i = parameter[i, 0];
                w_i = parameter[i, 1];

                log_w = Math.Log(w_i);
                w_to_b_by_t = Math.Pow(w_i, b) * t_i;
                sum_w_to_b_by_t += w_to_b_by_t;
                sum_w_to_b_by_t_by_log_w += w_to_b_by_t * log_w;
                sum_w_to_b_by_t_by_squared_log_w += w_to_b_by_t * log_w * log_w;
            }
            int k = argument.Count;
            DoubleMatrix hessian = DoubleMatrix.Dense(k, k);
            hessian[0, 0] = -n / (a * a);
            hessian[0, 1] = -sum_w_to_b_by_t_by_log_w;
            hessian[1, 0] = -sum_w_to_b_by_t_by_log_w;
            hessian[1, 1] = -a * sum_w_to_b_by_t_by_squared_log_w;

            return hessian;
        }
    }
}

