using System.Collections;
using Game.Scripts.Main.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.HPBar
{
    public class HPBarItem : MonoBehaviour
    {
        private const float AnimationSeconds = 0.3f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutSeconds = 0.3f;

        [SerializeField]
        private Slider m_HPBar = null;

        private Canvas m_ParentCanvas = null;
        private RectTransform m_CachedTransform = null;
        private CanvasGroup m_CachedCanvasGroup = null;
        private int m_OwnerId = 0;

        public Entity.EntityLogic.Entity Owner { get; private set; } = null;

        public void Init(Entity.EntityLogic.Entity owner, Canvas parentCanvas, float fromHpRatio, float toHpRatio)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            m_ParentCanvas = parentCanvas;

            gameObject.SetActive(true);
            StopAllCoroutines();

            m_CachedCanvasGroup.alpha = 1f;
            if (Owner != owner || m_OwnerId != owner.Id)
            {
                m_HPBar.value = fromHpRatio;
                Owner = owner;
                m_OwnerId = owner.Id;
            }

            Refresh();

            StartCoroutine(HpBarCo(toHpRatio, AnimationSeconds, KeepSeconds, FadeOutSeconds));
        }

        public bool Refresh()
        {
            if (m_CachedCanvasGroup.alpha <= 0f)
            {
                return false;
            }

            if (Owner == null || !Owner.Available || Owner.Id != m_OwnerId) return true;
            var worldPosition = Owner.CachedTransform.position + Vector3.forward;
            var screenPosition = Base.GameEntry.Scene.MainCamera.WorldToScreenPoint(worldPosition);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_ParentCanvas.transform, screenPosition,
                    m_ParentCanvas.worldCamera, out var position))
            {
                m_CachedTransform.localPosition = position;
            }

            return true;
        }

        public void Reset()
        {
            StopAllCoroutines();
            m_CachedCanvasGroup.alpha = 1f;
            m_HPBar.value = 1f;
            Owner = null;
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            m_CachedTransform = GetComponent<RectTransform>();
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

            m_CachedCanvasGroup = GetComponent<CanvasGroup>();
            if (m_CachedCanvasGroup != null) return;
            Log.Error("CanvasGroup is invalid.");
        }

        private IEnumerator HpBarCo(float value, float animationDuration, float keepDuration, float fadeOutDuration)
        {
            yield return m_HPBar.SmoothValue(value, animationDuration);
            yield return new WaitForSeconds(keepDuration);
            yield return m_CachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration);
        }
    }
}
