using System;
using UnityEngine;

namespace UI.Framework
{
    public class BaseUI : MonoBehaviour
    {
        [Header("CommonEditor")] [SerializeField]
        private GameObject PopUp;

        private void OnEnable()
        {
            RegisterUIEvent();
        }

        private void OnDisable()
        {
            UnRegisterUIEvent();
        }

        private void OnDestroy()
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

        #region UI生命周期
		
		public Action OnShowEvent = null;
		public Action OnRefreshEvent = null;
		public Action OnCloseEvent = null;

        protected BaseData baseData = null;

        protected virtual void RegisterUIEvent()
        {
            
        }

        protected virtual void UnRegisterUIEvent()
        {
            
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