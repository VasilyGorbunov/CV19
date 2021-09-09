using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class DirectoryViewModel: ViewModel
  {
    private readonly DirectoryInfo _directoryInfo;

    public IEnumerable<DirectoryViewModel> SubDirectories => _directoryInfo
      .EnumerateDirectories()
      .Select(dir_info => new DirectoryViewModel(dir_info.FullName));

    public IEnumerable<FileViewModel> Files => _directoryInfo
      .EnumerateFiles()
      .Select(file => new FileViewModel(file.FullName));

    public IEnumerable<object> DirectoryItems => SubDirectories
      .Cast<object>()
      .Concat(Files);

    public string Name => _directoryInfo.Name;

    public string Path => _directoryInfo.FullName;

    public DateTime CreationTime => _directoryInfo.CreationTime;

    public DirectoryViewModel(string path) => _directoryInfo = new DirectoryInfo(path);
  }
}