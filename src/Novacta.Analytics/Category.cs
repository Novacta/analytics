// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a value that can be taken on by a categorical variable.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="Category"/> instance represents a possible value of a
    /// categorical variable by means of a real number, 
    /// the <see cref="Code"/>, and a
    /// string, the <see cref="Label"/>.
    /// </para>
    /// <para>
    /// A <see cref="Category"/> object is immutable (read-only), 
    /// because its value cannot be modified after it has been instantiated.
    /// Categories can be created only by calling method 
    /// <see cref="CategoricalVariable.Add(double)"/> or
    /// <see cref="CategoricalVariable.Add(double, string)"/>, 
    /// adding them to 
    /// a <see cref="CategoricalVariable"/> object.
    /// </para>
    /// <para>Note that a category is identified by its 
    /// <see cref="Code"/> or by its <see cref="Label"/>. 
    /// As a consequence, a 
    /// <see cref="Category"/> cannot be added to 
    /// a <see cref="CategoricalVariable"/> 
    /// if such variable already contains another category having
    /// the same code or the same 
    /// label of the category to be added.</para>
    /// </remarks>
    /// <seealso cref="CategoricalVariable"/>
    [Serializable]
    public class Category : ISerializable
    {
        #region ISerializable

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="Category"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityPermission(
            SecurityAction.Demand,
            SerializationFormatter = true),
         System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1801:ReviewUnusedParameters",
            Justification = "Constructor requires parameter context.")]
        protected Category(
            SerializationInfo info,
            StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            this.Code =
                (double)info.GetValue(
                    "Code",
                    typeof(double));

            this.Label =
                (string)info.GetValue(
                    "Label",
                    typeof(string));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(
                "Code",
                this.Code,
                typeof(double));

            info.AddValue(
                "Label",
                this.Label,
                typeof(string));
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="code">The category code.</param>
        internal Category(double code)
            : this(code, Convert.ToString(code, CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
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
        internal Category(double code, string label)
        {
            ImplementationServices.ThrowOnNullOrWhiteSpace(
                label, 
                nameof(label));

            this.Code = code;
            this.Label = label;
        }

        /// <summary>
        /// Gets the category code.
        /// </summary>
        /// <value>The category code.</value>
        public double Code { get; }

        /// <summary>
        /// Gets the category label.
        /// </summary>
        /// <value>The category label.</value>
        public string Label { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(this.Code.ToString(CultureInfo.InvariantCulture));

            stringBuilder.Append(" - " + this.Label);

            return stringBuilder.ToString();
        }
    }
}