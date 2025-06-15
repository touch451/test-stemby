using UnityEngine;

namespace Scripts
{
    public class BoardBorder : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider2D;

        public void SetSizeAndPosition(Vector2 size, Vector3 position)
        {
            spriteRenderer.size = size;
            boxCollider2D.size = size;
            transform.position = position;
        }
    }
}