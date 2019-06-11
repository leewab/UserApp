using UI;
using UI.Framework;
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

    protected override void RegisterUIEvent()
    {
        btn_Login.onClick.AddListener(OnClickLoginEvent);
        btn_Register.onClick.AddListener(OnClickRegisterEvent);
        inp_UserName.onEndEdit.AddListener(OnUserNameEndEdit);
        inp_Password.onEndEdit.AddListener(OnPasswordEndEdit);
    }

    private void OnUserNameEndEdit(string act)
    {
        if (act == null || act.Equals(""))
        {
            txt_UserNameHint.text = "用户名不能为空！";
        }
        else if (!DataManager.Instance.HasUser(act))
        {
            txt_UserNameHint.text = "用户名不存在！";
        }
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
        var state = DataManager.Instance.IsLoginSuccess(username, password);
        if (state == DataManager.UserLoginState.SUCCESS)
        {
            txt_LoginHint.color = Color.green;
            txt_LoginHint.text = "登录成功";
            DataManager.Instance.CurUsername = username;
            InvokeRepeating("LoginSuccess", 3, 1);
        }
        else if (state == DataManager.UserLoginState.PSD_INVALID)
        {
            txt_LoginHint.text = "密码无效";
        }
        else if (state == DataManager.UserLoginState.UN_EMPTY)
        {
            txt_LoginHint.text = @"用户名不存在";
        }
    }

    private void OnClickRegisterEvent()
    {
        UIManager.Instance.OpenUI<UIRegisterPanel>();
        UIManager.Instance.CloseUI<UILoginPanel>();
    }

    private void LoginSuccess()
    {
        UIManager.Instance.OpenUI<UIMainPanel>();
        UIManager.Instance.CloseUI<UILoginPanel>();
        CancelInvoke();
    }
}

public class LoginData : BaseData
{
    public string usernameD;
    public string passwordD;
}
