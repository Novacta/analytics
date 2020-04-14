// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a device whose operations need 
    /// a <see cref="Analytics.RandomNumberGenerator"/>.
    /// </summary>
    public abstract class RandomDevice
    {
        #region Random number generator

        private RandomNumberGenerator randomNumberGenerator;

        /// <summary>
        /// Gets or sets the basic random generator for this instance.
        /// </summary>
        /// <value>The basic random generator of this instance.</value>
        /// <remarks>
        /// <para>
        /// The <see cref="RandomNumberGenerator"/> is
        /// a <see cref="Analytics.RandomNumberGenerator"/> able to sample random data
        /// from a continuous uniform distribution defined on the interval
        /// <c>[0, 1)</c>. Subclasses define transformation routines of
        /// such data in order to stream samples from the specific distributions
        /// they represent.
        /// If not set, it defaults to the single instruction, multiple data 
        /// Mersenne Twister 19937 generator 
        /// having seed equal to <c>0</c>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <b>null</b>.
        /// </exception>
        /// <seealso cref="RandomNumberGenerator.CreateSFMT19937(int)"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Mersenne_Twister"/>
        public RandomNumberGenerator RandomNumberGenerator
        {
            get
            {
                if (this.randomNumberGenerator is null) {
                    this.randomNumberGenerator = 
                        RandomNumberGenerator.CreateSFMT19937(0);
                }
                return this.randomNumberGenerator;
            }
            set
            {
                if (value is null) {
                    throw new ArgumentNullException(nameof(value));
                }
                this.randomNumberGenerator = value;
            }
        }

        #endregion
    }
}
