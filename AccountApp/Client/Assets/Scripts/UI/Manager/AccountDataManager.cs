using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.Framework;
using UnityEngine;

namespace UI.Manager
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
            GameManager.Instance.HttpHandler.ResponseGetEvent += s =>
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
            
            Dictionary<string, string> wwwData = new Dictionary<string, string>();
            wwwData.Add("searchKey", "*");
            wwwData.Add("protocolId", ((int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_QUESTDATA).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_RequestAccountData, wwwData, "accountInfo_get");
        }

        /// <summary>
        /// 添加账户数据
        /// </summary>
        /// <param name="wwwForm"></param>
        public void AddAccountData(WWWForm wwwForm)
        {
            Dictionary<string, string> wwwData = new Dictionary<string, string>();
            wwwData.Add("protocolId", ((int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_ADDDATA).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_AddAccountData, wwwData, "accountInfo_add");
        }

        public void RemoveAccountData(WWWForm wwwForm)
        {
            Dictionary<string, string> wwwData = new Dictionary<string, string>();
            wwwData.Add("protocolId", ((int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_REMOVEDATA).ToString());
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_RemoveAccountData, wwwData, "accountInfo_remove");
        }
    }
    
    
}