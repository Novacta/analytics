// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// correspondence resulting
    /// from the analysis of the 
    /// following contingency table
    /// <para /> 
    ///               Period Comma   OtherMarks <para />
    /// Rousseau      7836   13112   6026       <para />
    /// Chateaubriand 53655  102383  42413      <para />
    /// Hugo          115615 184541  59226      <para />
    /// Zola          161926 340479  62754      <para />
    /// Proust        38117  105101  12670      <para />
    /// Giraudoux     46371  58367   14229      <para />
    /// </summary>
    class TestableCorrespondence00 : TestableCorrespondence
    {
        static readonly DoubleMatrix data;

        static TestableCorrespondence00()
        {
            data = DoubleMatrix.Dense(
                new double[6, 3]
                {
                    {   7836,   13112,   6026 },
                    {  53655,  102383,  42413 },
                    { 115615,  184541,  59226 },
                    { 161926,  340479,  62754 },
                    {  38117,  105101,  12670 },
                    {  46371,   58367,  14229 }
                });

            var rowNames = new string[6]
                {
                    "Rousseau",
                    "Chateaubriand",
                    "Hugo",
                    "Zola",
                    "Proust",
                    "Giraudoux"
                };

            for (int i = 0; i < data.NumberOfRows; i++)
            {
                data.SetRowName(i, rowNames[i]);
            }

            var columnNames = new string[3]
                {
                    "Period",
                    "Comma",
                    "OtherMarks"
                };

            for (int j = 0; j < data.NumberOfColumns; j++)
            {
                data.SetColumnName(j, columnNames[j]);
            }
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableCorrespondence00" /> class.
        /// </summary>
        TestableCorrespondence00() : base(
            correspondence: Correspondence.Analyze(
                data),
            rowProfiles: TestableRowProfiles00.Get(),
            columnProfiles: TestableColumnProfiles00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableCorrespondence00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableCorrespondence00"/> class.</returns>
        public static TestableCorrespondence00 Get()
        {
            return new TestableCorrespondence00();
        }
    }
}
