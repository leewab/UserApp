using System;
using System.Collections.Generic;
using UI.Framework;
using UnityEngine;

namespace UI.Manager
{
    public class UserDataManager : Singleton<UserDataManager>
    {
        /// <summary>
        /// 当前登录的用户
        /// </summary>
        private UserData curUserData = null;

        public UserData CurUserData
        {
            get => curUserData;
            set => curUserData = value;
        }

        
        /// <summary>
        /// 是否有该用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="callBack"></param>
        public void HasUser(string username, Action<bool> callBack)
        {
            if (string.IsNullOrEmpty(username))
            {
                callBack?.Invoke(false);
                return;
            }
            if (username.Equals(curUserData?.Username))
            {
                callBack?.Invoke(true);
                return;
            }
            
            GameManager.Instance.HttpHandler.ResponseGetEvent += s =>
            {
                if (s.Equals("success"))
                {
                    callBack?.Invoke(true);
                }
                else
                {
                    callBack?.Invoke(false);
                }
            };
            GameManager.Instance.HttpHandler.RequestServerWithGet(string.Format(DefineData.url_GetUser, username));
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="data"></param>
        public void GetUser(Dictionary<string, string> data)
        {
            data.Add("protocolId", ((int) ProtocolEnum.REQ_USER_ACTION.USER_GET).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_GetUser, data, "userInfo_get");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="data"></param>
        public void UserLogin(Dictionary<string, string> data)
        {
            data.Add("protocolId", ((int) ProtocolEnum.REQ_USER_ACTION.USER_LOGIN).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_UserLogin, data, "userLogin");
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="data"></param>
        public void UserRegister(Dictionary<string, string> data)
        {
            data.Add("protocolId", ((int) ProtocolEnum.REQ_USER_ACTION.USER_REGISTER).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_AddUser, data, "userRegister");
        }
    }
}