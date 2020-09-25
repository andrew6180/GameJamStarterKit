using UnityEngine;

namespace GameJamStarterKit.Camera
{
    /// <summary>
    /// Provides a SpringArmComponent similar to Unreal Engine 4's SpringArmComponent.
    /// <para>Spring arm tries to maintain it's children a fixed distance away from the parent but retracts when colliding and springs back when no longer colliding.</para>
    /// </summary>
    [ExecuteInEditMode]
    public class SpringArm : MonoBehaviour
    {
        /// <summary>
        /// Lag the location of the arm?
        /// </summary>
        [Tooltip("Lag the location of the arm?")]
        public bool LagLocation = true;

        /// <summary>
        /// Lag the rotation of the arm relative to the parent?
        /// </summary>
        [Tooltip("Lag the rotation of the arm relative to the parent?")]
        public bool LagRotation = true;

        /// <summary>
        /// How quickly the location lag moves.
        /// </summary>
        [Header("Speed")]
        [Tooltip("How quickly the location lag moves.")]
        public float LocationLagSpeed = 2f;

        /// <summary>
        /// How quickly the rotation lag moves.
        /// </summary>
        [Tooltip("How quickly the rotation lag moves.")]
        public float RotationLagSpeed = 3f;

        /// <summary>
        /// Maximum distance the location can lag behind. 0 = disabled
        /// </summary>
        [Header("Distance")]
        [Tooltip("Maximum distance the location can lag behind. 0 = disabled")]
        public float MaxLocationDistance = 5f;

        /// <summary>
        /// Maximum angle the rotation can lag behind. 0 = disabled
        /// </summary>
        [Tooltip("Maximum angle the rotation can lag behind. 0 = disabled")]
        public float MaxRotationAngle = 30f;

        /// <summary>
        /// How far the probe should be from this objects position.
        /// </summary>
        [Tooltip("How far the probe should be from this objects position")]
        public float ProbeDistance = 5f;

        /// <summary>
        /// Layers the probe should check against
        /// </summary>
        [Header("Probe")]
        [Tooltip("Layers the probe should check against")]
        public LayerMask ProbeLayer = 0;

        /// <summary>
        /// The radius of the probe
        /// </summary>
        [Tooltip("The radius of the probe")]
        public float ProbeRadius = 0.2f;

        /// <summary>
        /// Offset for the end position of the probe.
        /// </summary>
        [Tooltip("Offset for the end position of the probe.")]
        public Vector3 EndOffset = Vector3.zero;

        /// <summary>
        /// Enables drawing of debug lines
        /// </summary>
        [Header("Debug")]
        [Tooltip("Enables drawing of debug lines")]
        public bool DrawDebugLines;

        /// <summary>
        /// Lag in editor? This will change the position.
        /// </summary>
        [Tooltip("Lag in editor? This will change the position")]
        public bool ArmLagInEditor;

        private float _currentProbeDistance;
        private Vector3 _currentProbeEnd = Vector3.zero;
        private Vector3 _targetPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;

        private Quaternion _startRotation = Quaternion.identity;
        private Quaternion _targetRotation = Quaternion.identity;
        private Quaternion _currentRotation = Quaternion.identity;


        private void Start()
        {
            var tr = transform;
            _targetRotation = tr.rotation;
            _targetPosition = tr.position;
            _startRotation = tr.localRotation;
            _currentProbeDistance = ProbeDistance;
        }

        protected void LateUpdate()
        {
            if (ShouldDoRotationLag())
            {
                _targetRotation = GetTargetRotation();
                _currentRotation = GetLaggedRotation();
                transform.rotation = _currentRotation;
            }

            _targetPosition = GetTargetPosition();
            _currentPosition = ShouldDoPositionLag() ? GetLaggedPosition() : _targetPosition;
            _currentProbeDistance = ShouldDoPositionLag() ? GetLaggedDistance() : ProbeDistance;
            _currentProbeEnd = GetProbePosition(_currentPosition, _currentProbeDistance);

            MoveChildren();
        }

        private float GetLaggedDistance()
        {
            return Mathf.Lerp(_currentProbeDistance, ProbeDistance, LocationLagSpeed * Time.deltaTime);
        }

        private void MoveChildren()
        {
            foreach (Transform child in transform)
            {
                child.position = _currentProbeEnd;
            }
        }

        private bool ShouldDoPositionLag()
        {
            return LagLocation && Application.isPlaying || LagLocation && ArmLagInEditor && !Application.isPlaying;
        }

        private bool ShouldDoRotationLag()
        {
            return LagRotation && Application.isPlaying || LagRotation && ArmLagInEditor && !Application.isPlaying;
        }

        private Vector3 GetLaggedPosition()
        {
            var pos = Vector3.Lerp(_currentPosition, _targetPosition, LocationLagSpeed * Time.deltaTime);
            if (MaxLocationDistance > 0 && Vector3.Distance(pos, _targetPosition) > MaxLocationDistance)
            {
                pos = _currentPosition.DirectionTo(_targetPosition) * MaxLocationDistance;
            }

            return pos;
        }

        private Quaternion GetLaggedRotation()
        {
            var rot = Quaternion.Lerp(_currentRotation, _targetRotation, RotationLagSpeed * Time.deltaTime);
            if (MaxRotationAngle > 0 && Quaternion.Angle(_currentRotation, _targetRotation) > MaxRotationAngle)
            {
                rot = Quaternion.RotateTowards(_targetRotation, _currentRotation, MaxRotationAngle);
            }

            return rot;
        }

        private Vector3 GetProbePosition(Vector3 currentArmStart, float currentProbeDistance)
        {
            var tr = transform;

            var backwards = -tr.forward;
            var endPos = currentArmStart + EndOffset + backwards * ProbeDistance;
            if (EndOffset != Vector3.zero)
            {
                backwards = endPos - currentArmStart;
                backwards.Normalize();
            }

            if (currentProbeDistance < ProbeDistance)
            {
                endPos = Vector3.MoveTowards(currentArmStart, endPos, currentProbeDistance);
            }

            var ray = new Ray(currentArmStart, backwards);

            if (Physics.SphereCast(ray, ProbeRadius, out var hit, currentProbeDistance, ProbeLayer))
            {
                endPos = currentArmStart + backwards * hit.distance;
                if (hit.distance < _currentProbeDistance)
                    _currentProbeDistance = hit.distance;
            }

            return endPos;
        }

        private Vector3 GetTargetPosition()
        {
            return transform.position;
        }

        private Quaternion GetTargetRotation()
        {
            return _startRotation * transform.parent.rotation;
        }

        private void OnDrawGizmos()
        {
            if (!DrawDebugLines)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_currentProbeEnd, ProbeRadius);
            Gizmos.DrawLine(_currentPosition, _currentProbeEnd);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(_currentPosition, _targetPosition);
        }
    }
}