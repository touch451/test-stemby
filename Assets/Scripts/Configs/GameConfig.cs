using System.Collections.Generic;
using Types;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int figuresCount;

        [Space]
        [SerializeField] private List<FormType> usedForms = new List<FormType>();
        [SerializeField] private List<FruitType> usedFruits = new List<FruitType>();
        [SerializeField] private List<ColorType> usedColors = new List<ColorType>();

        public int FiguresCount => figuresCount;
        public List<FormType> UsedForms => usedForms;
        public List<FruitType> UsedFruits => usedFruits;
        public List<ColorType> UsedColors => usedColors;
    }
}