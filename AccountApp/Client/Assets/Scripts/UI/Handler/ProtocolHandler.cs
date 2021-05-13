using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.UIPanel;
using UnityEngine;

namespace UI
{
    public class ProtocolHandler
    {
        
        /// <summary>
        /// 协议事件响应
        /// </summary>
        public Action<int, ProtocolData> ProtocolDatasAction;            //该协议事件应该是分发 公共BaseUI分发

        /// <summary>
        /// 协议数据存储
        /// </summary>
        private Dictionary<int, ProtocolData> protocolDatasPool = new Dictionary<int, ProtocolData>();

        /// <summary>
        /// 通过协议Id获取协议数据
        /// </summary>
        /// <param name="protocolId"></param>
        /// <returns></returns>
        public ProtocolData GetProtocolData(int protocolId)
        {
            if (protocolDatasPool.ContainsKey(protocolId))
            {
                return protocolDatasPool[protocolId];
            }

            return null;
        }

        /// <summary>
        /// 存储协议数据
        /// </summary>
        /// <param name="protocolStr"></param>
        /// <typeparam name="T"></typeparam>
        public void SetProtocolData(int protocolId, ProtocolData protocolData)
        {
            ProtocolData curData = null;
            if (protocolDatasPool.TryGetValue(protocolId, out curData))
            {
                protocolDatasPool[protocolId] = protocolData;
            }
            else
            {
                protocolDatasPool.Add(protocolId, protocolData);
            }
        }
        
        /// <summary>
        /// 解析协议信息
        /// </summary>
        /// <param name="protocolStr"></param>
        public void ParseProtocolInfo(string protocolStr)
        {
            if (string.IsNullOrEmpty(protocolStr))
            {
                Debug.LogError("协议传输为空");
            }
            else
            {
                Debug.Log(protocolStr);
                var protocol = FormatData(protocolStr);
                if (protocol == null)
                {
                    Debug.LogError("传输协议反序列化为空");
                    return;
                }
                Debug.Log(protocol.Id);
                Debug.Log(protocol.Data);
                var protocolDataStr = protocol.Data;
                var protocolId = protocol.Id;  //到这里啦

                try
                {
                    var protocolData = JsonConvert.DeserializeObject<ProtocolData>(protocolDataStr.ToString());
                    SetProtocolData(protocolId, protocolData);
                    ProtocolDatasAction?.Invoke(protocolId, protocolData); //该协议事件应该是分发
                }
                catch (Exception e)
                {
                    Debug.LogError("数据格式映射有误");
                    ProtocolDatasAction?.Invoke(protocolId, null);
                    Console.WriteLine(e);
                    throw;
                }
                //接收到协议之后关闭网络请求的Loading界面
                UIManager.Instance.CloseUI<UINetLoadingPanel>();
            }
            
        }
        
                
        /// <summary>
        /// 格式化协议内容
        /// </summary>
        /// <param name="protocolStr"></param>
        /// <typeparam name="T"></typeparam>
        public Protocol FormatData(string protocolStr)
        {
            Debug.Log(protocolStr);
            return JsonConvert.DeserializeObject<Protocol>(protocolStr);
        }

        /// <summary>
        /// 释放Manager
        /// </summary>
        public void Dispose()
        {
            ProtocolDatasAction = null;
            protocolDatasPool.Clear();
        }
    }
}