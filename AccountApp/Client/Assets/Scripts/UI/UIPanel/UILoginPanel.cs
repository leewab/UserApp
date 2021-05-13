using System.Collections.Generic;
using UI;
using UI.Framework;
using UI.Manager;
using UI.UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class UILoginPanel : BaseUI
{
    [SerializeField] private InputField inp_UserName;
    [SerializeField] private InputField inp_Password;
    [SerializeField] private Button btn_Login;
    [SerializeField] private Button btn_Register;
    [SerializeField] private Text txt_UserNameHint;
    [SerializeField] private Text txt_PasswordHint;
    [SerializeField] private Text txt_LoginHint;
    
    private LoginData loginData = null;
    private string username;
    private string password;
    
    protected override void OnShow()
    {
        loginData = baseData as LoginData;
        if (loginData != null)
        {
            inp_UserName.text = loginData.usernameD;
//            inp_Password.text = loginData.password;
        }
    }

    protected override void OnRefresh()
    {
       
    }

    protected override void OnClose()
    {
        ClearPanel();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        btn_Login.onClick.AddListener(OnClickLoginEvent);
        btn_Register.onClick.AddListener(OnClickRegisterEvent);
        inp_UserName.onEndEdit.AddListener(OnUserNameEndEdit);
        inp_Password.onEndEdit.AddListener(OnPasswordEndEdit);
    }

    protected override void OnResponseNetEvent(int protocolId, ProtocolData protocolData)
    {
        base.OnResponseNetEvent(protocolId, protocolData);

        //用户协议数据
        ProtocolUserData userData = null;
        if (protocolData is ProtocolUserData)
        {
            userData = protocolData as ProtocolUserData;
        }
        
        //分协议走逻辑
        if (protocolId == (int)ProtocolEnum.RES_USER_STATE.USER_LOGIN_SUCCESS)
        {
            if (userData?.UserDatas?[0] != null)
            {
                txt_LoginHint.color = Color.green;
                txt_LoginHint.text = "登录成功";
                UserDataManager.Instance.CurUserData = userData.UserDatas[0];
                LoginSuccess();
            }
            else
            {
                Debug.LogError("数据解析异常 或 获取的服务器数据有误");
                txt_LoginHint.text = "数据解析异常 或 获取的服务器数据有误";
            }
        }
        else if (protocolId == (int)ProtocolEnum.RES_USER_STATE.USER_LOGIN_FAIL)
        {
            txt_LoginHint.text = "密码无效";
        }
        else if (protocolId == (int)ProtocolEnum.RES_USER_STATE.USER_LOGIN_NOUSER)
        {
            txt_LoginHint.text = @"用户名不存在";
        }
    }

    private void OnUserNameEndEdit(string act)
    {
        if (act == null || act.Equals(""))
        {
            txt_UserNameHint.text = "用户名不能为空！";
        }

        UserDataManager.Instance.HasUser(act, result =>
        {
            if (!result)
            {
                txt_UserNameHint.text = "用户名不存在！";
            }
        });
    }

    private void OnPasswordEndEdit(string act)
    {
        if (act == null || act.Equals(""))
        {
            txt_PasswordHint.text = "密码不能为空！";
        }
    }

    private void OnClickLoginEvent()
    {
        username = inp_UserName.text;
        password = inp_Password.text;
        Dictionary<string, string> wwwData = new Dictionary<string, string>();
        wwwData.Add("username", username);
        wwwData.Add("password", password);

        UIManager.Instance.OpenUI<UINetLoadingPanel>();
        UserDataManager.Instance.UserLogin(wwwData);
    }

    private void OnClickRegisterEvent()
    {
        UIManager.Instance.OpenUI<UIRegisterPanel>();
        UIManager.Instance.CloseUI<UILoginPanel>();
    }

    private void LoginSuccess()
    {
        UIManager.Instance.OpenUI<UIMainLoadingPanel>();
        UIManager.Instance.CloseUI<UILoginPanel>();
    }

    private void ClearPanel()
    {
        inp_UserName.text = "";
        inp_Password.text = "";
        txt_UserNameHint.text = "";
        txt_PasswordHint.text = "";
        txt_LoginHint.text = "";
        username = null;
        password = null;
    }
}

public class LoginData : BaseData
{
    public string usernameD;
    public string passwordD;
}
