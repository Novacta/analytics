// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Infrastructure
{
    internal delegate void
        MatrixInPlaceApplyOperator<TData>(
            MatrixImplementor<TData> matrix,
            Func<TData, TData> func);
}
