// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Infrastructure
{
    internal delegate TData FindExtremumOperator<TData>(MatrixImplementor<TData> data, out int index);
}
