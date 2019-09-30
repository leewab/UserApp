using System.Collections.Generic;

namespace UI
{
    public class UserData
    {
        public string Username;
        public string Password;
        public int PhoneNo;
        public int IdCard;
        public string Remark;
    }

    public class AccountData
    {
        public string Webname;
        public string WebURL;
        public string WebUsername;
        public string WebPassword;
        public string WebRemark;
    }

    /// <summary>
    /// 协议样式
    /// </summary>
    public class Protocol
    {
        public int Id;
        public object Data;
    }

    /// <summary>
    /// 协议数据
    /// </summary>
    public class ProtocolData
    {
        
    }

    public class ProtocolAccountData : ProtocolData
    {
        public List<AccountData> AccountDatas;
    }

    public class ProtocolUserData : ProtocolData
    {
        public List<UserData> UserDatas;
    }
}