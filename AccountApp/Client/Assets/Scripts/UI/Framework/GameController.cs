using UI.Manager;
using UnityEngine;

namespace UI.Framework
{
    public class GameController : MonoSingleton<GameController>
    {
        private void Awake()
        {
            GameManager.Instance.OnSingletonInit();
        }

        private void OnEnable()
        {
            if(GameManager.Instance.HttpHandler != null) GameManager.Instance.HttpHandler.ResponsePostEvent += OnResponsePostEvent;
        }
        
        private void Start()
        {
            LoadUI();
        }

        private void OnDisable()
        {
            if(GameManager.Instance.HttpHandler != null) GameManager.Instance.HttpHandler.ResponsePostEvent -= OnResponsePostEvent;
            Dispose();
        }

        private void OnDestroy()
        {
            Debug.Log("销毁GameController");
            
        }

        private void LoadUI()
        {
            UIManager.Instance.OpenUI<UILoginPanel>();
        }

        private void OnResponsePostEvent(string protocolStr)
        {
            Debug.Log("OnResponsePostEvent");
            Debug.Log(protocolStr);
            GameManager.Instance.ProtocolHandler.ParseProtocolInfo(protocolStr);
        }

        /// <summary>
        /// 通过GameController的Dispose 释放游戏中所有的对象 最后销毁GameController对象
        /// </summary>
        private void Dispose()
        {
            GameManager.Instance.OnSingletonDispose();
        }
    }
}