using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novacta.Analytics
{
    /// <summary>
    /// Defines properties to evaluate patterns
    /// in complex matrices. 
    /// </summary>
    public interface IComplexMatrixPatterns : IMatrixPatterns
    {
        /// <summary>
        /// Gets a value indicating whether this instance is Hermitian.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is Hermitian; 
        /// otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Property <see cref="IsHermitian"/> returns
        /// <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=\left(A_{j,i}\right)^{*},</latex>
        /// where <latex>z^*\left</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Hermitian_matrix" />
        bool IsHermitian
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is Skew-Hermitian.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is Skew-Hermitian; 
        /// otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <inheritdoc cref="DoubleMatrix" 
        /// path="para[@id='matrix']"/>
        /// <para>
        /// Property <see cref="IsSkewHermitian"/> returns
        /// <c>true</c> if <latex mode="inline">m=n</latex> and
        /// <latex mode="inline">A_{i,j}=-\left(A_{j,i}\right)^{*},</latex>
        /// where <latex>z^*\left</latex> is the conjugate of complex <latex>z</latex>.
        /// </para>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Skew-Hermitian_matrix" />
        bool IsSkewHermitian
        {
            get;
        }
    }
}
