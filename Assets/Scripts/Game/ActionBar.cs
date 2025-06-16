using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class ActionBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;
        [SerializeField] private List<ActionBarCell> cells = new List<ActionBarCell>();

        public bool IsFull => cells.TrueForAll(cell => cell.hasFigure);

        public void TryAddFigure(Figure figure)
        {
            foreach (var cell in cells)
            {
                if (!cell.hasFigure)
                {
                    SetupFigureIntoCell(figure, cell);
                    break;
                }
            }
        }

        public void Clear()
        {
            foreach (var cell in cells)
                cell.ClearFigure();
        }

        public void SetCounter(int figuresCount)
        {
            counter.text = figuresCount.ToString();
        }

        private void SetupFigureIntoCell(Figure figure, ActionBarCell targetCell)
        {
            float moveDuration = 0.5f;

            targetCell.SetupFigure(figure);

            if (HasMatchedCellsByFigure(figure, out List<ActionBarCell> matchedCells))
            {
                for (int i = 0; i < matchedCells.Count; i++)
                    matchedCells[i].CompleteFigure(delay: moveDuration + i * 0.1f);
            }    

            figure.MoveToActionBar(targetCell, moveDuration);
        }

        private bool HasMatchedCellsByFigure(Figure figure, out List<ActionBarCell> matchedCells)
        {
            matchedCells = cells.FindAll(cell => cell.hasFigure && cell.figure.IsEqualFigure(figure));

            if (matchedCells != null && matchedCells.Count >= 3)
                return true;
            else
                return false;
        }
    }
}