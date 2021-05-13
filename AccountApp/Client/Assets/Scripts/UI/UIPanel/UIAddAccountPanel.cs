using UI;
using UI.Framework;
using UI.Manager;
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
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("webname", inp_WebName.text);
        wwwForm.AddField("weburl", inp_WebURL.text);
        wwwForm.AddField("webusername", inp_WebUsername.text);
        wwwForm.AddField("webpassword", inp_Password.text);
        wwwForm.AddField("webremark", inp_Remark.text);
        AccountDataManager.Instance.AddAccountData(wwwForm);
        UIManager.Instance.CloseUI<UIAddAccountPanel>();
    }

}

