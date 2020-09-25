using System;
using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// Represents a key to map to a value.
    /// </summary>
    [Serializable]
    public class TextInterpolationData
    {
        /// <summary>
        /// The string to search for within curly brackets in the text to replace
        /// </summary>
        [Tooltip("the text to search for within curly brackets to replace with the data source.")]
        public string Key;
        
        /// <summary>
        /// The source GameObject to look for. Only accepts scene objects
        /// </summary>
        [Tooltip("The GameObject source of the component to read data from. Only accepts scene objects")]
        public GameObject Source;
        
        /// <summary>
        /// The component to watch a member of
        /// </summary>
        [Tooltip("The Component on Source to watch the member of")]
        public Component SourceComponent;
        
        /// <summary>
        /// The type of member to look for
        /// </summary>
        [Tooltip("The type of member to look for")]
        public DataMemberType MemberType = DataMemberType.Field;
        
        /// <summary>
        /// The member name
        /// </summary>
        [Tooltip("The member to look for")]
        public string Name;
        
        [SerializeField]
        [HideInInspector]
        private int SourceComponentIndex;

        [SerializeField]
        [HideInInspector]
        private int NameIndex;

        [SerializeField]
        [HideInInspector]
        [Tooltip("Shows a dropdown of all member names instead of a text field.")]
        // ReSharper disable once NotAccessedField.Local
        private bool UseDropdown = true;
    }
}