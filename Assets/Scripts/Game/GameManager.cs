using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig config;

        [Space]
        [SerializeField] private FigureGenerator generator;
        [SerializeField] private FigureSpawner spawner;
        [SerializeField] private GameBoard board;
        [SerializeField] private ActionBar actionBar;

        private List<Figure> figures = new List<Figure>();

        private void Start()
        {
            board.InitSize();
            
            var figureDatas = generator.GenerateFigureDatas(config);
            figures = spawner.SpawnFigures(figureDatas, OnFigureClick, OnFigureComplete);
        }

        private void OnFigureClick(Figure clickedFigure)
        {
            actionBar.AddFigure(clickedFigure);
        }

        private void OnFigureComplete(Figure completedFigure)
        {
        }

        public void OnRespawnButtonCLick()
        {
            DestroyFigures();

            var figureDatas = generator.GenerateFigureDatas(figures.Count, config);
            figures = spawner.SpawnFigures(figureDatas, OnFigureClick, OnFigureComplete);
        }

        private void DestroyFigures()
        {
            foreach (var figure in figures)
            {
                Destroy(figure.gameObject);
            }
        }
    }
}