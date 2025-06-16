using UnityEngine;
using Utils;

namespace Scripts
{
    public class SplashSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem splashPrefab;

        public static SplashSpawner Instance;

        private void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Spawn(Figure figure)
        {
            Color32 color32 = ScriptUtils.ConvertColorTypeToColor32(figure.data.color);
            Vector3 position = figure.transform.position;

            var splash = Instantiate(splashPrefab, position, Quaternion.identity);

            var main = splash.main;
            main.startColor = (Color)color32;

            splash.Play();
        }
    }
}