using System;
using UnityEngine;

namespace GameJamStarterKit.Editor
{
    internal class PackageInfos
    {
        [Serializable]
        internal class KitPackageInfo
        {
            [SerializeField]
            internal string PackageURL;

            [SerializeField]
            internal string UninstallURL;

            [SerializeField]
            internal string PackageName;

            [SerializeField]
            internal bool Install;

            [SerializeField]
            internal string Description;

            [SerializeField]
            internal bool ShowDescription;

            internal bool HasDescriptionBeenUpdated;

            public KitPackageInfo()
            {
                if (string.IsNullOrEmpty(UninstallURL))
                    UninstallURL = PackageURL;

                if (!string.IsNullOrEmpty(Description))
                    HasDescriptionBeenUpdated = true;
            }
        }

        private KitPackageInfo[] _packages2D;

        /// <summary>
        /// 2D Related Packages
        /// </summary>
        internal KitPackageInfo[] Packages2D
        {
            get
            {
                if (_packages2D == null)
                {
                    _packages2D = new[]
                    {
                        // 2d Sprite Editor
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.2d.sprite",
                            Install = true,
                            PackageName = "2D Sprite Editor"
                        },
                        // 2d Tile Maps
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.2d.tilemap",
                            Install = false,
                            PackageName = "2D Tilemap Editor"
                        },
                        // 2d Pixel Perfect Camera
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.2d.pixel-perfect",
                            Install = false,
                            PackageName = "(Preview) 2D Pixel Perfect"
                        },
                        // 2d Sprite Shapes
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.2d.spriteshape",
                            Install = false,
                            PackageName = "(Preview) 2D Sprite Shape"
                        }
                    };
                }

                return _packages2D;
            }
        }

        private KitPackageInfo[] _utilityPackages;

        /// <summary>
        /// Utility Packages
        /// </summary>
        internal KitPackageInfo[] UtilityPackages
        {
            get
            {
                if (_utilityPackages == null)
                {
                    _utilityPackages = new[]
                    {
                        // Input System
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.inputsystem",
                            Install = false,
                            PackageName = "(Preview) New Input System"
                        }
                    };
                }

                return _utilityPackages;
            }
        }

        private KitPackageInfo[] _packages3D;

        /// <summary>
        /// 3D Packages
        /// </summary>
        internal KitPackageInfo[] Packages3D
        {
            get
            {
                if (_packages3D == null)
                {
                    _packages3D = new[]
                    {
                        // ProBuilder
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.probuilder",
                            Install = false,
                            PackageName = "ProBuilder"
                        },
                        // ProGrids
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.progrids",
                            Install = false,
                            PackageName = "(Preview) ProGrids"
                        }
                    };
                }

                return _packages3D;
            }
        }


        private KitPackageInfo[] _renderingPackages;

        /// <summary>
        /// Rendering Related Packages
        /// </summary>
        internal KitPackageInfo[] RenderingPackages
        {
            get
            {
                if (_renderingPackages == null)
                {
                    _renderingPackages = new[]
                    {
                        // Universal / Lightweight render pipeline.
#if UNITY_2019_3_OR_NEWER
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.render-pipelines.universal",
                            Install = false,
                            PackageName = "Universal RP"
                        },
#else
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.render-pipelines.lightweight",
                            Install = false,
                            PackageName = "Lightweight RP"
                        },
#endif
                        // HD Render pipeline
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.render-pipelines.high-definition",
                            Install = false,
                            PackageName = "(Preview) High Definition RP"
                        },
                        // Post processing stack v2
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.postprocessing",
                            Install = false,
                            PackageName = "Post-Processing Stack v2"
                        },
                        // Open VR / Steam VR
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.xr.openvr.standalone",
                            Install = false,
                            PackageName = "OpenVR/SteamVR"
                        }
                    };
                }

                return _renderingPackages;
            }
        }

        private KitPackageInfo[] _dotsPackages;

        /// <summary>
        /// DOTS related packages.
        /// </summary>
        internal KitPackageInfo[] DotsPackages
        {
            get
            {
                if (_dotsPackages == null)
                {
                    _dotsPackages = new[]
                    {
                        // ECS
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.entities",
                            Install = false,
                            PackageName = "(Preview) ECS"
                        },
                        // Burst
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.burst",
                            Install = false,
                            PackageName = "Burst Compiler"
                        },
                        // Dots Collections
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.collections",
                            Install = false,
                            PackageName = "(Preview) Collections"
                        },
                        // Jobs
                        new KitPackageInfo
                        {
                            PackageURL = "com.unity.jobs",
                            Install = false,
                            PackageName = "(Preview) Jobs Addon"
                        }
                    };
                }

                return _dotsPackages;
            }
        }

        private KitPackageInfo[] _gitPackages;

        /// <summary>
        /// Git UPM packages.
        /// </summary>
        internal KitPackageInfo[] GitPackages
        {
            get
            {
                if (_gitPackages == null)
                {
                    _gitPackages = new[]
                    {
                        new KitPackageInfo
                        {
                            PackageName = "NaughtyAttributes - dbrizov",
                            Description =
                                "It expands the range of attributes that Unity provides so that you can create powerful inspectors without the need of custom editors or property drawers. " +
                                "It also provides attributes that can be applied to non-serialized fields or functions. " +
                                "Similar to OdinInspectors attribute system, not useful if using OdinInspector already. https://github.com/dbrizov/NaughtyAttributes ",
                            PackageURL = "https://github.com/JimmyCushnie/NaughtyAttributes.git",
                            UninstallURL = "com.dbrizov.naughtyattributes",
                            Install = false
                        },
                        new KitPackageInfo
                        {
                            PackageName = "Outline Effect - Cakeslice",
                            Description = "Adds an outline image effect " +
                                          "Just attach a \"Outline Effect\" to the camera and a \"Outline Component\" to renderers " +
                                          "https://github.com/cakeslice/Outline-Effect/",
                            PackageURL = "https://github.com/cakeslice/Outline-Effect.git",
                            UninstallURL = "com.cakeslice.outline-effect",
                            Install = false
                        },
                        new KitPackageInfo
                        {
                            PackageName = "Hierarchy Folders - xsduan",
                            Description =
                                "De-clutter your scene hierarchy by creating folders. No more empty GameObjects for organization! " +
                                "Folders are also compiled out of builds. " +
                                "https://github.com/xsduan/unity-hierarchy-folders",
                            PackageURL = "https://github.com/xsduan/unity-hierarchy-folders.git",
                            UninstallURL = "com.xsduan.hierarchy-folders",
                            Install = false
                        },
                        new KitPackageInfo
                        {
                            PackageName = "Scene Reference Class - JohannesMP",
                            Description = "A Reliable, user-friendly way to reference SceneAssets by script. " +
                                          "https://github.com/starikcetin/unity-scene-reference",
                            PackageURL = "https://github.com/starikcetin/unity-scene-reference.git#upm",
                            UninstallURL = "com.johannesmp.unityscenereference",
                            Install = false
                        },
                        new KitPackageInfo
                        {
                            PackageName = "Soft Mask For UGUI - mob-sakai",
                            Description = "SoftMask is a smooth masking component for uGUI elements in Unity. " +
                                          "By using SoftMask instead of default Mask, rounded edges of UI elements can be expressed beautifully. " +
                                          "https://github.com/mob-sakai/SoftMaskForUGUI",
                            PackageURL = "https://github.com/mob-sakai/SoftMaskForUGUI.git#upm",
                            UninstallURL = "com.coffee.softmask-for-ugui",
                            Install = false
                        }
                    };
                }

                return _gitPackages;
            }
        }
    }
}