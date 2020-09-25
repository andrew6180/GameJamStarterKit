using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameJamStarterKit.FXSystem
{
    /// <summary>
    /// Randomizes an animators parameters.
    /// </summary>
    public class RandomizeAnimatorParameters : MonoBehaviour
    {
        /// <summary>
        /// Data class defining a parameter to randomize
        /// </summary>
        [Serializable]
        public class RandomizeParameterData
        {
            /// <summary>
            /// Parameter key
            /// </summary>
            public string ParameterKey;
            /// <summary>
            /// Type of the parameter
            /// </summary>
            public AnimatorControllerParameterType ParameterType = AnimatorControllerParameterType.Float;
            
            /// <summary>
            /// If a number value, should the randomize range be overridden for this value?
            /// </summary>
            [Tooltip("should the randomize range be overridden for this value?")]
            public bool OverrideRange;
            
            /// <summary>
            /// if a number value and overriding the range, the minimum value 
            /// </summary>
            public float MinimumValue;
            
            /// <summary>
            /// if a number value and overriding the range, the maximum value 
            /// </summary>
            public float MaximumValue;
        }

        /// <summary>
        /// Defines when the parameter should be randomized
        /// </summary>
        public enum RandomizeParameterType
        {
            Awake,
            Start,
            Interval,
            IntervalRange,
            FixedUpdate
        }

        /// <summary>
        /// Find the attached animator component on awake rather than manually setting it in the inspector?
        /// </summary>
        [Tooltip("Find the attached animator component on awake rather than manually setting it.")]
        public bool UseAttachedAnimator;

        /// <summary>
        /// The animator to target
        /// </summary>
        [Tooltip("The animator to target")]
        public Animator Animator;
        
        /// <summary>
        /// When should the value be randomized
        /// </summary>
        public RandomizeParameterType RandomizeType = RandomizeParameterType.Interval;
        
        /// <summary>
        /// If RandomizeType is Interval, How often to randomize in seconds.
        /// </summary>
        [Tooltip("How often to randomize in seconds")]
        public float Interval = 5f;
        
        /// <summary>
        /// If RandomizeType is IntervalRange, minimum time to randomize in seconds.
        /// </summary>
        [Tooltip("Minimum time to randomize in seconds")]
        public float MinimumInterval = 1f;
        
        /// <summary>
        /// If RandomizeType is IntervalRange, maximum time to randomize in seconds.
        /// </summary>
        [Tooltip("Maximum time to randomize in seconds")]
        public float MaximumInterval = 5f;
        
        /// <summary>
        /// Parameters being randomized.
        /// </summary>
        [Tooltip("Parameters being randomized")]
        public List<RandomizeParameterData> ParameterData;
        
        /// <summary>
        /// For number values, the minimum value to randomize by
        /// </summary>
        [Tooltip("For number values, the minimum value to randomize by")]
        public float MinimumValue;
        
        /// <summary>
        /// For number values, the maximum value to randomize by
        /// </summary>
        [Tooltip("For number values, the maximum value to randomize by")]
        public float MaximumValue = 1f;

        private TimeSince _timeSinceInterval;

        private void Awake()
        {
            if (UseAttachedAnimator)
            {
                Animator = GetComponent<Animator>();
            }

            if (RandomizeType == RandomizeParameterType.Awake)
            {
                Randomize();
            }
        }

        private void Start()
        {
            if (RandomizeType == RandomizeParameterType.Start)
            {
                Randomize();
            }
            else if (RandomizeType == RandomizeParameterType.IntervalRange)
            {
                RandomizeInterval();
            }
        }

        private void FixedUpdate()
        {
            if (RandomizeType == RandomizeParameterType.FixedUpdate)
            {
                Randomize();
            }
        }

        private void Update()
        {
            if (RandomizeType != RandomizeParameterType.Interval &&
                RandomizeType != RandomizeParameterType.IntervalRange)
                return;

            if (_timeSinceInterval > Interval)
            {
                _timeSinceInterval = 0f;
                Randomize();
                if (RandomizeType == RandomizeParameterType.IntervalRange)
                {
                    RandomizeInterval();
                }
            }
        }

        private void RandomizeInterval()
        {
            Interval = Random.Range(MinimumInterval, MaximumInterval);
        }

        /// <summary>
        /// Randomizes the parameters being targeted
        /// </summary>
        public void Randomize()
        {
            foreach (var data in ParameterData)
            {
                if (Animator.HasParameterWithType(data.ParameterKey, data.ParameterType))
                {
                    float value;
                    if (data.OverrideRange)
                    {
                        value = Random.Range(data.MinimumValue, data.MaximumValue);
                    }
                    else
                    {
                        value = Random.Range(MinimumValue, MaximumValue);
                    }

                    switch (data.ParameterType)
                    {
                        case AnimatorControllerParameterType.Float:
                            Animator.SetFloat(data.ParameterKey, value);
                            break;
                        case AnimatorControllerParameterType.Int:
                            Animator.SetInteger(data.ParameterKey, Mathf.RoundToInt(value));
                            break;
                        case AnimatorControllerParameterType.Bool:
                            Animator.SetBool(data.ParameterKey, KitRandom.CoinToss());
                            break;
                        case AnimatorControllerParameterType.Trigger:
                            var set = KitRandom.CoinToss();
                            if (set)
                            {
                                Animator.SetTrigger(data.ParameterKey);
                            }
                            else
                            {
                                Animator.ResetTrigger(data.ParameterKey);
                            }

                            break;
                    }
                }
            }
        }
    }
}