using System.Diagnostics;
using System.IO;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class DirectoryViewModel : ViewModelBase
    {
        private readonly DirectoryInfo _directoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _directoryInfo
                                .EnumerateDirectories()
                                .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                    return Enumerable.Empty<DirectoryViewModel>();
                }
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _directoryInfo
                        .EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                    return Enumerable.Empty<FileViewModel>();
                }
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories
                                .Cast<object>()
                                .Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                    return Enumerable.Empty<FileViewModel>();
                }
            }
        }

        public string Name => _directoryInfo.Name;
        public string Path => _directoryInfo.FullName;
        public DateTime CreationTime => _directoryInfo.CreationTime;


        public DirectoryViewModel(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
        }
    }
}
