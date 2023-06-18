using UnityEngine;

namespace BluToolbox
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