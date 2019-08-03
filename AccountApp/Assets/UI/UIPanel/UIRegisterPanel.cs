using System.Collections.Generic;
using UI;
using UI.Framework;
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

    private void OnClickRegisterEvent()
    {
        if (isRegister)
        {
            DataManager.Instance.SetUserData(new UserData()
            {
                Username = username,
                Password = password1,
                Remark = "Register",
                AccountDatas = new List<AccountData>()
                {
                    new AccountData()
                    {
                        Webname = "ppppppp",
                        WebURL = "ooooooooooo",
                        WebUsername = "9999999999999",
                        WebPassword = "99999",
                        WebRemark = "8888"
                    },
                    new AccountData()
                    {
                        Webname = "ppppppp22",
                        WebURL = "oooooooooo222o",
                        WebUsername = "9999999999992229",
                        WebPassword = "99922299",
                        WebRemark = "88822228"
                    },
                    new AccountData()
                    {
                        Webname = "ppppppp333",
                        WebURL = "oooooooooo333o",
                        WebUsername = "999999999999339",
                        WebPassword = "9993399",
                        WebRemark = "88833328"
                    },
                    new AccountData()
                    {
                        Webname = "ppppppp4444",
                        WebURL = "oooooooooo24442o",
                        WebUsername = "99999999999944429",
                        WebPassword = "99444299",
                        WebRemark = "8882444428"
                    },
                }
            });
            DataManager.Instance.SaveUserData();
            InvokeRepeating("GoToLoginPanel", 2, 1);
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
        CancelInvoke();
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

