using System.Collections.Generic;
using System.Linq;
using Configs;
using Types;
using UnityEngine;

namespace Scripts
{
    public class FigureGenerator : MonoBehaviour
    {
        private string TAG = "[FigureGenerator] ";

        public List<FigureData> GenerateFigureDatas(GameConfig gameConfig)
        {
            return
                Generate(gameConfig.FiguresCount, gameConfig.UsedForms,
                gameConfig.UsedColors, gameConfig.UsedFruits);
        }

        public List<FigureData> GenerateFigureDatas(int count, GameConfig gameConfig)
        {
            return
                Generate(count, gameConfig.UsedForms,
                gameConfig.UsedColors, gameConfig.UsedFruits);
        }

        private List<FigureData> Generate(int count, List<FormType> usedForms,
            List<ColorType> usedColors, List<FruitType> usedFruits)
        {
            // Общее количество фигур должно быть кратно 3
            if (count == 0 || count % 3 != 0)
            {
                Debug.LogError(TAG + "Figures count must be a multiple of 3.");
                return new List<FigureData>();
            }

            var uniqueFigures = new List<FigureData>();

            // Создаём список всех возможных уникальных фигур
            foreach (FormType form in usedForms)
                foreach (ColorType color in usedColors)
                    foreach (FruitType fruit in usedFruits)
                    {
                        FigureData uniqueFigure = new FigureData
                        {
                            form = form,
                            color = color,
                            fruit = fruit
                        };

                        if (!uniqueFigures.Contains(uniqueFigure))
                            uniqueFigures.Add(uniqueFigure);
                    }

            Debug.Log(TAG + "Unique figures count = " + uniqueFigures.Count);

            // Отбираем случайные фигуры и делаем их количество кратным 3
            List<FigureData> result = new List<FigureData>();
            var rnd = new System.Random();

            while (result.Count < count)
            {
                var figure = uniqueFigures[rnd.Next(uniqueFigures.Count)];
                result.AddRange(Enumerable.Repeat(figure, 3));
            }

            Debug.Log(TAG + "Generated result count = " + result.Count);
            return result.OrderBy(x => rnd.Next()).ToList();
        }
    }
}