using UnityEngine;

namespace Platformer.View
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        /// <summary>
        /// Movement of the layer is scaled by this value.
        /// </summary>
        public Vector3 movementScale = Vector3.one;
        private bool disableVerticalParallax=false;
        Transform _camera;

        void Awake()
        {
            _camera = Camera.main.transform;
            Invoke("sc", 0.2f);
        }

        void LateUpdate()
        {
            if (disableVerticalParallax)
            {
                transform.position = Vector3.Scale(_camera.position, movementScale);
            }
        }
        private void FixedUpdate()
        {
            
            

        }
        void sc()
        {
            disableVerticalParallax = false;
        }

    }
}