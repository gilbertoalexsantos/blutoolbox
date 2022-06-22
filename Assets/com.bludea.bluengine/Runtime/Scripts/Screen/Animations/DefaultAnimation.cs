using System.Collections;
using Bludk;
using UnityEngine;

namespace BluEngine
{
    [CreateAssetMenu(menuName = "BluEngine/UI/Animations/Default", fileName = "DefaultUIAnimation")]
    public class DefaultAnimation : ScreenUIAnimation
    {
        public override IEnumerator ShowAnimation(ScreenController controller)
        {
            controller.BaseUI.gameObject.SetActive(true);
            return TxongaHelper.Empty();
        }

        public override IEnumerator HideAnimation(ScreenController controller)
        {
            controller.BaseUI.gameObject.SetActive(false);
            return TxongaHelper.Empty();
        }
    }
}