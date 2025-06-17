namespace Scripts.Popups
{
    public class StartPopup : PopupBase
    {
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