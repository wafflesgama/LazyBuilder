﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteLock.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Provides a convenience methodology for implementing writeable locked access to resources.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Common.Helpers
{
    using System;
    using System.Threading;

    /// <summary>
    /// Provides a convenience methodology for implementing writable locked access to resources.
    /// </summary>
    /// <remarks>
    /// Adapted from identically named class within <see href="https://github.com/umbraco/Umbraco-CMS"/>
    /// </remarks>
    internal sealed class WriteLock : IDisposable
    {
        /// <summary>
        /// The locker to lock against.
        /// </summary>
        private readonly ReaderWriterLockSlim locker;

        /// <summary>
        /// A value indicating whether this instance of the given entity has been disposed.
        /// </summary>
        /// <value><see langword="true"/> if this instance has been disposed; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// If the entity is disposed, it must not be disposed a second
        /// time. The isDisposed field is set the first time the entity
        /// is disposed. If the isDisposed field is true, then the Dispose()
        /// method will not dispose again. This help not to prolong the entity's
        /// life in the Garbage Collector.
        /// </remarks>
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteLock"/> class.
        /// </summary>
        /// <param name="locker">
        /// The locker.
        /// </param>
        public WriteLock(ReaderWriterLockSlim locker)
        {
            this.locker = locker;
            this.locker.EnterWriteLock();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="WriteLock"/> class. 
        /// </summary>
        /// <remarks>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method 
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </remarks>
        ~WriteLock()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes the object and frees resources for the Garbage Collector.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object and frees resources for the Garbage Collector.
        /// </summary>
        /// <param name="disposing">If true, the object gets disposed.</param>
        private void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.locker.ExitWriteLock();
            }

            // Note disposing is done.
            this.isDisposed = true;
        }
    }
}
