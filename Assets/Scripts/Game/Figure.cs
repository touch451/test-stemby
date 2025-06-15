using Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using DG.Tweening;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Figure : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private SpriteRenderer formSr;
        [SerializeField] private SpriteRenderer frameSr;
        [SerializeField] private SpriteRenderer fruitSr;

        private FigureData _data = null;
        private AsyncOperationHandle<Sprite> _fruitSprateHandle;

        private UnityEvent<Figure> onFigureClick = new UnityEvent<Figure>();
        private UnityEvent<Figure> onFigureComplete = new UnityEvent<Figure>();

        private bool inCell = false;

        private void Start()
        {
            LoadFruitSprite();
            SetFrameColor();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (inCell)
                return;

            onFigureClick?.Invoke(this);
        }

        public void Init(FigureData data, UnityAction<Figure> onClick, UnityAction<Figure> onComplete)
        {
            _data = data;
            onFigureClick.AddListener(onClick);
        }

        public void MoveToActionBarCell(ActionBarCell targetCell)
        {
            if (inCell)
                return;

            inCell = true;
            rigidbody2d.simulated = false;

            frameSr.sortingLayerName = "Figure-ActionBar";
            fruitSr.sortingLayerName = "Figure-ActionBar";
            formSr.sortingLayerName = "Figure-ActionBar";
            
            transform.DOKill();
            transform.DOMove(targetCell.transform.position, 0.5f).SetEase(Ease.InOutCubic);
            transform.DORotateQuaternion(Quaternion.identity, 0.5f);
        }

        public void Complete()
        {
            onFigureComplete?.Invoke(this);
        }

        private void LoadFruitSprite()
        {
            string key = "Fruits/" + _data.fruit.ToString();

            StartCoroutine(AddressablesUtils.LoadAsset_Co<Sprite>(key, OnLoaded));

            void OnLoaded(AsyncOperationHandle<Sprite> opHandle)
            {
                _fruitSprateHandle = opHandle;
                fruitSr.sprite = opHandle.Result;
            }
        }

        private void SetFrameColor()
        {
            frameSr.color = ScriptUtils.ConvertColorTypeToColor32(_data.color);
        }

        private void OnDestroy()
        {
            AddressablesUtils.ReleaseAsset(_fruitSprateHandle);
        }
    }
}