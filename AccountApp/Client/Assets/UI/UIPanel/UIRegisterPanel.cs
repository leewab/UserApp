using System.Collections.Generic;
using UI;
using UI.Framework;
using UI.Manager;
using UnityEngine;
using UnityEngine.UI;

public class UIRegisterPanel : BaseUI
{
    [SerializeField] private InputField inp_UserName;
    [SerializeField] private InputField inp_Password1;
    [SerializeField] private InputField inp_Password2;
    [SerializeField] private Button btn_Login;
    [SerializeField] private Button btn_Register;
    [SerializeField] private Text txt_UserNameHint;
    [SerializeField] private Text txt_Password1Hint;
    [SerializeField] private Text txt_Password2Hint;
    [SerializeField] private Text txt_RegisterHint;

    private string username;
    private string password1;
    private string password2;
    private bool isRegister = true;
    
    protected override void OnShow()
    {
        
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
        btn_Register.onClick.AddListener(OnClickRegisterEvent);
        btn_Login.onClick.AddListener(OnClickLoginEvent);
        inp_UserName.onEndEdit.AddListener(OnUsernameEndEditEvent);
        inp_Password1.onEndEdit.AddListener(OnPassword1EndEditEvent);
        inp_Password2.onEndEdit.AddListener(OnPassword2EndEditEvent);
    }

    protected override void OnResponseNetEvent(int protocolId, ProtocolData protocolData)
    {
        base.OnResponseNetEvent(protocolId, protocolData);
        Debug.Log("Register OnResponseNetEvent");
        if (protocolId == (int) ProtocolEnum.RES_USER_STATE.USER_REGISTER_SUCCESS)
        {
            Debug.Log("注册成功");
            GoToLoginPanel();
        }
        else if (protocolId == (int) ProtocolEnum.RES_USER_STATE.USER_REGISTER_FAIL)
        {
            Debug.Log("注册失败");
        }
    }

    private void OnClickRegisterEvent()
    {
        if (isRegister)
        {
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("username", username);
            wwwForm.AddField("password", password1);
            UserDataManager.Instance.UserRegister(wwwForm);
        }
    }

    private void OnClickLoginEvent()
    {
        UIManager.Instance.OpenUI<UILoginPanel>();
        UIManager.Instance.CloseUI<UIRegisterPanel>();
    }

    private void OnUsernameEndEditEvent(string value)
    {
        username = value;
        if (value == null || value.Equals(""))
        {
            txt_UserNameHint.text = "用户名不能为空！";
            isRegister = false;
        }
        else
        {
            isRegister = true;
        }
    }

    private void OnPassword1EndEditEvent(string value)
    {
        password1 = value;
        if (value == null || value.Equals(""))
        {
            txt_Password1Hint.text = "密码不能为空！";
            isRegister = false;
        }
        else
        {
            isRegister = true;
        }
    }

    private void OnPassword2EndEditEvent(string value)
    {
        password2 = value;
        if (value == null || value.Equals(""))
        {
            txt_Password2Hint.text = "密码不能为空！";
            isRegister = false;
        }
        else if (!password1.Equals(password2))
        {
            txt_Password2Hint.text = "密码不一致！";
            isRegister = false;
        }
        else
        {
            isRegister = true;
        }
    }

    private void GoToLoginPanel()
    {
        UIManager.Instance.OpenUI<UILoginPanel>(new LoginData()
        {
            usernameD = username,
            passwordD = password1
        });
        UIManager.Instance.CloseUI<UIRegisterPanel>();
    }
    
    private void ClearPanel()
    {
        inp_UserName.text = "";
        inp_Password1.text = "";
        inp_Password2.text = "";
        txt_UserNameHint.text = "";
        txt_Password1Hint.text = "";
        txt_Password2Hint.text = "";
        txt_RegisterHint.text = "";
        username = null;
        password1 = null;
        password2 = null;
    }

}

