// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.IO;

namespace Novacta.Analytics.Tests.TestableItems.GDA
{
    /// <summary>
    /// Provides methods to test the
    /// multiple correspondence resulting
    /// from the analysis of the 
    /// following categorical data set
    /// <para /> 
    /// COLOR  NUMBER    <para />
    /// Red    Negative  <para />
    /// Green  Zero      <para />
    /// Red    Negative  <para />
    /// Blue   Negative  <para />
    /// Blue   Positive  <para />
    /// </summary>
    class TestableMultipleCorrespondence00 : TestableMultipleCorrespondence
    {
        static readonly CategoricalDataSet dataSet;

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1810:Initialize reference type static fields inline",
            Justification = "Performance is not a concern.")]
        static TestableMultipleCorrespondence00()
        {
            // Create a data stream.
            string[] data = new string[6] {
            "COLOR,NUMBER",
            "Red,Negative",
            "Green,Zero",
            "Red,Negative",
            "Blue,Negative",
            "Blue,Positive" };

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < data.Length; i++)
            {
                writer.WriteLine(data[i].ToCharArray());
                writer.Flush();
            }
            stream.Position = 0;

            // Encode the categorical data set
            StreamReader streamReader = new StreamReader(stream);
            char columnDelimiter = ',';
            IndexCollection extractedColumns = IndexCollection.Range(0, 1);
            bool firstLineContainsColumnHeaders = true;
            dataSet = CategoricalDataSet.Encode(
                streamReader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsColumnHeaders);
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TestableMultipleCorrespondence00" /> class.
        /// </summary>
        TestableMultipleCorrespondence00() : base(
            multipleCorrespondence: MultipleCorrespondence.Analyze(
                dataSet),
            individuals: TestableIndividuals00.Get(),
            categories: TestableCategories00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableMultipleCorrespondence00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableMultipleCorrespondence00"/> class.</returns>
        public static TestableMultipleCorrespondence00 Get()
        {
            return new TestableMultipleCorrespondence00();
        }
    }
}
