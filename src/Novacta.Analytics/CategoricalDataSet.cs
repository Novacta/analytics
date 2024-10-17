// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Novacta.Analytics.Infrastructure;
using static System.Math;
using System.Text;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of observations for a set of 
    /// categorical variables
    /// </summary>
    /// <remarks>
    /// <para>
    /// A dataset is composed by a set of categorical variables, whose 
    /// list is returned by property
    /// <see cref="Variables"/>, and a matrix, returned by property 
    /// <see cref="Data"/>, consisting 
    /// of the data observed for such variables at a
    /// given collection of individuals.
    /// Each matrix column is associated to one of the categorical 
    /// variables under study, while
    /// the rows of the matrix are associated to the individuals.
    /// </para>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// New instances of the <see cref="CategoricalDataSet"/> class 
    /// can be initialized 
    /// from previously encoded data, through method 
    /// <see cref="FromEncodedData"/>, 
    /// or by encoding a data source, see, for example, 
    /// <see cref="CategoricalDataSet.Encode(
    /// TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, 
    /// IFormatProvider)"/>.  
    /// </para>
    /// <para>
    /// The source can contain information about 
    /// categorical or numerical variables
    /// observed at a given instance. 
    /// Encoding methods take into account numerical variables
    /// by delegating their discretization to special categorizers. 
    /// If needed,  
    /// categorizers can be identified by splitting the range of the 
    /// numerical data into 
    /// multiple intervals in order to minimize the intra-interval heterogeneity of 
    /// the given target, see, for example,  
    /// <see cref="CategoricalDataSet.CategorizeByEntropyMinimization(
    /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
    /// </para>
    /// <para>
    /// Reverting an encoding operation is provided by method 
    /// <see cref="Decode">Decode</see>.
    /// </para>
    /// <para>
    /// Parts of the <see cref="CategoricalDataSet"/> can be selected 
    /// through
    /// indexers, see, for example, 
    /// <see cref="this[IndexCollection, IndexCollection]"/>.
    /// </para>
    /// <para><b>Disjunctive forms</b></para>
    /// <para>
    /// Data about a categorical variable can be represented in disjunctive form
    /// by splitting the information for the variable in as many binary variables 
    /// as the number of 
    /// variable categories.
    /// The disjunctive representation of a <see cref="CategoricalDataSet"/>
    /// is returned by <see cref="Disjoin()"/>.
    /// Supplementary data,
    /// i.e. data containing 
    /// information about the same variables observed at different individuals, 
    /// can be obtained by method 
    /// <see cref="Disjoin(DoubleMatrix)"/>.
    /// </para>
    /// <para><b>Serialization</b></para>
    /// <para>
    /// Categorical data sets can be represented as JSON strings, 
    /// see <see cref="JsonSerialization"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="CategoricalVariable"/>
    /// <seealso cref="Category"/>
    /// <seealso cref="MultipleCorrespondence"/>
    public class CategoricalDataSet
        : IReadOnlyTabularCollection<Category, CategoricalDataSet>
    {
        #region State

        private readonly List<CategoricalVariable> variables;
        private readonly DoubleMatrix data;
        private string name;

        /// <summary>
        /// Gets the list of variables in the <see cref="CategoricalDataSet"/>.
        /// </summary>
        /// <value>
        /// The list of variables in the <see cref="CategoricalDataSet"/>.</value>
        public IReadOnlyList<CategoricalVariable> Variables { get; private set; }

        /// <summary>
        /// Gets the matrix of category codes in the <see cref="CategoricalDataSet"/>.
        /// </summary>
        /// <remarks>
        /// Each matrix column is associated to one of the categorical 
        /// variables under study, while
        /// the rows of the matrix are associated to the individuals at 
        /// which the variables have been observed.
        /// This implies that individuals can be identified by their 
        /// corresponding row indexes, and variables
        /// by their column indexes.
        /// </remarks>
        /// <value>
        /// The matrix of category codes in the <see cref="CategoricalDataSet"/>.</value>
        public ReadOnlyDoubleMatrix Data { get; private set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="CategoricalDataSet"/>.
        /// </summary>
        /// <value>The name of the <see cref="CategoricalDataSet"/>.</value>
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.data.Name = value;
            }
        }

        /// <summary>
        /// Decodes the <see cref="CategoricalDataSet"/>.
        /// </summary>
        /// <remarks>
        /// A <see cref="CategoricalDataSet"/> instance stores its data using 
        /// category codes, as returned by <see cref="Data"/>.
        /// When data are decoded, they are represented 
        /// by their corresponding category labels, disposed in a tabular format.
        /// </remarks>
        /// <returns>The table of decoding category labels.</returns>
        public string[][] Decode()
        {
            int numberOfIndividuals = this.Data.NumberOfRows;

            string[][] labels = new string[numberOfIndividuals][];

            int numberOfVariables = this.Data.NumberOfColumns;
            var variables = this.variables;
            var data = this.data;
            for (int i = 0; i < numberOfIndividuals; i++)
            {
                labels[i] = new string[numberOfVariables];
                for (int j = 0; j < numberOfVariables; j++)
                {
                    variables[j].TryGet(data[i, j], out Category category);
                    labels[i][j] = category.Label;
                }
            }

            return labels;
        }

        #endregion

        #region Constructors and factory methods

        internal CategoricalDataSet(
            List<CategoricalVariable> variables,
            DoubleMatrix data)
        {
            this.data = data;
            this.Data = this.data.AsReadOnly();
            this.variables = variables;
            this.Variables =
                new ReadOnlyCollection<CategoricalVariable>(this.variables);
        }

        #endregion

        #region Contingency tables

        /// <summary>
        /// Gets the contingency table representing the
        /// joint absolute frequency distribution
        /// of the specified categorical variables.
        /// </summary>
        /// <param name="rowVariableIndex">
        /// The index of the variable whose 
        /// categories will be assigned to the rows
        /// of the contingency table.
        /// </param>
        /// <param name="columnVariableIndex">
        /// The index of the variable whose 
        /// categories will be assigned to the columns
        /// of the contingency table.
        /// </param>
        /// <returns>
        /// The contingency table of the specified variables.
        /// </returns>
        /// <example>
        /// <para>
        /// In the following example, a contingency table is computed
        /// for two variables in a categorical data set.
        /// </para>
        /// <code title="Getting a contingency table" source="..\Novacta.Analytics.CodeExamples\CategoricalGetContingencyTableExample0.cs.txt" language="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowVariableIndex"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="rowVariableIndex"/> is greater than
        /// or equal to the <see cref="NumberOfColumns"/>
        /// of this instance.<br/>
        /// -or-<br/>
        /// <paramref name="columnVariableIndex"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="columnVariableIndex"/> is greater than
        /// or equal to the <see cref="NumberOfColumns"/>
        /// of this instance.
        /// </exception>
        public DoubleMatrix GetContingencyTable(
            int rowVariableIndex,
            int columnVariableIndex)
        {
            #region Input validation

            if (rowVariableIndex < 0
                ||
                this.data.NumberOfColumns <= rowVariableIndex)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(rowVariableIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            if (columnVariableIndex < 0
                ||
                this.data.NumberOfColumns <= columnVariableIndex)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(columnVariableIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
            }

            #endregion

            var rowVariable = this.variables[rowVariableIndex];
            var columnVariable = this.variables[columnVariableIndex];

            var table = DoubleMatrix.Dense(
                numberOfRows: rowVariable.NumberOfCategories,
                numberOfColumns: columnVariable.NumberOfCategories);

            SortedSet<DoubleMatrixRow> distinctRows =
                [];
            Dictionary<DoubleMatrixRow, int> counts =
                [];

            var rows = this.data[":",
                new IndexCollection(
                    [rowVariableIndex, columnVariableIndex], false)]
                        .AsRowCollection();

            bool isNotAlreadyInRowSet;
            int r = 0;
            foreach (var row in rows)
            {
                isNotAlreadyInRowSet = distinctRows.Add(row);
                if (isNotAlreadyInRowSet)
                {
                    counts.Add(row, 0);
                }

                counts[row]++;
                r++;
            }

            var rowMappedCodes =
                new Dictionary<double, int>(rowVariable.Categories.Count);

            for (int i = 0; i < table.NumberOfRows; i++)
            {
                var label = rowVariable.Categories[i].Label;
                var code = rowVariable.Categories[i].Code;

                table.SetRowName(i, label);
                rowMappedCodes[code] = i;
            }

            var columnMappedCodes =
                new Dictionary<double, int>(columnVariable.Categories.Count);

            for (int j = 0; j < table.NumberOfColumns; j++)
            {
                var label = columnVariable.Categories[j].Label;
                var code = columnVariable.Categories[j].Code;

                table.SetColumnName(j, label);
                columnMappedCodes[code] = j;
            }

            foreach (var row in distinctRows)
            {
                table[rowMappedCodes[row[0]], columnMappedCodes[row[1]]] = counts[row];
            }

            table.Name = rowVariable.Name + "-by-" + columnVariable.Name;

            return table;
        }

        #endregion

        #region Encoders

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CategoricalDataSet" /> class
        /// from previously encoded data.
        /// </summary>
        /// <param name="variables">
        /// The variables stored in the new dataset.</param>
        /// <param name="data">
        /// The data stored in the new dataset.</param>
        /// <remarks>
        /// <para>
        /// The constructor set every variable in <paramref name="variables"/> 
        /// as read only.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The categorical data set containing the specified 
        /// encoded data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="variables"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="data"/> has a number of columns other than the 
        /// count of <paramref name="variables"/>. <br/>
        /// -or-<br/>
        /// An entry exists in a column of <paramref name="data"/> not 
        /// equal to any code in 
        /// the variable corresponding to such column.
        /// </exception>
        public static CategoricalDataSet FromEncodedData(
            List<CategoricalVariable> variables,
            DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(variables);

            ArgumentNullException.ThrowIfNull(data);

            if (data.NumberOfColumns != variables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(data),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CAT_COLUMNS_NOT_EQUAL_TO_VARIABLES_COUNT"),
                        nameof(variables)));
            }

            for (int j = 0; j < data.NumberOfColumns; j++)
            {
                var variable = variables[j];
                variable.SetAsReadOnly();
                for (int i = 0; i < data.NumberOfRows; i++)
                {
                    if (!variable.TryGet(data[i, j], out _))
                    {
                        throw new ArgumentException(
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_MATRIX_ENTRY_NOT_IN_VARIABLE_LIST"),
                                nameof(data));
                    }
                }
            }

            var categoricalDataSet =
                new CategoricalDataSet(variables, (DoubleMatrix)data.Clone());

            for (int j = 0; j < data.NumberOfColumns; j++)
            {
                categoricalDataSet.data.SetColumnName(j, variables[j].Name);
            }

            return categoricalDataSet;
        }

        /// <summary>
        /// Encodes categorical or numerical data from the stream 
        /// underlying the specified text reader
        /// applying specific numerical data categorizers.
        /// </summary>
        /// <param name="reader">
        /// The reader having access to the data stream.</param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.
        /// </param>
        /// <param name="extractedColumns">
        /// The zero-based indexes of the columns from which 
        /// data are to be extracted.
        /// </param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.
        /// </param>
        /// <param name="specialCategorizers">
        /// A mapping from a subset of extracted column indexes to 
        /// a set of categorizers, to be executed when extracting 
        /// data from the corresponding columns.
        /// </param>
        /// <param name="provider">
        /// An object that provides formatting information to
        /// parse numeric values.
        /// </param>    
        /// <returns>The dataset containing information about the
        /// streamed data.
        /// </returns>
        /// <remarks>
        /// <para id='SpecialEncode.0'><b>Data Extraction</b></para>
        /// <para id='SpecialEncode.1'>
        /// Each line from the stream is interpreted as the information about 
        /// variables
        /// observed at a given instance. A line is split in tokens, 
        /// each corresponding
        /// to a (zero-based) column, which in turn stores the data
        /// of a given variable. Columns are assumed to be
        /// separated each other by the character passed as 
        /// <paramref name="columnDelimiter"/>.
        /// Data from a variable are extracted only if the corresponding 
        /// column index is in the 
        /// collection <paramref name="extractedColumns"/>. 
        /// </para>
        /// <para id='SpecialEncode.2'><b>Special Categorization</b></para>
        /// <para id ='SpecialEncode.3'>
        /// By default, tokens in a column are interpreted as 
        /// category labels of the corresponding variable, which is 
        /// inserted in the 
        /// dataset as such. This behavior can be overridden by mapping 
        /// a special categorizer
        /// to a given column by inserting, in the dictionary 
        /// <paramref name="specialCategorizers"/>, the categorizer as
        /// a value keyed with the 
        /// index of the column whose data are to be categorized. 
        /// A special categorizer can be
        /// useful if a given column corresponds to a numerical variable 
        /// which must be 
        /// discretized before its insertion in the dataset.
        /// For categorizers obtained
        /// by entropy minimization, 
        /// see, for example, 
        /// <see cref="CategorizeByEntropyMinimization(
        /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.4']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.5']"/>
        /// <para id='SpecialEncode.4'>
        /// In the following example, a data stream is read to encode
        /// a categorical dataset.
        /// The stream contains two columns, the first corresponding 
        /// to a categorical variable, 
        /// and the second to a numerical one. A special categorizer 
        /// is assigned to the second column 
        /// to discretize its data.
        ///</para>
        /// <para id='SpecialEncode.5'>
        /// <code title="Encoding a categorical dataset from a stream containing both categorical and 
        /// numerical data"
        /// source="..\Novacta.Analytics.CodeExamples\CategoricalEncodeExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        ///</example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <b>null</b>. <br/>
        /// -or- <br/>
        /// <paramref name="extractedColumns" />  is <b>null</b>. <br/>
        /// -or- <br/>
        /// <paramref name="specialCategorizers"/> is <b>null</b>. <br/>
        /// -or- <br/>
        /// <paramref name="provider"/> is <b>null</b>.
        /// </exception> 
        /// <exception cref="ArgumentException">        
        /// <paramref name="specialCategorizers"/> contains <b>null</b> values 
        /// or keys which are
        /// not in the <paramref name="extractedColumns"/> collection.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// There are no data rows in the stream accessed by 
        /// <paramref name="reader" />. <br/> 
        /// -or- <br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="extractedColumns" />. This can happen if 
        /// there are missing columns, 
        /// or if strings representing variable names or category labels,
        /// i.e. tokens extracted 
        /// from the stream or returned by a special categorizer, are <b>null</b> 
        /// or consist only of white-space characters. 
        /// In some cases, the <see cref="Exception.InnerException"/> 
        /// property is set to add 
        /// further details about the occurred error.
        /// </exception>
        /// <seealso cref="CategorizeByEntropyMinimization(
        /// TextReader, char, IndexCollection, bool, int, IFormatProvider)"/>
        /// <seealso cref="Categorizer" />
        public static CategoricalDataSet Encode(
            TextReader reader,
            char columnDelimiter,
            IndexCollection extractedColumns,
            bool firstLineContainsVariableNames,
            Dictionary<int, Categorizer> specialCategorizers,
            IFormatProvider provider)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(extractedColumns);
            ArgumentNullException.ThrowIfNull(specialCategorizers);

            foreach (var categorizer in specialCategorizers)
            {
                if (!extractedColumns.Contains(categorizer.Key))
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_CAT_CATEGORIZER_REFERS_TO_IRRELEVANT_KEY"),
                            "columns"),
                        nameof(specialCategorizers));
                }
                if (categorizer.Value is null)
                {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CAT_CATEGORIZER_IS_NULL"),
                        nameof(specialCategorizers));
                }
            }

            ArgumentNullException.ThrowIfNull(provider);

            #endregion

            int numberOfVariables = extractedColumns.Count;

            var variables = new List<CategoricalVariable>(numberOfVariables);

            List<double> rawData = new(100 * numberOfVariables);

            double[] nextAvailableCodes = new double[numberOfVariables];
            bool[] isSpecialColumn = new bool[numberOfVariables];
            for (int j = 0; j < numberOfVariables; j++)
            {
                isSpecialColumn[j] =
                    specialCategorizers.ContainsKey(extractedColumns[j]);
            }

            string line;
            string[] tokens;
            int lineNumber = 0, column = -1;
            double code;
            string categoryLabel;

            try
            {
                while ((line = reader.ReadLine()) != null)
                {
                    tokens = line.Split(columnDelimiter);

                    if (0 == lineNumber)
                    {
                        if (firstLineContainsVariableNames)
                        {
                            for (int j = 0; j < numberOfVariables; j++)
                            {
                                column = extractedColumns[j];
                                if (column >= tokens.Length)
                                {
                                    throw new InvalidDataException(
                                        string.Format(
                                            CultureInfo.InvariantCulture,
                                            ImplementationServices.GetResourceString(
                                                "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                            lineNumber,
                                            column));
                                }
                                variables.Add(new CategoricalVariable(tokens[column]));
                            }
                            lineNumber++;
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < numberOfVariables; j++)
                            {
                                variables.Add(new CategoricalVariable(
                                    Convert.ToString(j, CultureInfo.InvariantCulture)));
                            }
                        }
                    }

                    for (int j = 0; j < numberOfVariables; j++)
                    {
                        column = extractedColumns[j];
                        if (column >= tokens.Length)
                        {
                            throw new InvalidDataException(
                                string.Format(
                                    provider,
                                    ImplementationServices.GetResourceString(
                                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                    lineNumber,
                                    column));
                        }

                        if (isSpecialColumn[j])
                        {
                            categoryLabel =
                                specialCategorizers[column](tokens[column], provider);
                        }
                        else
                        {
                            categoryLabel = tokens[column];
                        }
                        bool isCategoryFound =
                            variables[j].TryGet(categoryLabel, out Category category);
                        if (!isCategoryFound)
                        {
                            code = nextAvailableCodes[j]++;
                            variables[j].Add(code, categoryLabel);
                        }
                        else
                        {
                            code = category.Code;
                        }
                        rawData.Add(code);
                    }

                    lineNumber++;
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException(
                    string.Format(
                        provider,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                        lineNumber,
                        column), e);
            }

            int numberOfItems =
                firstLineContainsVariableNames ? lineNumber - 1 : lineNumber;

            if (numberOfItems < 1)
            {
                throw new InvalidDataException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_DATA"));
            }

            DoubleMatrix data = DoubleMatrix.Dense(
                numberOfVariables, numberOfItems, [.. rawData], false);
            data.InPlaceTranspose();
            for (int j = 0; j < numberOfVariables; j++)
            {
                data.SetColumnName(j, variables[j].Name);
                variables[j].SetAsReadOnly();
            }

            var categoricalDataSet = new CategoricalDataSet(variables, data);

            return categoricalDataSet;
        }

        /// <summary>
        /// Encodes categorical data from the stream underlying the 
        /// specified text reader.
        /// </summary>
        /// <param name="reader">
        /// The reader having access to the data stream.
        /// </param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.
        /// </param>
        /// <param name="extractedColumns">
        /// The zero-based indexes of the columns from which 
        /// data are to be extracted.
        /// </param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.
        /// </param>
        /// <returns>
        /// The dataset containing information about the streamed data.</returns>
        /// <remarks>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.1']"/>
        /// <para>
        /// Data are encoded applying the <see cref="CultureInfo.InvariantCulture"/>.
        /// </para>        
        /// </remarks>
        /// <example>
        /// <para id='Encode.0'>
        /// In the following example, a data stream is read to encode a 
        /// categorical dataset.
        /// The stream contains data corresponding to two categorical variables. 
        ///</para>
        /// <para id='Encode.1'>
        /// <code title="Encoding a categorical dataset from a stream containing categorical data"
        /// source="..\Novacta.Analytics.CodeExamples\CategoricalEncodeExample2.cs.txt" 
        /// language="cs" />
        /// </para> 
        ///</example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <b>null</b>. <br/>
        /// -or-<br/> 
        /// <paramref name="extractedColumns" /> is <b>null</b>.
        /// </exception> 
        /// <exception cref="InvalidDataException">
        /// The stream accessed by <paramref name="reader" /> contains no 
        /// data rows.<br/>
        /// -or-<br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="extractedColumns" />. This can happen if 
        /// there are missing columns, 
        /// or if strings representing variable names or category 
        /// labels, i.e. tokens extracted 
        /// from the stream, 
        /// are <b>null</b> 
        /// or consist only of white-space characters. 
        /// In some cases, the <see cref="Exception.InnerException"/> 
        /// property is set to add 
        /// further details about the occurred error.
        /// </exception>
        /// <seealso cref="StreamReader" />
        /// <seealso cref="CategoricalVariable" />
        public static CategoricalDataSet Encode(
            TextReader reader,
            char columnDelimiter,
            IndexCollection extractedColumns,
            bool firstLineContainsVariableNames)
        {
            return Encode(reader,
                columnDelimiter,
                extractedColumns,
                firstLineContainsVariableNames,
                [],
                CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Encodes categorical data from the specified file.
        /// </summary>
        /// <param name="path">
        /// The data file to be opened for reading.
        /// </param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.
        /// </param>
        /// <param name="extractedColumns">
        /// The zero-based indexes of the columns from which 
        /// data are to be extracted.
        /// </param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.
        /// </param>
        /// <returns>
        /// The dataset containing information about the streamed data.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer},IFormatProvider)" 
        /// path="para[@id='SpecialEncode.1']"/>
        /// <para>
        /// Data are encoded applying the <see cref="CultureInfo.InvariantCulture"/>.
        /// </para>        
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool)" 
        /// path="para[@id='Encode.0']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool)" 
        /// path="para[@id='Encode.1']"/>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <b>null</b>. <br/>
        /// -or-<br/>
        /// <paramref name="extractedColumns" /> is <b>null</b>.
        /// </exception> 
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is an empty string ("").
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file cannot be found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="path"/> includes an incorrect or invalid syntax 
        /// for file name, directory name, or volume label.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// The stream to file having the specified 
        /// <paramref name="path" /> contains no data rows.<br/>
        /// -or-<br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="extractedColumns" />. This can happen if 
        /// there are missing columns, 
        /// or if strings representing variable names or category labels,
        /// i.e. tokens extracted 
        /// from the stream, 
        /// are <b>null</b> 
        /// or consist only of white-space characters. 
        /// In some cases, the <see cref="Exception.InnerException"/> 
        /// property is set to add 
        /// further details about the occurred error.
        /// </exception>
        /// <seealso cref="StreamReader" />
        /// <seealso cref="CategoricalVariable" />
        public static CategoricalDataSet Encode(
            string path,
            char columnDelimiter,
            IndexCollection extractedColumns,
            bool firstLineContainsVariableNames)
        {
            using StreamReader reader = new(path);

            return Encode(
                 reader,
                 columnDelimiter,
                 extractedColumns,
                 firstLineContainsVariableNames);
        }

        /// <summary>
        /// Encodes categorical or numerical data from the given file
        /// applying specific numerical data categorizers.
        /// </summary>
        /// <param name="path">
        /// The data file to be opened for reading.
        /// </param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.
        /// </param>
        /// <param name="extractedColumns">
        /// The zero-based indexes of the columns from which 
        /// data are to be extracted.
        /// </param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.
        /// </param>
        /// <param name="specialCategorizers">
        /// A mapping from a subset of extracted column indexes to 
        /// a set of categorizers, to be executed when extracting data 
        /// from the corresponding columns.
        /// </param>
        /// <param name="provider">
        /// An object that provides formatting 
        /// information to parse numeric values.
        /// </param>    
        /// <returns>
        /// The dataset containing information about the streamed data.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.0']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.1']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.2']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.3']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.4']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.5']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.4']"/>
        /// <inheritdoc cref="Encode(TextReader, char, IndexCollection, bool, Dictionary{int, Categorizer}, IFormatProvider)" 
        /// path="para[@id='SpecialEncode.5']"/>
        ///</example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <b>null</b>. <br/>
        /// -or-<br/>
        /// <paramref name="extractedColumns" /> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="specialCategorizers"/> is <b>null</b>. <br/>
        /// -or-<br/>
        /// <paramref name="provider"/> is <b>null</b>.
        /// </exception> 
        /// <exception cref="ArgumentException">
        /// <paramref name="specialCategorizers"/> contains <b>null</b>
        /// values or keys which are
        /// not in the <paramref name="extractedColumns"/> collection.<br/>
        /// -or-<br/>
        /// <paramref name="path"/> is an empty string ("").
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file cannot be found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="path"/> includes an incorrect or invalid syntax 
        /// for file name, directory name, or volume label.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// The stream to file having the specified <paramref name="path" /> contains no data rows.<br/>
        /// -or-<br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="extractedColumns" />. This can happen if there are missing columns, 
        /// or if strings representing variable names or category labels, i.e. tokens extracted 
        /// from the stream or returned by a special categorizer, are <b>null</b> 
        /// or consist only of white-space characters. 
        /// In some cases, the <see cref="Exception.InnerException"/> property is set to add 
        /// further details about the occurred error.
        /// </exception>
        /// <seealso cref="CategorizeByEntropyMinimization(string, char, 
        /// IndexCollection, bool, int, IFormatProvider)"/>
        /// <seealso cref="Categorizer" />
        /// <seealso cref="File"/>
        public static CategoricalDataSet Encode(
            string path,
            char columnDelimiter,
            IndexCollection extractedColumns,
            bool firstLineContainsVariableNames,
            Dictionary<int, Categorizer> specialCategorizers,
            IFormatProvider provider)
        {
            using StreamReader reader = new(path);

            return Encode(
                 reader,
                 columnDelimiter,
                 extractedColumns,
                 firstLineContainsVariableNames,
                 specialCategorizers,
                 provider);
        }

        #endregion

        #region Disjunctive forms

        /// <summary>
        /// Disjoins the specified data.
        /// </summary>
        /// <param name="data">The data to disjoint.</param>
        /// <param name="variables">The variables associated to the columns
        /// of the given data.</param>
        /// <param name="dataParameter">The data parameter name.</param>
        /// <returns>The disjunctive representation of the given data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="data"/> contains an entry which is not a code
        /// for the variable corresponding to the entry column.
        /// </exception>
        internal static DoubleMatrix Disjoin(DoubleMatrix data,
            IList<CategoricalVariable> variables,
            string dataParameter)
        {
            int numberOfVariables = variables.Count;

            int[] numberOfCategories = new int[numberOfVariables];

            for (int j = 0; j < numberOfVariables; j++)
            {
                numberOfCategories[j] = variables[j].Categories.Count;
            }

            DoubleMatrix disjunctiveData = DoubleMatrix.Sparse(
                data.NumberOfRows,
                numberOfCategories.Sum(),
                data.NumberOfRows * numberOfVariables);

            int leadingIndex = 0, c, columnIndex;
            for (int j = 0; j < numberOfVariables; j++)
            {
                if (j > 0)
                {
                    leadingIndex += numberOfCategories[j - 1];
                }

                var partition = IndexPartition.Create(data[":", j]);
                var notRecognizedAsCodes =
                    partition.Identifiers.Except(variables[j].CategoryCodes);
                if (notRecognizedAsCodes.Any())
                {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_MATRIX_ENTRY_NOT_IN_VARIABLE_LIST"),
                        dataParameter);
                }
                c = 0;
                foreach (var category in variables[j].Categories)
                {
                    columnIndex = leadingIndex + c;
                    disjunctiveData.SetColumnName(columnIndex, category.Label);
                    if (partition.TryGetPart(category.Code, out IndexCollection codeIndexes))
                    {
                        disjunctiveData[codeIndexes, columnIndex] += 1.0;
                    }
                    c++;
                }
            }

            return disjunctiveData;
        }

        /// <summary>
        /// Disjoins supplementary data.
        /// </summary>
        /// <param name="supplementaryData">
        /// The supplementary data to disjoint.</param>
        /// <returns>
        /// The disjunctive matrix representing the given 
        /// supplementary data.</returns>
        /// <remarks>
        /// <para>
        /// Data are considered as supplemental with respect to the 
        /// current dataset if they contain
        /// information about the same variables, but observed
        /// at different individuals.
        /// </para>
        /// <para>
        /// The data in a categorical dataset can be represented in 
        /// disjunctive form by means of a
        /// matrix having as many rows as the number of individuals 
        /// in the dataset, while its number
        /// of columns is equal to the sum of the categories in each 
        /// dataset variable.
        /// For a given row-column pair, the matrix has entry equal 
        /// to 1 if the category corresponding
        /// to the given column has been observed at the individual 
        /// corresponding to the given row, otherwise
        /// the entry is zero.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supplementaryData" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="supplementaryData" /> has a number of 
        /// columns other than the number of variables
        /// in the <see cref="CategoricalDataSet" />.<br/>
        /// -or-<br/>
        /// <paramref name="supplementaryData" /> contains an entry 
        /// which is not a category code
        /// for the variable corresponding to the entry column.
        /// </exception>
        public DoubleMatrix Disjoin(DoubleMatrix supplementaryData)
        {
            ArgumentNullException.ThrowIfNull(supplementaryData);

            if (supplementaryData.NumberOfColumns != this.Variables.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_COLUMNS_NOT_EQUAL_TO_DATASET_VARIABLES"),
                    nameof(supplementaryData));
            }

            return CategoricalDataSet.Disjoin(supplementaryData, this.variables,
                "supplementaryData");
        }

        /// <summary>
        /// Disjoins the data of the <see cref="CategoricalDataSet"/>.
        /// </summary>
        /// <remarks>
        /// The data in a categorical dataset can be represented in disjunctive form 
        /// by means of a matrix having as many rows as the number of individuals in 
        /// the dataset, while its number of columns is equal to the sum of the 
        /// categories in each dataset variable.
        /// For a given row-column pair, the matrix has entry equal to 1 if the 
        /// category corresponding to the given column has been observed at the 
        /// individual corresponding to the given row, otherwise the entry is zero.
        /// </remarks>
        /// <returns>The disjunctive matrix representing the data of the 
        /// <see cref="CategoricalDataSet"/>.</returns>
        public DoubleMatrix Disjoin()
        {
            var variables = this.variables;
            var data = this.data;

            int numberOfVariables = variables.Count;

            int[] numberOfCategories = new int[numberOfVariables];

            for (int j = 0; j < numberOfVariables; j++)
            {
                numberOfCategories[j] = variables[j].Categories.Count;
            }

            DoubleMatrix disjunctiveData = DoubleMatrix.Sparse(
                data.NumberOfRows,
                numberOfCategories.Sum(),
                data.NumberOfRows * numberOfVariables);

            int leadingIndex = 0, c, columnIndex;
            for (int j = 0; j < numberOfVariables; j++)
            {
                if (j > 0)
                {
                    leadingIndex += numberOfCategories[j - 1];
                }

                var partition = IndexPartition.Create(data[":", j]);

                c = 0;
                foreach (var category in variables[j].Categories)
                {
                    columnIndex = leadingIndex + c;

                    disjunctiveData.SetColumnName(columnIndex, category.Label);

                    if (partition.TryGetPart(category.Code, out IndexCollection codeIndexes))
                    {
                        disjunctiveData[codeIndexes, columnIndex] += 1.0;
                    }

                    c++;
                }
            }

            return disjunctiveData;
        }

        #endregion

        #region Discretization

        /// <summary>
        /// Discretizes numerical data from the stream underlying the specified file
        /// by defining multiple intervals of the numerical data range.
        /// Intervals are identified by minimizing the intra-interval entropy 
        /// of the specified target data.
        /// </summary>
        /// <param name="path">
        /// The data file to be opened for reading.</param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.</param>
        /// <param name="numericalColumns">
        /// The zero-based indexes of the columns from which numerical
        /// data are to be extracted.</param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.</param>
        /// <param name="targetColumn">
        /// The zero-based index of the column from which target
        /// data are to be extracted.</param>
        /// <param name="provider">
        /// An object that provides formatting information to
        /// parse numeric values.</param>       
        /// <returns>
        /// A mapping from the set of extracted numerical column indexes to 
        /// a set of categorizers of the corresponding data.</returns>
        /// <remarks>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.0']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.1']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.2']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.3']"/>
        /// </remarks>
        /// <example>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.4']"/>
        /// <inheritdoc cref="CategorizeByEntropyMinimization(TextReader, char, IndexCollection, bool, int, IFormatProvider)" 
        /// path="para[@id='CategorizeByEntropyMinimization.5']"/>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="numericalColumns" /> is <b>null</b>.</exception> 
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is an empty string ("").
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file cannot be found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, such as being on an unmapped drive.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="path"/> includes an incorrect or invalid syntax 
        /// for file name, directory name, or volume label.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="targetColumn"/> is negative.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// The stream accessed through file <paramref name="path" /> contains 
        /// no data rows. <br/>
        /// -or-<br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="targetColumn"/> or <paramref name="numericalColumns" />. 
        /// This can happen if there are missing columns, 
        /// or if strings representing target category labels, are <b>null</b> 
        /// or consist only of white-space characters, or if 
        /// strings representing numerical values cannot be converted to 
        /// an equivalent double-precision floating-point number. 
        /// In some cases, the <see cref="Exception.InnerException"/> property 
        /// is set to add 
        /// further details about the occurred error.
        /// </exception>
        /// <seealso cref="Categorizer" />
        public static Dictionary<int, Categorizer> CategorizeByEntropyMinimization(
            string path,
            char columnDelimiter,
            IndexCollection numericalColumns,
            bool firstLineContainsVariableNames,
            int targetColumn,
            IFormatProvider provider)
        {
            using StreamReader reader = new(path);

            return CategorizeByEntropyMinimization(
                 reader,
                 columnDelimiter,
                 numericalColumns,
                 firstLineContainsVariableNames,
                 targetColumn,
                 provider);
        }

        /// <summary>
        /// Discretizes numerical data from the stream underlying the specified 
        /// text reader
        /// by defining multiple intervals of the numerical data range.
        /// Intervals are identified by minimizing the intra-interval 
        /// entropy of the specified target data.
        /// </summary>
        /// <param name="reader">
        /// The reader having access to the data stream.</param>
        /// <param name="columnDelimiter">
        /// The delimiter used to separate columns in data lines.</param>
        /// <param name="numericalColumns">
        /// The zero-based indexes of the columns from which numerical
        /// data are to be extracted.</param>
        /// <param name="firstLineContainsVariableNames">
        /// If set to <c>true</c> signals that the first
        /// line contains variable names.</param>
        /// <param name="targetColumn">
        /// The zero-based index of the column from which target
        /// data are to be extracted.</param>
        /// <param name="provider">
        /// An object that provides formatting information to
        /// parse numeric values.</param>
        /// <returns>
        /// A mapping from the set of extracted numerical column indexes to
        /// a set of categorizers of the corresponding data.
        /// </returns>
        /// <example>
        ///   <para id="CategorizeByEntropyMinimization.4">
        /// In the following example, a stream contains two columns, 
        /// the first corresponding to a numerical variable,
        /// and the second to a categorical one, which is interpreted 
        /// as the target.
        /// A special categorizer, obtained by intra interval entropy 
        /// minimization, is assigned to
        /// the first column to discretize its data, then both columns 
        /// are encoded in a categorical dataset.
        /// </para>
        ///   <para id="CategorizeByEntropyMinimization.5">
        ///     <code title="Categorizing numerical data by intra interval entropy minimization and&#xD;&#xA;subsequent encoding in a categorical dataset" source="..\Novacta.Analytics.CodeExamples\CategoricalEncodeExample1.cs.txt" language="cs" />
        ///   </para>
        /// </example>
        /// <seealso cref="Categorizer" />
        /// <remarks>
        /// <para id="CategorizeByEntropyMinimization.0">
        ///   <b>Data Extraction</b>
        /// </para>
        /// <para id="CategorizeByEntropyMinimization.1">
        /// Each line from the stream is interpreted as the information about
        /// categorical or numerical variables
        /// observed at a given instance. A line is split in tokens, each 
        /// corresponding to a (zero-based) column, which in turn stores the data
        /// of a given variable. Columns are assumed to be
        /// separated each other by the character passed 
        /// as <paramref name="columnDelimiter" />.
        /// Data from a variable are extracted only if the corresponding 
        /// column index is
        /// equal to <paramref name="targetColumn" /> or in the
        /// collection  <paramref name="numericalColumns" />.
        /// </para>
        /// <para id="CategorizeByEntropyMinimization.2">
        ///   <b>Intra Interval Entropy Minimization</b>
        /// </para>
        /// <para id="CategorizeByEntropyMinimization.3">
        /// By default, when encoding a <see cref="CategoricalDataSet" />, 
        /// tokens in a column are interpreted as
        /// category labels of the corresponding variable, which are 
        /// inserted in the
        /// dataset as such. This behavior can be overridden by mapping a
        /// special <see cref="Categorizer" />
        /// to a given column.
        /// Following Fayyad and Irani, 
        /// (1993)<cite>fayyad-irani-1993</cite>, this method selects a
        /// categorizer by splitting the range of the numerical data into
        /// multiple intervals in order to minimize the intra-interval 
        /// heterogeneity of the given target.
        /// A dictionary is returned in which, for each numerical column,
        /// the corresponding categorizer is inserted as a value keyed with the
        /// index of the given column. A special categorizer can be
        /// useful if a given column refers to a numerical variable which must be
        /// discretized before its insertion in a categorical dataset.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader" /> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="numericalColumns" /> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="provider" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="targetColumn" /> is negative.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// The stream accessed by <paramref name="reader" /> contains 
        /// no data rows.<br/>
        /// -or-<br/>
        /// There is at least a row
        /// which contains not enough data for any column specified by 
        /// <paramref name="targetColumn"/> or
        /// <paramref name="numericalColumns" />. 
        /// This can happen if there are missing columns, 
        /// or if strings representing target category labels, are <b>null</b> 
        /// or consist only of white-space characters, or if 
        /// strings representing numerical values cannot be converted to 
        /// an equivalent double-precision floating-point number. 
        /// In some cases, the <see cref="Exception.InnerException"/> property 
        /// is set to add 
        /// further details about the occurred error.
        /// </exception>
        public static Dictionary<int, Categorizer> CategorizeByEntropyMinimization(
            TextReader reader,
            char columnDelimiter,
            IndexCollection numericalColumns,
            bool firstLineContainsVariableNames,
            int targetColumn,
            IFormatProvider provider)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(numericalColumns);
            ArgumentNullException.ThrowIfNull(provider);
            if (targetColumn < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(targetColumn),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            #endregion

            double nextAvailableCode = 0, code;
            CategoricalVariable targetVariable = null;
            string categoryLabel;

            int numberOfNumericalVariables = numericalColumns.Count;

            List<double> rawData =
                new(100 * numberOfNumericalVariables);

            string line;
            string[] tokens;
            int lineNumber = 0, column = -1;

            try
            {
                while ((line = reader.ReadLine()) != null)
                {

                    tokens = line.Split(columnDelimiter);

                    if (0 == lineNumber)
                    {
                        targetVariable = new CategoricalVariable("Target");
                        if (firstLineContainsVariableNames)
                        {
                            lineNumber++;
                            continue;
                        }
                    }

                    for (int i = 0; i < numberOfNumericalVariables; i++)
                    {
                        column = numericalColumns[i];
                        if (column >= tokens.Length)
                        {
                            throw new InvalidDataException(
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    ImplementationServices.GetResourceString(
                                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                    lineNumber,
                                    column));
                        }

                        rawData.Add(Convert.ToDouble(tokens[column], provider));
                    }

                    column = targetColumn;
                    if (column >= tokens.Length)
                    {
                        throw new InvalidDataException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                lineNumber,
                                column));
                    }

                    categoryLabel = tokens[column];
                    bool isCategoryFound =
                        targetVariable.TryGet(categoryLabel, out Category category);
                    if (!isCategoryFound)
                    {
                        code = nextAvailableCode++;
                        targetVariable.Add(code, categoryLabel);
                    }
                    else
                    {
                        code = category.Code;
                    }
                    rawData.Add(code);

                    lineNumber++;
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException(
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    ImplementationServices.GetResourceString(
                                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_ROW_DATA"),
                                    lineNumber,
                                    column),
                                e);
            }

            int numberOfItems = firstLineContainsVariableNames
                ? lineNumber - 1 : lineNumber;

            if (numberOfItems < 1)
            {
                throw new InvalidDataException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_DATASET_NOT_ENOUGH_DATA"));
            }

            DoubleMatrix data = DoubleMatrix.Dense(
                1 + numberOfNumericalVariables,
                numberOfItems,
                [.. rawData]);
            data.InPlaceTranspose();

            // Get categorizers
            var categorizers = new Dictionary<int, Categorizer>();

            var targetData = data[":", numberOfNumericalVariables];
            for (int j = 0; j < numericalColumns.Count; j++)
            {
                var numericalAttributeData = data[":", j];
                var categorizer = Discretize(
                    numericalAttributeData,
                    targetData);
                categorizers.Add(numericalColumns[j], categorizer);
            }

            return categorizers;
        }

        /// <summary>
        /// Tries to split the specified range of numerical blocks.
        /// </summary>
        /// <param name="node">
        /// The node whose data represent the range to be split.</param>
        /// <param name="blocks">
        /// The list of all numerical blocks.</param>
        private static void TrySplitBlockRange(
            TreeNode<NumericalBlockRange> node,
            List<NumericalBlock> blocks)
        {
            NumericalBlockRange bestLeft = null, bestRight = null;
            var range = node.Data;
            double minimalEntropy = Double.PositiveInfinity;
            bool isSplittable = false;

            for (int i = range.FirstIndex; i < range.LastIndex - 1; i++)
            {
                if (!isSplittable)
                {
                    isSplittable = true;
                }
                var left = new NumericalBlockRange(range.FirstIndex, i, blocks);
                var right = new NumericalBlockRange(i + 1, range.LastIndex, blocks);

                // Compute the entropy induced by the bi-partition
                var currentEntropy = GetBipartitionEntropy(left, right);

                // Save the partition if it is the current entropy minimizer
                if (currentEntropy < minimalEntropy)
                {
                    minimalEntropy = currentEntropy;
                    bestLeft = left;
                    bestRight = right;
                }
            }
            if (isSplittable)
            {
                if (CategoricalDataSet.IsBipartitionAcceptableByMdl(
                    minimalEntropy, range, bestLeft, bestRight))
                {
                    node.AddChild(bestLeft);
                    TrySplitBlockRange(node.Children[0], blocks);
                    node.AddChild(bestRight);
                    TrySplitBlockRange(node.Children[1], blocks);
                }
            }
        }

        /// <summary>
        /// Discretizes the specified numerical attribute data with respect 
        /// to the given target data by splitting the range of the numerical 
        /// data into multiple intervals in order to minimize the intra-interval 
        /// heterogeneity of the given target.
        /// </summary>
        /// <param name="numericalAttributeData">
        /// The numerical attribute data.</param>
        /// <param name="targetData">
        /// The target data.</param>
        /// <returns>
        /// The categorizer implementing the optimal discretization.</returns>
        /// <remarks>
        /// <para>
        /// This is based on the algorithm of Elomaa and Rousu 
        /// (1999)<cite>elomaa-rousu-1999</cite>.
        /// </para>
        /// </remarks>
        private static Categorizer Discretize(
            DoubleMatrix numericalAttributeData,
            DoubleMatrix targetData)
        {
            var blocks = NumericalBlock.GetNumericalBlocks(
                numericalAttributeData,
                targetData);

            int firstIndex = 0;
            int lastIndex = blocks.Count - 1;
            var rootRange = new NumericalBlockRange(firstIndex, lastIndex, blocks);
            var root = new TreeNode<NumericalBlockRange>(rootRange);

            CategoricalDataSet.TrySplitBlockRange(root, blocks);

            // Get the categorizer
            var terminalNodes = root.SelfAndDescendants.Where(
                    node => node.IsTerminal).ToArray();
            var categories =
                new Tuple<string, Predicate<double>>[terminalNodes.Length];
            StringBuilder stringBuilder = new();

            for (int i = 0; i < terminalNodes.Length; i++)
            {
                // START WARNING: the following variables must be
                // in the scope of the for loop, otherwise
                // they will be captured in the wrong way when
                // defining the lambda expression used to assign
                // the categorizer
                double lowerBound, upperBound;
                string upperBoundRepresentation, lowerBoundRepresentation;
                // END WARNING
                stringBuilder.Clear();
                var blockRange = terminalNodes[i].Data;
                if (blockRange.FirstIndex == 0)
                {
                    // First block is in the current range
                    stringBuilder.Append("]-Inf,");
                    lowerBound = Double.NegativeInfinity;
                }
                else
                {
                    lowerBound = (blocks[blockRange.FirstIndex - 1].LastValue +
                        blocks[blockRange.FirstIndex].FirstValue) / 2.0;
                    lowerBoundRepresentation =
                        Convert.ToString(lowerBound,
                        NumberFormatInfo.InvariantInfo);
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture, "]{0},", lowerBoundRepresentation);
                    lowerBound =
                        Convert.ToDouble(lowerBoundRepresentation,
                        NumberFormatInfo.InvariantInfo);
                }
                if (blockRange.LastIndex == lastIndex)
                {
                    // Last block is in the current range
                    stringBuilder.Append(" Inf[");
                    upperBound = Double.PositiveInfinity;
                }
                else
                {
                    upperBound = (blocks[blockRange.LastIndex].LastValue +
                        blocks[blockRange.LastIndex + 1].FirstValue) / 2.0;
                    upperBoundRepresentation =
                        Convert.ToString(upperBound,
                        NumberFormatInfo.InvariantInfo);
                    stringBuilder.AppendFormat(
                        CultureInfo.InvariantCulture, " {0}]", upperBoundRepresentation);
                    upperBound =
                        Convert.ToDouble(upperBoundRepresentation,
                        NumberFormatInfo.InvariantInfo);
                }
                Predicate<double> predicate;
                if (Double.IsPositiveInfinity(upperBound))
                {
                    predicate = (x) => { return lowerBound < x && x < upperBound; };
                }
                else
                {
                    predicate = (x) => { return lowerBound < x && x <= upperBound; };
                }

                categories[i] = Tuple.Create(
                    stringBuilder.ToString(), predicate);
            }

            string categorizer(string token, IFormatProvider provider)
            {
                double value = Convert.ToDouble(token, provider);
                string label = null;
                for (int j = 0; j < categories.Length; j++)
                {
                    Tuple<string, Predicate<double>> category = categories[j];
                    if (category.Item2(value))
                    {
                        label = category.Item1;
                        break;
                    }
                }
                return label;
            }

            return categorizer;
        }

        /// <summary>
        /// Determines whether the specified bipartition is acceptable 
        /// for the MDL criterion. 
        /// </summary>
        /// <param name="bipartitionEntropy">
        /// The class entropy of the bipartition.</param>
        /// <param name="parent">
        /// The parent range to be bi-partitioned.</param>
        /// <param name="left">
        /// The left part of the bipartition.</param>
        /// <param name="right">
        /// The right part of the bipartition.</param>
        /// <returns><c>true</c> if the is bipartition acceptable by MDL; 
        /// otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <para>
        /// This is the MDLPC Criterion of Fayyad and Irani 
        /// (1993)<cite>fayyad-irani-1993</cite>.
        /// </para>
        /// </remarks>
        private static bool IsBipartitionAcceptableByMdl(
            double bipartitionEntropy,
            NumericalBlockRange parent,
            NumericalBlockRange left,
            NumericalBlockRange right)
        {
            int k = parent.NumberOfObservedClasses;
            int k1 = left.NumberOfObservedClasses;
            int k2 = right.NumberOfObservedClasses;

            double e = parent.Entropy;
            double e1 = left.Entropy;
            double e2 = right.Entropy;

            int n = parent.NumberOfInstances;

            double gain = e - bipartitionEntropy;

            double delta = Log(Pow(3.0, k) - 2.0, 2.0) - k * e + k1 * e1 + k2 * e2;

            return gain > (Log(n - 1, 2.0) + delta) / n;
        }

        /// <summary>
        /// Gets the Class Information Entropy of the bipartition 
        /// whose parts are represented by the specified ranges of 
        /// numerical blocks.
        /// </summary>
        /// <param name="left">The left part.</param>
        /// <param name="right">The right part.</param>
        /// <returns>The Class Information Entropy of the 
        /// specified bipartition.</returns>
        /// <remarks>
        /// <para>
        /// This is Equation (1) of Fayyad and Irani 
        /// (1993)<cite>fayyad-irani-1993</cite>.
        /// </para>
        /// </remarks>
        private static double GetBipartitionEntropy(
            NumericalBlockRange left,
            NumericalBlockRange right)
        {
            int leftSize = left.NumberOfInstances;
            int rightSize = right.NumberOfInstances;
            int size = leftSize + rightSize;
            return (left.Entropy * leftSize + right.Entropy * rightSize) / size;
        }

        #endregion

        #region IReadOnlyTabularCollection

        /// <inheritdoc/>
        public int NumberOfRows { get { return this.data.NumberOfRows; } }

        /// <inheritdoc/>
        public int NumberOfColumns { get { return this.data.NumberOfColumns; } }

        #region IndexCollection, *

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to 
        /// the specified individuals
        /// and variables.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the individuals to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the variables to get.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains values which 
        /// are not valid row indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains values which are 
        /// not valid column indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public CategoricalDataSet this[
            IndexCollection rowIndexes,
            IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (rowIndexes.maxIndex >= this.data.NumberOfRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (columnIndexes.maxIndex >= this.data.NumberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var subVariables =
                    new List<CategoricalVariable>(columnIndexes.Count);
                foreach (var i in columnIndexes)
                {
                    subVariables.Add(this.variables[i]);
                }

                var subData = this.data[rowIndexes, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding 
        /// to the specified individuals
        /// and variables.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the individuals to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the 
        /// variables to get. The value must be <c>":"</c>, which means that all valid indexes
        /// are specified.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains values 
        /// which are not valid row indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved 
        /// for matrix sub-referencing.</exception>
        public CategoricalDataSet this[
            IndexCollection rowIndexes,
            string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (rowIndexes.maxIndex >= this.data.NumberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (0 != string.Compare(
                        columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndexes),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subVariables = new List<CategoricalVariable>(this.variables);

                var subData = this.data[rowIndexes, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to the specified 
        /// individuals and variable.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the individuals to get.</param>
        /// <param name="columnIndex">
        /// The zero-based index of the variable to get.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/>
        /// is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> contains values
        /// which are not valid row indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> contains a value
        /// which is not a valid column index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public CategoricalDataSet this[
            IndexCollection rowIndexes,
            int columnIndex]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                if (rowIndexes.maxIndex >= this.data.NumberOfRows)
                {
                    throw new ArgumentOutOfRangeException(nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (columnIndex < 0 || this.data.NumberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var subVariables = new List<CategoricalVariable>(1)
                {
                    this.variables[columnIndex]
                };

                var subData = this.data[rowIndexes, columnIndex];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        #endregion

        #region String, *

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to the 
        /// specified individuals and variables.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the 
        /// individuals to get. The value must by <c>":"</c>,
        /// which means that all valid indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the 
        /// variables to get. The value must by <c>":"</c>, 
        /// which means that all valid indexes
        /// are specified.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/>is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> 
        /// is not a string reserved for matrix sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains values
        /// which are not valid column indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public CategoricalDataSet this[
            string rowIndexes,
            IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (columnIndexes.maxIndex >= this.data.NumberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var subVariables =
                    new List<CategoricalVariable>(columnIndexes.Count);
                foreach (var i in columnIndexes)
                {
                    subVariables.Add(this.variables[i]);
                }

                var subData = this.data[rowIndexes, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to the 
        /// specified individuals
        /// and variables.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the 
        /// individuals to get. The value must by <c>":"</c>, 
        /// which means that all valid indexes
        /// are specified.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the 
        /// variables to get. The value must by <c>":"</c>, 
        /// which means that all valid indexes
        /// are specified.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> is not a string reserved 
        /// for matrix sub-referencing.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved 
        /// for matrix sub-referencing.
        /// </exception>
        public CategoricalDataSet this[
            string rowIndexes,
            string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subVariables = new List<CategoricalVariable>(this.variables);

                var subData = this.data[rowIndexes, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to the 
        /// specified individuals
        /// and variable.
        /// </summary>
        /// <param name="rowIndexes">
        /// The zero-based indexes of the 
        /// individuals to get. The value must by <c>":"</c>, 
        /// which means that all valid indexes
        /// are specified.</param>
        /// <param name="columnIndex">
        /// The zero-based index of the variable to get.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rowIndexes"/>
        /// is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndexes"/> 
        /// is not a string reserved for matrix sub-referencing.<br/> 
        /// -or-<br/>
        /// <paramref name="columnIndex"/> contains a value
        /// which is not a valid column index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public CategoricalDataSet this[
            string rowIndexes,
            int columnIndex]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(rowIndexes);

                if (0 != string.Compare(rowIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                if (columnIndex < 0 || this.data.NumberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var subVariables = new List<CategoricalVariable>(1)
                {
                    this.variables[columnIndex]
                };

                var subData = this.data[rowIndexes, columnIndex];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        #endregion

        #region int, *

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to 
        /// the specified individual and variables.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based index of the individual to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the variables to get.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> contains a value which 
        /// is not a valid row index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> contains values which 
        /// are not valid column indexes for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public CategoricalDataSet this[
            int rowIndex,
            IndexCollection columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (rowIndex < 0 || this.data.NumberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (columnIndexes.maxIndex >= this.data.NumberOfColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var subVariables =
                    new List<CategoricalVariable>(columnIndexes.Count);
                foreach (var i in columnIndexes)
                {
                    subVariables.Add(this.variables[i]);
                }

                var subData = this.data[rowIndex, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to the 
        /// specified individual and variables.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based index of the individual to get.</param>
        /// <param name="columnIndexes">
        /// The zero-based indexes of the 
        /// variables to get. The value must by <c>":"</c>, which means 
        /// that all valid indexes
        /// are specified.</param>
        /// <value>
        /// The categorical dataset containing the specified information.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="columnIndexes"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> contains a value 
        /// which is not a valid row index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndexes"/> is not a string reserved 
        /// for matrix sub-referencing.</exception>
        public CategoricalDataSet this[
            int rowIndex,
            string columnIndexes]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(columnIndexes);

                if (rowIndex < 0 || this.data.NumberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (0 != string.Compare(columnIndexes, ":", StringComparison.Ordinal))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndexes),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX"));
                }

                var subVariables = new List<CategoricalVariable>(this.variables);

                var subData = this.data[rowIndex, columnIndexes];

                return new CategoricalDataSet(subVariables, subData);
            }
        }

        /// <summary>
        /// Gets the information
        /// in the <see cref="CategoricalDataSet" /> corresponding to 
        /// the specified individual and variable.
        /// </summary>
        /// <param name="rowIndex">
        /// The zero-based index of the individual to get.</param>
        /// <param name="columnIndex">
        /// The zero-based index of the variable to get.</param>
        /// <value>
        /// The category of the specified variable observed at the specified individual.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> contains a value 
        /// which is not a valid row index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.<br/>
        /// -or-<br/>
        /// <paramref name="columnIndex"/> contains a value 
        /// which is not a valid column index for the 
        /// <see cref="Data"/> of 
        /// the <see cref="CategoricalDataSet"/>.
        /// </exception>
        public Category this[
            int rowIndex,
            int columnIndex]
        {
            get
            {
                if (rowIndex < 0 || this.data.NumberOfRows <= rowIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(rowIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                if (columnIndex < 0 || this.data.NumberOfColumns <= columnIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(columnIndex),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS"));
                }

                var categoryCode = this.data[rowIndex, columnIndex];
                var variable = this.variables[columnIndex];

                variable.TryGet(categoryCode, out Category category);

                return category;
            }
        }

        #endregion

        #endregion
    }
}
