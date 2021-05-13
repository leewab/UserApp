using System.Collections.Generic;
using UI;
using UI.Framework;
using UI.Manager;
using UI.UIPanel.InvicaationCode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIInvicationCodePanel : BaseUI
{
    [SerializeField] private Button btn_SignOut;
    [SerializeField] private Button btn_AddAccount;
    [SerializeField] private Text txt_UserName;
    [SerializeField] private Image img_Header;
    [SerializeField] private Toggle tog_Main;
    [SerializeField] private Toggle tog_Record;
    [SerializeField] private Toggle tog_Generate;
    
    [SerializeField] private UIGenerateCodePanel uiGenerateCodePanel;
    [SerializeField] private UICodeMainPanel uiCodeMainPanel;
    [SerializeField] private UIRecordCodePanel uiRecordCodePanel;
    
    private UserData userData = null;
    public List<AccountData> accountDatas = null;
    private bool isInited = false;
    private CodePartState codeState = CodePartState.MainPanel; 

    private ContentSizeFitter contentSizeFitter;
    protected override void OnShow()
    {
        userData = UserDataManager.Instance.CurUserData;
        accountDatas = AccountDataManager.Instance.AccountDatas;
        txt_UserName.text = userData?.Username;
        if (accountDatas != null) isInited = true;
    }

    protected override void OnRefresh()
    {
        OnClose();
        OnShow();
    }

    protected override void OnClose()
    {
        Clear();
    }

    protected override void RegisterUIEvent()
    {
       if(btn_SignOut != null) btn_SignOut.onClick.AddListener(OnClickSignOutEvent);
       if(btn_AddAccount != null) btn_AddAccount.onClick.AddListener(OnClickAddAccountEvent);
       tog_Main.onValueChanged.AddListener(OnToggleMainEvent);
       tog_Record.onValueChanged.AddListener(OnToggleRecordEvent);
       tog_Generate.onValueChanged.AddListener(OnToggleGenerateEvent);
    }

    protected override void UnRegisterUIEvent()
    {
        if(btn_SignOut != null) btn_SignOut.onClick.RemoveListener(OnClickSignOutEvent);
        if(btn_AddAccount != null) btn_AddAccount.onClick.RemoveListener(OnClickAddAccountEvent);
        tog_Main.onValueChanged.RemoveListener(OnToggleMainEvent);
        tog_Record.onValueChanged.RemoveListener(OnToggleRecordEvent);
        tog_Generate.onValueChanged.RemoveListener(OnToggleGenerateEvent);
    }

    private void OnClickSignOutEvent()
    {
        UIManager.Instance.CloseUI<UIMainPanel>();
        UserDataManager.Instance.CurUserData = null;
        UIManager.Instance.OpenUI<UILoginPanel>();
    }

    private void OnClickAddAccountEvent()
    {
        var uiAddAccountPanel = UIManager.Instance.OpenUI<UIAddAccountPanel>();
        uiAddAccountPanel.OnCloseEvent += () =>
        {
            Refresh();
        };
    }

    private void OnToggleMainEvent(bool isOn)
    {
        if (isOn) codeState = CodePartState.MainPanel;
        SwitchCodePartState();
    }

    private void OnToggleRecordEvent(bool isOn)
    {
        if (isOn) codeState = CodePartState.RecordPanel;
        SwitchCodePartState();
    }

    private void OnToggleGenerateEvent(bool isOn)
    {
        if (isOn) codeState = CodePartState.GeneratePanel;
        SwitchCodePartState();
    }

    private void SwitchCodePartState()
    {
        ClearPanel();
        switch (codeState)
        {
            case CodePartState.MainPanel:
                uiCodeMainPanel.InitPanel();
                break;
            case CodePartState.RecordPanel:
                uiRecordCodePanel.InitPanel();
                break;
            case CodePartState.GeneratePanel:
                uiGenerateCodePanel.InitPanel();
                break;
        }
    }
    
    private void ClearPanel()
    {
        if(uiGenerateCodePanel != null) uiGenerateCodePanel.ClearPlane();
        if(uiCodeMainPanel != null) uiCodeMainPanel.ClearPlane();
        if(uiRecordCodePanel != null) uiRecordCodePanel.ClearPlane();
    }

    public void Clear()
    {
        if(txt_UserName != null) txt_UserName.text = "";
        ClearPanel();
    }
    
    public enum CodePartState
    {
        MainPanel,
        RecordPanel,
        GeneratePanel
    }

}
