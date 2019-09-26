using System;
using Boo.Lang;
using Newtonsoft.Json;
using UI.Framework;
using UnityEngine;

namespace UI
{
    public class AccountDataManager : Singleton<AccountDataManager>
    {
        /// <summary>
        /// 账户信息
        /// </summary>
        private List<AccountData> accountDatas = null;

        public List<AccountData> AccountDatas => accountDatas;

        /// <summary>
        /// 请求账户数据
        /// </summary>
        /// <param name="callBack"></param>
        public void RequestUserAccountData()
        {
            GameController.Instance.HttpHandler.ResponseGetEvent += s =>
            {
                try
                {
                    accountDatas = JsonConvert.DeserializeObject<List<AccountData>>(s);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
            
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("searchkey", "*");
            RequestServerWithPost(string.Format(DefineManager.url_RequestAccountData), wwwForm);
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