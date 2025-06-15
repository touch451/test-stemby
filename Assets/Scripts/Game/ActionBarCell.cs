using UnityEngine;

namespace Scripts
{
    public class ActionBarCell : MonoBehaviour
    {
        private Figure figure = null;

        public bool hasFigure => figure != null;

        public void SetupFigure(Figure figure)
        {
            this.figure = figure;
        }
    }
}