using System;
using System.Collections.Generic;

namespace UI
{
    public static class ProtocolEnum
    {
        public enum USERINFO
        {
            USER_LOGIN_SUCCESS = 10000,
            USER_LOGIN_FAIL = 10001,
            USER_LOGIN_ERROE = 1002,
            
            USER_REGISTER_SUCCESS = 10010,
            USER_REGISTER_FAIL = 10011,
        }
        
        public enum ACCOUNTINFO
        {
            
        }
    }
    
    public class GameProtocolMessage
    {
        
        private Dictionary<int, Type> dataTypeDic = new Dictionary<int, Type>();

        private void kkk()
        {
            dataTypeDic.Add((int)ProtocolEnum.USERINFO.USER_LOGIN_FAIL, typeof(UserData));

            var d = dataTypeDic[(int) ProtocolEnum.USERINFO.USER_LOGIN_FAIL];
            ProtocolHandler.SetProtocolData<>("ddd");
        }

    }
}