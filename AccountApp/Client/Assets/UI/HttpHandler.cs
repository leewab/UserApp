using System;
using System.Collections;
using UI.Framework;
using UnityEngine;

namespace Framework.Network.HttpMoudle
{
    public class HttpHandler
    {
        /// <summary>
        /// post方式的 response响应结果
        /// </summary>
        private Action<string> response_PostEvent;

        public Action<string> ResponsePostEvent
        {
            get => response_PostEvent;
            set => response_PostEvent = value;
        }

        /// <summary>
        /// Get方式的 response响应结果
        /// </summary>
        private Action<string> response_GetEvent;

        public Action<string> ResponseGetEvent
        {
            get => response_GetEvent;
            set => response_GetEvent = value;
        }

        /// <summary>
        /// Post表单请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public IEnumerator IPostFormRequest(string url, WWWForm form)
        {
            WWW www = new WWW(url, form);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("PostRequest Error");
            }
            else
            {
                response_PostEvent?.Invoke(www.text);
            }
        }

        /// <summary>
        /// Get表单请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IEnumerator IGetFormRequest(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("GetRequest Error");
            }
            else
            {
                response_GetEvent?.Invoke(www.text);
            }
        }
    }
}