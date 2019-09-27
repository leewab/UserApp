using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace UI
{
    public static class ProtocolHandler
    {
        /// <summary>
        /// 协议数据存储
        /// </summary>
        private static Dictionary<int, ProtocolData> protocolDatasPool = new Dictionary<int, ProtocolData>();
        
        /// <summary>
        /// 通过协议Id获取协议数据
        /// </summary>
        /// <param name="protocolId"></param>
        /// <returns></returns>
        public static ProtocolData GetProtocolData(int protocolId)
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
        public static void SetProtocolData<T>(string protocolStr) where T : ProtocolData
        {
            var protocol = FormatData(protocolStr);
            if (protocol == null)
            {
                Debug.LogError("传输协议为空");
                return;
            }
            var protocolData = protocol.Data;
            var protocolId = protocol.Id;
            if (protocolData is T)
            {
                ProtocolData curData = null;
                if (protocolDatasPool.TryGetValue(protocolId, out curData))
                {
                    protocolDatasPool[protocolId] = (T) protocolData;
                }
                else
                {
                    protocolDatasPool.Add(protocolId, (T) protocolData);
                }
            }
            else
            {
                Debug.LogError("数据格式映射有误");
            }
        }
        
        /// <summary>
        /// 格式化协议内容
        /// </summary>
        /// <param name="protocolStr"></param>
        /// <typeparam name="T"></typeparam>
        public static Protocol FormatData(string protocolStr)
        {
            return JsonConvert.DeserializeObject<Protocol>(protocolStr);
        }
    }
}