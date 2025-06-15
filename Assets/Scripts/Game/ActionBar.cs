using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ActionBar : MonoBehaviour
    {
        [SerializeField] private List<ActionBarCell> cells;

        public void AddFigure(Figure figure)
        {
            foreach (var cell in cells)
            {
                if (!cell.hasFigure)
                {
                    cell.SetupFigure(figure);
                    figure.MoveToActionBarCell(cell);
                    break;
                }
            }
        }
    }
}