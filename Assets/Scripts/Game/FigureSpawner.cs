using System;
using System.Collections;
using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform figuresContainer;

        [Space]
        [SerializeField] private float spawnDelay;

        [Header("Figure Prefabs:")]
        [SerializeField] private Figure squareFigurePrefab;
        [SerializeField] private Figure triangleFigurePrefab;
        [SerializeField] private Figure circleFigurePrefab;

        float spawnPosX;
        float spawnStepX = 1f;

        public List<Figure> SpawnFigures(List<FigureData> figureDatas,
            UnityAction<Figure> onFigureClick, UnityAction<Figure> onFigureComplete)
        {
            spawnPosX = -1f;

            List<Figure> figures = new List<Figure>();

            for (int i = 0; i < figureDatas.Count; i++)
            {
                var figureData = figureDatas[i];
                var figure = CreateFigure(figureData.form);

                figure.Init(figureData, onFigureClick, onFigureComplete);
                figures.Add(figure);
            }

            StartCoroutine(DoFallingFigures(figures));

            return figures;
        }

        private Figure CreateFigure(FormType form)
        {
            Figure figurePrefab = null;

            switch (form)
            {
                case FormType.Circle:
                    figurePrefab = circleFigurePrefab;
                    break;
                case FormType.Square:
                    figurePrefab = squareFigurePrefab;
                    break;
                case FormType.Triangle:
                    figurePrefab = triangleFigurePrefab;
                    break;
            }

            figurePrefab.gameObject.SetActive(false);

            if (spawnPosX < -1f || spawnPosX > 1f)
                spawnStepX *= -1f;

            spawnPosX += spawnStepX;
            Vector3 position = new Vector3(spawnPosX, spawnPoint.position.y, 0);
            
            float randomZ = UnityEngine.Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, randomZ);

            return Instantiate(figurePrefab, position, rotation, figuresContainer);
        }

        private IEnumerator DoFallingFigures(List<Figure> figures)
        {
            foreach (var figure in figures)
            {
                figure.gameObject.SetActive(true);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}