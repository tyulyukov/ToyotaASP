using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Toyota.Helpers
{
    public class Root
    {
        public IReadOnlyList<DirectoryInfo> Directories => directories;
        private List<DirectoryInfo> directories;

        public IReadOnlyList<FileInformation> Files => files;
        private List<FileInformation> files;

        public Root()
        {
            ConfigureRoot();
        }

        public void ConfigureRoot()
        {
            var currentDirs = Directory.GetDirectories(Media.StoragePath);
            directories = new List<DirectoryInfo>();

            foreach (var directory in currentDirs)
            {
                directories.Add(new DirectoryInfo(directory, Media.StoragePath + "\\" + directory, Directory.GetFiles(Media.StoragePath + "\\" + directory)));
            }

            var currentFiles = Directory.GetFiles(Media.StoragePath);
            files = new List<FileInformation>();

            foreach (var file in currentFiles)
            {
                files.
            }
        }
    }

    public class DirectoryInfo
    {
        public readonly Guid Id;
        public readonly String Name;
        public readonly String Path;
        public readonly List<FileInformation> Files;

        public DirectoryInfo(String directoryName, String path, String[] files)
        {
            Id = Guid.NewGuid();
            Name = directoryName;
            Path = path;

            Files = new List<FileInformation>();

            foreach (var file in files)
                Files.Add(new FileInformation(file, path + "\\" + file));
        }
    }

    public class FileInformation
    {
        public readonly Guid Id;
        public readonly String Name;
        public readonly String Path;

        public FileInformation(String name, String path)
        {
            Name = name;
            Path = path;
        }
    }
}
