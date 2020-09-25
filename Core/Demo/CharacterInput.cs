using UnityEngine;

namespace GameJamStarterKit.Demo
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterInput : MonoBehaviour
    {
        public float MoveSpeed = 2f;
        public float TurnSpeed = 5f;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            var forward = Input.GetAxisRaw("Vertical") * MoveSpeed * transform.forward;
            var right = Input.GetAxisRaw("Horizontal") * MoveSpeed * transform.right;

            _controller.SimpleMove(forward + right);

            transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X") * TurnSpeed, 0f));
        }
    }
}