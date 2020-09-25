using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace GameJamStarterKit.UI
{
    /// <summary>
    /// Component to support interpolating strings on Text Mesh Pro text
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class InterpolateText : MonoBehaviour
    {
        /// <summary>
        /// A list of <see cref="TextInterpolationData"/> 
        /// </summary>
        public List<TextInterpolationData> DataMap = new List<TextInterpolationData>();
        
        private TMP_Text _text;
        private string _rawText;

        private MatchCollection _matches;
        private Regex _regex;
        
        [SerializeField]
        [HideInInspector]
        private bool ShowDataMap;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            SetText(_text.text);
            _regex = new Regex(@"\{(?<key>[^:]+?)(?(?=\:)\:(?<formatter>.+?)|(?>))\}");
        }

        private void Update()
        {
            var text = _regex.Replace(_rawText, match =>
            {
                var format = "";

                if (match.Groups.Count < 1)
                    return "";

                var key = match.Groups["key"].Value;
                
                if (match.Groups.Count >= 2)
                {
                    format = match.Groups["formatter"].Value;
                }

                var data = DataMap.FirstOrDefault(item => item.Key == key);
                if (data == null)
                    return "";

                string replaceValue;
                switch (data.MemberType)
                {
                    case DataMemberType.Method:
                        replaceValue = GetMethodValue(data);
                        break;
                    case DataMemberType.Property:
                        replaceValue = GetPropertyValue(data);
                        break;
                    case DataMemberType.Field:
                        replaceValue = GetFieldValue(data);
                        break;
                    default:
                        replaceValue = "";
                        break;
                }

                if (decimal.TryParse(replaceValue, out var value))
                {
                    return string.Format($"{{0:{format}}}", value);
                }
                
                return replaceValue;
            });
            
            _text.SetText(text);
        }

        private string GetMethodValue(TextInterpolationData data)
        {
            if (GetMemberInfo(data) is MethodInfo methodInfo)
            {
                return methodInfo.Invoke(data.SourceComponent, null).ToString();
            }
            
            return "";
        }

        private string GetPropertyValue(TextInterpolationData data)
        {
            if (GetMemberInfo(data) is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(data.SourceComponent).ToString();
            }

            return "";
        }

        private string GetFieldValue(TextInterpolationData data)
        {
            if (GetMemberInfo(data) is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(data.SourceComponent).ToString();
            }

            return "";
        }

        private MemberInfo GetMemberInfo(TextInterpolationData data)
        {
            var type = data.SourceComponent.GetType();
            return type.GetMember(data.Name).FirstOrDefault();
        }

        /// <summary>
        /// Sets the base text for the attached TextMeshProUGUI component
        /// </summary>
        /// <param name="text">the text to use</param>
        public void SetText(string text)
        {
            _rawText = text;
        }
    }
}