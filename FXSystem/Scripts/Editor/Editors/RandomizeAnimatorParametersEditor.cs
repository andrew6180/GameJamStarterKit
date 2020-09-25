using GameJamStarterKit.Editor;
using UnityEditor;

namespace GameJamStarterKit.FXSystem.Editor
{
    [CustomEditor(typeof(RandomizeAnimatorParameters), true)]
    public class RandomizeAnimatorParametersEditor : UnityEditor.Editor
    {
        private SerializedProperty _useAttachedAnimator;
        private SerializedProperty _animator;
        private SerializedProperty _randomizeType;
        private SerializedProperty _interval;
        private SerializedProperty _minimumInterval;
        private SerializedProperty _maximumInterval;
        private SerializedProperty _parameterData;
        private SerializedProperty _minimumValue;
        private SerializedProperty _maximumValue;

        private void OnEnable()
        {
            _useAttachedAnimator = serializedObject.FindProperty("UseAttachedAnimator");
            _animator = serializedObject.FindProperty("Animator");
            _randomizeType = serializedObject.FindProperty("RandomizeType");
            _interval = serializedObject.FindProperty("Interval");
            _minimumInterval = serializedObject.FindProperty("MinimumInterval");
            _maximumInterval = serializedObject.FindProperty("MaximumInterval");
            _parameterData = serializedObject.FindProperty("ParameterData");
            _minimumValue = serializedObject.FindProperty("MinimumValue");
            _maximumValue = serializedObject.FindProperty("MaximumValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _useAttachedAnimator.DrawLayout();

            if (!_useAttachedAnimator.boolValue)
            {
                _animator.DrawLayout();
            }

            _randomizeType.DrawLayout();

            var typeValue = _randomizeType.EnumValue<RandomizeAnimatorParameters.RandomizeParameterType>();

            switch (typeValue)
            {
                case RandomizeAnimatorParameters.RandomizeParameterType.Interval:
                    _interval.DrawLayout();
                    break;
                case RandomizeAnimatorParameters.RandomizeParameterType.IntervalRange:
                    _minimumInterval.DrawLayout();
                    _maximumInterval.DrawLayout();
                    break;
            }

            _parameterData.DrawLayout(true);

            _minimumValue.DrawLayout();
            _maximumValue.DrawLayout();

            serializedObject.ApplyModifiedProperties();
        }
    }
}