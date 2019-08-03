using UI;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIAddAccountPanel : BaseUI
{
    [SerializeField] private InputField inp_WebName;
    [SerializeField] private InputField inp_WebURL;
    [SerializeField] private InputField inp_WebUsername;
    [SerializeField] private InputField inp_Password;
    [SerializeField] private InputField inp_Remark;
    [SerializeField] private Button btn_Confirm;
    [SerializeField] private Button btn_Close;
    
    protected override void OnShow()
    {
        
    }

    protected override void OnRefresh()
    {
       
    }

    protected override void RegisterUIEvent()
    {
        btn_Close.onClick.AddListener(OnClickCloseEvent);
        btn_Confirm.onClick.AddListener(OnClickConfirmEvent);
    }

    protected override void UnRegisterUIEvent()
    {
        btn_Close.onClick.RemoveListener(OnClickCloseEvent);
        btn_Confirm.onClick.RemoveListener(OnClickConfirmEvent);
    }

    private void OnClickCloseEvent()
    {
        UIManager.Instance.CloseUI<UIAddAccountPanel>();
    }

    private void OnClickConfirmEvent()
    {
        DataManager.Instance.AddAccountSingleData(new AccountData()
        {
            Webname = inp_WebName.text,
            WebURL = inp_WebURL.text,
            WebUsername = inp_WebUsername.text,
            WebPassword = inp_Password.text,
            WebRemark = inp_Remark.text
        });
        UIManager.Instance.CloseUI<UIAddAccountPanel>();
    }

}

