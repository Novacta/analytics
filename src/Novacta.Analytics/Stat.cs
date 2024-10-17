// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Threading.Tasks;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides static methods for common statistical functions.
    /// </summary>
    public static class Stat
    {
        private const int dense = (int)StorageScheme.Dense;
        private const int sparse = (int)StorageScheme.CompressedRow;
        private const int numberOfStorageSchemes = 2;

        #region Correlations

        /// <summary>
        /// Returns the correlations among the rows or the columns 
        /// of the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dataOperation">A constant to specify if the correlations 
        /// are to be computed among the rows or among the columns.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// Given a sequence of <latex mode="inline">L</latex> random variables <latex mode="inline">Y_0,\dots,Y_{L-1}</latex>,
        /// their correlation matrix can be defined as that having generic entry:
        /// <latex mode="display">
        /// \rho_{l,k}=\frac{E \left[ \left( Y_l - E \left[ Y_l \right] \right) \left( Y_k - E \left[ Y_k \right] \right) \right]}
        /// {\sqrt{E \left[ \left( Y_l - E \left[ Y_l \right] \right) ^2 \right ] E\left[ \left( Y_k - E \left[ Y_k \right] \right)^2 \right]}},\hspace{12pt} l,k=0,\dots,L-1.
        /// </latex>        
        /// </para>
        /// <para>
        /// By interpreting the rows or the columns of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns estimates of the correlation matrix of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns the estimated correlation matrix,
        /// say <latex mode="inline">\hat{\rho}</latex>, 
        /// among the <latex mode="inline">m</latex> rows
        /// of <paramref name="data"/>. 
        /// For <latex mode="inline">l,k=0,\dots,m-1,</latex> entry 
        /// <latex mode="inline">\hat{\rho}_{l,k}</latex> 
        /// is <see cref="Double.NaN"/> if 
        /// at least one among the <i>l</i>-th and <i>k</i>-th rows has zero variance. 
        /// Otherwise, <latex mode="inline">\rho_{l,k}</latex> is estimated through the coefficient
        /// <latex mode="display">
        /// \hat{\rho}_{l,k} = \frac{\sum_{j=0}^{n-1} \left ( x_{l,j} -\overline{x}_{l,\cdot} \right )\left (x_{k,j} - \overline{x}_{k,\cdot} \right )}
        /// {\sqrt{\sum_{j=0}^{n-1} \left ( x_{l,j} -\overline{x}_{l,\cdot} \right )^2
        /// \sum_{j=0}^{n-1} \left ( x_{k,j} -\overline{x}_{k,\cdot} \right )^2}}
        /// ,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} x_{i,j},\hspace{12pt} i=0,\dots,m-1.
        /// </latex>
        /// is the sample mean of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns the estimated correlation matrix 
        /// among the <latex mode="inline">n</latex> columns
        /// of <paramref name="data"/>. 
        /// For <latex mode="inline">l,k=0,\dots,n-1,</latex> entry 
        /// <latex mode="inline">\hat{\rho}_{l,k}</latex> 
        /// is <see cref="Double.NaN"/> if 
        /// at least one among the <i>l</i>-th and <i>k</i>-th columns has zero variance. 
        /// Otherwise, <latex mode="inline">\rho_{l,k}</latex> is estimated 
        /// through the coefficient
        /// <latex mode="display">
        /// \hat{\rho}_{l,k} = \frac{\sum_{i=0}^{m-1} \left ( x_{i,l} -\overline{x}_{\cdot,l} \right )\left (x_{i,k} - \overline{x}_{\cdot,k} \right )}
        /// {\sqrt{\sum_{i=0}^{m-1} \left ( x_{i,l} -\overline{x}_{\cdot,l} \right )^2
        /// \sum_{i=0}^{m-1} \left ( x_{i,k} -\overline{x}_{\cdot,k} \right )^2}}
        /// ,    
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} x_{i,j},\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// is the sample mean of the 
        /// <i>j</i>-th column.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the correlations among the rows and those among the columns of a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\CorrelationExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The correlations among the rows or the columns 
        /// of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Correlation"/>
        public static DoubleMatrix Correlation(
            DoubleMatrix data,
            DataOperation dataOperation)
        {
            DoubleMatrix covariance = Stat.Covariance(data, false, dataOperation);

            int numberOfVariables = dataOperation == DataOperation.OnColumns ?
                data.NumberOfColumns : data.NumberOfRows;

            var diagInvStandardDeviations = new SparseCsr3DoubleMatrixImplementor(
                numberOfVariables, numberOfVariables, numberOfVariables);

            for (int i = 0; i < numberOfVariables; i++)
            {
                diagInvStandardDeviations.SetValue(i, i, 1.0 / Math.Sqrt(covariance[i, i]));
            }
            var inverseStd = new DoubleMatrix(diagInvStandardDeviations);

            var correlation = inverseStd * covariance * inverseStd;

            return correlation;
        }

        /// <inheritdoc cref="Stat.Correlation(DoubleMatrix, DataOperation)"/>
        public static DoubleMatrix Correlation(
            ReadOnlyDoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Correlation(data.matrix, dataOperation);
        }

        #endregion

        #region Covariance

        /// <summary>
        /// Returns the covariances among the rows or the columns 
        /// of the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals 
        /// that the covariances 
        /// are adjusted for bias.
        /// </param>
        /// <param name="dataOperation">A constant to specify if the covariances 
        /// are to be computed among the rows or among the columns.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// Given a sequence of <latex mode="inline">L</latex> random 
        /// variables <latex mode="inline">Y_0,\dots,Y_{L-1}</latex>,
        /// their covariance matrix can be defined as that having generic entry:
        /// <latex mode="display">
        /// \sigma_{l,k}=E \left[ \left( Y_l - E \left[ Y_l \right] \right) \left( Y_k - E \left[ Y_k \right] \right) \right],\hspace{12pt} l,k=0,\dots,L-1.
        /// </latex>        
        /// </para>
        /// <para>
        /// By interpreting the rows or the columns 
        /// of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns estimates of the 
        /// covariance matrix of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns the estimated covariance matrix 
        /// among the <latex mode="inline">m</latex> rows
        /// of <paramref name="data"/>.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// <latex mode="inline">\sigma_{l,k}</latex> is estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{l,k} = \frac{1}{n}\sum_{j=0}^{n-1} \left ( x_{l,j} - \overline{x}_{l,\cdot} \right )\left ( x_{k,j} - \overline{x}_{k,\cdot} \right ),\hspace{12pt} l,k=0,\dots,m-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} x_{i,j},\hspace{12pt} i=0,\dots,m-1.
        /// </latex>
        /// is the sample mean of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set 
        /// to <c>true</c>, then 
        /// the estimator is corrected for bias
        /// and <latex mode="inline">\sigma_{l,k}</latex> is evaluated through 
        /// the coefficient
        /// <latex mode="display">
        /// s_{l,k}= \frac{n}{n-1} \hat{\sigma}_{l,k},
        /// </latex>
        /// provided that <latex mode="inline">n</latex> is greater than 1;
        /// otherwise, an exception is thrown.
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns the estimated covariance matrix among the <latex mode="inline">n</latex> columns
        /// of <paramref name="data"/>.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// <latex mode="inline">\sigma_{l,k}</latex> is estimated through the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{l,k} = \frac{1}{m}\sum_{i=0}^{m-1} \left ( x_{i,l} - \overline{x}_{\cdot,l} \right )\left ( x_{i,k} - \overline{x}_{\cdot,k} \right ),\hspace{12pt} l,k=0,\dots,n-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} x_{i,j},\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// is the sample mean of the 
        /// <i>j</i>-th column.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// the estimator is corrected for bias
        /// and <latex mode="inline">\sigma_{l,k}</latex> is evaluated through the coefficient
        /// <latex mode="display">
        /// s_{l,k}= \frac{m}{m-1} \hat{\sigma}_{l,k},
        /// </latex>
        /// provided that <latex mode="inline">m</latex> is greater than 1;
        /// otherwise, an exception is thrown.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the covariances among the rows and those among the columns of a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\CovarianceExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The covariances among the rows or the columns 
        /// of the specified data, eventually adjusted for bias.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.<br/> 
        /// -or-<br/> 
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/> 
        /// and the <paramref name="data"/> number of columns 
        /// is less than 2.<br/> 
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/> 
        /// and the <paramref name="data"/> number of rows 
        /// is less than 2.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Covariance"/>
        public static DoubleMatrix Covariance(
            DoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return dataOperation switch
            {
                DataOperation.OnRows => CovarianceOnRows(data, adjustForBias),
                DataOperation.OnColumns => CovarianceOnColumns(data, adjustForBias),
                _ => throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                        nameof(dataOperation)),
            };
        }

        private static DoubleMatrix CovarianceOnColumns(
            DoubleMatrix data,
            bool adjustForBias)
        {
            int numberOfIndividuals = data.NumberOfRows;

            double adjustment = 0.0;
            if (adjustForBias)
            {
                if (numberOfIndividuals < 2.0)
                {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED"));
                }

                adjustment = numberOfIndividuals / (numberOfIndividuals - 1.0);
            }

            var mean = Stat.Mean(data, DataOperation.OnColumns).GetStorage();

            int numberOfVariables = data.NumberOfColumns;

            DoubleMatrix covariance = DoubleMatrix.Dense(numberOfVariables, numberOfVariables);
            var covArray = covariance.GetStorage();

            DoubleMatrix[] x = new DoubleMatrix[numberOfVariables];

            Parallel.For(0, numberOfVariables, j =>
            {
                x[j] = data[":", j];
                double xMean = mean[j];

                for (int k = j; k < numberOfVariables; k++)
                {
                    int offset = k * numberOfVariables;
                    double result = 0.0;
                    double yMean = mean[k];
                    var y = data[":", k];
                    for (int i = 0; i < numberOfIndividuals; i++)
                    {
                        double xDev = x[j][i] - xMean;
                        double yDev = y[i] - yMean;
                        result += (xDev * yDev - result) / (i + 1);
                    }

                    if (adjustForBias)
                    {
                        result *= adjustment;
                    }
                    covArray[j + offset] = result;
                    if (k != j)
                    {
                        covArray[k + j * numberOfVariables] = result;
                    }
                }
            });

            return covariance;
        }

        private static DoubleMatrix CovarianceOnRows(DoubleMatrix data, bool adjustForBias)
        {
            int numberOfIndividuals = data.NumberOfColumns;

            double adjustment = 0.0;
            if (adjustForBias)
            {
                if (numberOfIndividuals < 2.0)
                {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED"));
                }

                adjustment = numberOfIndividuals / (numberOfIndividuals - 1.0);
            }

            var mean = Stat.Mean(data, DataOperation.OnRows).GetStorage();

            int numberOfVariables = data.NumberOfRows;

            DoubleMatrix covariance = DoubleMatrix.Dense(numberOfVariables, numberOfVariables);
            var covArray = covariance.GetStorage();

            DoubleMatrix[] x = new DoubleMatrix[numberOfVariables];

            Parallel.For(0, numberOfVariables, i =>
            {
                x[i] = data[i, ":"];
                double xMean = mean[i];

                for (int k = i; k < numberOfVariables; k++)
                {
                    int offset = k * numberOfVariables;
                    double result = 0.0;
                    double yMean = mean[k];
                    var y = data[k, ":"];
                    for (int j = 0; j < numberOfIndividuals; j++)
                    {
                        double xDev = x[i][j] - xMean;
                        double yDev = y[j] - yMean;
                        result += (xDev * yDev - result) / (j + 1);
                    }

                    if (adjustForBias)
                    {
                        result *= adjustment;
                    }
                    covArray[i + offset] = result;
                    if (k != i)
                    {
                        covArray[k + i * numberOfVariables] = result;
                    }

                }
            });

            return covariance;
        }

        /// <inheritdoc cref="Stat.Covariance(DoubleMatrix, bool, DataOperation)"/>
        public static DoubleMatrix Covariance(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Covariance(data.matrix, adjustForBias, dataOperation);
        }

        #endregion

        #region Kurtosis

        private static DenseMatrixImplementor<ByDimensionSummaryOperator<double>> KurtosisByDimDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<ByDimensionSummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new ByDimensionSummaryOperator<double>(StatisticOperators.Dense_Kurtosis);
            operators[sparse] = new ByDimensionSummaryOperator<double>(StatisticOperators.Sparse_Kurtosis);
            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionSummaryOperator<double>>
            kurtosisByDimDoubleOperators = KurtosisByDimDoubleOperators();

        private static DenseMatrixImplementor<SummaryOperator<double>> KurtosisDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<SummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new SummaryOperator<double>(StatisticOperators.Dense_Kurtosis);
            operators[sparse] = new SummaryOperator<double>(StatisticOperators.Sparse_Kurtosis);
            return operators;
        }

        private static readonly DenseMatrixImplementor<SummaryOperator<double>>
            kurtosisDoubleOperators = KurtosisDoubleOperators();

        /// <summary>
        /// Returns the kurtosis of the specified data, 
        /// eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the kurtosis 
        /// is adjusted for bias.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// The kurtosis of a random variable <latex mode="inline">X</latex> 
        /// can be defined as follows:
        /// <latex mode="display">
        /// \gamma_2=\frac{\kappa_4}{\kappa_2^{2}},
        /// </latex>        
        /// where <latex mode="inline">\kappa_2=\mu_2</latex> and 
        /// <latex mode="inline">
        /// \kappa_4 = \mu_4 - 3 \mu_2^2
        /// </latex> are         
        /// the cumulants of order 2 and 4, respectively, and 
        /// <latex mode="display">
        /// \mu_r = E \left[ \left( X - E \left[ X \right] \right)^r \right]
        /// </latex>        
        /// is the <latex mode="inline">X</latex> central moment 
        /// of order <latex mode="inline">r</latex>.
        /// </para>
        /// <para>
        /// The  <latex mode="inline">\gamma_2</latex> parameter
        /// can be estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// g_2=\frac{\hat{\mu}_4}{\hat{\mu}_2^{2}}-3
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_r=\frac{1}{L}\sum_{l=0}^{L-1} \left ( x_l - \overline{x} \right )^r
        /// </latex>
        /// is the sample central moment of order 
        /// <latex mode="inline">r</latex>,
        /// <latex mode="inline">L</latex> is 
        /// the <paramref name="data"/> <see cref="System.Collections.Generic.ICollection{T}.Count"/> and 
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1.
        /// </latex>
        /// Note that <latex mode="inline">g_2</latex> is undefined 
        /// if the standard deviation of <paramref name="data"/> is zero. 
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_2</latex> is
        /// a biased estimator of <latex mode="inline">\gamma_2</latex>.
        /// However,  
        /// provided that <latex mode="inline">L</latex> is greater than
        /// 3, it can be corrected for bias
        /// and the kurtosis evaluated through the coefficient
        /// <latex mode="display">
        /// G_2=\frac{L-1}{\left( L-2 \right) \left( L-3 \right)} 
        /// \left[ \left( L+1 \right) g_2 + 6 \right].
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// this method returns <latex mode="inline">g_2</latex> if it
        /// is defined, otherwise <see cref="System.Double.NaN"/> is
        /// returned.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, 
        /// then this methods operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">L</latex> is less than
        /// 4, i.e. the cumulant estimators cannot be corrected for bias,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_2</latex> is defined,
        /// then <latex mode="inline">G_2</latex> is returned, otherwise
        /// the return value is <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the kurtosis of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\KurtosisExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The kurtosis of the specified data, eventually adjusted for bias.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="adjustForBias"/> is <c>true</c> and the number of
        /// entries in
        /// <paramref name="data"/> is less than 4.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Kurtosis"/>
        public static double Kurtosis(
            DoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);
            double n = data.Count;

            if (adjustForBias && (n < 4.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_KURTOSIS_ADJUST_FOR_BIAS_UNDEFINED"));
            }

            var implementor = data.implementor;
            double result = kurtosisDoubleOperators[(int)implementor.StorageScheme](implementor);

            if (adjustForBias)
            {
                double c = (n - 1.0) / ((n - 2.0) * (n - 3.0));
                result = ((n + 1) * result + 6.0) * c;
            }

            return result;
        }

        /// <summary>
        /// Returns the kurtosis of each row or column in the specified
        /// data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the kurtosis 
        /// is adjusted for bias.
        /// </param>
        /// <param name="dataOperation">A constant to specify if the kurtosis 
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="Stat.Kurtosis(DoubleMatrix,bool)" 
        /// path="para[@id='0']"/>
        /// <para>
        /// By interpreting the rows or the columns 
        /// of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns the kurtosis 
        /// estimates of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector
        /// whose length
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of such column returns the kurtosis of the <i>i</i>-th 
        /// <paramref name="data"/> row, say <latex mode="inline">\gamma_{2,i,\cdot}.</latex>
        /// </para>
        /// <para>
        /// The  <latex mode="inline">\gamma_{2,i,\cdot}</latex> parameter can be 
        /// estimated through the coefficient
        /// <latex mode="display">
        /// g_{2,i,\cdot}=\frac{\hat{\mu}_{4,i,\cdot}}{\hat{\mu}_{2,i,\cdot}^{2}}-3,\hspace{12pt} i=0,\dots,m-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} \left ( x_{i,j} - \overline{x}_{i,\cdot} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para>
        /// Note that <latex mode="inline">g_{2,i,\cdot}</latex> is
        /// undefined if the standard deviation 
        /// of the <i>i</i>-th row of <paramref name="data"/> is zero.
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_{2,i,\cdot}</latex> is  
        /// a biased estimator of <latex mode="inline">\gamma_{2,i,\cdot}</latex>
        /// However, provided that the number of 
        /// columns in <paramref name="data"/> is greater than 3,
        /// it can be corrected for bias and the corresponding kurtosis 
        /// evaluated through the coefficient
        /// <latex mode="display">
        /// G_{2,i,\cdot}=\frac{\left( n-1 \right)}{\left( n-2 \right)\left( n-3 \right)}
        /// \left[ \left( n+1 \right) g_{2,i,\cdot} + 6 \right].
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// then <latex mode="inline">\gamma_{2,i,\cdot}</latex> is estimated 
        /// through <latex mode="inline">g_{2,i,\cdot}</latex> if it is 
        /// defined, otherwise the <i>i</i>-th position in the returned 
        /// value evaluates to <see cref="System.Double.NaN"/>.  
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// this method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">n</latex> is less than
        /// 4, i.e. the cumulant estimators cannot be corrected for bias,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_{2,i,\cdot}</latex> is defined,
        /// then <latex mode="inline">G_{2,i,\cdot}</latex> is stored in 
        /// the <i>i</i>-th position of the returned value, otherwise
        /// such position stores <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the kurtosis of 
        /// the <i>j</i>-th 
        /// <paramref name="data"/> column, 
        /// say <latex mode="inline">\gamma_{2,\cdot,j}.</latex>
        /// </para>
        /// <para>
        /// The <latex mode="inline">\gamma_{2,\cdot,j}</latex> parameter 
        /// can be estimated through the coefficient
        /// <latex mode="display">
        /// g_{2,\cdot,j}=\frac{\hat{\mu}_{4,\cdot,j}}{\hat{\mu}_{2,\cdot,j}^{2}}-3,\hspace{12pt} j=0,\dots,n-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} \left ( x_{i,j} - \overline{x}_{\cdot,j} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central
        /// moment of the <i>j</i>-th column.
        /// </para>
        /// <para>
        /// Note that <latex mode="inline">g_{2,\cdot,j}</latex> is
        /// undefined if the standard deviation 
        /// of the <i>j</i>-th column of <paramref name="data"/> is zero.
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_{2,\cdot,j}</latex> is 
        /// a biased estimator of <latex mode="inline">\gamma_{2,\cdot,j}</latex>.
        /// However, provided that the number of 
        /// rows in <paramref name="data"/> is greater than 3,
        /// it can be corrected for bias and the corresponding kurtosis 
        /// evaluated through the coefficient
        /// <latex mode="display">
        /// G_{2,\cdot,j}=\frac{\left( m-1 \right)}{\left( m-2 \right)\left( m-3 \right)}
        /// \left[ \left( m+1 \right) g_{2,\cdot,j} + 6 \right].
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// then <latex mode="inline">\gamma_{2,\cdot,j}</latex> is estimated 
        /// through <latex mode="inline">g_{2,\cdot,j}</latex> if it is 
        /// defined, otherwise the <i>j</i>-th position in the returned 
        /// value evaluates to <see cref="System.Double.NaN"/>.  
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// this method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">m</latex> is less than
        /// 4, i.e. the cumulant estimators cannot be corrected for bias,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_{2,\cdot,j}</latex> is defined,
        /// then <latex mode="inline">G_{2,\cdot,j}</latex> is stored in 
        /// the <i>j</i>-th position of the returned value, otherwise
        /// such position stores <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column kurtosis estimates in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\KurtosisExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The kurtosis of each row or column in the specified data, 
        /// eventually adjusted for bias. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>, 
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/> and
        /// the number of columns
        /// of <paramref name="data"/> is less than <c>4</c>.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>, 
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/> and
        /// the <paramref name="data"/> number of rows 
        /// is less than <c>4</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Kurtosis"/>
        public static DoubleMatrix Kurtosis(DoubleMatrix data, bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            double n = (DataOperation.OnColumns == dataOperation) ?
                data.NumberOfRows : data.NumberOfColumns;

            if (adjustForBias && (n < 4.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_KURTOSIS_ADJUST_FOR_BIAS_UNDEFINED"));
            }

            var implementor = data.implementor;
            var result = new DoubleMatrix(
                kurtosisByDimDoubleOperators[(int)implementor.StorageScheme]
                (implementor, (int)dataOperation));

            if (adjustForBias)
            {
                double c = (n - 1.0) / ((n - 2.0) * (n - 3.0));
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = ((n + 1) * result[i] + 6.0) * c;
                }
            }

            return result;
        }

        /// <inheritdoc cref="Stat.Kurtosis(DoubleMatrix, bool)"/>
        public static double Kurtosis(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Kurtosis(data.matrix, adjustForBias);
        }

        /// <inheritdoc cref="Stat.Kurtosis(DoubleMatrix, bool, 
        /// DataOperation)"/>
        public static DoubleMatrix Kurtosis(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Kurtosis(data.matrix, adjustForBias, dataOperation);
        }

        #endregion

        #region Max

        private static DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>> MaxByDimDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new ByDimensionExtremeFindOperator<double>(StatisticOperators.Dense_Max);
            operators[sparse] = new ByDimensionExtremeFindOperator<double>(StatisticOperators.Sparse_Max);

            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>>
            maxByDimDoubleOperators = MaxByDimDoubleOperators();

        private static DenseMatrixImplementor<FindExtremumOperator<double>> MaxDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<FindExtremumOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new FindExtremumOperator<double>(StatisticOperators.Dense_Max);
            operators[sparse] = new FindExtremumOperator<double>(StatisticOperators.Sparse_Max);

            return operators;
        }

        private static readonly DenseMatrixImplementor<FindExtremumOperator<double>>
            maxDoubleOperators = MaxDoubleOperators();

        /// <summary>
        /// Returns the maximum value and the linear index of its first occurrence 
        /// in the specified data.
        /// </summary>
        /// <param name="data">The data to search for a maximum.</param>
        /// <remarks>
        /// The method returns an <see cref="IndexValuePair"/> structure which exposes the maximum data value through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first linear position can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// <note type="note">
        /// In the Novacta.Analytics assembly, positions of matrix entries are 
        /// interpreted as linearly ordered following a column major ordering.
        /// </note>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the largest entry of the 
        /// specified data is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MaxExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The pair given by the maximum data value and its first linear index.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="IndexValuePair"/>
        /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
        public static IndexValuePair Max(
            DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;
            var value = maxDoubleOperators[(int)implementor.StorageScheme](implementor, out int index);

            IndexValuePair results;
            results.index = index;
            results.value = value;
            return results;
        }

        /// <inheritdoc cref="Stat.Max(DoubleMatrix)"/>
        public static IndexValuePair Max(
            ReadOnlyDoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Max(data.matrix);
        }

        /// <summary>
        /// Returns the largest entries in rows or columns of the specified data.
        /// </summary>
        /// <param name="data">The data to search for maxima.</param>
        /// <param name="dataOperation">A constant to specify if row or column 
        /// maxima are to be computed.</param>
        /// <remarks>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns an array of <see cref="IndexValuePair"/> structures, one for each row in <paramref name="data"/>.
        /// The <i>i</i>-th array entry exposes the maximum value in the <i>i</i>-th 
        /// row through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first column position 
        /// in the row can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns an array of <see cref="IndexValuePair"/> structures, one for each column in <paramref name="data"/>.
        /// The <i>j</i>-th array entry exposes the maximum value in the <i>j</i>-th 
        /// column through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first row position 
        /// in the column can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column largest entries of the 
        /// specified data are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MaxExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The pairs given by the maximum values and their first 
        /// indexes.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        /// <seealso cref="IndexValuePair"/>
        public static IndexValuePair[] Max(
            DoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            var implementor = data.implementor;
            var values = new DoubleMatrix(maxByDimDoubleOperators[(int)implementor.StorageScheme]
                (implementor, out IndexCollection indexes, (int)dataOperation));

            IndexValuePair[] results = new IndexValuePair[indexes.Count];
            var indexArray = indexes.Indexes;
            var valueArray = values.GetStorage();

            for (int i = 0; i < indexArray.Length; i++)
            {
                IndexValuePair pair;
                pair.index = indexArray[i];
                pair.value = valueArray[i];
                results[i] = pair;
            }

            return results;
        }

        /// <inheritdoc cref="Stat.Max(DoubleMatrix, DataOperation)"/>
        public static IndexValuePair[] Max(
            ReadOnlyDoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Max(data.matrix, dataOperation);
        }

        #endregion

        #region Mean

        /// <summary>
        /// Returns the arithmetic mean of the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// <para id='0'>
        /// This method returns the arithmetic mean
        /// of the <paramref name="data"/> entries.
        /// Let us define  
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1,
        /// </latex>
        /// where <latex mode="inline">L</latex> is the 
        /// <paramref name="data"/> length. 
        /// Then the returned value 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \frac{1}{L}\sum_{l=0}^{L-1} x_l.
        /// </latex>        
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the arithmetic mean of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MeanExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The arithmetic mean of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        public static double Mean(
            DoubleMatrix data)
        {
            return (Sum(data) / ((double)data.Count));
        }

        /// <inheritdoc cref="Stat.Mean(DoubleMatrix)"/>
        public static double Mean(
            ReadOnlyDoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Mean(data.matrix);
        }

        /// <summary>
        /// Returns the arithmetic mean of each row or column in the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dataOperation">A constant to specify if the arithmetic mean
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method returns the arithmetic mean of <paramref name="data"/>
        /// rows or columns.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector whose length 
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of the returned column exposes the arithmetic 
        /// mean of the <i>i</i>-th 
        /// <paramref name="data"/> row. 
        /// </para>
        /// <para>  
        /// The arithmetic mean of the <i>i</i>-th row 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \frac{1}{n}\sum_{j=0}^{n-1} x_{i,j}.
        /// </latex>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the mean of the <i>j</i>-th 
        /// <paramref name="data"/> column. 
        /// </para>
        /// <para>
        /// The arithmetic mean of the <i>j</i>-th column 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \frac{1}{m}\sum_{i=0}^{m-1} x_{i,j}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column arithmetic means in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MeanExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The arithmetic mean of each row or column in the specified data. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        public static DoubleMatrix Mean(DoubleMatrix data, DataOperation dataOperation)
        {
            DoubleMatrix result = Sum(data, dataOperation);
            if (0 == dataOperation)
            {
                return (result / ((double)data.NumberOfColumns));
            }
            return (result / ((double)data.NumberOfRows));
        }

        /// <inheritdoc cref="Stat.Mean(DoubleMatrix, DataOperation)"/>
        public static DoubleMatrix Mean(ReadOnlyDoubleMatrix data, DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Mean(data.matrix, dataOperation);
        }

        #endregion

        #region Min

        private static DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>> MinByDimDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new ByDimensionExtremeFindOperator<double>(StatisticOperators.Dense_Min);
            operators[sparse] = new ByDimensionExtremeFindOperator<double>(StatisticOperators.Sparse_Min);
            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionExtremeFindOperator<double>>
            minByDimDoubleOperators = MinByDimDoubleOperators();

        private static DenseMatrixImplementor<FindExtremumOperator<double>> MinDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<FindExtremumOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new FindExtremumOperator<double>(StatisticOperators.Dense_Min);
            operators[sparse] = new FindExtremumOperator<double>(StatisticOperators.Sparse_Min);

            return operators;
        }

        private static readonly DenseMatrixImplementor<FindExtremumOperator<double>>
            minDoubleOperators = MinDoubleOperators();

        /// <summary>
        /// Returns the minimum value and the linear index of its first occurrence 
        /// in the specified data.
        /// </summary>
        /// <param name="data">The data to search for a minimum.</param>
        /// <remarks>
        /// The method returns an <see cref="IndexValuePair"/> structure which exposes the minimum data value through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first linear position can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// <note type="note">
        /// In the Novacta.Analytics assembly, positions of matrix entries are 
        /// interpreted as linearly ordered following a column major ordering.
        /// </note>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the smallest entry of the 
        /// specified data is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MinExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The pair given by the minimum data value and its first 
        /// linear index.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="IndexValuePair"/>
        /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
        public static IndexValuePair Min(
            DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;
            var value = minDoubleOperators[(int)implementor.StorageScheme](implementor, out int index);

            IndexValuePair results;
            results.index = index;
            results.value = value;
            return results;
        }

        /// <inheritdoc cref="Stat.Min(DoubleMatrix)"/>
        public static IndexValuePair Min(
            ReadOnlyDoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Min(data.matrix);
        }

        /// <summary>
        /// Returns the smallest entries in rows or columns of the specified data.
        /// </summary>
        /// <param name="data">The data to search for minima.</param>
        /// <param name="dataOperation">A constant to specify if row or column 
        /// minima are to be computed.</param>
        /// <remarks>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns an array of <see cref="IndexValuePair"/> structures, one for each row in <paramref name="data"/>.
        /// The <i>i</i>-th array entry exposes the minimum value in the <i>i</i>-th 
        /// row through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first column position 
        /// in the row can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns an array of <see cref="IndexValuePair"/> structures, one for each column in <paramref name="data"/>.
        /// The <i>j</i>-th array entry exposes the minimum value in the <i>j</i>-th 
        /// column through
        /// property <see cref="IndexValuePair.Value"/>, while the corresponding first row position 
        /// in the column can be 
        /// inspected by getting property
        /// <see cref="IndexValuePair.Index"/>. 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column smallest entries of the 
        /// specified data are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\MinExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The pairs given by the minimum values and their first 
        /// indexes.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        /// <seealso cref="IndexValuePair"/>
        public static IndexValuePair[] Min(
            DoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            var implementor = data.implementor;
            var values = new DoubleMatrix(minByDimDoubleOperators[(int)implementor.StorageScheme]
                (implementor, out IndexCollection indexes, (int)dataOperation));

            IndexValuePair[] results = new IndexValuePair[indexes.Count];
            var indexArray = indexes.Indexes;
            var valueArray = values.GetStorage();

            for (int i = 0; i < indexArray.Length; i++)
            {
                IndexValuePair pair;
                pair.index = indexArray[i];
                pair.value = valueArray[i];
                results[i] = pair;
            }

            return results;
        }

        /// <inheritdoc cref="Stat.Min(DoubleMatrix,DataOperation)"/>
        public static IndexValuePair[] Min(
            ReadOnlyDoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Min(data.matrix, dataOperation);
        }

        #endregion

        #region Quantile

        /// <summary>
        /// Returns the quantiles of the given data for
        /// the specified probabilities.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="probabilities">The probabilities whose quantiles 
        /// are to be computed.</param>
        /// <remarks>
        /// <para id='0'>
        /// The quantile of a distribution <latex mode="inline">F</latex> 
        /// for the probability <latex mode="inline">p</latex>
        /// can be defined as follows:
        /// <latex mode="display">
        /// Q_p = \inf\left\{x : F\left(x\right) \geq p \right\}.
        /// </latex>     
        /// </para>
        /// <para>
        /// The method returns a <see cref="DoubleMatrix"/> instance which includes the quantiles of <paramref name="data"/>.
        /// It has the same dimensions of <paramref name="probabilities"/>. 
        /// Let <latex mode="inline">K</latex> be 
        /// the <see cref="DoubleMatrix.Count">Length</see> of <paramref name="probabilities"/> and
        /// define
        /// <latex mode="display">
        /// p_k=\mathit{probabilities}[k],\hspace{12pt} k=0,\dots,K-1.
        /// </latex>
        /// Then the <i>k</i>-th
        /// linear position of the returned matrix is occupied by the quantile
        /// corresponding to 
        /// <latex mode="inline">p_k</latex>, 
        /// say <latex mode="inline">q_k</latex>. 
        /// It is computed as proposed by Hyndman and Fan (1996)<cite>hyndman-fan-1996</cite>. 
        /// Let <latex mode="inline">L</latex> be 
        /// the <see cref="DoubleMatrix.Count">Length</see> of <paramref name="data"/> and
        /// let
        /// <latex mode="display">
        /// x_{(0)}\leq\dots\leq x_{(l)}\leq \dots \leq x_{(L-1)} 
        /// </latex>
        /// be the values in <paramref name="data"/> sorted in ascending order.
        /// Value <latex mode="inline">x_{(l)}</latex> is taken as
        /// the quantile corresponding to the probability
        /// <latex mode="display">
        /// \pi_l=\frac{l+2/3}{L+1/3},
        /// </latex>
        /// and quantile <latex mode="inline">q_k</latex> is obtained by
        /// linear interpolation of the points
        /// <latex mode="display">
        /// \left(\pi_l, x_{(l)} \right),\hspace{12pt} l=0,\dots,L-1,
        /// </latex>
        /// hence setting
        /// <list type="bullet">
        ///     <item>
        ///         <latex mode="inline">q_k = x_{(0)}</latex> if <latex mode="inline">p_k &lt; \pi_0</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_k = x_{(l)}+\frac{p_k-\pi_l}{\pi_{l-1}-\pi_l} \left(x_{(l+1)}-x_{(l)} \right)</latex>
        ///         if <latex mode="inline">\exists l \in \{0,\dots,L-2\}: \pi_{l} \leq p_k &lt; \pi_{l+1}</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_k = x_{(L-1)}</latex> if <latex mode="inline">\pi_{L-1} \leq p_k</latex>.
        ///     </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the quantiles of the values in a 
        /// data matrix are computed for probabilities .005, .50, .75, and .999.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\QuantileExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The data quantiles corresponding
        /// to the specified probabilities.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="probabilities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="probabilities"/> has entries not belonging to the
        /// closed interval <c>[0, 1]</c>.
        /// </exception>
        public static DoubleMatrix Quantile(DoubleMatrix data, DoubleMatrix probabilities)
        {
            ArgumentNullException.ThrowIfNull(data);

            ArgumentNullException.ThrowIfNull(probabilities);

            for (int p = 0; p < probabilities.Count; p++)
            {
                double probability = probabilities[p];
                if (probability < 0.0 || 1.0 < probability)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL"), 0, 1),
                        nameof(probabilities));
                }
            }

            int dataLength = data.Count;
            int numberOfProbabilities = probabilities.Count;
            double denominator = dataLength + 1.0 / 3.0;
            double increment = 2.0 / 3.0;
            double[] knownProbabilities = new double[dataLength];
            for (int l = 0; l < dataLength; l++)
            {
                knownProbabilities[l] = (l + increment) / denominator;
            }

            var quantiles = DoubleMatrix.Dense(probabilities.NumberOfRows,
                    probabilities.NumberOfColumns);

            int[] maxIndexes = new int[numberOfProbabilities];

            for (int p = 0; p < numberOfProbabilities; p++)
            {
                maxIndexes[p] = -1;
                for (int l = 0; l < dataLength; l++)
                {
                    if (probabilities[p] < knownProbabilities[l])
                    {
                        maxIndexes[p] = l;
                        break;
                    }
                }
            }

            // Sort data
            var sortedData = Stat.Sort(data, SortDirection.Ascending);

            for (int p = 0; p < numberOfProbabilities; p++)
            {
                switch (maxIndexes[p])
                {
                    case 0:
                        quantiles[p] = sortedData[0];
                        break;
                    case -1:
                        quantiles[p] = sortedData[dataLength - 1];
                        break;
                    default:
                        {
                            int index1 = maxIndexes[p] - 1;
                            double x1 = knownProbabilities[index1];
                            if (probabilities[p] == x1)
                                quantiles[p] = sortedData[index1];
                            else
                            {
                                // Linear interpolation
                                int index2 = maxIndexes[p];
                                double x2, y1, y2;
                                x2 = knownProbabilities[index2];
                                y1 = sortedData[index1];
                                y2 = sortedData[index2];
                                double m = (y2 - y1) / (x2 - x1);
                                quantiles[p] = m * (probabilities[p] - x1) + y1;
                            }
                            break;
                        }
                }
            }

            return quantiles;
        }

        /// <inheritdoc cref="Stat.Quantile(DoubleMatrix, 
        /// DoubleMatrix)"/>
        public static DoubleMatrix Quantile(
            ReadOnlyDoubleMatrix data,
            DoubleMatrix probabilities)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Quantile(data.matrix, probabilities);
        }

        /// <summary>
        /// Returns the quantiles of rows or columns in the given data corresponding
        /// to the specified probabilities.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="probabilities">The probabilities whose quantiles are to be computed.
        /// </param>
        /// <param name="dataOperation">A constant to specify if row or column 
        /// quantiles are to be computed.</param>
        /// <remarks>
        /// <inheritdoc cref="Stat.Quantile(DoubleMatrix,DoubleMatrix)" 
        /// path="para[@id='0']"/>
        /// <para>
        /// By interpreting rows or columns of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns the quantile estimates of their distributions
        /// for the specified probabilities. They are computed as proposed by Hyndman and Fan (1996)<cite>hyndman-fan-1996</cite>. 
        /// Let <latex mode="inline">K</latex> be 
        /// the <see cref="DoubleMatrix.Count">Length</see> of <paramref name="probabilities"/> and
        /// define
        /// <latex mode="display">
        /// p_k=\mathit{probabilities}[k],\hspace{12pt} k=0,\dots,K-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns an array of <see cref="DoubleMatrix"/> instances, one for each row in <paramref name="data"/>.
        /// The <i>i</i>-th array entry contains the quantiles of the <i>i</i>-th 
        /// row. It has the same dimensions of <paramref name="probabilities"/>. 
        /// Its <i>k</i>-th
        /// linear position is occupied by the row quantile
        /// corresponding to 
        /// <latex mode="inline">p_k</latex>, 
        /// say <latex mode="inline">q_{i,\cdot,k}.</latex>       
        /// </para>
        /// <para>
        /// 
        /// Let <latex mode="inline">n</latex> be 
        /// the <paramref name="data"/> number of columns and
        /// let
        /// <latex mode="display">
        /// x_{i,(0)}\leq\dots\leq x_{i,(j)}\leq \dots \leq x_{i,(n-1)} 
        /// </latex>
        /// be the values in the <i>i</i>-th row of <paramref name="data"/> sorted in ascending order.
        /// Value <latex mode="inline">x_{i,(j)}</latex> is taken as
        /// the quantile corresponding to the probability
        /// <latex mode="display">
        /// \pi_j=\frac{j+2/3}{n+1/3},
        /// </latex>
        /// and quantile <latex mode="inline">q_{i,\cdot,k}</latex> is obtained by
        /// linear interpolation of the points
        /// <latex mode="display">
        /// \left(\pi_j, x_{i,(j)} \right),\hspace{12pt} j=0,\dots,n-1,
        /// </latex>
        /// hence setting
        /// <list type="bullet">
        ///     <item>
        ///         <latex mode="inline">q_{i,\cdot,k} = x_{i,(0)}</latex> if <latex mode="inline">p_k &lt; \pi_0</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_{i,\cdot,k} = x_{i,(j)}+\frac{p_k-\pi_j}{\pi_{j-1}-\pi_j} \left(x_{i,(j+1)}-x_{i,(j)} \right)</latex>
        ///         if <latex mode="inline">\exists j \in \{0,\dots,n-2\}: \pi_{j} \leq p_k &lt; \pi_{j+1}</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_{i,\cdot,k} = x_{i,(n-1)}</latex> if <latex mode="inline">\pi_{n-1} \leq p_k</latex>.
        ///     </item>
        /// </list>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns an array of <see cref="DoubleMatrix"/> instances, one for each column in <paramref name="data"/>.
        /// The <i>j</i>-th array entry contains the quantiles of the <i>j</i>-th 
        /// column. It has the same dimensions of <paramref name="probabilities"/>. 
        /// Its <i>k</i>-th
        /// linear position is the column quantile
        /// corresponding to 
        /// <latex mode="inline">p_k</latex>, 
        /// say <latex mode="inline">q_{\cdot,j,k}</latex>.        
        /// </para>
        /// Let <latex mode="inline">m</latex> be 
        /// the number of rows of <paramref name="data"/> and
        /// let
        /// <latex mode="display">
        /// x_{(0),j}\leq\dots\leq x_{(i),j}\leq \dots \leq x_{(m-1),j} 
        /// </latex>
        /// be the values in the <i>j</i>-th column of <paramref name="data"/> sorted in ascending order.
        /// Value <latex mode="inline">x_{(i),j}</latex> is taken as
        /// the quantile corresponding to the probability
        /// <latex mode="display">
        /// \pi_i=\frac{i+2/3}{m+1/3},
        /// </latex>
        /// and quantile <latex mode="inline">q_{\cdot,j,k}</latex> is obtained by
        /// linear interpolation of the points
        /// <latex mode="display">
        /// \left(\pi_i, x_{(i),j} \right),\hspace{12pt} i=0,\dots,m-1,
        /// </latex>
        /// hence setting
        /// <list type="bullet">
        ///     <item>
        ///         <latex mode="inline">q_{\cdot,j,k} = x_{(0),j}</latex> if <latex mode="inline">p_k &lt; \pi_0</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_{\cdot,j,k} = x_{(i),j}+\frac{p_k-\pi_i}{\pi_{i-1}-\pi_i} \left(x_{(i+1),j}-x_{(i),j} \right)</latex>
        ///         if <latex mode="inline">\exists i \in \{0,\dots,m-2\}: \pi_{i} \leq p_k &lt; \pi_{i+1}</latex>;
        ///     </item>
        ///     <item>
        ///         <latex mode="inline">q_{\cdot,j,k} = x_{(m-1),j}</latex> if <latex mode="inline">\pi_{m-1} \leq p_k</latex>.
        ///     </item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, column quantiles of the 
        /// specified data are computed for probabilities  .005, .50, .75, and .999.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\QuantileExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// <para>
        /// In the following example, row quantiles of the 
        /// specified data are computed for probabilities .33 and .66.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\QuantileExample2.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The quantiles of data rows or columns corresponding
        /// to the specified probabilities.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="probabilities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is 
        /// not a field of 
        /// <see cref="DataOperation"/>.<br/>
        /// -or-<br/>
        /// <paramref name="probabilities"/> has entries not belonging to the
        /// closed interval <c>[0, 1]</c>.
        /// </exception>
        public static DoubleMatrix[] Quantile(
            DoubleMatrix data,
            DoubleMatrix probabilities,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            ArgumentNullException.ThrowIfNull(probabilities);

            for (int p = 0; p < probabilities.Count; p++)
            {
                double probability = probabilities[p];
                if (probability < 0.0 || 1.0 < probability)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL"), 0, 1),
                        nameof(probabilities));
                }
            }

            return dataOperation switch
            {
                DataOperation.OnRows => QuantileOnRows(data, probabilities),
                DataOperation.OnColumns => QuantileOnColumns(data, probabilities),
                _ => throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                        nameof(dataOperation)),
            };
        }

        private static DoubleMatrix[] QuantileOnRows(
            DoubleMatrix data,
            DoubleMatrix probabilities)
        {
            int numberOfColumns = data.NumberOfColumns;
            int numberOfRows = data.NumberOfRows;
            int numberOfProbabilities = probabilities.Count;

            double denominator = numberOfColumns + 1.0 / 3.0;
            double increment = 2.0 / 3.0;

            double[] knownProbabilities = new double[numberOfColumns];
            for (int j = 0; j < numberOfColumns; j++)
            {
                knownProbabilities[j] = (j + increment) / denominator;
            }

            var quantiles = new DoubleMatrix[numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
            {
                quantiles[i] = DoubleMatrix.Dense(probabilities.NumberOfRows,
                    probabilities.NumberOfColumns);
            }

            int[] maxIndexes = new int[numberOfProbabilities];

            for (int p = 0; p < numberOfProbabilities; p++)
            {
                maxIndexes[p] = -1;
                for (int j = 0; j < numberOfColumns; j++)
                { // dimensionLength
                    if (probabilities[p] < knownProbabilities[j])
                    {
                        maxIndexes[p] = j;
                        break;
                    }
                }
            }

            DoubleMatrix[] rows = new DoubleMatrix[numberOfRows];

            Parallel.For(0, numberOfRows, i =>
            {
                // Sort row
                rows[i] = data[i, ":"];
                StatisticOperators.Dense_Sort(rows[i].implementor, SortDirection.Ascending);

                for (int p = 0; p < numberOfProbabilities; p++)
                {
                    switch (maxIndexes[p])
                    {
                        case 0:
                            quantiles[i][p] = rows[i][0];
                            break;
                        case -1:
                            quantiles[i][p] = rows[i][numberOfColumns - 1];
                            break;
                        default:
                            {
                                int index1 = maxIndexes[p] - 1;
                                double x1 = knownProbabilities[index1];
                                if (probabilities[p] == x1)
                                    quantiles[i][p] = rows[i][index1];
                                else
                                {
                                    // Linear interpolation
                                    int index2 = maxIndexes[p];
                                    double x2, y1, y2;
                                    x2 = knownProbabilities[index2];
                                    y1 = rows[i][index1];
                                    y2 = rows[i][index2];
                                    double m = (y2 - y1) / (x2 - x1);
                                    quantiles[i][p] = m * (probabilities[p] - x1) + y1;
                                }
                                break;
                            }
                    }
                }
            });

            return quantiles;
        }

        private static DoubleMatrix[] QuantileOnColumns(
            DoubleMatrix data,
            DoubleMatrix probabilities)
        {
            int numberOfColumns = data.NumberOfColumns;
            int numberOfRows = data.NumberOfRows;
            int numberOfProbabilities = probabilities.Count;

            double denominator = numberOfRows + 1.0 / 3.0;
            double increment = 2.0 / 3.0;

            double[] knownProbabilities = new double[numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
            {
                knownProbabilities[i] = (i + increment) / denominator;
            }

            var quantiles = new DoubleMatrix[numberOfColumns];
            for (int j = 0; j < numberOfColumns; j++)
            {
                quantiles[j] = DoubleMatrix.Dense(probabilities.NumberOfRows,
                    probabilities.NumberOfColumns);
            }

            int[] maxIndexes = new int[numberOfProbabilities];

            for (int p = 0; p < numberOfProbabilities; p++)
            {
                maxIndexes[p] = -1;
                for (int i = 0; i < numberOfRows; i++)
                {
                    if (probabilities[p] < knownProbabilities[i])
                    {
                        maxIndexes[p] = i;
                        break;
                    }
                }
            }

            DoubleMatrix[] columns = new DoubleMatrix[numberOfColumns];

            Parallel.For(0, numberOfColumns, j =>
            {
                // Sort column
                columns[j] = data[":", j];
                StatisticOperators.Dense_Sort(columns[j].implementor, SortDirection.Ascending);

                for (int p = 0; p < numberOfProbabilities; p++)
                {
                    switch (maxIndexes[p])
                    {
                        case 0:
                            quantiles[j][p] = columns[j][0];
                            break;
                        case -1:
                            quantiles[j][p] = columns[j][numberOfRows - 1];
                            break;
                        default:
                            {
                                int index1 = maxIndexes[p] - 1;
                                double x1 = knownProbabilities[index1];
                                if (probabilities[p] == x1)
                                    quantiles[j][p] = columns[j][index1];
                                else
                                {
                                    // Linear interpolation
                                    int index2 = maxIndexes[p];
                                    double x2, y1, y2;
                                    x2 = knownProbabilities[index2];
                                    y1 = columns[j][index1];
                                    y2 = columns[j][index2];
                                    double m = (y2 - y1) / (x2 - x1);
                                    quantiles[j][p] = m * (probabilities[p] - x1) + y1;
                                }
                                break;
                            }
                    }
                }
            });

            return quantiles;
        }

        /// <inheritdoc cref="Stat.Quantile(DoubleMatrix, 
        /// DoubleMatrix, DataOperation)"/>
        public static DoubleMatrix[] Quantile(
            ReadOnlyDoubleMatrix data,
            DoubleMatrix probabilities,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Quantile(data.matrix, probabilities, dataOperation);
        }

        #endregion

        #region Skewness

        private static DenseMatrixImplementor<ByDimensionSummaryOperator<double>> SkewnessByDimDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<ByDimensionSummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new ByDimensionSummaryOperator<double>(StatisticOperators.Dense_Skewness);
            operators[sparse] = new ByDimensionSummaryOperator<double>(StatisticOperators.Sparse_Skewness);
            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionSummaryOperator<double>>
            skewnessByDimDoubleOperators = SkewnessByDimDoubleOperators();

        private static DenseMatrixImplementor<SummaryOperator<double>> SkewnessDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<SummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new SummaryOperator<double>(StatisticOperators.Dense_Skewness);
            operators[sparse] = new SummaryOperator<double>(StatisticOperators.Sparse_Skewness);
            return operators;
        }

        private static readonly DenseMatrixImplementor<SummaryOperator<double>>
            skewnessDoubleOperators = SkewnessDoubleOperators();

        /// <summary>
        /// Returns the skewness of the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the skewness 
        /// is adjusted for bias.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// The skewness of a random variable <latex mode="inline">X</latex> 
        /// can be defined as follows:
        /// <latex mode="display">
        /// \gamma_1=\frac{\kappa_3}{\kappa_2^{3/2}},
        /// </latex>        
        /// where
        /// <latex  mode="inline">
        /// \kappa_{2} = \mu_{2}
        /// </latex> and
        /// <latex  mode="inline">
        /// \kappa_3 = \mu_3
        /// </latex>   
        /// are the cumulants of order 2 and 3, respectively, and 
        /// <latex mode="display">
        /// \mu_r = E \left[ \left( X - E \left[ X \right] \right)^r \right]
        /// </latex>        
        /// is the <latex mode="inline">X</latex> central moment 
        /// of order <latex mode="inline">r</latex>.
        /// </para>
        /// <para>
        /// Index <latex mode="inline">\gamma_1</latex> can be estimated
        /// through the coefficient
        /// <latex mode="display">
        /// g_1=\frac{\hat{\mu}_3}{\hat{\mu}_2^{3/2}}
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_r=\frac{1}{L}\sum_{l=0}^{L-1} \left ( x_l - \overline{x} \right )^r
        /// </latex>
        /// is the sample central moment of order 
        /// <latex mode="inline">r</latex>,
        /// <latex mode="inline">L</latex> is 
        /// the <paramref name="data"/> <see cref="System.Collections.Generic.ICollection{T}.Count"/> and 
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1.
        /// </latex>
        /// </para>
        /// <para>
        /// Note that <latex mode="inline">g_1</latex> is undefined 
        /// if the standard deviation of <paramref name="data"/> is zero. 
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_1</latex> is
        /// a biased estimator of <latex mode="inline">\gamma_1</latex>.
        /// However,  
        /// provided that <latex mode="inline">L</latex> is greater than
        /// 2, a correction for bias is defined 
        /// and the kurtosis evaluated through the coefficient
        /// <latex mode="display">
        /// G_1=g_1 \frac{\surd{\{L\left(L-1\right)}\}}{\left(L-2\right)}.
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then
        /// this method returns <latex mode="inline">g_1</latex> if
        /// it is defined, otherwise <see cref="Double.NaN"/> is returned.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, 
        /// then this methods operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">L</latex> is less than
        /// 3, i.e. the correction for bias is undefined,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_1</latex> is defined,
        /// then <latex mode="inline">G_1</latex> is returned, otherwise
        /// <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the skewness of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SkewnessExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The skewness of the specified data, eventually adjusted for bias.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="adjustForBias"/> is <c>true</c> and the number of entries 
        /// in <paramref name="data"/> is less than <c>3</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Skewness"/>
        public static double Skewness(DoubleMatrix data, bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;
            double n = data.Count;

            if (adjustForBias && (n < 3.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_SKEWNESS_ADJUST_FOR_BIAS_UNDEFINED"));
            }
            double result = skewnessDoubleOperators[(int)implementor.StorageScheme](implementor);
            if (adjustForBias)
            {
                result *= Math.Sqrt(n * (n - 1.0)) / (n - 2.0);
            }
            return result;
        }

        /// <inheritdoc cref="Stat.Skewness(DoubleMatrix, bool)"/>
        public static double Skewness(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Skewness(data.matrix, adjustForBias);
        }

        /// <summary>
        /// Returns the skewness of each row or column 
        /// in the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the skewness 
        /// is adjusted for bias.
        /// </param>
        /// <param name="dataOperation">A constant to specify if the skewness 
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="Stat.Skewness(DoubleMatrix,bool)" 
        /// path="para[@id='0']"/>
        /// <para>
        /// By interpreting the rows or the columns 
        /// of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns the skewness 
        /// estimates of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector
        /// whose length
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of such column returns the skewness of the <i>i</i>-th 
        /// <paramref name="data"/> row, say <latex mode="inline">\gamma_{1,i,\cdot}.</latex>
        /// </para>
        /// <para>
        /// The  <latex mode="inline">\gamma_{1,i,\cdot}</latex> parameter can be 
        /// estimated through the coefficient
        /// <latex mode="display">
        /// g_{1,i,\cdot}=\frac{\hat{\mu}_{3,i,\cdot}}{\hat{\mu}_{2,i,\cdot}^{3/2}},\hspace{12pt} i=0,\dots,m-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} \left ( x_{i,j} - \overline{x}_{i,\cdot} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para>
        /// Note that <latex mode="inline">g_{1,i,\cdot}</latex> is
        /// undefined if the standard deviation 
        /// of the <i>i</i>-th row of <paramref name="data"/> is zero.
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_{1,i,\cdot}</latex> is  
        /// a biased estimator of <latex mode="inline">\gamma_{1,i,\cdot}</latex>
        /// However, provided that the number of 
        /// columns in <paramref name="data"/> is greater than 3,
        /// it can be corrected for bias and the corresponding kurtosis 
        /// evaluated through the coefficient
        /// <latex mode="display">
        /// G_{1,i,\cdot}=g_{1,i,\cdot} \frac{\surd{\{n\left(n-1\right)}\}}{\left(n-2\right)}.
        /// \left[ \left( n+1 \right) g_{2,i,\cdot} + 6 \right].
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// then <latex mode="inline">\gamma_{1,i,\cdot}</latex> is estimated 
        /// through <latex mode="inline">g_{1,i,\cdot}</latex> if it is 
        /// defined, otherwise the <i>i</i>-th position in the returned 
        /// value evaluates to <see cref="System.Double.NaN"/>.  
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// this method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">n</latex> is less than
        /// 4, i.e. the correction for bias is undefined,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_{1,i,\cdot}</latex> is defined,
        /// then <latex mode="inline">G_{1,i,\cdot}</latex> is stored in 
        /// the <i>i</i>-th position of the returned value, otherwise
        /// such position stores <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the skewness of 
        /// the <i>j</i>-th 
        /// <paramref name="data"/> column, 
        /// say <latex mode="inline">\gamma_{1,\cdot,j}.</latex>
        /// </para>
        /// <para>
        /// The <latex mode="inline">\gamma_{1,\cdot,j}</latex> parameter 
        /// can be estimated through the coefficient
        /// <latex mode="display">
        /// g_{1,\cdot,j}=\frac{\hat{\mu}_{3,\cdot,j}}{\hat{\mu}_{2,\cdot,j}^{3/2}},\hspace{12pt} j=0,\dots,n-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} \left ( x_{i,j} - \overline{x}_{\cdot,j} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>j</i>-th column.
        /// </para>
        /// <para>
        /// Note that <latex mode="inline">g_{1,\cdot,j}</latex> is
        /// undefined if the standard deviation 
        /// of the <i>j</i>-th column of <paramref name="data"/> is zero.
        /// </para>
        /// <para>
        /// The statistic <latex mode="inline">g_{1,\cdot,j}</latex> is 
        /// a biased estimator of <latex mode="inline">\gamma_{1,\cdot,j}</latex>.
        /// However, provided that the number of 
        /// rows in <paramref name="data"/> is greater than 2,
        /// it can be corrected for bias and the corresponding skewness 
        /// evaluated through the coefficient
        /// <latex mode="display">
        /// G_{1,\cdot,j}=g_{1,\cdot,j} \frac{\surd{\{m\left(m-1\right)}\}}{\left(m-2\right)}.
        /// </latex>
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// then <latex mode="inline">\gamma_{1,\cdot,j}</latex> is estimated 
        /// through <latex mode="inline">g_{1,\cdot,j}</latex> if it is 
        /// defined, otherwise the <i>j</i>-th position in the returned 
        /// value evaluates to <see cref="System.Double.NaN"/>.  
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// this method operates as follows.
        /// <list type="bullet">
        /// <item>
        /// If <latex mode="inline">m</latex> is less than
        /// 3, i.e. the correction for bias is undefined,
        /// then an exception is thrown.
        /// </item>
        /// <item>
        /// Differently, if <latex mode="inline">g_{1,\cdot,j}</latex> is defined,
        /// then <latex mode="inline">G_{1,\cdot,j}</latex> is stored in 
        /// the <i>j</i>-th position of the returned value, otherwise
        /// such position stores <see cref="System.Double.NaN"/>.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column skewness estimates in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SkewnessExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The skewness of each row or column in the specified data, 
        /// eventually adjusted for bias. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>, 
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/> and
        /// the <paramref name="data"/> number of columns 
        /// is less than 3.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>, <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/> and
        /// the <paramref name="data"/> number of rows 
        /// is less than 3.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Skewness"/>
        public static DoubleMatrix Skewness(
            DoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            double n = (DataOperation.OnColumns == dataOperation) ?
                data.NumberOfRows : data.NumberOfColumns;

            if (adjustForBias && (n < 3.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_SKEWNESS_ADJUST_FOR_BIAS_UNDEFINED"));
            }

            var implementor = data.implementor;
            var results = new DoubleMatrix(skewnessByDimDoubleOperators[
                (int)implementor.StorageScheme](implementor, (int)dataOperation));

            if (adjustForBias)
            {
                results *= Math.Sqrt(n * (n - 1.0)) / (n - 2.0);
            }

            return results;
        }

        /// <inheritdoc cref="Stat.Skewness(DoubleMatrix, bool, DataOperation)"/>
        public static DoubleMatrix Skewness(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Skewness(data.matrix, adjustForBias, dataOperation);
        }

        #endregion

        #region Sort

        private static DenseMatrixImplementor<SortOperator<double>> SortDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<SortOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new SortOperator<double>(StatisticOperators.Dense_Sort);
            operators[sparse] = new SortOperator<double>(StatisticOperators.Sparse_Sort);

            return operators;
        }

        private static readonly DenseMatrixImplementor<SortOperator<double>>
            sortDoubleOperators = SortDoubleOperators();

        private static DenseMatrixImplementor<SortIndexTableOperator<double>> SortIndexTableDoubleOperators()
        {
            var operators =
                new DenseMatrixImplementor<SortIndexTableOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] =
                new SortIndexTableOperator<double>(StatisticOperators.Dense_Sort_IndexTable);
            operators[sparse] =
                new SortIndexTableOperator<double>(StatisticOperators.Sparse_Sort_IndexTable);

            return operators;
        }

        private static readonly DenseMatrixImplementor<SortIndexTableOperator<double>>
            sortIndexTableDoubleOperators = SortIndexTableDoubleOperators();

        /// <summary>
        /// Sorts the specified data in ascending or descending order.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sortDirection">A constant to specify if to sort 
        /// in ascending or descending order.</param>
        /// <remarks>
        /// <para>
        /// This method returns a matrix having the same dimensions of 
        /// <paramref name="data"/>.
        /// The entry occupying the <i>l</i>-th linear position of the returned matrix
        /// is the entry of the original matrix occupying the <i>l</i>-th position in
        /// the requested ordering.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a data matrix 
        /// is sorted.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SortExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The sorted data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="sortDirection"/> is not a field of 
        /// <see cref="SortDirection"/>.
        /// </exception>
        public static DoubleMatrix Sort(
            DoubleMatrix data,
            SortDirection sortDirection)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((SortDirection.Ascending != sortDirection)
                && (SortDirection.Descending != sortDirection))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_SORT_DIRECTION"),
                    nameof(sortDirection));
            }

            DoubleMatrix result = data.Clone();
            var implementor = result.implementor;

            sortDoubleOperators[(int)implementor.StorageScheme](implementor, sortDirection);

            return result;
        }

        /// <summary>
        /// Sorts the specified data in ascending or descending order.
        /// The linear indexes
        /// of the sorted entries are similarly arranged. 
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sortDirection">A constant to specify if to sort 
        /// in ascending or descending order.</param>
        /// <remarks>
        /// <inheritdoc cref="SortIndexResults" 
        /// path="para[@id='linearPositions']"/>
        /// <para>
        /// Each entry in <paramref name="data"/> occupies a given linear position. 
        /// When an entry is repositioned during the sorting, the corresponding linear position
        /// is similarly repositioned. Therefore, the original linear indexes are 
        /// sorted according to the arrangement of the corresponding entries 
        /// in the <paramref name="data"/> matrix.
        /// This method returns an instance of class <see cref="SortIndexResults"/>, which exposes the sorted data matrix through
        /// property <see cref="SortIndexResults.SortedData"/>, while the correspondingly arranged linear positions
        /// can be 
        /// inspected by getting property
        /// <see cref="SortIndexResults.SortedIndexes"/>. 
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the entries of a data matrix 
        /// are sorted. Their linear positions are arranged accordingly.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SortIndexExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>The results of the sorting operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="sortDirection"/> is not a field of 
        /// <see cref="SortDirection"/>.
        /// </exception>
        /// <seealso cref="SortIndexResults"/>
        public static SortIndexResults SortIndex(DoubleMatrix data, SortDirection sortDirection)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((SortDirection.Ascending != sortDirection)
                && (SortDirection.Descending != sortDirection))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_SORT_DIRECTION"),
                    nameof(sortDirection));
            }

            DoubleMatrix sortedData = data.Clone();
            var implementor = sortedData.implementor;
            sortIndexTableDoubleOperators[(int)implementor.StorageScheme](implementor, sortDirection, out IndexCollection indexTable);

            SortIndexResults results = new()
            {
                SortedData = sortedData,
                SortedIndexes = indexTable
            };

            return results;
        }

        /// <inheritdoc cref="Stat.Sort(DoubleMatrix, SortDirection)"/>
        public static DoubleMatrix Sort(
            ReadOnlyDoubleMatrix data,
            SortDirection sortDirection)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Sort(data.matrix, sortDirection);
        }

        /// <inheritdoc cref="Stat.SortIndex(DoubleMatrix, SortDirection)"/>
        public static SortIndexResults SortIndex(ReadOnlyDoubleMatrix data, SortDirection sortDirection)
        {
            ArgumentNullException.ThrowIfNull(data);

            return SortIndex(data.matrix, sortDirection);
        }

        #endregion

        #region Standard deviation

        /// <summary>
        /// Returns the standard deviation of the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the  
        /// standard deviation is adjusted for bias.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// The standard deviation of a random variable <latex mode="inline">X</latex> 
        /// can be defined as follows:
        /// <latex mode="display">
        /// \sigma=\sqrt{\mu_2}
        /// </latex>        
        /// where
        /// <latex mode="display">
        /// \mu_r = E \left[ \left( X - E \left[ X \right] \right)^r \right]
        /// </latex>        
        /// is the <latex mode="inline">X</latex> central moment 
        /// of order <latex mode="inline">r</latex>.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// the standard deviation is estimated through the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}=\sqrt{\hat{\mu}_2},
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_r=\frac{1}{L}\sum_{l=0}^{L-1} \left ( x_l - \overline{x} \right )^r
        /// </latex>
        /// is the sample central moment of order 
        /// <latex mode="inline">r</latex>,
        /// <latex mode="inline">L</latex> is the <paramref name="data"/>
        /// length and 
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1.
        /// </latex>
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// the variance estimator is corrected for bias
        /// and the standard deviation is evaluated through the coefficient
        /// <latex mode="display">
        /// s=\sqrt{\frac{L}{L-1}} \hat{\sigma}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the standard deviation of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\StandardDeviationExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The standard deviation of the specified data, eventually adjusted for bias.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="adjustForBias"/> is <c>true</c> and the number of entries
        /// in <paramref name="data"/> is less than <c>2</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Standard_deviation"/>
        public static double StandardDeviation(
            DoubleMatrix data,
            bool adjustForBias)
        {
            return Math.Sqrt(Variance(data, adjustForBias));
        }

        /// <summary>
        /// Returns the standard deviation of each row or column 
        /// in the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the  
        /// standard deviation is adjusted for bias.
        /// </param>
        /// <param name="dataOperation">A constant to specify if the standard deviation 
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="Stat.StandardDeviation(DoubleMatrix,bool)" 
        /// path="para[@id='0']"/>
        /// <para>
        /// By interpreting rows or columns of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns the standard deviation estimates of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector
        /// whose length
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of such column returns the standard deviation of the <i>i</i>-th 
        /// <paramref name="data"/> row. 
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// the standard deviation of the <i>i</i>-th row is estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{i,\cdot} = \sqrt{\hat{\mu}_{2,i,\cdot}},\hspace{12pt} i=0,\dots,m-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} \left ( x_{i,j} - \overline{x}_{i,\cdot} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// the variance estimator is corrected for bias
        /// and the <i>i</i>-th standard deviation is evaluated through the coefficient
        /// <latex mode="display">
        /// s_{i,\cdot} = \sqrt{\frac{n}{n-1}} \hat{\sigma}_{i,\cdot}.
        /// </latex>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the standard deviation 
        /// of the <i>j</i>-th 
        /// <paramref name="data"/> column. 
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// the standard deviation of the <i>j</i>-th column is estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{\cdot,j} = \sqrt{\hat{\mu}_{2,\cdot,j}},\hspace{12pt} j=0,\dots,n-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} \left ( x_{i,j} - \overline{x}_{\cdot,j} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>j</i>-th column.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, 
        /// then the variance estimator is corrected for bias
        /// and the <i>j</i>-th standard deviation is evaluated through the coefficient
        /// <latex mode="display">
        /// s_{\cdot,j} = \sqrt{\frac{m}{m-1}} \hat{\sigma}_{\cdot,j}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column standard deviation estimates in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\StandardDeviationExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The standard deviation of each row or column in the specified data, 
        /// eventually adjusted for bias. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.<br/>
        /// -or<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/> 
        /// and the <paramref name="data"/> number of columns 
        /// is less than <c>2</c>.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/> 
        /// and the <paramref name="data"/> number of rows 
        /// is less than <c>2</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Standard_deviation"/>
        public static DoubleMatrix StandardDeviation(
            DoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            DoubleMatrix variances = Variance(
                data, adjustForBias, dataOperation);
            for (int i = 0; i < variances.Count; i++)
            {
                variances[i] = Math.Sqrt(variances[i]);
            }
            return variances;
        }

        /// <inheritdoc cref="Stat.StandardDeviation(DoubleMatrix, bool)"/>
        public static double StandardDeviation(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            return StandardDeviation(data.matrix, adjustForBias);
        }

        /// <inheritdoc cref="Stat.StandardDeviation(DoubleMatrix, bool, DataOperation)"/>
        public static DoubleMatrix StandardDeviation(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return StandardDeviation(data.matrix, adjustForBias, dataOperation);
        }

        #endregion

        #region Sum

        private static DenseMatrixImplementor<ByDimensionSummaryOperator<double>> SumByDimDoubleOperators()
        {
            var operators =
                new DenseMatrixImplementor<ByDimensionSummaryOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] = new ByDimensionSummaryOperator<double>(StatisticOperators.Dense_Sum);
            operators[sparse] = new ByDimensionSummaryOperator<double>(StatisticOperators.Sparse_Sum);

            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionSummaryOperator<double>>
            sumByDimDoubleOperators = SumByDimDoubleOperators();

        private static DenseMatrixImplementor<SummaryOperator<double>> SumDoubleOperators()
        {
            var operators =
                new DenseMatrixImplementor<SummaryOperator<double>>(numberOfStorageSchemes, 1);

            operators[dense] = new SummaryOperator<double>(StatisticOperators.Dense_Sum);

            operators[sparse] = new SummaryOperator<double>(StatisticOperators.Sparse_Sum);

            return operators;
        }

        private static readonly DenseMatrixImplementor<SummaryOperator<double>>
            sumDoubleOperators = SumDoubleOperators();

        /// <summary>
        /// Returns the sum of the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// <para id='0'>
        /// This method returns the sum
        /// of the <paramref name="data"/> entries.
        /// Let us define  
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1,
        /// </latex>
        /// where <latex mode="inline">L</latex> is the 
        /// <paramref name="data"/> length. 
        /// Then the returned value 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \sum_{l=0}^{L-1} x_l.
        /// </latex>        
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the sum of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SumExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The sum of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        public static double Sum(
            DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;
            return sumDoubleOperators[(int)implementor.StorageScheme](implementor);
        }

        /// <summary>
        /// Returns the sum of each row or column in the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dataOperation">A constant to specify if the sum is to be 
        /// computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method returns the sum of <paramref name="data"/>
        /// rows or columns.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector whose length 
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of the returned column exposes the total of the <i>i</i>-th 
        /// <paramref name="data"/> row. 
        /// </para>
        /// <para>  
        /// The sum of the <i>i</i>-th row 
        /// can be represented by the expression
        /// <latex mode="display"> 
        /// \sum_{j=0}^{n-1} x_{i,j}.
        /// </latex>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the total of the <i>j</i>-th 
        /// <paramref name="data"/> column. 
        /// </para>
        /// <para>
        /// The sum of the <i>j</i>-th column 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \sum_{i=0}^{m-1} x_{i,j}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column sums in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SumExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The sum of each row or column in the specified data. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        public static DoubleMatrix Sum(
            DoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            var implementor = data.implementor;
            return new DoubleMatrix(
                sumByDimDoubleOperators[(int)implementor.StorageScheme](implementor, (int)dataOperation));
        }

        /// <inheritdoc cref="Stat.Sum(DoubleMatrix)"/>
        public static double Sum(
            ReadOnlyDoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Sum(data.matrix);
        }

        /// <inheritdoc cref="Stat.Sum(DoubleMatrix, DataOperation)"/>
        public static DoubleMatrix Sum(
            ReadOnlyDoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Sum(data.matrix, dataOperation);
        }

        #endregion

        #region Sum of squared deviations

        private static DenseMatrixImplementor<ByDimensionSummaryOperator<double>> SumOfSquaredDeviationsByDimDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<ByDimensionSummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new ByDimensionSummaryOperator<double>(StatisticOperators.Dense_SumOfSquaredDeviations);
            operators[sparse] = new ByDimensionSummaryOperator<double>(StatisticOperators.Sparse_SumOfSquaredDeviations);
            return operators;
        }

        private static readonly DenseMatrixImplementor<ByDimensionSummaryOperator<double>>
            sumOfSquaredDeviationsByDimDoubleOperators = SumOfSquaredDeviationsByDimDoubleOperators();

        private static DenseMatrixImplementor<SummaryOperator<double>> SumOfSquaredDeviationsDoubleOperators()
        {
            var operators = new DenseMatrixImplementor<SummaryOperator<double>>(numberOfStorageSchemes, 1);
            operators[dense] = new SummaryOperator<double>(StatisticOperators.Dense_SumOfSquaredDeviations);
            operators[sparse] = new SummaryOperator<double>(StatisticOperators.Sparse_SumOfSquaredDeviations);
            return operators;
        }

        private static readonly DenseMatrixImplementor<SummaryOperator<double>>
            sumOfSquaredDeviationsDoubleOperators = SumOfSquaredDeviationsDoubleOperators();

        /// <summary>
        /// Returns the sum of squared deviations of the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// <para id='0'>
        /// This method returns the sum of squared deviations 
        /// of <paramref name="data"/> entries from their arithmetic mean.
        /// Let us define  
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1,
        /// </latex>
        /// where <latex mode="inline">L</latex> is the 
        /// <paramref name="data"/> length. 
        /// Then the returned value 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \sum_{l=0}^{L-1} \left ( x_l - \overline{x} \right )^2
        /// </latex>        
        /// where
        /// <latex mode="display">
        /// \overline{x}=\frac{1}{L}\sum_{l=0}^{L-1}  x_l 
        /// </latex>
        /// is the <paramref name="data"/> arithmetic mean.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the sum of squared deviations of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SumOfSquaredDeviationsExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The sum of squared deviations of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <b>null</b>.
        /// </exception>
        public static double SumOfSquaredDeviations(
            DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;

            return sumOfSquaredDeviationsDoubleOperators[(int)implementor.StorageScheme](implementor);
        }

        /// <summary>
        /// Returns the sum of squared deviations of each row or column 
        /// in the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dataOperation">A constant to specify if the sum of squared deviations 
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method returns the sum of squared deviations of
        /// rows or columns of <paramref name="data"/> from their arithmetic means.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector
        /// whose length
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of such column returns the sum of squared 
        /// deviations of the <i>i</i>-th 
        /// <paramref name="data"/> row. 
        /// </para>
        /// <para>  
        /// The sum of squared deviations of the <i>i</i>-th row 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \sum_{j=0}^{n-1} \left ( x_{i,j} - \overline{x}_{i,\cdot} \right )^2
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{i,\cdot} = \frac{1}{n}\sum_{j=0}^{n-1} x_{i,j} 
        /// </latex>
        /// is the  arithmetic mean of the <i>i</i>-th row.
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the 
        /// sum of squared deviations of the <i>j</i>-th 
        /// <paramref name="data"/> column. 
        /// </para>
        /// <para>
        /// The sum of squared deviations of the <i>j</i>-th column 
        /// can be represented by the expression
        /// <latex mode="display">
        /// \sum_{i=0}^{m-1} \left ( x_{i,j} - \overline{x}_{\cdot,j} \right )^2
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \overline{x}_{\cdot,j} = \frac{1}{m}\sum_{i=0}^{m-1} x_{i,j} 
        /// </latex>
        /// is the  arithmetic mean of the <i>j</i>-th column.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column sums of squared deviations in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\SumOfSquaredDeviationsExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The sum of squared deviations of each row or column in the specified data. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.
        /// </exception>
        public static DoubleMatrix SumOfSquaredDeviations(
            DoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            var implementor = data.implementor;
            return new DoubleMatrix(
                sumOfSquaredDeviationsByDimDoubleOperators[(int)implementor.StorageScheme]
                (implementor, (int)dataOperation));
        }

        /// <inheritdoc cref="Stat.SumOfSquaredDeviations(DoubleMatrix)"/>
        public static double SumOfSquaredDeviations(
            ReadOnlyDoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return SumOfSquaredDeviations(data.matrix);
        }

        /// <inheritdoc cref="Stat.SumOfSquaredDeviations(DoubleMatrix, DataOperation)"/>
        public static DoubleMatrix SumOfSquaredDeviations(
            ReadOnlyDoubleMatrix data,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return SumOfSquaredDeviations(data.matrix, dataOperation);
        }

        #endregion

        #region Variance

        /// <summary>
        /// Returns the variance of the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the variance 
        /// is adjusted for bias.
        /// </param>
        /// <remarks>
        /// <para id='0'>
        /// The variance of a random variable <latex mode="inline">X</latex> 
        /// can be defined as follows:
        /// <latex mode="display">
        /// \sigma^2=\mu_2
        /// </latex>        
        /// where
        /// <latex mode="display">
        /// \mu_r = E \left[ \left( X - E \left[ X \right] \right)^r \right]
        /// </latex>        
        /// is the <latex mode="inline">X</latex> central moment 
        /// of order <latex mode="inline">r</latex>.
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// the variance is estimated through the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}^2=\hat{\mu}_2,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_r=\frac{1}{L}\sum_{l=0}^{L-1} \left ( x_l - \overline{x} \right )^r
        /// </latex>
        /// is the sample central moment of order 
        /// <latex mode="inline">r</latex>,
        /// <latex mode="inline">L</latex> is the <paramref name="data"/>
        /// length and 
        /// <latex mode="display">
        /// x_l=\mathit{data}[l],\hspace{12pt} l=0,\dots,L-1.
        /// </latex>
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, 
        /// then the estimator is corrected for bias
        /// and the variance is evaluated through the coefficient
        /// <latex mode="display">
        /// s^2=\frac{L}{L-1} \hat{\sigma}^2.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, the variance of a data matrix is computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\VarianceExample1.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The variance of the specified data, eventually adjusted for bias.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="adjustForBias"/> is <c>true</c> and the number of entries
        /// in <paramref name="data"/> is less than <c>2</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Variance"/>
        public static double Variance(
            DoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            var implementor = data.implementor;

            double n = data.Count;

            if (adjustForBias && (n < 2.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED"));
            }

            double numerator =
                sumOfSquaredDeviationsDoubleOperators[(int)implementor.StorageScheme](implementor);

            double denominator = adjustForBias ? n - 1.0 : n;

            return numerator / denominator;
        }

        /// <summary>
        /// Returns the variance of each row or column 
        /// in the specified data, eventually adjusted for bias.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="adjustForBias">If set to <c>true</c> signals that the variance 
        /// is adjusted for bias.
        /// </param>
        /// <param name="dataOperation">A constant to specify if the variance 
        /// is to be computed for rows or columns.
        /// </param>
        /// <remarks>
        /// <inheritdoc cref="Stat.Variance(DoubleMatrix,bool)" 
        /// path="para[@id='0']"/>
        /// <para>
        /// By interpreting rows or columns of <paramref name="data"/> as samples drawn from
        /// random variables, this method returns the variance estimates of such variables.
        /// Let <latex mode="inline">m</latex> and  
        /// <latex mode="inline">n</latex> be the <paramref name="data"/>
        /// number of rows and columns, respectively, and define
        /// <latex mode="display">
        /// x_{i,j}=\mathit{data}[i,j],\hspace{12pt} i=0,\dots,m-1,\hspace{12pt} j=0,\dots,n-1.
        /// </latex>
        /// </para>
        /// <para><b>Operating on rows</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/>,
        /// then the method returns a column vector
        /// whose length
        /// equals the number of rows of <paramref name="data"/>.
        /// The <i>i</i>-th entry of such column returns the variance of the <i>i</i>-th 
        /// <paramref name="data"/> row. 
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, then 
        /// the variance of the <i>i</i>-th row is estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{i,\cdot}^2 = \hat{\mu}_{2,i,\cdot},\hspace{12pt} i=0,\dots,m-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,i,\cdot}=\frac{1}{n}\sum_{j=0}^{n-1} \left ( x_{i,j} - \overline{x}_{i,\cdot} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>i</i>-th row.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, then 
        /// the estimator is corrected for bias
        /// and the <i>i</i>-th variance is evaluated through the coefficient
        /// <latex mode="display">
        /// s_{i,\cdot}^2= \frac{n}{n-1} \hat{\sigma}_{i,\cdot}^2.
        /// </latex>
        /// </para>
        /// <para><b>Operating on columns</b></para>
        /// <para>
        /// If <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/>,
        /// then the method returns a row vector 
        /// whose length is the <paramref name="data"/> number of columns.
        /// The <i>j</i>-th entry of the returned row exposes the variance of the <i>j</i>-th 
        /// <paramref name="data"/> column. 
        /// </para>
        /// <para>
        /// If <paramref name="adjustForBias"/> is set to <c>false</c>, 
        /// then the variance of the <i>j</i>-th column is estimated through 
        /// the coefficient
        /// <latex mode="display">
        /// \hat{\sigma}_{\cdot,j}^2=\hat{\mu}_{2,\cdot,j},\hspace{12pt} j=0,\dots,n-1,
        /// </latex>
        /// where
        /// <latex mode="display">
        /// \hat{\mu}_{r,\cdot,j}=\frac{1}{m}\sum_{i=0}^{m-1} \left ( x_{i,j} - \overline{x}_{\cdot,j} \right )^r
        /// </latex>
        /// is the sample <latex mode="inline">r</latex>-th central moment of the 
        /// <i>j</i>-th column.
        /// </para>
        /// <para>
        /// Such estimator is biased. If <paramref name="adjustForBias"/> is set to <c>true</c>, 
        /// then the estimator is corrected for bias
        /// and the <i>j</i>-th variance is evaluated through the coefficient
        /// <latex mode="display">
        /// s_{\cdot,j}^2= \frac{m}{m-1} \hat{\sigma}_{\cdot,j}^2.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, row and column variance estimates in a data matrix 
        /// are computed.
        /// </para>
        /// <para>
        /// <code source="..\Novacta.Analytics.CodeExamples\VarianceExample0.cs.txt" 
        /// language="cs" />
        /// </para>
        /// </example>
        /// <returns>
        /// The variance of each row or column in the specified data, 
        /// eventually adjusted for bias. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dataOperation"/> is not a field of 
        /// <see cref="DataOperation"/>.<br/>
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnRows"/> 
        /// and the <paramref name="data"/> number of columns 
        /// is less than <c>2</c>.<br/> 
        /// -or-<br/>
        /// <paramref name="adjustForBias"/> is <c>true</c>,
        /// <paramref name="dataOperation"/> is <see cref="DataOperation.OnColumns"/> 
        /// and the <paramref name="data"/> number of rows 
        /// is less than <c>2</c>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Variance"/>
        public static DoubleMatrix Variance(
            DoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            if ((DataOperation.OnColumns != dataOperation)
                &&
                (DataOperation.OnRows != dataOperation))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_NOT_FIELD_OF_DATA_OPERATION"),
                    nameof(dataOperation));
            }

            double n = (DataOperation.OnColumns == dataOperation) ?
                data.NumberOfRows : data.NumberOfColumns;

            if (adjustForBias && (n < 2.0))
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED"));
            }

            var implementor = data.implementor;
            var numerator = new DoubleMatrix(sumOfSquaredDeviationsByDimDoubleOperators
                [(int)implementor.StorageScheme](implementor, (int)dataOperation));

            double denominator = adjustForBias ? n - 1.0 : n;

            return numerator / denominator;
        }

        /// <inheritdoc cref="Stat.Variance(DoubleMatrix, bool)"/>
        public static double Variance(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Variance(data.matrix, adjustForBias);
        }

        /// <inheritdoc cref="Stat.Variance(DoubleMatrix, bool, DataOperation)"/>
        public static DoubleMatrix Variance(
            ReadOnlyDoubleMatrix data,
            bool adjustForBias,
            DataOperation dataOperation)
        {
            ArgumentNullException.ThrowIfNull(data);

            return Variance(data.matrix, adjustForBias, dataOperation);
        }

        #endregion
    }
}