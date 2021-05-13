using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UI;
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
        /// Get方式请求服务器
        /// </summary>
        /// <param name="allUrl"></param>
        public void RequestServerWithGet(string allUrl)
        {
            GameController.Instance.StartCoroutine(IGetFormRequest(allUrl));
        }

        /// <summary>
        /// Post方式请求服务器
        /// </summary>
        /// <param name="allUrl"></param>
        /// <param name="form"></param>
        public void RequestServerWithPost(string allUrl, Dictionary<string, string> data, string formName)
        {
//            GameController.Instance.StartCoroutine(IPostMFormRequest(allUrl, data));
            HttpRequestFormPost(allUrl, data, formName);
        }

        /// <summary>
        /// 释放HttpHandler
        /// </summary>
        public void Dispose()
        {
            response_PostEvent = null;
            response_GetEvent = null;
        }

        public IEnumerator IPostMFormRequest(string url, Dictionary<string, string> data)
        {
            //将文本转为byte数组  
            byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes("");
            Debug.Log("JSON数据：" + bs);
            Debug.Log(url);
            //向HTTP服务器提交Post数据  
            WWW www = new WWW(url, bs, data);
 
            //等待服务器的响应  
            yield return www;
            //如果出现错误  
            if (www.error != null)
            {
                //获取服务器的错误信息  
                Debug.LogError("PostRequest Error");
                yield return null;
            }
            else
            {
                response_PostEvent?.Invoke(www.text);
            }
        }

        /// <summary>
        /// Post表单请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public IEnumerator IPostFormRequest(string url, WWWForm form)
        {
            Debug.Log(url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["Content-Type"] = "application/json";
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
        
        
        #region Post Part

        /// <summary>
        /// Post响应超时
        /// </summary>
        public static Action OnHttpResponsePoseTimeoutEvent;

        /// <summary>
        /// Post响应
        /// </summary>
        public static Action<string> OnHttpResponsePostEvent;

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formDic"></param>
        public void HttpRequestFormPost(string url, Dictionary<string, string> formDic, string formName)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            req.Method = "POST";

            //拼接Form表单里的信息 string requestForm = "username=test&password=123456";
            StringBuilder sb = new StringBuilder("");
            foreach (var keyValue in formDic)
            {
                sb.Append($"{keyValue.Key}={keyValue.Value}&");
            }

            sb.Append($"form={formName}");
            string requestForm = sb.ToString();
            Debug.Log(requestForm + "//////FormValue");
            byte[] bs = Encoding.ASCII.GetBytes(requestForm);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                //往请求流中写入表单
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }

            using (WebResponse wr = req.GetResponse())
            {
                //对接收到的内容进行处理
                Stream respStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                string result = reader.ReadToEnd();
                wr.Close();
                response_PostEvent?.Invoke(result);
            }
        }

        #endregion

        #region Get Part

        /// <summary>
        /// Get响应超时
        /// </summary>
        public static Action OnHttpResponseGetTimeoutEvent;

        /// <summary>
        /// Get响应
        /// </summary>
        public static Action<string> OnHttpResponseGetEvent;

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        public static void HttpRequestFromGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                //对接收到的内容进行处理
                Stream respStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                string result = reader.ReadToEnd();
                respStream.Close();
                wr.Close();
                OnHttpResponseGetEvent?.Invoke(result);
            }
        }
        
        #endregion
    }
}