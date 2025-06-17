using DG.Tweening;
using UnityEngine;

namespace Scripts.Popups
{
    public class PopupBase : MonoBehaviour
    {
        [SerializeField] private float timeToShow = 1f;
        [SerializeField] private float timeToHide = 0.5f;

        [Space]
        [SerializeField] private RectTransform bodyRect;
        [SerializeField] private CanvasGroup bodyCanvasGroup;
        
        private int yAnchPosHide = 500;
        private int yAnchPosShow = 100;

        private bool isShown;

        protected virtual void PreparingToShow()
        {
            bodyRect.anchoredPosition = new Vector2(0, yAnchPosHide);
            bodyCanvasGroup.blocksRaycasts = false;
            bodyCanvasGroup.alpha = 0f;
            gameObject.SetActive(true);
        } 

        public virtual void ShowBase()
        {
            if (isShown)
                return;

            PreparingToShow();

            isShown = true;

            bodyRect.DOKill();
            bodyRect
                .DOAnchorPosY(yAnchPosShow, timeToShow)
                .SetEase(Ease.OutBack);

            bodyCanvasGroup.DOKill();
            bodyCanvasGroup
                .DOFade(1f, timeToShow)
                .OnComplete(() => bodyCanvasGroup.blocksRaycasts = true);
        }

        public virtual void HideBase()
        {
            if (!isShown)
                return;

            isShown = false;
            bodyCanvasGroup.blocksRaycasts = false;

            bodyRect.DOKill();
            bodyRect
                .DOAnchorPosY(yAnchPosHide, timeToHide)
                .SetEase(Ease.InBack);

            bodyCanvasGroup.DOKill();
            bodyCanvasGroup
                .DOFade(0f, timeToHide)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}