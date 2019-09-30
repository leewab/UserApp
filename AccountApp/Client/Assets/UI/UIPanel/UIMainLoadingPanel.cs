using UI.Framework;
using UI.Manager;

namespace UI.UIPanel
{
    /// <summary>
    /// UIMainLoading用于游戏登录之后开始加载 加载所有游戏数据
    ///  
    /// 在此load远程数据 存入到DataManager中
    ///     原则：   已有数据只进行初次加载，一次远端访问
    ///             新增数据在客户端已有数据上添加，之后统一上传 ，删除数据同样
    /// </summary>
    public class UIMainLoadingPanel : BaseUI
    {
        protected override void OnShow()
        {
            AccountDataManager.Instance.RequestUserAccountData();
            Invoke("StartUI", 2f);
        }

        private void StartUI()
        {
            UIManager.Instance.OpenUI<UIMainPanel>();
            UIManager.Instance.CloseUI<UIMainLoadingPanel>();
        }
    }
}