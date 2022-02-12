// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Novacta.Analytics
{
    class ComplexMatrixEnumerator : IEnumerator<Complex>
    {
        private readonly ComplexMatrix matrix;
        private int position = -1;

        public ComplexMatrixEnumerator(ComplexMatrix matrix)
        {
            this.matrix = matrix;
        }

        ~ComplexMatrixEnumerator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                /*
                 * The object is being explicitly disposed/closed, not finalized.
                 * It is therefore safe for category in this "if" statement to access 
                 * fields that reference other objects, because the Finalize() 
                 * method of these other objects hasn't yet been called
                 */
            }
        }

        public bool MoveNext()
        {
            this.position++;
            return (this.position < this.matrix.Count);
        }

        public void Reset()
        {
            this.position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                if ((this.position < 0) || (this.position >= this.matrix.Count))
                {
                    throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ENU_OUT_OF_BOUNDS"));
                }

                return this.matrix[this.position];
            }
        }

        public Complex Current
        {
            get
            {
                if ((this.position < 0) || (this.position >= this.matrix.Count))
                {
                    throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ENU_OUT_OF_BOUNDS"));
                }

                return this.matrix[this.position]; ;
            }
        }

    }
}