// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics
{
    /// <summary>
    /// Contains constants for controlling if an operation must be executed
    /// on the rows or the columns of a data matrix.
    /// </summary>
    /// <remarks>
    /// A <see cref="DataOperation"/> constant can be used to specify 
    /// if a given operation must be executed separately on rows or on columns.
    /// For example, some methods in class
    /// <see cref="Stat"/>
    /// have a parameter of type 
    /// <see cref="DataOperation"/>. If constant <see cref="OnColumns"/> is
    /// passed as the corresponding argument, then the method operates separately on 
    /// the columns of the 
    /// data and returns a collection of results, one for each column. If, otherwise, the
    /// constant <see cref="OnRows"/> is the argument, 
    /// then the method operates separately on the rows of the 
    /// data and returns a collection of row results. 
    /// </remarks>
    public enum DataOperation
    {
        /// <summary>
        /// The operation is applied separately on each row of the data.
        /// </summary>
        OnRows = 0,
        /// <summary>
        /// The operation is applied separately on each column of the data.
        /// </summary>
        OnColumns = 1
    }
}
