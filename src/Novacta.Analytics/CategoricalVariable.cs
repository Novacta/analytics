// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Novacta.Analytics.Infrastructure;
using System.Collections;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a statistical variable that can take on one of a 
    /// fixed number of possible values.
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>You can instantiate a <b>CategoricalVariable</b> object 
    /// by calling its 
    /// constructor. See 
    /// the <see cref="CategoricalVariable(string)">
    /// CategoricalVariable</see> constructor summary.</para>
    /// <para><b>Categories</b></para>
    /// <para>
    /// Each of the possible values of a categorical variable is 
    /// represented by an instance of type
    /// <see cref="Category"/>. Categories can be added to or removed 
    /// from a given categorical variable,
    /// provided that it wasn't set as read-only by 
    /// calling <see cref="SetAsReadOnly"/>. 
    /// This condition can be verified by inspecting 
    /// property <see cref="IsReadOnly"/>.
    /// Categories must be identifiable, both by their
    /// <see cref="Category.Code"/> and by their
    /// <see cref="Category.Label"/>.
    /// As a consequence, a category can be added only if, in the 
    /// given categorical variable, 
    /// no other category already exists 
    /// having the same code or the same label. Furthermore, a 
    /// category can be removed by passing its 
    /// code to 
    /// method 
    /// <see cref="CategoricalVariable.Remove(double)"/>, or
    /// its label to method
    /// <see cref="CategoricalVariable.Remove(string)"/>.
    /// </para>
    /// <para>
    /// Use property <see cref="NumberOfCategories"/> 
    /// to know how many categories are currently
    /// associated to a given variable.
    /// Property <see cref="Categories"/> lists 
    /// category instances. Category codes, 
    /// or labels, 
    /// can be enumerated by inspecting, respectively, properties 
    /// <see cref="CategoryCodes"/>
    /// and <see cref="CategoryLabels"/>.
    /// </para>
    /// <para>
    /// You can verify if a category is 
    /// in the variable by passing its code to method
    /// <see cref="TryGet(double, out Category)"/>, or
    /// its label to method
    /// <see cref="TryGet(string, out Category)"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Category"/>
    /// <seealso cref="CategoricalDataSet"/>
    public class CategoricalVariable : IEnumerable<Category>
    {
        #region Status

        private string name;
        private readonly List<Category> categories = new();

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; 
        /// otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get; private set; } = false;

        /// <summary>
        /// Gets or sets the variable name.
        /// </summary>
        /// <value>The variable name.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is
        /// <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is empty, or consists only 
        /// of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="CategoricalVariable"/> is read-only.</exception>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.ThrowIfVariableIsReadOnly();

                ImplementationServices.ThrowOnNullOrWhiteSpace(
                    value, nameof(value));

                this.name = value;
            }
        }

        /// <summary>
        /// Gets the number of categories.
        /// </summary>
        /// <value>The number of categories.</value>
        public int NumberOfCategories { get { return this.categories.Count; } }

        /// <summary>
        /// Exposes the list of categories
        /// in the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <value>The read only list of categories.</value>
        public IReadOnlyList<Category> Categories { get { return this.categories; } }

        /// <summary>
        /// Exposes the enumerator which iterates over the collection 
        /// of category codes
        /// in the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <value>The enumerable collection of category codes.</value>
        public IEnumerable<double> CategoryCodes
        {
            get
            {
                foreach (var category in this.categories)
                {
                    yield return category.Code;
                }
            }
        }

        /// <summary>
        /// Exposes the enumerator which iterates over the collection 
        /// of category labels
        /// in the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <value>The enumerable collection of category labels.</value>
        public IEnumerable<string> CategoryLabels
        {
            get
            {
                foreach (var category in this.categories)
                {
                    yield return category.Label;
                }
            }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CategoricalVariable" /> class 
        /// having the specified name.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="name"/> is empty, or consists only of 
        /// white-space characters.
        /// </exception>
        public CategoricalVariable(string name)
        {
            ImplementationServices.ThrowOnNullOrWhiteSpace(
                name, nameof(name));

            this.name = name;
        }

        #endregion

        #region Object

        /// <summary>
        /// Returns a <see cref="System.String" /> that 
        /// represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that 
        /// represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();

            // Variable Name
            builder.Append(this.Name);

            // Variable Categories
            builder.Append(": ");

            int numberOfCategories = this.categories.Count;

            builder.Append("[");

            for (int i = 0; i < numberOfCategories; i++)
            {
                builder.Append(string.Format(
                    CultureInfo.InvariantCulture, "{0}", this.categories[i]));
                if (i < numberOfCategories - 1)
                {
                    builder.Append(", ");
                }
            }

            builder.Append("]");

            return builder.ToString();
        }

        #endregion

        #region Categories

        /// <summary>
        /// Sets the <see cref="CategoricalVariable"/> as read only.
        /// </summary>
        /// <remarks>
        /// An instance cannot be reverted to a writable state 
        /// after being set as read-only.
        /// </remarks>
        public void SetAsReadOnly()
        {
            this.IsReadOnly = true;
        }

        /// <summary>
        /// Tries to get the category corresponding to a given label.
        /// </summary>
        /// <param name="categoryLabel">
        /// Name of the category to retrieve.</param>
        /// <param name="category">
        /// The category corresponding to <paramref name="categoryLabel"/>.</param>
        /// <returns>
        /// Returns <c>true</c> if a category having as label
        /// <paramref name="categoryLabel"/> was found; 
        /// otherwise, <c>false</c>.</returns>
        public bool TryGet(string categoryLabel, out Category category)
        {
            bool isCategoryFound = false;
            category = null;
            foreach (var c in this.categories)
            {
                if (string.CompareOrdinal(categoryLabel, c.Label) == 0)
                {
                    category = c;
                    isCategoryFound = true;
                    break;
                }
            }

            return isCategoryFound;
        }

        /// <summary>
        /// Tries to get the category corresponding to a given code.
        /// </summary>
        /// <param name="categoryCode">
        /// Code of the category to retrieve.</param>
        /// <param name="category">
        /// The category corresponding to <paramref name="categoryCode"/>.</param>
        /// <returns>
        /// Returns <c>true</c> if a category having as code
        /// <paramref name="categoryCode"/> was found; 
        /// otherwise, <c>false</c>.</returns>
        public bool TryGet(double categoryCode, out Category category)
        {
            bool isCategoryFound = false;
            category = null;
            foreach (var c in this.categories)
            {
                if (categoryCode.CompareTo(c.Code) == 0)
                {
                    category = c;
                    isCategoryFound = true;
                    break;
                }
            }

            return isCategoryFound;
        }

        private void ThrowIfVariableIsReadOnly()
        {
            if (this.IsReadOnly)
            {
                throw new InvalidOperationException(
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }
        }

        private void ThrowIfCategoryAlreadyExists(
            double categoryCode,
            string categoryLabel)
        {
            foreach (var category in this.categories)
            {
                if (category.Code.CompareTo(categoryCode) == 0
                    || string.CompareOrdinal(category.Label, categoryLabel) == 0)
                {
                    throw new InvalidOperationException(
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_CAT_ALREADY_EXISTS_IN_VARIABLE_LIST"));
                }
            }
        }

        /// <summary>
        /// Adds a category having the specified code and a default 
        /// label to
        /// the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <param name="categoryCode">The category code.</param>
        /// <remarks>
        /// The category label defaults to the result of the 
        /// conversion of the category code to a string, its
        /// description to the empty string.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="CategoricalVariable"/> is read-only.<br/>
        /// -or-<br/>
        /// A category with the same code or label
        /// already exists in the <see cref="CategoricalVariable"/>.
        /// </exception>
        public void Add(double categoryCode)
        {
            this.Add(
                categoryCode,
                Convert.ToString(categoryCode, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a category having the specified code and label to
        /// the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <param name="code">The category code.</param>
        /// <param name="label">The category label.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="label"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="label"/> is empty, or consists only of 
        /// white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="CategoricalVariable"/> is read-only.<br/>
        /// -or-<br/>
        /// A category with the same code or label
        /// already exists in the <see cref="CategoricalVariable"/>.
        /// </exception>
        public void Add(double code, string label)
        {
            ImplementationServices.ThrowOnNullOrWhiteSpace(label, nameof(label));

            this.ThrowIfVariableIsReadOnly();
            this.ThrowIfCategoryAlreadyExists(code, label);

            this.categories.Add(new Category(code, label));
        }

        /// <summary>
        /// Removes the category having the specified code from  
        /// the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <param name="code">The code of the category to be removed.</param>
        /// <returns>
        /// <c>true</c> if the element is successfully found and 
        /// removed; otherwise,
        /// <c>false</c>. This method returns <c>false</c> if 
        /// there is no category in the 
        /// current object having code equal to <paramref name="code"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="CategoricalVariable"/> is read-only.</exception>
        public bool Remove(double code)
        {
            this.ThrowIfVariableIsReadOnly();

            bool isCodeContained = false;
            foreach (var category in this.categories)
            {
                if (code == category.Code)
                {
                    this.categories.Remove(category);
                    isCodeContained = true;
                    break;
                }
            }
            return isCodeContained;
        }

        /// <summary>
        /// Removes the category having the specified label from  
        /// the <see cref="CategoricalVariable"/>.
        /// </summary>
        /// <param name="label">
        /// The label of the category to be removed.</param>
        /// <returns>
        /// <c>true</c> if the element is successfully found and 
        /// removed; otherwise,
        /// <c>false</c>. This method returns <c>false</c> 
        /// if there is no category in the 
        /// current object having label equal to <paramref name="label"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="label"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="CategoricalVariable"/> is read-only.</exception>
        public bool Remove(string label)
        {
            this.ThrowIfVariableIsReadOnly();

            if (label is null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            bool isNameContained = false;
            foreach (var category in this.categories)
            {
                if (label.Equals(category.Label, StringComparison.InvariantCulture))
                {
                    this.categories.Remove(category);
                    isNameContained = true;
                    break;
                }
            }
            return isNameContained;
        }

        #endregion

        #region Conversion operators

        /// <summary>
        /// Performs an explicit conversion 
        /// from <see cref="CategoricalVariable"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">
        /// The object to convert.</param>
        /// <remarks>
        /// <para>
        /// The variable is converted to a column vector whose number 
        /// of rows equals the number of 
        /// categories in the variable. Each row corresponds to a category,
        /// whose label is the row name,
        /// while the category code is the matrix entry in the row.
        /// The name of the matrix is set equal to the variable name.
        /// </para>
        /// </remarks>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static explicit operator DoubleMatrix(CategoricalVariable value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int numberOfRows = value.NumberOfCategories;
            var doubleMatrix = DoubleMatrix.Dense(numberOfRows, 1);

            doubleMatrix.Name = value.Name;

            int i = 0;
            foreach (var category in value.Categories)
            {
                doubleMatrix[i] = category.Code;
                doubleMatrix.SetRowName(i, category.Label);
                i++;
            }

            return doubleMatrix;
        }

        /// <summary>
        /// Converts from <see cref="CategoricalVariable"/> to <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <param name="value">
        /// The object to convert.</param>
        /// <remarks>
        /// <para>
        /// The variable is converted to a column vector whose number 
        /// of rows equals the number of 
        /// categories in the variable. Each row corresponds to a category,
        /// whose label is the row name,
        /// while the category code is the matrix entry in the row.
        /// The name of the matrix is set equal to the variable name.
        /// </para>
        /// </remarks>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix ToDoubleMatrix(CategoricalVariable value)
        {
            return (DoubleMatrix)value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="DoubleMatrix" /> to
        /// <see cref="CategoricalVariable" />.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// The converted object.</returns>
        /// <remarks>
        /// The matrix is successfully converted only if it represents 
        /// a column vector.
        /// The variable result of the conversion will have a number 
        /// of categories equals the
        /// matrix number of rows. Each matrix row corresponds to a 
        /// category, whose code is
        /// the value on the row, while its label is the row name, 
        /// if any, or
        /// the result of the conversion of the code to a string.
        /// The name of the matrix is set equal to the variable name.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is not a column vector.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> has two equal entries.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> has two rows having the same name.
        /// </exception>
        public static explicit operator CategoricalVariable(DoubleMatrix value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!value.IsColumnVector)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR"));
            }

            var variable = new CategoricalVariable(value.Name);

            int numberOfCategories = value.NumberOfRows;
            for (int i = 0; i < numberOfCategories; i++)
            {
                if (value.RowNames.TryGetValue(i, out string rowName))
                {
                    variable.Add(value[i], rowName);
                }
                else
                {
                    variable.Add(value[i]);
                }
            }

            return variable;
        }

        /// <summary>
        /// Converts 
        /// from <see cref="DoubleMatrix" /> to
        /// <see cref="CategoricalVariable" />.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <remarks>
        /// The matrix is successfully converted only if it represents a 
        /// column vector.
        /// The variable result of the conversion will have a number of 
        /// categories equal the
        /// matrix number of rows. Each matrix row corresponds to a category,
        /// whose code is
        /// the value on the row, while its label is the row name, if any, or
        /// the result of the conversion of the code to a string.
        /// The name of the matrix is set equal to the variable name.
        /// </remarks>
        /// <returns>The converted object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is not a column vector.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> has two equal entries.<br/>
        /// -or-<br/>
        /// <paramref name="value"/> has two rows having the same name.
        /// </exception>
        public static CategoricalVariable FromDoubleMatrix(DoubleMatrix value)
        {
            return (CategoricalVariable)value;
        }

        #endregion

        #region IEnumerable

        /// <inheritdoc/>
        public IEnumerator<Category> GetEnumerator()
        {
            return ((IEnumerable<Category>)this.categories).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Category>)this.categories).GetEnumerator();
        }

        #endregion
    }
}