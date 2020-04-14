// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Infrastructure
{
    internal delegate MatrixImplementor<TData> 
        ByDimensionSummaryOperator<TData>(
            MatrixImplementor<TData> data, 
            int dimension);
}
