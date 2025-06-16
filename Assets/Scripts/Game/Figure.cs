using Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using DG.Tweening;
using System;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Figure : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private SpriteRenderer formSr;
        [SerializeField] private SpriteRenderer frameSr;
        [SerializeField] private SpriteRenderer fruitSr;

        private AsyncOperationHandle<Sprite> _fruitSprateHandle = new AsyncOperationHandle<Sprite>();

        private UnityEvent<Figure> onFigureClick = new UnityEvent<Figure>();
        private UnityEvent<Figure> onFigureComplete = new UnityEvent<Figure>();

        private bool inActionBar = false;
        public FigureData data { get; private set; } = null;

        private void Start()
        {
            LoadFruitSprite();
            SetFrameColor();
        }

        public void Init(FigureData data, UnityAction<Figure> onClick, UnityAction<Figure> onComplete)
        {
            this.data = data;
            onFigureClick.AddListener(onClick);
            onFigureComplete.AddListener(onComplete);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inActionBar)
                return;

            onFigureClick?.Invoke(this);
        }

        public void MoveToActionBar(ActionBarCell targetCell, float duration)
        {
            if (inActionBar)
                return;

            inActionBar = true;
            rigidbody2d.simulated = false;

            frameSr.sortingLayerName = "Figure-ActionBar";
            fruitSr.sortingLayerName = "Figure-ActionBar";
            formSr.sortingLayerName = "Figure-ActionBar";
            
            transform.DORotateQuaternion(Quaternion.identity, duration);

            transform
                .DOMove(targetCell.transform.position, duration)
                .SetEase(Ease.InOutCubic);
        }

        public void CompleteFigure(float delay = 0f)
        {
            DoScaleFigure(delay, () => onFigureComplete?.Invoke(this));
        }

        public void DoScaleFigure(float delay, Action onComplete = null)
        {
            rigidbody2d.simulated = false;
            rigidbody2d.bodyType = RigidbodyType2D.Static;

            transform
                .DOScale(1.15f, 0.25f)
                .SetDelay(delay)
                .OnComplete(() => onComplete?.Invoke());
        }

        private void LoadFruitSprite()
        {
            string key = "Fruits/" + data.fruit.ToString();

            StartCoroutine(AddressablesUtils.LoadAsset_Co<Sprite>(key, OnLoaded));

            void OnLoaded(AsyncOperationHandle<Sprite> opHandle)
            {
                _fruitSprateHandle = opHandle;
                fruitSr.sprite = opHandle.Result;
            }
        }

        private void SetFrameColor()
        {
            frameSr.color = ScriptUtils.ConvertColorTypeToColor32(data.color);
        }

        public bool IsEqualFigure(Figure figure)
        {
            return
                data.form == figure.data.form &&
                data.fruit == figure.data.fruit &&
                data.color == figure.data.color;
        }

        private void OnDestroy()
        {
            AddressablesUtils.ReleaseAsset(_fruitSprateHandle);
            onFigureClick.RemoveAllListeners();
            onFigureComplete.RemoveAllListeners();
        }
    }
}