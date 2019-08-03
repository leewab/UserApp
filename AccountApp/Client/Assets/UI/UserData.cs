using System.Collections.Generic;

namespace UI
{
    [System.Serializable]
    public class UserData
    {
        public string Username;
        public int MobileNo;
        public string Password;
        public string Remark;
        public List<AccountData> AccountDatas;
    }

    [System.Serializable]
    public class AccountData
    {
        public string Webname;
        public string WebURL;
        public string WebUsername;
        public string WebPassword;
        public string WebRemark;
    }
}