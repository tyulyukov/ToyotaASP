using System;
using System.Collections.Generic;
using System.IO;

namespace Toyota.Helpers
{
    public class Root
    {
        public IReadOnlyList<DirectoryInformation> Directories => directories;
        public IReadOnlyList<FileInformation> Files => files;

        private List<DirectoryInformation> directories;
        private List<FileInformation> files;

        public Root()
        {
            ConfigureRoot();
        }

        public void ConfigureRoot()
        {
            var currentDirs = Directory.GetDirectories(Media.StoragePath);
            directories = new List<DirectoryInformation>();

            foreach (var directory in currentDirs)
                directories.Add(new DirectoryInformation(directory, Media.StoragePath + "\\" + directory));

            var currentFiles = Directory.GetFiles(Media.StoragePath);
            files = new List<FileInformation>();

            foreach (var file in currentFiles)
                files.Add(new FileInformation(file, Media.StoragePath + "\\" + file));
        }
    }

    [Serializable]
    public class DirectoryInformation
    {
        public readonly Guid Id;
        public readonly String Name;
        public readonly String Path;
        public IReadOnlyList<FileInformation> Files => files;
        public IReadOnlyList<DirectoryInformation> Directories => directories;

        private List<FileInformation> files;
        private List<DirectoryInformation> directories;

        public DirectoryInformation(String directoryName, String path)
        {
            Id = Guid.NewGuid();
            Name = directoryName;
            Path = path;

            Refresh();
        }

        public void Refresh()
        {
            var currentDirectories = Directory.GetDirectories(Path);
            directories = new List<DirectoryInformation>();

            foreach (var directory in currentDirectories)
                directories.Add(new DirectoryInformation(directory, Path + "\\" + directory));

            var currentFiles = Directory.GetFiles(Path);
            files = new List<FileInformation>();

            foreach (var file in currentFiles)
                files.Add(new FileInformation(file, Path + "\\" + file));
        }
    }

    [Serializable]
    public class FileInformation
    {
        public readonly Guid Id;
        public readonly String Name;
        public readonly String Path;

        public FileInformation(String name, String path)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
        }
    }
}
