// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to draw samples having the specified
    /// size from a finite population.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A finite population of size <latex mode="inline">M</latex> can be 
    /// defined as the 
    /// set <latex mode="inline">\mathcal{P}=\{0,\dots,M-1\}</latex>. 
    /// Elements of <latex mode="inline">\mathcal{P}</latex> are referred 
    /// to as <i>units</i>. Property <see cref="PopulationSize"/> should 
    /// return the size of the population.
    /// </para>
    /// <para>
    /// A <i>sample</i> is every non empty subset 
    /// of <latex mode="inline">\mathcal{P}</latex>.
    /// Class <see cref="RandomSampling"/> exposes several methods to 
    /// draw samples
    /// having size returned by <see cref="SampleSize"/> from a finite 
    /// population.
    /// </para>
    /// <para id='next'>
    /// Method <see cref="NextIndexCollection()"/> returns a sample 
    /// which is represented as 
    /// an <see cref="IndexCollection"/> instance
    /// having <see cref="IndexCollection.Count"/> equal 
    /// to <see cref="SampleSize"/> and
    /// whose indexes are less than <see cref="PopulationSize"/> .
    /// </para>
    /// <para id='nextdoublematrix'>
    /// Method <see cref="NextDoubleMatrix()"/> returns
    /// a sample represented as a <see cref="DoubleMatrix"/> instance, having 
    /// one row and a number of columns equal to <see cref="PopulationSize"/>.
    /// In this context, the sample is the set of linear indexes corresponding 
    /// to entries in the returned matrix 
    /// equal to <c>1</c>, other entries storing <c>0</c> otherwise. For
    /// an introduction to linear indexes, see the remarks of the
    /// <see cref="DoubleMatrix"/> class.
    /// </para>
    /// </remarks>
    /// <seealso cref="UnequalProbabilityRandomSampling"/>
    /// <seealso cref="SimpleRandomSampling"/>
    /// <seealso cref="RandomDevice" />
    public abstract class RandomSampling : RandomDevice
    {
        /// <summary>
        /// Gets the size of the population from which this
        /// instance draws samples.
        /// </summary>
        /// <value>The size of the sampled population.</value>
        public abstract int PopulationSize { get; }

        /// <summary>
        /// Gets the size of the samples drawn by this
        /// instance.
        /// </summary>
        /// <value>The size of the samples drawn by this instance.</value>
        public abstract int SampleSize { get; }

        /// <summary>
        /// Draws a random sample represented as a <see cref="DoubleMatrix"/>
        /// instance.
        /// </summary>
        /// <returns>The matrix of doubles representing the sample.</returns>
        /// <remarks>
        /// <inheritdoc cref="RandomSampling" 
        /// path="para[@id='nextdoublematrix']"/>
        /// </remarks>
        public abstract DoubleMatrix NextDoubleMatrix();

        /// <summary>
        /// Draws a sample represented as an <see cref="IndexCollection"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// The collection of indexes representing the sample.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <see cref="NextIndexCollection()"/> method allows derived classes to
        /// implement their own algorithm to draw the sample.
        /// </para>
        /// <inheritdoc cref="RandomSampling" 
        /// path="para[@id='next']"/>
        /// </remarks>
        public abstract IndexCollection NextIndexCollection();

        /// <summary>
        /// Gets the inclusion probabilities of the 
        /// population units.
        /// </summary>
        /// <value>A matrix whose entries represent the inclusion probabilities
        /// of the population units.</value>
        /// <remarks>
        /// <para>
        /// The <see cref="InclusionProbabilities"/> property allows 
        /// derived classes to
        /// implement their own algorithm to compute the inclusion probabilities
        /// of the population units.
        /// </para>
        /// <para><b>Notes to Inheritors</b></para>
        /// <para>
        /// Population units are indexed from <c>0</c> to <see cref="PopulationSize"/>
        /// minus one. 
        /// When overriding <see cref="InclusionProbabilities"/>, 
        /// a column vector should be returned having a number of rows equal to 
        /// <see cref="PopulationSize"/>, in which the <c>i</c>-th entry stores the 
        /// inclusion probability of unit <c>i</c>.
        /// </para>
        /// </remarks>
        public abstract ReadOnlyDoubleMatrix InclusionProbabilities { get; }
    }
}
