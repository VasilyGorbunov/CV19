using System;
using System.IO;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class FileViewModel: ViewModel
  {
    private readonly FileInfo _fileInfo;

    public string Name => _fileInfo.Name;

    public string Path => _fileInfo.FullName;

    public DateTime CreationTime => _fileInfo.CreationTime;

    public FileViewModel(string path) => _fileInfo = new FileInfo(path);
  }
}