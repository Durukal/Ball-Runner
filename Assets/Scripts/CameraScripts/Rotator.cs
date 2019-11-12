using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class Rotator : MonoBehaviour {

        // Update is called once per frame
        [UsedImplicitly]
        private void Update ()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }
}
