using UnityEngine;

namespace GameJamStarterKit.Components
{
    /// <summary>
    /// Moves a GameObject along an axis for a given distance at a given speed.
    /// </summary>
    public class MoveAlongDistance : MonoBehaviour
    {
        /// <summary>
        /// Distance from the current position to travel
        /// </summary>
        [Tooltip("Distance from the current position to travel")]
        public float Distance = 1f;
        /// <summary>
        /// Speed to travel at
        /// </summary>
        [Tooltip("Speed to travel at")]
        public float Speed = 2f;
        
        /// <summary>
        /// Axis to move along
        /// </summary>
        [Tooltip("Axis to move along")]
        public KitAxis KitAxis;

        /// <summary>
        /// Move only right along the axis?
        /// </summary>
        [Tooltip("Move only right along the axis?")]
        public bool OnlyRight;
        
        /// <summary>
        /// Move only left along the axis?
        /// </summary>
        [Tooltip("Move only left along the axis?")]
        public bool OnlyLeft;

        private float _start;
        private float _end;
        private float _target;

        private void Awake()
        {
            GetStartAndEnd(out _start, out _end);

            _target = _start;
        }

        private void Update()
        {
            var position = transform.position;
            float pos;
            switch (KitAxis)
            {
                default:
                case KitAxis.X:
                    pos = position.x;
                    break;
                case KitAxis.Y:
                    pos = position.y;
                    break;
                case KitAxis.Z:
                    pos = position.z;
                    break;
            }

            var distance = Mathf.Abs(pos - _target);
            if (Mathf.Approximately(distance, 0))
            {
                _target = Mathf.Approximately(_target, _start) ? _end : _start;
            }

            switch (KitAxis)
            {
                default:
                case KitAxis.X:
                    position.x = _target;
                    break;
                case KitAxis.Y:
                    position.y = _target;
                    break;
                case KitAxis.Z:
                    position.z = _target;
                    break;
            }

            transform.position = Vector3.MoveTowards(transform.position, position, Speed * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;

            var position = transform.position;
            GetStartAndEnd(out var axisStart, out var axisEnd);

            Vector3 start;
            Vector3 end;

            switch (KitAxis)
            {
                default:
                case KitAxis.X:
                    start = position.WithX(axisStart);
                    end = position.WithX(axisEnd);
                    break;
                case KitAxis.Y:
                    start = position.WithY(axisStart);
                    end = position.WithY(axisEnd);
                    break;
                case KitAxis.Z:
                    start = position.WithZ(axisStart);
                    end = position.WithZ(axisEnd);
                    break;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);
            Gizmos.DrawCube(start, Vector3.one * 0.1f);
            Gizmos.DrawCube(end, Vector3.one * 0.1f);
        }

        private void GetStartAndEnd(out float start, out float end)
        {
            var position = transform.position;
            switch (KitAxis)
            {
                default:
                case KitAxis.X:
                    if (OnlyRight)
                    {
                        start = position.x + Distance * 2f;
                        end = position.x;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.x - Distance * 2f;
                        end = position.x;
                    }
                    else
                    {
                        start = position.x + Distance * 2f;
                        end = position.x - Distance * 2f;
                    }

                    break;
                case KitAxis.Y:
                    if (OnlyRight)
                    {
                        start = position.y + Distance * 2f;
                        end = position.y;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.y - Distance * 2f;
                        end = position.y;
                    }
                    else
                    {
                        start = position.y + Distance * 2f;
                        end = position.y - Distance * 2f;
                    }

                    break;
                case KitAxis.Z:
                    if (OnlyRight)
                    {
                        start = position.z + Distance * 2f;
                        end = position.z;
                    }
                    else if (OnlyLeft)
                    {
                        start = position.z - Distance * 2f;
                        end = position.z;
                    }
                    else
                    {
                        start = position.z + Distance * 2f;
                        end = position.z - Distance * 2f;
                    }

                    break;
            }
        }
    }
}