using System.Collections;
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
        [SerializeField] private SplashSpawner splash;

        [Space]
        [SerializeField] private GameBoard board;
        [SerializeField] private ActionBar actionBar;

        private List<Figure> figures = new List<Figure>();

        private void Start()
        {
            Play();
        }

        private void Play()
        {
            board.InitSize();
            SpawnFigures(config.FiguresCount);
            actionBar.SetCounter(config.FiguresCount);
        }

        private void SpawnFigures(int count)
        {
            var figureDatas = generator.GenerateFigureDatas(count, config);
            figures = spawner.SpawnFigures(figureDatas, OnFigureClick, OnFigureComplete);
        }

        private void OnFigureClick(Figure clickedFigure)
        {
            actionBar.TryAddFigure(clickedFigure);

            if (actionBar.IsFull)
                OnLose();
        }

        private void OnFigureComplete(Figure completedFigure)
        {
            figures.Remove(completedFigure);
            actionBar.SetCounter(figures.Count);
            splash.Spawn(completedFigure);

            Destroy(completedFigure.gameObject);

            if (figures.Count == 0)
                OnWin();
        }

        private void OnWin()
        {
            Debug.LogWarning("WIN");
        }

        private void OnLose()
        {
            Debug.LogWarning("LOSE");
        }

        public void OnRespawnButtonCLick()
        {
            int figuresCount = figures.Count;

            for (int i = 0; i < figures.Count; i++)
            {
                Figure figure = figures[i];
                float delay = 1f / figuresCount;

                figure.DoScaleFigure(i * delay, onComplete: () =>
                {
                    splash.Spawn(figure);
                    Destroy(figure.gameObject);
                });
            }

            actionBar.Clear();
            figures.Clear();

            SpawnFigures(figuresCount);
        }
    }
}