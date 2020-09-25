using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    [Serializable]
    internal class SetupControl
    {
        internal PackageInfos PackageInfo = new PackageInfos();
        internal const string SETUP_RUN_KEY = "GameJamToolKit.SetupRun";

        internal void SetupFolderStructure(FolderFlags folders, string parent = "")
        {
            var values = Enum.GetValues(typeof(FolderFlags)).Cast<FolderFlags>()
                .Where(flag => flag != FolderFlags.None && flag != FolderFlags.Default);

            foreach (var value in values)
            {
                if (!folders.HasFlag(value))
                    continue;

                Directory.CreateDirectory(Path.Combine(Application.dataPath, parent, value.ToString()));
            }

            AssetDatabase.Refresh();
        }

        internal void InstallPackages(IEnumerable<string> packages)
        {
            foreach (var package in packages)
            {
                var request = Client.Add(package);
            }
        }

        internal void UninstallPackages(IEnumerable<string> packages)
        {
            foreach (var package in packages)
            {
                var request = Client.Remove(package);
            }
        }

        internal async Task UpdatePackageInfo(PackageInfos.KitPackageInfo packageInfo)
        {
            packageInfo.HasDescriptionBeenUpdated = true;
            var request = Client.Search(packageInfo.PackageURL);
            var tries = 0;
            while (!request.IsCompleted && ++tries < 5000)
            {
                await Task.Delay(1);
            }

            if (request.Status == StatusCode.Success)
            {
                if (request.Result[0] != null)
                {
                    packageInfo.Description = request.Result[0].description;
                }
            }
        }
    }
}