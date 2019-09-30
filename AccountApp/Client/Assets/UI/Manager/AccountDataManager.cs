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
            
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("searchkey", "*");
            wwwForm.AddField("protocolId", (int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_QUESTDATA);
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_RequestAccountData, wwwForm);
        }

        /// <summary>
        /// 添加账户数据
        /// </summary>
        /// <param name="wwwForm"></param>
        public void AddAccountData(WWWForm wwwForm)
        {
            wwwForm.AddField("protocolId", (int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_ADDDATA);
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_AddAccountData, wwwForm);
        }

        public void RemoveAccountData(WWWForm wwwForm)
        {
            wwwForm.AddField("protocolId", (int)ProtocolEnum.REQ_ACCOUNT_ACTION.ACCOUNT_REMOVEDATA);
            GameManager.Instance.HttpHandler.RequestServerWithPost(DefineData.url_RemoveAccountData, wwwForm);
        }
    }
    
    
}