using System.Collections;
using UnityEngine;

namespace Bludk
{
    public abstract class ScreenUIAnimation : ScriptableObject
    {
        public abstract IEnumerator ShowAnimation(ScreenController controller);
        public abstract IEnumerator HideAnimation(ScreenController controller);
    }
}