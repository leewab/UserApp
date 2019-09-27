using Framework.Network.HttpMoudle;
using UnityEngine;

namespace UI.Framework
{
    public class GameController : MonoSingleton<GameController>
    {
        private void Awake()
        {
            if(GameManager.Instance.HttpHandler.ResponsePostEvent != null) GameManager.Instance.HttpHandler.ResponsePostEvent += OnResponsePostEvent;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance.HttpHandler.ResponsePostEvent != null) GameManager.Instance.HttpHandler.ResponsePostEvent += OnResponsePostEvent;
        }

        private void InitData()
        {
            
        }

        /// <summary>
        /// 在此load远程数据 存入到DataManager中
        ///     原则：   已有数据只进行初次加载，一次远端访问
        ///             新增数据在客户端已有数据上添加，之后统一上传 ，删除数据同样
        /// </summary>
        private void LoadData()
        {
            
        }
        
        private void LoadUI()
        {
            UIManager.Instance.OpenUI<UILoginPanel>();
        }

        private void OnResponsePostEvent(string protocolStr)
        {
            
        }
    }
}