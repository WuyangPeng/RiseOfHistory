using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.FileSystem;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using Path = System.IO.Path;

namespace Game.Scripts.Main.Runtime.FileSystem
{


    public class FileSystemComponent : GameFrameworkComponent
    {
        private readonly Dictionary<string, IFileSystem> fileSystem = new();

        public IFileSystem CreateFileSystem(string directory, string pathName)
        {
            Directory.CreateDirectory(directory);

            var rootPath = Path.Combine(directory, pathName);

            if (fileSystem.TryGetValue(rootPath, out var result))
            {
                return result;
            }

            var file = File.Exists(rootPath) ?
                GameEntry.FileSystem.LoadFileSystem(rootPath, FileSystemAccess.ReadWrite) :
                GameEntry.FileSystem.CreateFileSystem(rootPath, FileSystemAccess.ReadWrite, 1024, 1024);

            if (file != null)
            {
                fileSystem.Add(rootPath, file);
            }

            return file;
        }
    }
}