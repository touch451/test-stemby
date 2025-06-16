using UnityEngine;

namespace Scripts
{
    public class ActionBarCell : MonoBehaviour
    {
        public Figure figure { get; private set; } = null;
        public bool hasFigure => figure != null;

        public void SetupFigure(Figure figure)
        {
            this.figure = figure;
        }

        public void CompleteFigure(float delay)
        {
            figure.CompleteFigure(delay);
            figure = null;
        }

        public void ClearFigure()
        {
            figure = null;
        }
    }
}