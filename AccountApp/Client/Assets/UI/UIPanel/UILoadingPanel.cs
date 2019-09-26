using UI.Framework;

namespace UI.UIPanel
{
    public class UILoadingPanel : BaseUI
    {
        protected override void OnShow()
        {
            AccountDataManager.Instance.RequestUserAccountData();
            Invoke("StartUI", 1f);
        }

        private void StartUI()
        {
            UIManager.Instance.OpenUI<UIMainPanel>();
        }
    }
}