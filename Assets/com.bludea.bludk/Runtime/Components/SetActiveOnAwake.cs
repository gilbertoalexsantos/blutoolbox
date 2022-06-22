using UnityEngine;

namespace Bludk
{
    public class SetActiveOnAwake : MonoBehaviour
    {
        [SerializeField]
        private bool _setActive;

        private void Awake()
        {
            gameObject.SetActive(_setActive);
        }
    }
}