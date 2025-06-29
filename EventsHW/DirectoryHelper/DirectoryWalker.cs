using EventsHW.Models;

namespace EventsHW.DirectoryHelper
{
    public class DirectoryWalker
    {
        public event EventHandler<FileArgs> FileFound;

        public void Walk(string directory)
        {
            foreach (var file in Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories))
            {
                var args = new FileArgs(file);
                FileFound?.Invoke(this, args);

                if (_cancelRequested)
                    break;
            }
        }

        private bool _cancelRequested = false;

        public void Cancel() => _cancelRequested = true;
    }
}
