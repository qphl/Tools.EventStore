// <copyright file="FileWorkingDaySource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.FileSource
{
    using System;
    using System.IO;
    using StringSource;

    /// <inheritdoc cref="IWorkingDaySource"/>
    /// <inheritdoc cref="IDisposable"/>
    /// <summary>
    /// An implementation of IWorkingDaySource which uses the returned content of a File to determine whether a given DateTime is on a Working Day or a Non-Working Day.
    /// </summary>
    public sealed class FileWorkingDaySource<T> : StringWorkingDaySource<T>, IDisposable
    {
        private readonly FileSystemWatcher _fileWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWorkingDaySource{T}"/> class with a file watcher that watched the provided filepath and updates a state of type T based on its content, which is used to check whether a given DateTime is on a Working Day or a Non-Working Day.
        /// </summary>
        /// <param name="filePath">The File to watch and get the state from.</param>
        /// <param name="parseFileContentAction">The action to get the state from the file's content.</param>
        /// <param name="checkAction">The action to get whether a DateTime is on a Working Day based on the current state from the file.</param>
        public FileWorkingDaySource(string filePath, Func<string, T> parseFileContentAction, Func<DateTime, T, bool> checkAction)
            : base(parseFileContentAction, checkAction)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "Cannot initialize a File Working Day Source without a File Path.");
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The File Path provided to the constructor of a File Working Day Source was invalid; the file '{filePath}' doesn't exist.", nameof(filePath));
            }

            _fileWatcher = new FileSystemWatcher(filePath);
            _fileWatcher.Changed += OnChange;

            State = ParseAction(File.ReadAllText(filePath));
        }

        /// <inheritdoc />
        public void Dispose() => _fileWatcher?.Dispose();

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            lock (State)
            {
                State = ParseAction(File.ReadAllText(e.FullPath));
            }
        }
    }
}
