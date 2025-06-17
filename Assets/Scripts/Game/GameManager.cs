using System.Collections;
using System.Collections.Generic;
using Configs;
using Scripts.Popups;
using Types;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig configEasy;
        [SerializeField] private GameConfig configMedium;
        [SerializeField] private GameConfig configHard;
        [SerializeField] private GameConfig configSurvival;

        [Space]
        [SerializeField] private FigureGenerator generator;
        [SerializeField] private FigureSpawner spawner;
        [SerializeField] private SplashSpawner splash;

        [Space]
        [SerializeField] private GameBoard board;
        [SerializeField] private ActionBar actionBar;
        [SerializeField] private PopupsManager popups;

        [Space]
        [SerializeField] private GameObject respawnButton;

        private List<Figure> figures = new List<Figure>();
        private GameConfig currentConfig;

        private bool wasFinished;
        private bool isSurvivalMode;

        public static GameManager Instance;

        private void Awake()
        {
            SetInstance();
        }

        private void Start()
        {
            board.InitSize();
            popups.startPopup.ShowBase();
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

        public void NewPlay(DifficultyType difficulty)
        {
            wasFinished = false;
            isSurvivalMode = false;

            actionBar.Clear();
            DestroyAllFigures();

            respawnButton.SetActive(difficulty != DifficultyType.Survival);

            switch (difficulty)
            {
                case DifficultyType.Easy:
                    Play(configEasy);
                    break;

                case DifficultyType.Medium:
                    Play(configMedium);
                    break;

                case DifficultyType.Hard:
                    Play(configHard);
                    break;

                case DifficultyType.Survival:
                    isSurvivalMode = true;
                    Play(configSurvival);
                    break;
            }
        }

        private void Play(GameConfig config)
        {
            currentConfig = config;

            SpawnNewFigures(config.FiguresCount, config);
            actionBar.SetCounter(config.FiguresCount);
        }

        private void SpawnNewFigures(int count, GameConfig config)
        {
            figures.Clear();

            var figureDatas = generator.GenerateFigureDatas(count, config);
            figures = spawner.SpawnFigures(figureDatas, OnFigureClick, OnFigureComplete);
        }

        private void AddFigures(int count)
        {
            var figureDatas = generator.GenerateFigureDatas(count, currentConfig);
            var addFigures = spawner.SpawnFigures(figureDatas, OnFigureClick, OnFigureComplete);

            figures.AddRange(addFigures);
            actionBar.SetCounter(figures.Count);
        }

        private void OnFigureClick(Figure clickedFigure)
        {
            actionBar.TryAddFigure(clickedFigure);

            if (actionBar.IsFull)
                StartCoroutine(Finish_Co(isWin: false));
        }

        private void OnFigureComplete(Figure completedFigure)
        {
            figures.Remove(completedFigure);
            actionBar.SetCounter(figures.Count);
            splash.Spawn(completedFigure);

            Destroy(completedFigure.gameObject);

            if (isSurvivalMode)
            {
                if (figures.Count <= 15)
                    AddFigures(currentConfig.FiguresCount);

                return;
            }

            if (figures.Count == 0)
                StartCoroutine(Finish_Co(isWin: true));
        }

        private IEnumerator Finish_Co(bool isWin)
        {
            if (wasFinished)
                yield break;

            wasFinished = true;

            yield return new WaitForSeconds(0.5f);

            if (isWin)
                popups.finishPopup.ShowWin();
            else
                popups.finishPopup.ShowLost();
        }

        public void OnRespawnButtonCLick()
        {
            if (spawner.isSpawningProcess)
                return;

            int figuresCount = figures.Count;

            actionBar.Clear();
            DestroyAllFigures();
            SpawnNewFigures(figuresCount, currentConfig);
        }

        private void DestroyAllFigures()
        {
            if (figures == null || figures.Count == 0)
                return;

            float delay = 1f / figures.Count;

            for (int i = 0; i < figures.Count; i++)
            {
                Figure figure = figures[i];
                
                figure.DoScaleFigure(i * delay, onComplete: () =>
                {
                    splash.Spawn(figure);
                    Destroy(figure.gameObject);
                });
            }
        }
    }
}