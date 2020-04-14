// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems.Correlation;
using Novacta.Analytics.Tests.TestableItems.Covariance;
using Novacta.Analytics.Tests.TestableItems.Kurtosis;
using Novacta.Analytics.Tests.TestableItems.Max;
using Novacta.Analytics.Tests.TestableItems.Mean;
using Novacta.Analytics.Tests.TestableItems.Min;
using Novacta.Analytics.Tests.TestableItems.Quantile;
using Novacta.Analytics.Tests.TestableItems.Skewness;
using Novacta.Analytics.Tests.TestableItems.Sort;
using Novacta.Analytics.Tests.TestableItems.SortIndex;
using Novacta.Analytics.Tests.TestableItems.StandardDeviation;
using Novacta.Analytics.Tests.TestableItems.Sum;
using Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations;
using Novacta.Analytics.Tests.TestableItems.Variance;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class StatTests
    {
        [TestMethod()]
        public void CorrelationTest()
        {
            #region AlongDimension

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullCorrelation.Get(
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullCorrelation.Get(
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.NotAdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldCorrelation.Get());

            #region OnRows

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsDataIsColumnCorrelation.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsCorrelation00.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsDataIsRowCorrelation.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsCorrelation00.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void CovarianceTest()
        {
            #region AlongDimension

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullCovariance.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullCovariance.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullCovariance.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullCovariance.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldCovariance.Get(
                    adjustForBias: true));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldCovariance.Get(
                    adjustForBias: false));

            #region OnRows

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsDataIsColumnAdjustedCovariance.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsDataIsColumnUnadjustedCovariance.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsUnadjustableCovariance.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedCovariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsNotAdjustedCovariance00.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsDataIsRowAdjustedCovariance.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsDataIsRowUnadjustedCovariance.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsUnadjustableCovariance.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedCovariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedCovariance00.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void KurtosisTest()
        {
            #region OnAll 

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullKurtosis.Get(adjustForBias: true));
            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullKurtosis.Get(adjustForBias: false));

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllUnadjustableKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllDataIsScalarAdjustedKurtosis.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnAllDataIsScalarUnadjustedKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedKurtosis00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedKurtosis01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedKurtosis02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedKurtosis00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedKurtosis01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedKurtosis02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullKurtosis.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullKurtosis.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullKurtosis.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullKurtosis.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldKurtosis.Get(
                    adjustForBias: true));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldKurtosis.Get(
                    adjustForBias: false));

            #region OnRows

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsDataIsColumnAdjustedKurtosis.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsDataIsColumnUnadjustedKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsUnadjustableKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedKurtosis00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedKurtosis01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedKurtosis02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsNotAdjustedKurtosis00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedKurtosis01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedKurtosis02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsDataIsRowAdjustedKurtosis.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsDataIsRowUnadjustedKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsUnadjustableKurtosis.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedKurtosis00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedKurtosis01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedKurtosis02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedKurtosis00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedKurtosis01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedKurtosis02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void MaxTest()
        {
            #region OnAll 

            ExtremumOperationTest.DataIsNull(
                OnAllDataIsNullMax.Get());

            ExtremumOperationTest.Succeed(
                OnAllDataIsScalarMax.Get());

            ExtremumOperationTest.Succeed(OnAllMax00.Get());
            ExtremumOperationTest.Succeed(OnAllMax01.Get());
            ExtremumOperationTest.Succeed(OnAllMax02.Get());
            ExtremumOperationTest.Succeed(OnAllMax03.Get());
            ExtremumOperationTest.Succeed(OnAllMax04.Get());
            ExtremumOperationTest.Succeed(OnAllMax05.Get());
            ExtremumOperationTest.Succeed(OnAllMax06.Get());

            #endregion

            #region AlongDimension

            ExtremumOperationTest.DataIsNull(
                AlongDimensionDataIsNullMax.Get(
                    dataOperation: DataOperation.OnRows));

            ExtremumOperationTest.DataIsNull(
                AlongDimensionDataIsNullMax.Get(
                    dataOperation: DataOperation.OnColumns));

            ExtremumOperationTest.Fail(
                AlongDimensionNotDataOperationFieldMax.Get());

            #region OnRows

            ExtremumOperationTest.Succeed(
                OnRowsDataIsColumnMax.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax00.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax01.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax02.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax03.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax04.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax05.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMax06.Get());

            #endregion

            #region OnColumns

            ExtremumOperationTest.Succeed(
                OnColumnsDataIsRowMax.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax00.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax01.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax02.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax03.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax04.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax05.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMax06.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void MeanTest()
        {
            #region OnAll 

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                OnAllDataIsNullMean.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnAllDataIsScalarMean.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllMean00.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllMean01.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllMean02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullMean.Get(
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullMean.Get(
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.NotAdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldMean.Get());

            #region OnRows

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsDataIsColumnMean.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsMean00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsMean01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsMean02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsDataIsRowMean.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsMean00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsMean01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsMean02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void MinTest()
        {
            #region OnAll 

            ExtremumOperationTest.DataIsNull(
                OnAllDataIsNullMin.Get());

            ExtremumOperationTest.Succeed(
                OnAllDataIsScalarMin.Get());

            ExtremumOperationTest.Succeed(OnAllMin00.Get());
            ExtremumOperationTest.Succeed(OnAllMin01.Get());
            ExtremumOperationTest.Succeed(OnAllMin02.Get());
            ExtremumOperationTest.Succeed(OnAllMin03.Get());
            ExtremumOperationTest.Succeed(OnAllMin04.Get());
            ExtremumOperationTest.Succeed(OnAllMin05.Get());
            ExtremumOperationTest.Succeed(OnAllMin06.Get());

            #endregion

            #region AlongDimension

            ExtremumOperationTest.DataIsNull(
                AlongDimensionDataIsNullMin.Get(
                    dataOperation: DataOperation.OnRows));

            ExtremumOperationTest.DataIsNull(
                AlongDimensionDataIsNullMin.Get(
                    dataOperation: DataOperation.OnColumns));

            ExtremumOperationTest.Fail(
                AlongDimensionNotDataOperationFieldMin.Get());

            #region OnRows

            ExtremumOperationTest.Succeed(
                OnRowsDataIsColumnMin.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin00.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin01.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin02.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin03.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin04.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin05.Get());

            ExtremumOperationTest.Succeed(
                OnRowsMin06.Get());

            #endregion

            #region OnColumns

            ExtremumOperationTest.Succeed(
                OnColumnsDataIsRowMin.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin00.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin01.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin02.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin03.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin04.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin05.Get());

            ExtremumOperationTest.Succeed(
                OnColumnsMin06.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void QuantilesTest()
        {
            #region OnAll 

            QuantileOperationTest.DataIsNull(
                OnAllDataIsNullQuantile.Get());

            QuantileOperationTest.ProbabilitiesIsNull(
                OnAllProbabilitiesIsNullQuantile.Get());

            QuantileOperationTest.Fail(OnAllOutOfRangeProbabilitiesQuantile00.Get());
            QuantileOperationTest.Fail(OnAllOutOfRangeProbabilitiesQuantile01.Get());

            QuantileOperationTest.Succeed(
                OnAllDataIsScalarQuantile.Get());

            QuantileOperationTest.Succeed(OnAllQuantile00.Get());
            QuantileOperationTest.Succeed(OnAllQuantile01.Get());

            #endregion

            #region AlongDimension

            QuantileOperationTest.DataIsNull(
                AlongDimensionDataIsNullQuantile.Get(
                    dataOperation: DataOperation.OnRows));

            QuantileOperationTest.DataIsNull(
                AlongDimensionDataIsNullQuantile.Get(
                    dataOperation: DataOperation.OnColumns));

            QuantileOperationTest.ProbabilitiesIsNull(
                AlongDimensionProbabilitiesIsNullQuantile.Get(
                    dataOperation: DataOperation.OnRows));

            QuantileOperationTest.ProbabilitiesIsNull(
                AlongDimensionProbabilitiesIsNullQuantile.Get(
                    dataOperation: DataOperation.OnColumns));

            QuantileOperationTest.Fail(
                AlongDimensionOutOfRangeProbabilitiesQuantile00.Get(
                    dataOperation: DataOperation.OnRows));
            QuantileOperationTest.Fail(
                AlongDimensionOutOfRangeProbabilitiesQuantile01.Get(
                    dataOperation: DataOperation.OnRows));

            QuantileOperationTest.Fail(
                AlongDimensionOutOfRangeProbabilitiesQuantile00.Get(
                    dataOperation: DataOperation.OnColumns));
            QuantileOperationTest.Fail(
                AlongDimensionOutOfRangeProbabilitiesQuantile01.Get(
                    dataOperation: DataOperation.OnColumns));

            QuantileOperationTest.Fail(
                AlongDimensionNotDataOperationFieldQuantile.Get());

            #region OnRows

            QuantileOperationTest.Succeed(
                OnRowsDataIsColumnQuantile.Get());

            QuantileOperationTest.Succeed(
                OnRowsQuantile00.Get());

            QuantileOperationTest.Succeed(
                OnRowsQuantile01.Get());

            #endregion

            #region OnColumns

            QuantileOperationTest.Succeed(
                OnColumnsDataIsRowQuantile.Get());

            QuantileOperationTest.Succeed(
                OnColumnsQuantile00.Get());

            QuantileOperationTest.Succeed(
                OnColumnsQuantile01.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void SortTest()
        {
            SortingOperationTest.Sort.DataIsNull(
                DataIsNullSort.Get(SortDirection.Ascending));

            SortingOperationTest.Sort.DataIsNull(
               DataIsNullSort.Get(SortDirection.Descending));

            SortingOperationTest.Sort.Fail(
                NotSortDirectionFieldSort.Get());

            SortingOperationTest.Sort.Succeed(Sort00.Get());
            SortingOperationTest.Sort.Succeed(Sort01.Get());
            SortingOperationTest.Sort.Succeed(Sort02.Get());
            SortingOperationTest.Sort.Succeed(Sort03.Get());
        }

        [TestMethod()]
        public void SortIndexTest()
        {
            SortingOperationTest.SortIndex.DataIsNull(
                DataIsNullSortIndex.Get(SortDirection.Ascending));

            SortingOperationTest.SortIndex.DataIsNull(
               DataIsNullSortIndex.Get(SortDirection.Descending));

            SortingOperationTest.SortIndex.Fail(
                NotSortDirectionFieldSortIndex.Get());

            SortingOperationTest.SortIndex.Succeed(SortIndex00.Get());
            SortingOperationTest.SortIndex.Succeed(SortIndex01.Get());
            SortingOperationTest.SortIndex.Succeed(SortIndex02.Get());
            SortingOperationTest.SortIndex.Succeed(SortIndex03.Get());
        }

        [TestMethod()]
        public void SumTest()
        {
            #region OnAll 

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                OnAllDataIsNullSum.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnAllDataIsScalarSum.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSum00.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSum01.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSum02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSum.Get(
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSum.Get(
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.NotAdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldSum.Get());

            #region OnRows

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsDataIsColumnSum.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSum00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSum01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSum02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsDataIsRowSum.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSum00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSum01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSum02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void SkewnessTest()
        {
            #region OnAll 

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullSkewness.Get(adjustForBias: true));
            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullSkewness.Get(adjustForBias: false));

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllUnadjustableSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllDataIsScalarAdjustedSkewness.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnAllDataIsScalarUnadjustedSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedSkewness00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedSkewness01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedSkewness02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedSkewness00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedSkewness01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedSkewness02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSkewness.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullSkewness.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSkewness.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullSkewness.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldSkewness.Get(
                    adjustForBias: true));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldSkewness.Get(
                    adjustForBias: false));

            #region OnRows

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsDataIsColumnAdjustedSkewness.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsDataIsColumnUnadjustedSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsUnadjustableSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedSkewness00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedSkewness01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedSkewness02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsNotAdjustedSkewness00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedSkewness01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedSkewness02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsDataIsRowAdjustedSkewness.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsDataIsRowUnadjustedSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsUnadjustableSkewness.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedSkewness00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedSkewness01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedSkewness02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedSkewness00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedSkewness01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedSkewness02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            #region OnAll 

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullStandardDeviation.Get(adjustForBias: true));
            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullStandardDeviation.Get(adjustForBias: false));

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllUnadjustableStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllDataIsScalarAdjustedStandardDeviation.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnAllDataIsScalarUnadjustedStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedStandardDeviation00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedStandardDeviation01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedStandardDeviation02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedStandardDeviation00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedStandardDeviation01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedStandardDeviation02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullStandardDeviation.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullStandardDeviation.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullStandardDeviation.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullStandardDeviation.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldStandardDeviation.Get(
                    adjustForBias: true));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldStandardDeviation.Get(
                    adjustForBias: false));

            #region OnRows

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsDataIsColumnAdjustedStandardDeviation.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsDataIsColumnUnadjustedStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsUnadjustableStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedStandardDeviation00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedStandardDeviation01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedStandardDeviation02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsNotAdjustedStandardDeviation00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedStandardDeviation01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedStandardDeviation02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsDataIsRowAdjustedStandardDeviation.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsDataIsRowUnadjustedStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsUnadjustableStandardDeviation.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedStandardDeviation00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedStandardDeviation01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedStandardDeviation02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedStandardDeviation00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedStandardDeviation01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedStandardDeviation02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void SumOfSquaredDeviationsTest()
        {
            #region OnAll 

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                OnAllDataIsNullSumOfSquaredDeviations.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnAllDataIsScalarSumOfSquaredDeviations.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSumOfSquaredDeviations00.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSumOfSquaredDeviations01.Get());
            SummaryOperationTest.NotAdjustableForBias.Succeed(OnAllSumOfSquaredDeviations02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSumOfSquaredDeviations.Get(
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.NotAdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullSumOfSquaredDeviations.Get(
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.NotAdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldSumOfSquaredDeviations.Get());

            #region OnRows

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsDataIsColumnSumOfSquaredDeviations.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSumOfSquaredDeviations00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSumOfSquaredDeviations01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnRowsSumOfSquaredDeviations02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsDataIsRowSumOfSquaredDeviations.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSumOfSquaredDeviations00.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSumOfSquaredDeviations01.Get());

            SummaryOperationTest.NotAdjustableForBias.Succeed(
                OnColumnsSumOfSquaredDeviations02.Get());

            #endregion

            #endregion
        }

        [TestMethod()]
        public void VarianceTest()
        {
            #region OnAll 

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullVariance.Get(adjustForBias: true));
            SummaryOperationTest.AdjustableForBias.DataIsNull(
                OnAllDataIsNullVariance.Get(adjustForBias: false));

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllUnadjustableVariance.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnAllDataIsScalarAdjustedVariance.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnAllDataIsScalarUnadjustedVariance.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedVariance00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedVariance01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllAdjustedVariance02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedVariance00.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedVariance01.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(OnAllNotAdjustedVariance02.Get());

            #endregion

            #region AlongDimension

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullVariance.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullVariance.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnRows));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
                AlongDimensionDataIsNullVariance.Get(
                    adjustForBias: true,
                    dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.DataIsNull(
               AlongDimensionDataIsNullVariance.Get(
                   adjustForBias: false,
                   dataOperation: DataOperation.OnColumns));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldVariance.Get(
                    adjustForBias: true));

            SummaryOperationTest.AdjustableForBias.Fail(
                AlongDimensionNotDataOperationFieldVariance.Get(
                    adjustForBias: false));

            #region OnRows

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsDataIsColumnAdjustedVariance.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsDataIsColumnUnadjustedVariance.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnRowsUnadjustableVariance.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedVariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedVariance01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsAdjustedVariance02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnRowsNotAdjustedVariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedVariance01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
               OnRowsNotAdjustedVariance02.Get());

            #endregion

            #region OnColumns

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsDataIsRowAdjustedVariance.Get());
            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsDataIsRowUnadjustedVariance.Get());

            SummaryOperationTest.AdjustableForBias.Fail(
                OnColumnsUnadjustableVariance.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedVariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedVariance01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsAdjustedVariance02.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedVariance00.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedVariance01.Get());

            SummaryOperationTest.AdjustableForBias.Succeed(
                OnColumnsNotAdjustedVariance02.Get());

            #endregion

            #endregion
        }
    }
}
