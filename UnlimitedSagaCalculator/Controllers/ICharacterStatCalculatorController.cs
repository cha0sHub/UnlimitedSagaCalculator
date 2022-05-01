namespace UnlimitedSagaCalculator.Controllers
{
    public interface ICharacterStatCalculatorController
    {
        void UpdateLanguage();
        void SetStartingPanels();
        void Calculate();
        void Transfer();
        void SetMainController(IMainController mainController);
    }
}
