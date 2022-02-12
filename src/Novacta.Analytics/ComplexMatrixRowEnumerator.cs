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
    class ComplexMatrixRowEnumerator : IEnumerator<Complex>
    {
        private readonly ComplexMatrixRow matrixRow;
        private int position = -1;

        public ComplexMatrixRowEnumerator(ComplexMatrixRow matrixRow)
        {
            this.matrixRow = matrixRow;
        }

        ~ComplexMatrixRowEnumerator()
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
            return (this.position < this.matrixRow.Length);
        }

        public void Reset()
        {
            this.position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                if ((this.position < 0) || (this.position >= this.matrixRow.Length))
                {
                    throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ENU_OUT_OF_BOUNDS"));
                }

                return this.matrixRow[this.position];
            }
        }

        public Complex Current
        {
            get
            {
                if ((this.position < 0) || (this.position >= this.matrixRow.Length))
                {
                    throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_ENU_OUT_OF_BOUNDS"));
                }

                return this.matrixRow[this.position];
            }
        }
    }
}