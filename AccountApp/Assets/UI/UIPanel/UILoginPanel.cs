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
    
    protected override void OnShow()
    {
        loginData = baseData as LoginData;
        
    }
}

public class LoginData : BaseData
{
    
}
