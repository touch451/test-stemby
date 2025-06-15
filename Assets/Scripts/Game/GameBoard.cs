using Utils;
using UnityEngine;

namespace Scripts
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;

        [Header("Borders:")]
        [SerializeField] private BoardBorder leftBorder;
        [SerializeField] private BoardBorder rightBorder;
        [SerializeField] private BoardBorder bottomBorder;

        public void InitSize()
        {
            Bounds bounds = ScriptUtils.GetCameraWorldBounds(Camera.main);

            spawnPoint.position = new Vector3(0, bounds.max.y + 2f, 0);

            leftBorder.SetSizeAndPosition(
                size: new Vector2(1f, bounds.size.y),
                position: new Vector3(bounds.min.x, 0, 0));

            rightBorder.SetSizeAndPosition(
                size: new Vector2(1f, bounds.size.y),
                position: new Vector3(bounds.max.x, 0, 0));

            bottomBorder.SetSizeAndPosition(
                size: new Vector2(bounds.size.x, 4f),
                position: new Vector3(0, bounds.min.y, 0));
        }
    }
}

