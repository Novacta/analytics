// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="CategoricalDataSet"/> instances.
    /// </summary>
    static class CategoricalDataSetTest
    {
        #region Categorize

        /// <summary>
        /// Provides methods to test that the
        /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
        /// string, char, IndexCollection, bool, int, IFormatProvider)"/>
        /// and
        /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
        /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
        /// methods have
        /// been properly implemented.
        /// </summary>
        public static class Categorize
        {
            /// <summary>
            /// Provides methods to test that the
            /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
            /// string, char, IndexCollection, bool, int, IFormatProvider)"/>
            /// and
            /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
            /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
            /// methods terminate successfully when expected.
            public static class Succeed
            {
                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// string, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data set contains
                /// no variable names.
                /// </summary>
                public static void FromPathWithoutVariableNames()
                {
                    // This test analyze the UCI IRIS data set.
                    // @misc{,
                    //    author = "M. LICHMAN",
                    //    year = "2013",
                    //    title = "{UCI} Machine Learning Repository",
                    //    URL = "http://archive.ics.uci.edu/ml",
                    //    institution = "University of California, Irvine, School of Information and Computer Sciences" }

                    var path = "Data" + Path.DirectorySeparatorChar + "iris-all.csv";

                    // Identify the special categorizers
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(0, 3);
                    bool firstLineContainsColumnHeaders = false;
                    int targetColumn = 4;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                        path,
                        columnDelimiter,
                        numericalColumns,
                        firstLineContainsColumnHeaders,
                        targetColumn,
                        provider);

                    //| Discretization into bins of size 0
                    //|
                    //| Discretization of Attribute: "sepal-length"
                    //|
                    //|Bin0: (-INF, 5.55)
                    //|Bin1: [5.55,6.15)
                    //|Bin2: [6.15,+INF)
                    Assert.AreEqual("]-Inf, 5.55]", specialCategorizers[0]("5.54", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]-Inf, 5.55]", specialCategorizers[0]("5.55", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]5.55, 6.15]", specialCategorizers[0]("5.56", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]5.55, 6.15]", specialCategorizers[0]("6.15", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]5.55, 6.15]", specialCategorizers[0]("6.14999999999995", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]6.15, Inf[", specialCategorizers[0]("6.15000000000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]6.15, Inf[", specialCategorizers[0]("7.21", CultureInfo.InvariantCulture));

                    //|Discretization of Attribute: "sepal-width"
                    //|
                    //|Bin0: (-INF, 2.95)
                    //|Bin1: [2.95,3.35)
                    //|Bin2: [3.35,+INF)
                    Assert.AreEqual("]-Inf, 2.95]", specialCategorizers[1]("2.94", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]-Inf, 2.95]", specialCategorizers[1]("2.95", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]2.95, 3.35]", specialCategorizers[1]("2.9500000000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]2.95, 3.35]", specialCategorizers[1]("3.35", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]3.35, Inf[", specialCategorizers[1]("3.35000000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]3.35, Inf[", specialCategorizers[1]("7.21", CultureInfo.InvariantCulture));

                    //|Discretization of Attribute: "petal-length"
                    //|
                    //|Bin0: (-INF, 2.45)
                    //|Bin1: [2.45,4.75)
                    //|Bin2: [4.75,+INF)
                    Assert.AreEqual("]-Inf, 2.45]", specialCategorizers[2]("2.4499999999", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]-Inf, 2.45]", specialCategorizers[2]("2.45", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]2.45, 4.75]", specialCategorizers[2]("2.450000000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]2.45, 4.75]", specialCategorizers[2]("4.75", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]4.75, Inf[", specialCategorizers[2]("4.7500000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]4.75, Inf[", specialCategorizers[2]("7.21", CultureInfo.InvariantCulture));

                    //|Discretization of Attribute: "petal-width"
                    //|
                    //|Bin0: (-INF, 0.8)
                    //|Bin1: [0.8,1.75)
                    //|Bin2: [1.75,+INF)
                    Assert.AreEqual("]-Inf, 0.8]", specialCategorizers[3](".7999999", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]-Inf, 0.8]", specialCategorizers[3](".8", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]0.8, 1.75]", specialCategorizers[3](".8000000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]0.8, 1.75]", specialCategorizers[3]("1.75", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]1.75, Inf[", specialCategorizers[3]("1.7500000001", CultureInfo.InvariantCulture));
                    Assert.AreEqual("]1.75, Inf[", specialCategorizers[3]("7.21", CultureInfo.InvariantCulture));
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// string, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data set contains
                /// variable names.
                /// </summary>
                public static void FromPathWithVariableNames()
                {
                    var path = "Data" + Path.DirectorySeparatorChar + "elomaa-rousu.csv";

                    // Create a data stream.
                    // This stream contains the example discussed in
                    // @ARTICLE{ ,
                    //          author={ ELOMAA, T. and ROUSU, J.},
                    //          title={ General and Efficient Multi Splitting of Numerical Attributes},
                    //          year ={ 1999 },
                    //          publisher={ KLUWER Academic Publishers },
                    //          journal={ Machine Learning },
                    //          issue={ 3 },
                    //          pages={ 201-244 },
                    //          volume={ 36 },
                    //          URL={ http://periodici.caspur.it/cgi-bin/sciserv.pl?collection=journals&journal=08856125&issue=v36i0003&article=201_gaemona },
                    //}
                    const int numberOfInstances = 27;
                    string[] data = new string[numberOfInstances + 1] {
                        "NUMERICAL,TARGET",
                        "0,A",
                        "0,A",
                        "0,A",
                        "1,B",
                        "1,B",
                        "1,B",
                        "1,B",
                        "2,B",
                        "2,B",
                        "3,C",
                        "3,C",
                        "3,C",
                        "4,B",
                        "4,B",
                        "4,B",
                        "4,C",
                        "5,A",
                        "5,A",
                        "6,A",
                        "7,C",
                        "7,C",
                        "7,C",
                        "8,C",
                        "8,C",
                        "9,C",
                        "9,C",
                        "9,C" };

                    // Identify the special categorizer for variable NUMERICAL
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(0, 0);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 1;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                        path,
                        columnDelimiter,
                        numericalColumns,
                        firstLineContainsColumnHeaders,
                        targetColumn,
                        provider);

                    // Encode the categorical data set using the special categorizer
                    IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        path,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        provider);

                    // Expected dataset is:

                    // NUMERICAL,TARGET,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    CategoricalVariable numerical = new CategoricalVariable("NUMERICAL")
                    {
                        { 0, "]-Inf, 2.5]" },
                        { 1, "]2.5, Inf[" }
                    };
                    numerical.SetAsReadOnly();

                    CategoricalVariable number = new CategoricalVariable("TARGET")
                    {
                        { 0, "A" },
                        { 1, "B" },
                        { 2, "C" }
                    };
                    number.SetAsReadOnly();

                    List<CategoricalVariable> expectedVariables =
                        new List<CategoricalVariable>() { numerical, number };

                    DoubleMatrix expectedData = DoubleMatrix.Dense(numberOfInstances, 2);

                    expectedData.SetColumnName(0, "NUMERICAL");
                    expectedData.SetColumnName(1, "TARGET");

                    double targetCode;
                    for (int i = 1; i < data.Length; i++)
                    {
                        var tokens = data[i].Split(',');
                        var targetLabel = tokens[1];
                        switch (targetLabel)
                        {
                            case "A":
                                targetCode = 0.0;
                                break;
                            case "B":
                                targetCode = 1.0;
                                break;
                            case "C":
                                targetCode = 2.0;
                                break;
                            default:
                                throw new Exception("Unrecognized target label.");
                        }
                        expectedData[i - 1, 1] = targetCode;
                    }

                    for (int i = 9; i < expectedData.NumberOfRows; i++)
                    {
                        expectedData[i, 0] = 1.0;
                    }

                    CategoricalDataSet expected = new CategoricalDataSet(
                        expectedVariables, expectedData);

                    CategoricalDataSetAssert.AreEqual(expected, actual);
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data set contains
                /// variable names.
                /// </summary>
                public static void FromTextReaderWithVariableNames()
                {
                    // Create a data stream.
                    // This stream contains the example discussed in
                    // @ARTICLE{ ,
                    //          author={ ELOMAA, T. and ROUSU, J.},
                    //          title={ General and Efficient Multi Splitting of Numerical Attributes},
                    //          year ={ 1999 },
                    //          publisher={ KLUWER Academic Publishers },
                    //          journal={ Machine Learning },
                    //          issue={ 3 },
                    //          pages={ 201-244 },
                    //          volume={ 36 },
                    //          URL={ http://periodici.caspur.it/cgi-bin/sciserv.pl?collection=journals&journal=08856125&issue=v36i0003&article=201_gaemona },
                    //}
                    const int numberOfInstances = 27;
                    string[] data = new string[numberOfInstances + 1] {
                        "NUMERICAL,TARGET",
                        "0,A",
                        "0,A",
                        "0,A",
                        "1,B",
                        "1,B",
                        "1,B",
                        "1,B",
                        "2,B",
                        "2,B",
                        "3,C",
                        "3,C",
                        "3,C",
                        "4,B",
                        "4,B",
                        "4,B",
                        "4,C",
                        "5,A",
                        "5,A",
                        "6,A",
                        "7,C",
                        "7,C",
                        "7,C",
                        "8,C",
                        "8,C",
                        "9,C",
                        "9,C",
                        "9,C" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Identify the special categorizer for variable NUMERICAL
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(0, 0);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 1;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                        streamReader,
                        columnDelimiter,
                        numericalColumns,
                        firstLineContainsColumnHeaders,
                        targetColumn,
                        provider);

                    // Encode the categorical data set using the special categorizer
                    stream.Position = 0;
                    IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        streamReader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        provider);

                    // Expected dataset is:

                    // NUMERICAL,TARGET,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    CategoricalVariable numerical = new CategoricalVariable("NUMERICAL")
                    {
                        { 0, "]-Inf, 2.5]" },
                        { 1, "]2.5, Inf[" }
                    };
                    numerical.SetAsReadOnly();

                    CategoricalVariable number = new CategoricalVariable("TARGET")
                    {
                        { 0, "A" },
                        { 1, "B" },
                        { 2, "C" }
                    };
                    number.SetAsReadOnly();

                    List<CategoricalVariable> expectedVariables =
                        new List<CategoricalVariable>() { numerical, number };

                    DoubleMatrix expectedData = DoubleMatrix.Dense(numberOfInstances, 2);

                    expectedData.SetColumnName(0, "NUMERICAL");
                    expectedData.SetColumnName(1, "TARGET");

                    double targetCode;
                    for (int i = 1; i < data.Length; i++)
                    {
                        var tokens = data[i].Split(',');
                        var targetLabel = tokens[1];
                        switch (targetLabel)
                        {
                            case "A":
                                targetCode = 0.0;
                                break;
                            case "B":
                                targetCode = 1.0;
                                break;
                            case "C":
                                targetCode = 2.0;
                                break;
                            default:
                                throw new Exception("Unrecognized target label.");
                        }
                        expectedData[i - 1, 1] = targetCode;
                    }

                    for (int i = 9; i < expectedData.NumberOfRows; i++)
                    {
                        expectedData[i, 0] = 1.0;
                    }

                    CategoricalDataSet expected = new CategoricalDataSet(
                        expectedVariables, expectedData);

                    CategoricalDataSetAssert.AreEqual(expected, actual);
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method terminates successfully as expected when
                /// its parameters are valid and the data set contains
                /// no variable names.
                /// </summary>
                public static void FromTextReaderWithoutVariableNames()
                {
                    // Create a data stream.
                    // This stream contains the example discussed in
                    // @ARTICLE{ ,
                    //          author={ ELOMAA, T. and ROUSU, J.},
                    //          title={ General and Efficient Multi Splitting of Numerical Attributes},
                    //          year ={ 1999 },
                    //          publisher={ KLUWER Academic Publishers },
                    //          journal={ Machine Learning },
                    //          issue={ 3 },
                    //          pages={ 201-244 },
                    //          volume={ 36 },
                    //          URL={ http://periodici.caspur.it/cgi-bin/sciserv.pl?collection=journals&journal=08856125&issue=v36i0003&article=201_gaemona },
                    //}
                    const int numberOfInstances = 27;
                    string[] data = new string[numberOfInstances] {
                        //"NUMERICAL,TARGET",
                        "0,A",
                        "0,A",
                        "0,A",
                        "1,B",
                        "1,B",
                        "1,B",
                        "1,B",
                        "2,B",
                        "2,B",
                        "3,C",
                        "3,C",
                        "3,C",
                        "4,B",
                        "4,B",
                        "4,B",
                        "4,C",
                        "5,A",
                        "5,A",
                        "6,A",
                        "7,C",
                        "7,C",
                        "7,C",
                        "8,C",
                        "8,C",
                        "9,C",
                        "9,C",
                        "9,C" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Identify the special categorizer for variable NUMERICAL
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(0, 0);
                    bool firstLineContainsColumnHeaders = false;
                    int targetColumn = 1;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                        streamReader,
                        columnDelimiter,
                        numericalColumns,
                        firstLineContainsColumnHeaders,
                        targetColumn,
                        provider);

                    // Encode the categorical data set using the special categorizer
                    stream.Position = 0;
                    IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        streamReader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        provider);

                    // Expected dataset is:

                    // 0,1,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],A,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]-Inf, 2.5],B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,B,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,A,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    // ]2.5, Inf[,C,
                    CategoricalVariable numerical = new CategoricalVariable("0")
                    {
                        { 0, "]-Inf, 2.5]" },
                        { 1, "]2.5, Inf[" }
                    };
                    numerical.SetAsReadOnly();

                    CategoricalVariable number = new CategoricalVariable("1")
                    {
                        { 0, "A" },
                        { 1, "B" },
                        { 2, "C" }
                    };
                    number.SetAsReadOnly();

                    List<CategoricalVariable> expectedVariables =
                        new List<CategoricalVariable>() { numerical, number };

                    DoubleMatrix expectedData = DoubleMatrix.Dense(numberOfInstances, 2);

                    expectedData.SetColumnName(0, "0");
                    expectedData.SetColumnName(1, "1");

                    double targetCode;
                    for (int i = 0; i < data.Length; i++)
                    {
                        var tokens = data[i].Split(',');
                        var targetLabel = tokens[1];
                        switch (targetLabel)
                        {
                            case "A":
                                targetCode = 0.0;
                                break;
                            case "B":
                                targetCode = 1.0;
                                break;
                            case "C":
                                targetCode = 2.0;
                                break;
                            default:
                                throw new Exception("Unrecognized target label.");
                        }
                        expectedData[i, 1] = targetCode;
                    }

                    for (int i = 9; i < expectedData.NumberOfRows; i++)
                    {
                        expectedData[i, 0] = 1.0;
                    }

                    CategoricalDataSet expected = new CategoricalDataSet(
                        expectedVariables, expectedData);

                    CategoricalDataSetAssert.AreEqual(expected, actual);
                }
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
            /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
            /// method fails when expected.
            public static class Fail
            {
                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// a category label is missing.
                /// </summary>
                public static void CategoryLabelIsMissing()
                {
                    // Category label is missing

                    // Create an invalid data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        ",TRUE, 0.0", // Labels cannot be empty or consisting of white spaces
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Try to get the special categorizer
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    int lineNumber = 2;
                    int column = 0;
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(InvalidDataException),
                        expectedMessage: string.Format(
                                                ImplementationServices.GetResourceString(
                                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                                lineNumber, column),
                        expectedInnerType: typeof(ArgumentOutOfRangeException),
                        expectedInnerMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE") +
                            Environment.NewLine + "Parameter name: label");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// an extracted column is missing in any data row.
                /// </summary>
                public static void ExtractedColumnIsMissingInDataRow()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER,HOMER,D'OH!",
                        "Red,TRUE,  -2.2",
                        "Black,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // A numerical column is missing 

                    // Try to get the special categorizer
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(2, 3);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    int lineNumber = 1;
                    int column = 3;
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                                streamReader,
                                columnDelimiter,
                                numericalColumns,
                                firstLineContainsColumnHeaders,
                                targetColumn,
                                provider);
                        },
                        expectedType: typeof(InvalidDataException),
                        expectedMessage: string.Format(
                                                ImplementationServices.GetResourceString(
                                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                                lineNumber, column));

                    // The target column is missing 

                    numericalColumns = IndexCollection.Range(2, 2);

                    // Try to get the special categorizer
                    targetColumn = 8;
                    lineNumber = 1;
                    column = targetColumn;
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(InvalidDataException),
                        expectedMessage: string.Format(
                                                ImplementationServices.GetResourceString(
                                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                                lineNumber, column));
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// the index of the target column is negative.
                /// </summary>
                public static void TargetColumnIsNegative()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER,HOMER,D'OH!",
                        "Red,TRUE,  -2.2",
                        "Black,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Try to get the special categorizer
                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = -1;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers = CategoricalDataSet.CategorizeByEntropyMinimization(
                                streamReader,
                                columnDelimiter,
                                numericalColumns,
                                firstLineContainsColumnHeaders,
                                targetColumn,
                                provider);
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                                                    "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                        expectedParameterName: "targetColumn");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// the data set contains no rows.
                /// </summary>
                public static void NoDataRows()
                {
                    // Create a data stream 
                    string[] data = new string[1] {
                        "COLOR,HAPPINESS,NUMBER" }; // No data rows

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    StreamReader streamReader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = CultureInfo.InvariantCulture;
                    ExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(InvalidDataException),
                        expectedMessage: ImplementationServices.GetResourceString(
                                            "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_DATA"));
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// the text reader is <b>null</b>.
                /// </summary>
                public static void ReaderIsNull()
                {
                    // reader is null

                    StreamReader streamReader = null;
                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Range(2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = CultureInfo.InvariantCulture;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "reader");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// the <see cref="IndexCollection"/> representing the 
                /// requested numerical columns is <b>null</b>.
                /// </summary>
                public static void NumericalColumnsIsNull()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;
                    StreamReader streamReader = new StreamReader(stream);

                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = null;
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = CultureInfo.InvariantCulture;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "numericalColumns");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
                /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
                /// method fails as expected when
                /// the <see cref="IFormatProvider"/> representing the 
                /// requested format provider is <b>null</b>.
                /// </summary>
                public static void ProviderIsNull()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;
                    StreamReader streamReader = new StreamReader(stream);

                    char columnDelimiter = ',';
                    IndexCollection numericalColumns = IndexCollection.Sequence(0, 2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    int targetColumn = 0;
                    IFormatProvider provider = null;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var specialCategorizers =
                                CategoricalDataSet.CategorizeByEntropyMinimization(
                                    streamReader,
                                    columnDelimiter,
                                    numericalColumns,
                                    firstLineContainsColumnHeaders,
                                    targetColumn,
                                    provider);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "provider");
                }
            }
        }

        #endregion

        #region Disjoin 

        /// <summary>
        /// Provides methods to test that 
        /// Disjoin methods have
        /// been properly implemented.
        /// </summary>
        public static class Disjoin
        {
            /// <summary>
            /// Tests that the
            /// <see cref="CategoricalDataSet.Disjoin"/> method 
            /// terminates successfully as expected.
            /// </summary>
            public static void Succeed()
            {
                // Create a data stream 
                string[] data = new string[6] {
                    "COLOR,HAPPINESS,NUMBER",
                    "Red,TRUE,  -2.2",
                    "Green,TRUE, 0.0",
                    "Red,FALSE,  -3.3",
                    "Black,TRUE,-1.1",
                    "Black,FALSE, 4.4" };

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                for (int i = 0; i < data.Length; i++)
                {
                    writer.WriteLine(data[i].ToCharArray());
                    writer.Flush();
                }
                stream.Position = 0;

                // Define a special categorizer for variable NUMBER
                string numberCategorizer(string token, IFormatProvider provider) 
                {
                    double datum = Convert.ToDouble(token, provider);
                    if (datum == 0)
                    {
                        return "Zero";
                    }
                    else if (datum < 0)
                    {
                        return "Negative";
                    }
                    else
                    {
                        return "Positive";
                    }
                }

                // Attach the special categorizer to variable NUMBER
                int numberColumnIndex = 2;
                var specialCategorizers = new Dictionary<int, Categorizer>
                {
                    { numberColumnIndex, numberCategorizer }
                };

                // Encode the categorical data set
                StreamReader reader = new StreamReader(stream);
                char columnDelimiter = ',';
                IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                bool firstLineContainsColumnHeaders = true;
                CategoricalDataSet actual = CategoricalDataSet.Encode(
                    reader,
                    columnDelimiter,
                    extractedColumns,
                    firstLineContainsColumnHeaders,
                    specialCategorizers,
                    CultureInfo.InvariantCulture);

                DoubleMatrix disjoinedExpected = DoubleMatrix.Dense(5, 6);

                //CategoricalVariable color = new CategoricalVariable("COLOR");
                //color.AddCategory(0, "Red");
                //color.AddCategory(1, "Green");
                //color.AddCategory(2, "Black");
                disjoinedExpected.SetColumnName(0, "Red");
                disjoinedExpected.SetColumnName(1, "Green");
                disjoinedExpected.SetColumnName(2, "Black");

                //CategoricalVariable number = new CategoricalVariable("NUMBER");
                //number.AddCategory(0, "Negative");
                //number.AddCategory(1, "Zero");
                //number.AddCategory(2, "Positive");
                disjoinedExpected.SetColumnName(3, "Negative");
                disjoinedExpected.SetColumnName(4, "Zero");
                disjoinedExpected.SetColumnName(5, "Positive");

                //"COLOR,HAPPINESS,NUMBER",
                //0,"Red,TRUE,  -2.2",
                //1,"Green,TRUE, 0.0",
                //2,"Red,FALSE,  -3.3",
                //3,"Black,TRUE,-1.1",
                //4,"Black,FALSE, 4.4"
                disjoinedExpected[0, 0] = 1; // Red
                disjoinedExpected[0, 3] = 1; // Negative

                disjoinedExpected[1, 1] = 1; // Green
                disjoinedExpected[1, 4] = 1; // Zero

                disjoinedExpected[2, 0] = 1; // Red
                disjoinedExpected[2, 3] = 1; // Negative

                disjoinedExpected[3, 2] = 1; // Black
                disjoinedExpected[3, 3] = 1; // Negative

                disjoinedExpected[4, 2] = 1; // Black
                disjoinedExpected[4, 5] = 1; // Positive

                var disjoinedActual = actual.Disjoin();

                DoubleMatrixAssert.AreEqual(disjoinedExpected, disjoinedActual, 1e-2);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="CategoricalDataSet.Disjoin(DoubleMatrix)"/>
            /// method have
            /// been properly implemented.
            /// </summary>
            public static class SupplementaryData
            {
                /// Tests that the
                /// <see cref="CategoricalDataSet.Disjoin(DoubleMatrix)"/> 
                /// method terminates successfully as expected when
                /// its parameter is valid.
                /// </summary>
                public static void IsValid()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Define a special categorizer for variable NUMBER
                    string numberCategorizer(string token, IFormatProvider provider)
                    {
                        double datum = Convert.ToDouble(token, provider);
                        if (datum == 0)
                        {
                            return "Zero";
                        }
                        else if (datum < 0)
                        {
                            return "Negative";
                        }
                        else
                        {
                            return "Positive";
                        }
                    }

                    // Attach the special categorizer to variable NUMBER
                    int numberColumnIndex = 2;
                    var specialCategorizers = new Dictionary<int, Categorizer>
                    {
                        { numberColumnIndex, numberCategorizer }
                    };

                    // Encode the categorical data set
                    StreamReader reader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        reader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        CultureInfo.InvariantCulture);

                    DoubleMatrix disjoinedExpected = DoubleMatrix.Dense(5, 6);

                    //CategoricalVariable color = new CategoricalVariable("COLOR");
                    //color.AddCategory(0, "Red");
                    //color.AddCategory(1, "Green");
                    //color.AddCategory(2, "Black");
                    disjoinedExpected.SetColumnName(0, "Red");
                    disjoinedExpected.SetColumnName(1, "Green");
                    disjoinedExpected.SetColumnName(2, "Black");

                    //CategoricalVariable number = new CategoricalVariable("NUMBER");
                    //number.AddCategory(0, "Negative");
                    //number.AddCategory(1, "Zero");
                    //number.AddCategory(2, "Positive");
                    disjoinedExpected.SetColumnName(3, "Negative");
                    disjoinedExpected.SetColumnName(4, "Zero");
                    disjoinedExpected.SetColumnName(5, "Positive");

                    //"COLOR,HAPPINESS,NUMBER",
                    //0,"Red,TRUE,  -2.2",
                    //1,"Green,TRUE, 0.0",
                    //2,"Red,FALSE,  -3.3",
                    //3,"Black,TRUE,-1.1",
                    //4,"Black,FALSE, 4.4"
                    disjoinedExpected[0, 0] = 1; // Red
                    disjoinedExpected[0, 3] = 1; // Negative

                    disjoinedExpected[1, 1] = 1; // Green
                    disjoinedExpected[1, 4] = 1; // Zero

                    disjoinedExpected[2, 0] = 1; // Red
                    disjoinedExpected[2, 3] = 1; // Negative

                    disjoinedExpected[3, 2] = 1; // Black
                    disjoinedExpected[3, 3] = 1; // Negative

                    disjoinedExpected[4, 2] = 1; // Black
                    disjoinedExpected[4, 5] = 1; // Positive

                    var disjoinedActual = actual.Disjoin((DoubleMatrix)actual.Data);

                    DoubleMatrixAssert.AreEqual(disjoinedExpected, disjoinedActual, 1e-2);
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.Disjoin(DoubleMatrix)"/> 
                /// method fails as expected when
                /// its parameter is <b>null</b>.
                /// </summary>
                public static void IsNull()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Define a special categorizer for variable NUMBER
                    string numberCategorizer(string token, IFormatProvider provider)
                    {
                        double datum = Convert.ToDouble(token, provider);
                        if (datum == 0)
                        {
                            return "Zero";
                        }
                        else if (datum < 0)
                        {
                            return "Negative";
                        }
                        else
                        {
                            return "Positive";
                        }
                    }

                    // Attach the special categorizer to variable NUMBER
                    int numberColumnIndex = 2;
                    var specialCategorizers = new Dictionary<int, Categorizer>
                    {
                        { numberColumnIndex, numberCategorizer }
                    };

                    // Encode the categorical data set
                    StreamReader reader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        reader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        CultureInfo.InvariantCulture);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            actual.Disjoin(null);
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "supplementaryData");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.Disjoin(DoubleMatrix)"/> 
                /// method fails as expected when
                /// its parameter contains unexpected codes.
                /// </summary>
                public static void ContainsUnexpectedCodes()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Define a special categorizer for variable NUMBER
                    string numberCategorizer(string token, IFormatProvider provider)
                    {
                        double datum = Convert.ToDouble(token, provider);
                        if (datum == 0)
                        {
                            return "Zero";
                        }
                        else if (datum < 0)
                        {
                            return "Negative";
                        }
                        else
                        {
                            return "Positive";
                        }
                    }

                    // Attach the special categorizer to variable NUMBER
                    int numberColumnIndex = 2;
                    var specialCategorizers = new Dictionary<int, Categorizer>
                    {
                        { numberColumnIndex, numberCategorizer }
                    };

                    // Encode the categorical data set
                    StreamReader reader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        reader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        CultureInfo.InvariantCulture);

                    var supplementaryData = DoubleMatrix.Dense(10, 2);
                    supplementaryData[9, 1] = -1;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            actual.Disjoin(
                                supplementaryData);
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_MATRIX_ENTRY_NOT_IN_VARIABLE_LIST"),
                        expectedParameterName: "supplementaryData");
                }

                /// Tests that the
                /// <see cref="CategoricalDataSet.Disjoin(DoubleMatrix)"/> 
                /// method fails as expected when
                /// its parameter has the wrong number of columns.
                /// </summary>
                public static void HasWrongNumberOfColumns()
                {
                    // Create a data stream 
                    string[] data = new string[6] {
                        "COLOR,HAPPINESS,NUMBER",
                        "Red,TRUE,  -2.2",
                        "Green,TRUE, 0.0",
                        "Red,FALSE,  -3.3",
                        "Black,TRUE,-1.1",
                        "Black,FALSE, 4.4" };

                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    for (int i = 0; i < data.Length; i++)
                    {
                        writer.WriteLine(data[i].ToCharArray());
                        writer.Flush();
                    }
                    stream.Position = 0;

                    // Define a special categorizer for variable NUMBER
                    string numberCategorizer(string token, IFormatProvider provider)
                    {
                        double datum = Convert.ToDouble(token, provider);
                        if (datum == 0)
                        {
                            return "Zero";
                        }
                        else if (datum < 0)
                        {
                            return "Negative";
                        }
                        else
                        {
                            return "Positive";
                        }
                    }

                    // Attach the special categorizer to variable NUMBER
                    int numberColumnIndex = 2;
                    var specialCategorizers = new Dictionary<int, Categorizer>
                    {
                        { numberColumnIndex, numberCategorizer }
                    };

                    // Encode the categorical data set
                    StreamReader reader = new StreamReader(stream);
                    char columnDelimiter = ',';
                    IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                    bool firstLineContainsColumnHeaders = true;
                    CategoricalDataSet actual = CategoricalDataSet.Encode(
                        reader,
                        columnDelimiter,
                        extractedColumns,
                        firstLineContainsColumnHeaders,
                        specialCategorizers,
                        CultureInfo.InvariantCulture);

                    var supplementaryData = DoubleMatrix.Dense(10, 20);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            actual.Disjoin(
                                supplementaryData);
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_COLUMNS_NOT_EQUAL_TO_DATASET_VARIABLES"),
                        expectedParameterName: "supplementaryData");
                }
            }
        }

        #endregion

        #region Encode

        /// <summary>
        /// Provides methods to test that the
        /// encoding methods in <see cref="CategoricalDataSet"/> 
        /// have been properly implemented.
        /// </summary>
        public static class Encode
        {
            /// <summary>
            /// Provides methods to test that the
            /// basic encoding methods in <see cref="CategoricalDataSet"/> 
            /// (i.e., those with no special categorizers or format providers)
            /// have been properly implemented.
            /// </summary>
            public static class Basic
            {
                /// <summary>
                /// Provides methods to test that the
                /// advanced encoding methods in <see cref="CategoricalDataSet"/> 
                /// terminate successfully when expected.
                public static class Succeed
                {
                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// string, char, IndexCollection, bool)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// variable names.
                    /// </summary>
                    public static void FromPathWithVariableNames()
                    {
                        var path = "Data" + Path.DirectorySeparatorChar + "encode-path.csv";

                        // Encode the categorical data set
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            path,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders);

                        CategoricalVariable color = new CategoricalVariable("COLOR")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable happiness = new CategoricalVariable("HAPPINESS")
                        {
                            { 0, "TRUE" },
                            { 1, "FALSE" }
                        };
                        happiness.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, happiness };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "COLOR");
                        expectedData.SetColumnName(1, "HAPPINESS");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 0;
                        expectedData[2, 1] = 1;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 1;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// variable names.
                    /// </summary>
                    public static void FromTextReaderWithVariableNames()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            reader,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders);

                        CategoricalVariable color = new CategoricalVariable("COLOR")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable happiness = new CategoricalVariable("HAPPINESS")
                        {
                            { 0, "TRUE" },
                            { 1, "FALSE" }
                        };
                        happiness.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, happiness };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "COLOR");
                        expectedData.SetColumnName(1, "HAPPINESS");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 0;
                        expectedData[2, 1] = 1;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 1;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// no variable names.
                    /// </summary>
                    public static void FromTextReaderWithoutVariableNames()
                    {
                        // Mainstream use case - without variable names

                        // Create a data stream 
                        string[] data = new string[5] {
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = false;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            reader,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders);

                        CategoricalVariable color = new CategoricalVariable("0")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable happiness = new CategoricalVariable("1")
                        {
                            { 0, "TRUE" },
                            { 1, "FALSE" }
                        };
                        happiness.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, happiness };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "0");
                        expectedData.SetColumnName(1, "1");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 0;
                        expectedData[2, 1] = 1;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 1;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="CategoricalDataSet.Encode(
                /// TextReader, char, IndexCollection, bool)"/>
                /// method fails when expected.
                public static class Fail
                {
                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// a variable name is missing in the data set.
                    /// </summary>
                    public static void VariableNameIsMissing()
                    {
                        // Variable name is missing

                        // Create an invalid data stream 
                        string[] data = new string[6] {
                            "COLOR,  ,NUMBER", // Names cannot be empty or consisting of white spaces
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 0;
                        int column = 1;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                                    ImplementationServices.GetResourceString(
                                                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                                    lineNumber, column),
                            expectedInnerType: typeof(ArgumentOutOfRangeException),
                            expectedInnerMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE") + 
                                Environment.NewLine + "Parameter name: name");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// a category label is missing in the data set.
                    /// </summary>
                    public static void CategoryLabelIsMissing()
                    {
                        // Category label is missing

                        // Create an invalid data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            ",TRUE, 0.0", // Labels cannot be empty or consisting of white spaces
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 2;
                        int column = 0;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                                    ImplementationServices.GetResourceString(
                                                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                                    lineNumber, column),
                            expectedInnerType: typeof(ArgumentOutOfRangeException),
                            expectedInnerMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE") + 
                                Environment.NewLine + "Parameter name: label");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// any extracted column is missing in the header row of the data set.
                    /// </summary>
                    public static void ExtractedColumnIsMissingInHeaderRow()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Black,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        // Column 4 does not exist
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 4);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 0;
                        int column = 4;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// any extracted column is missing in a data row.
                    /// </summary>
                    public static void ExtractedColumnIsMissingInDataRow()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER,HOMER,D'OH!",
                            "Red,TRUE,  -2.2",
                            "Black,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        // Column 4 does not exist
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 4);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 1;
                        int column = 4;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// the data set contains no rows.
                    /// </summary>
                    public static void NoDataRows()
                    {
                        // Create a data stream 
                        string[] data = new string[1] {
                        "COLOR,HAPPINESS,NUMBER" }; // No data rows

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_DATA"));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// the text reader is <b>null</b>.
                    /// </summary>
                    public static void ReaderIsNull()
                    {
                        // reader is null

                        StreamReader reader = null;
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Range(0, 1);
                        bool firstLineContainsColumnHeaders = true;

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "reader");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool)"/>
                    /// method fails as expected when
                    /// the <see cref="IndexCollection"/> representing the 
                    /// columns to extract is <b>null</b>.
                    /// </summary>
                    public static void ExtractedColumnsIsNull()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = null;
                        bool firstLineContainsColumnHeaders = true;

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "extractedColumns");
                    }
                }
            }

            /// <summary>
            /// Provides methods to test that the
            /// advanced encoding methods in <see cref="CategoricalDataSet"/> 
            /// (i.e., those with special categorizers and format providers)
            /// have been properly implemented.
            /// </summary>
            public static class Advanced
            {
                /// <summary>
                /// Provides methods to test that the
                /// advanced encoding methods in <see cref="CategoricalDataSet"/> 
                /// terminate successfully when expected.
                public static class Succeed
                {
                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// string, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// variable names.
                    /// </summary>
                    public static void FromPathWithVariableNames()
                    {
                        var path = "Data" + Path.DirectorySeparatorChar + "encode-path.csv";

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            path,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders,
                            specialCategorizers,
                            CultureInfo.InvariantCulture);

                        CategoricalVariable color = new CategoricalVariable("COLOR")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable number = new CategoricalVariable("NUMBER")
                        {
                            { 0, "Negative" },
                            { 1, "Zero" },
                            { 2, "Positive" }
                        };
                        number.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, number };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "COLOR");
                        expectedData.SetColumnName(1, "NUMBER");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 1;
                        expectedData[2, 1] = 0;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 2;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// string, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// variable names.
                    /// </summary>
                    public static void FromTextReaderWithVariableNames()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            reader,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders,
                            specialCategorizers,
                            CultureInfo.InvariantCulture);

                        CategoricalVariable color = new CategoricalVariable("COLOR")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable number = new CategoricalVariable("NUMBER")
                        {
                            { 0, "Negative" },
                            { 1, "Zero" },
                            { 2, "Positive" }
                        };
                        number.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, number };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "COLOR");
                        expectedData.SetColumnName(1, "NUMBER");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 1;
                        expectedData[2, 1] = 0;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 2;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// string, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method terminates successfully as expected when
                    /// its parameters are valid and the data set contains
                    /// no variable names.
                    /// </summary>
                    public static void FromTextReaderWithoutVariableNames()
                    {
                        // Mainstream use case - without variable names

                        // Create a data stream 
                        string[] data = new string[5] {
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = false;
                        CategoricalDataSet actual = CategoricalDataSet.Encode(
                            reader,
                            columnDelimiter,
                            extractedColumns,
                            firstLineContainsColumnHeaders,
                            specialCategorizers,
                            CultureInfo.InvariantCulture);

                        CategoricalVariable color = new CategoricalVariable("0")
                        {
                            { 0, "Red" },
                            { 1, "Green" },
                            { 2, "Black" }
                        };
                        color.SetAsReadOnly();

                        CategoricalVariable number = new CategoricalVariable("1")
                        {
                            { 0, "Negative" },
                            { 1, "Zero" },
                            { 2, "Positive" }
                        };
                        number.SetAsReadOnly();

                        List<CategoricalVariable> expectedVariables =
                            new List<CategoricalVariable>() { color, number };

                        DoubleMatrix expectedData = DoubleMatrix.Dense(5, 2);

                        expectedData.SetColumnName(0, "0");
                        expectedData.SetColumnName(1, "1");

                        expectedData[0, 0] = 0;
                        expectedData[1, 0] = 1;
                        expectedData[2, 0] = 0;
                        expectedData[3, 0] = 2;
                        expectedData[4, 0] = 2;

                        expectedData[0, 1] = 0;
                        expectedData[1, 1] = 1;
                        expectedData[2, 1] = 0;
                        expectedData[3, 1] = 0;
                        expectedData[4, 1] = 2;

                        CategoricalDataSet expected = new CategoricalDataSet(
                            expectedVariables, expectedData);

                        CategoricalDataSetAssert.AreEqual(expected, actual);
                    }
                }

                /// <summary>
                /// Provides methods to test that the
                /// <see cref="CategoricalDataSet.Encode(
                /// TextReader, char, IndexCollection, bool, 
                /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                /// method fails when expected.
                public static class Fail
                {
                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// a variable name is missing in the data set.
                    /// </summary>
                    public static void VariableNameIsMissing()
                    {
                        // Variable name is missing

                        // Create an invalid data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,  ", // Names cannot be empty or consisting of white spaces
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 0;
                        int column = 2;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column),
                            expectedInnerType: typeof(ArgumentOutOfRangeException),
                            expectedInnerMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE") + 
                                Environment.NewLine + "Parameter name: name");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// a category label is missing in the data set.
                    /// </summary>
                    public static void CategoryLabelIsMissing()
                    {
                        // Category label is missing

                        // Create an invalid data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            ",TRUE, 0.0", // Labels cannot be empty or consisting of white spaces
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 2;
                        int column = 0;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column),
                            expectedInnerType: typeof(ArgumentOutOfRangeException),
                            expectedInnerMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE") + 
                                Environment.NewLine + "Parameter name: label");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// any extracted column is missing in the header row of the data set.
                    /// </summary>
                    public static void ExtractedColumnIsMissingInHeaderRow()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Black,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        // Column 4 does not exist
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 4);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 0;
                        int column = 4;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// any extracted column is missing in a data row.
                    /// </summary>
                    public static void ExtractedColumnIsMissingInDataRow()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER,HOMER,D'OH!",
                            "Red,TRUE,  -2.2",
                            "Black,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        // Column 4 does not exist
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 4);
                        bool firstLineContainsColumnHeaders = true;
                        int lineNumber = 1;
                        int column = 4;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: string.Format(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber, column));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the data set contains no rows.
                    /// </summary>
                    public static void NoDataRows()
                    {
                        // Create a data stream 
                        string[] data = new string[1] {
                        "COLOR,HAPPINESS,NUMBER" }; // No data rows

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        // Encode the categorical data set
                        StreamReader reader = new StreamReader(stream);
                        char columnDelimiter = ',';

                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;
                        ExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(InvalidDataException),
                            expectedMessage: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_DATA"));
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the text reader is <b>null</b>.
                    /// </summary>
                    public static void ReaderIsNull()
                    {
                        // reader is null

                        StreamReader reader = null;
                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "reader");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the <see cref="IndexCollection"/> representing the 
                    /// columns to extract is <b>null</b>.
                    /// </summary>
                    public static void ExtractedColumnsIsNull()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = null;
                        bool firstLineContainsColumnHeaders = true;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "extractedColumns");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the <see cref="Dictionary{System.Int32, Categorizer}"/> representing the 
                    /// special categorizers to apply is <b>null</b>.
                    /// </summary>
                    public static void SpecialCategorizersIsNull()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;

                        // Attach the special categorizer to variable NUMBER
                        Dictionary<int, Categorizer> specialCategorizers = null;

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "specialCategorizers");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the <see cref="IFormatProvider"/> representing the 
                    /// format provider is <b>null</b>.
                    /// </summary>
                    public static void ProviderIsNull()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;

                        // Define a special categorizer for variable NUMBER
                        string numberCategorizer(string token, IFormatProvider provider)
                        {
                            double datum = Convert.ToDouble(token, provider);
                            if (datum == 0)
                            {
                                return "Zero";
                            }
                            else if (datum < 0)
                            {
                                return "Negative";
                            }
                            else
                            {
                                return "Positive";
                            }
                        }

                        // Attach the special categorizer to variable NUMBER
                        int numberColumnIndex = 2;
                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { numberColumnIndex, numberCategorizer }
                        };

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    null);
                            },
                            expectedType: typeof(ArgumentNullException),
                            expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                            expectedParameterName: "provider");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the <see cref="Dictionary{System.Int32, Categorizer}"/> representing the 
                    /// special categorizers to apply contains
                    /// a wrong key.
                    /// </summary>
                    public static void SpecialCategorizersContainsIrrelevantKey()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;

                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { -1, (token, provider) => { return Convert.ToString(token); } }
                        };

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(ArgumentException),
                            expectedPartialMessage: string.Format(
                                    ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_CATEGORIZER_REFERS_TO_IRRELEVANT_KEY"),
                                    "columns"),
                            expectedParameterName: "specialCategorizers");
                    }

                    /// Tests that the
                    /// <see cref="CategoricalDataSet.Encode(
                    /// TextReader, char, IndexCollection, bool, 
                    /// Dictionary{int, Categorizer}, IFormatProvider)"/>
                    /// method fails as expected when
                    /// the <see cref="Dictionary{System.Int32, Categorizer}"/> representing the 
                    /// special categorizers to apply contains
                    /// a <b>null</b> value.
                    /// </summary>
                    public static void SpecialCategorizersContainsNullValue()
                    {
                        // Create a data stream 
                        string[] data = new string[6] {
                            "COLOR,HAPPINESS,NUMBER",
                            "Red,TRUE,  -2.2",
                            "Green,TRUE, 0.0",
                            "Red,FALSE,  -3.3",
                            "Black,TRUE,-1.1",
                            "Black,FALSE, 4.4" };

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        for (int i = 0; i < data.Length; i++)
                        {
                            writer.WriteLine(data[i].ToCharArray());
                            writer.Flush();
                        }
                        stream.Position = 0;
                        StreamReader reader = new StreamReader(stream);

                        char columnDelimiter = ',';
                        IndexCollection extractedColumns = IndexCollection.Sequence(0, 2, 2);
                        bool firstLineContainsColumnHeaders = true;

                        var specialCategorizers = new Dictionary<int, Categorizer>
                        {
                            { 0, null }
                        };

                        ArgumentExceptionAssert.Throw(
                            () =>
                            {
                                CategoricalDataSet.Encode(
                                    reader,
                                    columnDelimiter,
                                    extractedColumns,
                                    firstLineContainsColumnHeaders,
                                    specialCategorizers,
                                    CultureInfo.InvariantCulture);
                            },
                            expectedType: typeof(ArgumentException),
                            expectedPartialMessage: ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_CATEGORIZER_IS_NULL"),
                            expectedParameterName: "specialCategorizers");
                    }
                }
            }
        }

        #endregion
    }
}
