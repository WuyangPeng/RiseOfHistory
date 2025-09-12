using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Scripts.Main.Runtime.UI.UICommon
{
    public class CommonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float FadeTime = 0.3f;
        private const float OnHoverAlpha = 0.7f;
        private const float OnClickAlpha = 0.6f;

        [SerializeField]
        private UnityEvent m_OnHover;

        [SerializeField]
        private UnityEvent m_OnClick;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            canvasGroup.alpha = 1f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            StopAllCoroutines();
            StartCoroutine(canvasGroup.FadeToAlpha(OnHoverAlpha, FadeTime));
            m_OnHover.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            StopAllCoroutines();
            StartCoroutine(canvasGroup.FadeToAlpha(1f, FadeTime));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            canvasGroup.alpha = OnClickAlpha;
            m_OnClick.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            canvasGroup.alpha = OnHoverAlpha;
        }
    }
}
