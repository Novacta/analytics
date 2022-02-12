// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics.Interop
{
    /// <summary>
    /// Wraps a handle to a VSL stream state descriptor.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class represents a partial wrapper for the VSL's random stream 
    /// state descriptor.
    /// </para>
    /// </remarks>
    class VslSafeStreamStateDescriptor : SafeHandle
    {
        /// <summary>
        /// Creates a new <see cref="VslSafeStreamStateDescriptor"/> instance 
        /// to wrap the specified VSL stream state descriptor.
        /// </summary>
        /// <param name="streamStateDescriptor">
        /// The stream state descriptor to wrap.</param>
        /// <remarks>
        /// <para>
        /// This constructor calls the base <see cref="SafeHandle" /> one,
        /// with <see cref="IntPtr.Zero" /> as the invalid handle value
        /// and <c>true</c> to signal that this instance will own the
        /// handle during the finalization phase.
        /// </para>
        /// </remarks>
        private VslSafeStreamStateDescriptor(IntPtr streamStateDescriptor)
            : base(IntPtr.Zero, true)
        {
            this.SetHandle(streamStateDescriptor);
        }

        /// <summary>
        /// Creates a VSL stream state descriptor
        /// for the specified matrix.
        /// </summary>
        /// <param name="brng">The basic random number generator index
        /// to initialize the stream.</param>
        /// <param name="seed">The seed of the stream.</param>
        /// <returns>The <see cref="VslSafeStreamStateDescriptor"/> instance
        /// wrapping the specified stream state descriptor.</returns>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during the initialization of the random
        /// stream.
        /// </exception>
        public static VslSafeStreamStateDescriptor Create(
           int brng,
           int seed)
        {
            var streamStateDescriptor = IntPtr.Zero;
            var status = SafeNativeMethods.VSL.NewStream(
                ref streamStateDescriptor, brng, seed);

            if (status != SafeNativeMethods.VSL.STATUS.VSL_STATUS_OK)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_VSL_CANNOT_CREATE_STREAM_DESCRIPTOR"));
            }

            return new VslSafeStreamStateDescriptor(streamStateDescriptor);
        }

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            var status = SafeNativeMethods.VSL.DeleteStream(ref this.handle);
            return status == SafeNativeMethods.VSL.STATUS.VSL_STATUS_OK;
        }

        /// <inheritdoc/>
        public override bool IsInvalid
        {
            get
            {
                return this.handle == IntPtr.Zero;
            }
        }
    }
}