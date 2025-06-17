using TMPro;
using UnityEngine;

namespace Scripts.Popups
{
    public class FinishPopup : PopupBase
    {
        [SerializeField] private TextMeshProUGUI titleText;

        public void ShowWin()
        {
            titleText.text = "YOU WON!";
            base.ShowBase();
        }

        public void ShowLost()
        {
            titleText.text = "YOU LOST";
            base.ShowBase();
        }

        public void OnEasyButtonCLick()
        {
            base.HideBase();
            GameManager.Instance.NewPlay(Types.DifficultyType.Easy);
        }

        public void OnMediumButtonCLick()
        {
            base.HideBase();
            GameManager.Instance.NewPlay(Types.DifficultyType.Medium);
        }

        public void OnHardButtonCLick()
        {
            base.HideBase();
            GameManager.Instance.NewPlay(Types.DifficultyType.Hard);
        }

        public void OnSurvivalButtonCLick()
        {
            base.HideBase();
            GameManager.Instance.NewPlay(Types.DifficultyType.Survival);
        }
    }
}