using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour {

        public GameObject Player;

        private Vector3 _offset;

        // Use this for initialization
        [UsedImplicitly]
        private void Start ()
        {
            _offset = transform.position - Player.transform.position;
        }

        // Uplatedate is called once per frame
        [UsedImplicitly]
        private void LateUpdate ()
        {
            transform.position = new Vector3(Player.transform.position.x + _offset.x, 
                transform.position.y, 
                Player.transform.position.z + _offset.z);
        }
    }
}
