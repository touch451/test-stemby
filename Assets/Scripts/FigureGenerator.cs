using System;
using System.Collections.Generic;
using System.Linq;
using Types;
using UnityEngine;

namespace Scripts
{
    public class FigureGenerator : MonoBehaviour
    {
        public List<FigureData> GenerateFigureSet(int totalCount)
        {
            // Общее кол-во фигур должно быть кратно 3
            if (totalCount == 0 || totalCount % 3 != 0)
            {
                Debug.LogError("totalCount must be a multiple of 3.");
                return new List<FigureData>();
            }

            var uniqueFigures = new List<FigureData>();

            // Создаём список всех возможных уникальных фигур
            foreach (FormType form in Enum.GetValues(typeof(FormType)))
                foreach (FrameType frame in Enum.GetValues(typeof(FrameType)))
                    foreach (FruitType fruit in Enum.GetValues(typeof(FruitType)))
                    {
                        FigureData uniqueFigure = new FigureData
                        {
                            form = form,
                            frame = frame,
                            fruit = fruit
                        };

                        uniqueFigures.Add(uniqueFigure);
                    }

            // Отбираем случайные фигуры и делаем их количество кратным 3
            List<FigureData> result = new List<FigureData>();
            var rnd = new System.Random();

            while (result.Count < totalCount)
            {
                var figure = uniqueFigures[rnd.Next(uniqueFigures.Count)];
                result.AddRange(Enumerable.Repeat(figure, 3));
            }

            return result.OrderBy(x => rnd.Next()).ToList();
        }
    }
}