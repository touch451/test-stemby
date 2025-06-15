using Types;
using UnityEngine;

namespace Utils
{
    public static class ScriptUtils
    {
        public static Color32 ConvertColorTypeToColor32(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Red:
                    return new Color32(231, 26, 75, 255);

                case ColorType.Yellow:
                    return new Color32(254, 218, 31, 255);

                case ColorType.Blue:
                    return new Color32(105, 127, 241, 255);

                case ColorType.Purple:
                    return new Color32(216, 106, 245, 255);

                case ColorType.Orange:
                    return new Color32(254, 106, 0, 255);

                case ColorType.Green:
                    return new Color32(160, 237, 0, 255);

                default:
                    Debug.LogError("Can't convert ColorType to Color32. Type is - " + colorType);
                    return new Color32(255, 255, 255, 255);
            }
        }

        public static Bounds GetCameraWorldBounds(Camera camera)
        {
            Bounds bounds = new Bounds();

            Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
            Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

            bounds.SetMinMax(bottomLeft, topRight);
            return bounds;
        }
    }
}