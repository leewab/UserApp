using System;
using System.Collections.Generic;
using UI.Framework;
using UnityEngine;

namespace UI
{
    public class UserDataManager : Singleton<UserDataManager>
    {

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        private UserData curUserData = null;

        public UserData CurUserData => curUserData;


        #region URL

        private string port = "45419";                   //端口动态获取 不固定
        private string url_IsUser = "ibinggame.ticp.io:{0}/login_Check?usename={1}";
        private string url_AddUser = "ibinggame.ticp.io:{0}/login_Add";
        private string url_IsLoginSuccess = "ibinggame.ticp.io:{0}/login";

        #endregion
        
        public enum UserLoginState
        {
            SUCCESS,            //登录成功
            UN_REPETITION,      //用户名重复
            UN_EMPTY,           //用户名不存在
            PSD_INVALID,        //密码错误
        }
        
        /// <summary>
        /// 是否有该用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="callBack"></param>
        public void HasUser(string username, Action<bool> callBack)
        {
            GameController.Instance.HttpHandler.ResponseGetEvent += s =>
            {
                if (s.Equals("true"))
                {
                    callBack?.Invoke(true);
                }
                else
                {
                    callBack?.Invoke(false);
                }
            };
            RequestServerWithGet(string.Format(url_IsUser, port, username));
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userForm"></param>
        public void AddUser(WWWForm userForm, Action<bool> callBack)
        {
            GameController.Instance.HttpHandler.ResponsePostEvet += s =>
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
            RequestServerWithPost(string.Format(url_IsUser, port), userForm);
        }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        /// <param name="wwwForm"></param>
        /// <param name="callBack"></param>
        public void IsLoginSuccess(WWWForm wwwForm, Action<int> callBack)
        {
            GameController.Instance.HttpHandler.ResponsePostEvet += s =>
            {
                curUserData = new UserData();  到这里的协议传送问题
                callBack?.Invoke();
            };
            RequestServerWithPost(string.Format(url_IsLoginSuccess, port), wwwForm);
        }
        
        
        /// <summary>
        /// Get方式请求服务器
        /// </summary>
        /// <param name="allUrl"></param>
        public void RequestServerWithGet(string allUrl)
        {
            GameController.Instance.StartCoroutine(GameController.Instance.HttpHandler.IGetFormRequest(allUrl));
        }

        /// <summary>
        /// Post方式请求服务器
        /// </summary>
        /// <param name="allUrl"></param>
        /// <param name="form"></param>
        public void RequestServerWithPost(string allUrl, WWWForm form)
        {
            GameController.Instance.StartCoroutine(GameController.Instance.HttpHandler.IPostFormRequest(allUrl, form));
        }

    }
}