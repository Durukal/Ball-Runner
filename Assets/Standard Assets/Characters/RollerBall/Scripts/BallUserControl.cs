using JetBrains.Annotations;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Ball;

namespace Assets.Standard_Assets.Characters.RollerBall.Scripts
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball _ball; // Reference to the ball controller.
        private Vector3 _move; // the world-relative desired move direction, calculated from the camForward and user input.
        private Transform _cam; // A reference to the main camera in the scenes transform
        private Vector3 _camForward; // The current forward direction of the camera
        private bool _jump; // whether the jump button is currently pressed

        [UsedImplicitly]
        private void Awake()
        {
            // Set up the reference.
            _ball = GetComponent<Ball>();


            // get the transform of the main camera
            if (Camera.main != null)
            {
                _cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
            }
        }

        [UsedImplicitly]
        private void Update()
        {
            // Get the axis and jump input.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            _jump = CrossPlatformInputManager.GetButton("Jump");

            // Calculate move direction
            if (_cam != null)
            {
                // Calculate camera relative direction to move:
                _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
                _move = (_camForward + h*_cam.right).normalized;
            }
            else
            {
                // We use world-relative directions in the case of no main camera
                _move = (Vector3.forward + h*Vector3.right).normalized;
            }
            
        }

        [UsedImplicitly]
        private void FixedUpdate()
        {
            // Call the Move function of the ball controller
            _ball.Move(_move, _jump);
            _jump = false;
           
        }

    }
}
