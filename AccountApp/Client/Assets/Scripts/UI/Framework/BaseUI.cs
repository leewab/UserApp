using System;
using UI.Manager;
using UnityEngine;

namespace UI.Framework
{
    public class BaseUI : MonoBehaviour
    {
        [Header("CommonEditor")] [SerializeField]
        private GameObject PopUp;

        protected  virtual void OnEnable()
        {
            RegisterUIEvent();
        }

        protected  virtual  void OnDisable()
        {
            UnRegisterUIEvent();
        }

        protected  virtual  void OnDestroy()
        {
            OnDispose();
        }

        public void InitUI(IBaseData data = null)
        {
            if (data != null) baseData = data as BaseData;
        }

        public void Show()
        {
			OnShow();
			OnShowEvent?.Invoke();
            gameObject.SetActive(true);
        }

        public void Refresh(IBaseData data = null)
        {
            if (data != null) baseData = data as BaseData;
            OnRefresh();
            OnRefreshEvent?.Invoke();
        }

        public void Close(bool isDestory = true)
        {
			OnClose();
            OnCloseEvent?.Invoke();
            if (isDestory)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        #region Net Event
        
        protected virtual void OnResponseNetEvent(int protocolId, ProtocolData protocolData)
        {
            Debug.Log("BaseRespinseNetEvent");
        }

        #endregion

        #region UI生命周期
		
		public Action OnShowEvent = null;
		public Action OnRefreshEvent = null;
		public Action OnCloseEvent = null;

        protected BaseData baseData = null;

        protected virtual void RegisterUIEvent()
        {
            if(GameManager.Instance.ProtocolHandler != null) GameManager.Instance.ProtocolHandler.ProtocolDatasAction += OnResponseNetEvent;
        }

        protected virtual void UnRegisterUIEvent()
        {
            if(GameManager.Instance.ProtocolHandler != null) GameManager.Instance.ProtocolHandler.ProtocolDatasAction -= OnResponseNetEvent;
        }

        protected virtual void OnShow()
        {
            
        }

        protected virtual void OnRefresh()
        {
            
        }

        protected virtual void OnClose()
        {
            
        }

        protected virtual void OnDispose()
        {
            
        }

        #endregion
    }
}