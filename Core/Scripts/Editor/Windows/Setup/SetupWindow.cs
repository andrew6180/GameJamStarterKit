using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_2_OR_NEWER
using Unity.CodeEditor;
#endif

namespace GameJamStarterKit.Editor
{
    internal class SetupWindow : EditorWindow
    {
        internal const string ROOT_MENU = "Tools/Game Jam Starter Kit/";

        private readonly SetupControl _control = new SetupControl();

        [SerializeField]
        internal string RootFolder = "MyProject";

        [SerializeField]
        internal FolderFlags Folders = FolderFlags.Default;

        [SerializeField]
        private bool ShowGitInstaller;

        [SerializeField]
        private bool ShowPackageInstaller;

        [SerializeField]
        private bool ShowFolderSetup = true;

        [SerializeField]
        private bool ShowProjectSettings;

        [SerializeField]
        private Vector2 ScrollPosition;

        [SerializeField]
        private bool Show2DPackages;

        [SerializeField]
        private bool Show3DPackages;

        [SerializeField]
        private bool ShowUtilityPackages;

        [SerializeField]
        private bool ShowRenderingPackages;

        [SerializeField]
        private bool ShowDotsPackages;

        private const int RESPONSIVE_WIDTH = 450;

        [MenuItem(ROOT_MENU + "Setup", false, -1)]
        internal static void Init()
        {
            var window = GetWindow<SetupWindow>();
            window.titleContent = new GUIContent("Game Jam Starter Kit Setup");
            window.minSize = new Vector2(300, 500);
        }

        private void OnGUI()
        {
            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
            DrawInfoPanel();
            DrawCreateFolders();
            DrawPackageInstaller();
            DrawGitPackageInstaller();
            DrawProjectSettings();

            EditorGUILayout.Separator();
            DrawOpenProjectButton();
            EditorGUILayout.EndScrollView();
        }

        private void DrawInfoPanel()
        {
            const string MSG =
                "Game Jam Starter Kit comes with many default assets. If you've downloaded this from the asset store, you can ignore this message!\n" +
                "For Package Manager users, premade assets can be found under Packages below your assets folder.\n" +
                "If you wish to use any of the pre-made scenes, you will need to copy the scene into your Assets folder.";
            EditorGUILayout.HelpBox(MSG, MessageType.Warning);
        }

        private void DrawCreateFolders()
        {
            KitGUILayout.BeginCleanFoldout("Create Folder Structure", ref ShowFolderSetup);
            if (ShowFolderSetup)
            {
                EditorGUILayout.Separator();

                const string MSG =
                    "This sets up the initial project structure by adding the selected folders to the \"Root Folder\" in the Assets Directory." +
                    "Leave blank for no root folder (new folders will be created under Assets/).";
                EditorGUILayout.HelpBox(MSG, MessageType.Info);

                KitGUILayout.BeginResponsive(RESPONSIVE_WIDTH);
                RootFolder = EditorGUILayout.TextField("Root Folder: ", RootFolder);
                Folders = (FolderFlags) EditorGUILayout.EnumFlagsField("Folders: ", Folders);
                KitGUILayout.EndResponsive(RESPONSIVE_WIDTH);

                if (GUILayout.Button("Create Folders", GUILayout.Height(30)))
                {
                    _control.SetupFolderStructure(Folders, RootFolder);
                }
            }

            KitGUILayout.EndCleanFoldout();
        }


        private void DrawPackageInstaller()
        {
            KitGUILayout.BeginCleanFoldout("Quick Install Packages", ref ShowPackageInstaller);

            if (ShowPackageInstaller)
            {
                EditorGUILayout.Separator();

                const string MSG =
                    "This will install the selected packages from the package manager." +
                    "This is slow and could take a few minutes to install all the packages depending on how many are selected.";
                EditorGUILayout.HelpBox(MSG, MessageType.Info);

                const string PREVIEW_NOTICE =
                    "Preview packages work just fine for quick (Game Jam) projects. Unity marks a package as preview if it is still subject to large changes in the future.";
                EditorGUILayout.HelpBox(PREVIEW_NOTICE, MessageType.Warning);

                DrawPackageSection("2D", ref Show2DPackages, _control.PackageInfo.Packages2D);
                DrawPackageSection("3D", ref Show3DPackages, _control.PackageInfo.Packages3D);
                DrawPackageSection("Utility", ref ShowUtilityPackages, _control.PackageInfo.UtilityPackages);
                DrawPackageSection("Rendering", ref ShowRenderingPackages, _control.PackageInfo.RenderingPackages);
                DrawPackageSection("DOTS", ref ShowDotsPackages, _control.PackageInfo.DotsPackages);
            }

            KitGUILayout.EndCleanFoldout();
        }

        private void DrawPackageSection(string sectionTitle, ref bool show, PackageInfos.KitPackageInfo[] packages)
        {
            KitGUILayout.BeginCleanFoldout(sectionTitle, ref show);
            if (show)
            {
                foreach (var package in packages)
                {
                    DrawPackageInfo(package);
                }
            }

            KitGUILayout.EndCleanFoldout();
        }

        private void DrawGitPackageInstaller()
        {
            KitGUILayout.BeginCleanFoldout("Quick Install Git Repositories", ref ShowGitInstaller);
            if (ShowGitInstaller)
            {
                EditorGUILayout.Separator();
                const string MSG = "This will install the selected Git repositories using the package manager." +
                                   "This is slow and could take a few minutes to install packages.";
                EditorGUILayout.HelpBox(MSG, MessageType.Info);

                foreach (var packageInfo in _control.PackageInfo.GitPackages)
                {
                    DrawPackageInfo(packageInfo);
                }

                if (GUILayout.Button("Install Selected Git Repositories", GUILayout.Height(30)))
                {
                    _control.InstallPackages(_control.PackageInfo.GitPackages.Where(p => p.Install)
                        .Select(p => p.PackageURL));
                }

                if (GUILayout.Button("Uninstall Selected Git Repositories", GUILayout.Height(30)))
                {
                    _control.UninstallPackages(_control.PackageInfo.GitPackages.Where(p => p.Install)
                        .Select(p => p.UninstallURL));
                }
            }

            KitGUILayout.EndCleanFoldout();
        }

        private void DrawPackageInfo(PackageInfos.KitPackageInfo kitPackageInfo)
        {
            EditorGUILayout.BeginVertical();
            if (!kitPackageInfo.HasDescriptionBeenUpdated)
            {
#pragma warning disable 4014
                _control.UpdatePackageInfo(kitPackageInfo);
#pragma warning restore 4014
            }

            KitGUILayout.BeginResponsiveCleanFoldout(kitPackageInfo.PackageName, RESPONSIVE_WIDTH,
                ref kitPackageInfo.ShowDescription, true, 12);
            GUILayout.FlexibleSpace();
            kitPackageInfo.Install = EditorGUILayout.Toggle(kitPackageInfo.Install);

            EditorGUILayout.EndVertical();

            if (kitPackageInfo.ShowDescription)
            {
                EditorGUILayout.HelpBox(kitPackageInfo.Description, MessageType.Info);
            }


            KitGUILayout.EndResponsiveCleanFoldout(RESPONSIVE_WIDTH);
        }

        private void DrawProjectSettings()
        {
            KitGUILayout.BeginCleanFoldout("Project Settings", ref ShowProjectSettings);
            if (ShowProjectSettings)
            {
                EditorGUILayout.Separator();
                //////////
                // Project Settings -> Editor
                //////////
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel);

                // Serialization Mode
                var serializationMode = EditorSettings.serializationMode;
                serializationMode =
                    (SerializationMode) EditorGUILayout.EnumPopup("Asset Serialization Mode", serializationMode);

                if (serializationMode != EditorSettings.serializationMode)
                {
                    EditorSettings.serializationMode = serializationMode;
                }

                //////////
                // Project Settings -> Player Settings
                //////////
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Player Settings", EditorStyles.boldLabel);

                // company name
                var company = PlayerSettings.companyName;
                company = EditorGUILayout.TextField("Company Name", company);

                if (company != PlayerSettings.companyName)
                {
                    PlayerSettings.companyName = company;
                }

                // product name
                var product = PlayerSettings.productName;
                product = EditorGUILayout.TextField("Product Name", product);

                if (product != PlayerSettings.productName)
                {
                    PlayerSettings.productName = product;
                }

                // api
                EditorGUILayout.LabelField("API Compatibility Level", EditorStyles.boldLabel);
                var standaloneApi = PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Standalone);

                standaloneApi =
                    (ApiCompatibilityLevel) EditorGUILayout.EnumPopup("Standalone", standaloneApi);

                if (standaloneApi != PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Standalone))
                {
                    PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, standaloneApi);
                }

                var webApi = PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.WebGL);
                webApi = (ApiCompatibilityLevel) EditorGUILayout.EnumPopup("WEBGL", webApi);

                if (webApi != PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.WebGL))
                {
                    PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.WebGL, webApi);
                }
            }

            KitGUILayout.EndCleanFoldout();
        }

        private void DrawOpenProjectButton()
        {
#if UNITY_2019_2_OR_NEWER
            if (GUILayout.Button("Open C# Project"))
            {
                CodeEditor.CurrentEditor.OpenProject();
            }
#endif
        }
    }
}