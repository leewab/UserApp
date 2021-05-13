using System.Collections.Generic;
using UI.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPanel.InvicaationCode
{
    public class UIICodeDetailPanel : BaseUI
    {
        [SerializeField] private Button btn_Close;
        [SerializeField] private InputField inp_ResCode;
        [SerializeField] private InputField inp_ICode;
        [SerializeField] private InputField inp_UserName;
        [SerializeField] private InputField inp_ResUsername;
        [SerializeField] private InputField inp_Phone;
        
        private CodeData curCodeData;
        
        public void Init(CodeData data)
        {
            if (data == null) return;
            curCodeData = data;
        }

        protected override void RegisterUIEvent()
        {
            base.RegisterUIEvent();
            btn_Close.onClick.AddListener(OnClickCloseEvent);
        }

        protected override void UnRegisterUIEvent()
        {
            base.UnRegisterUIEvent();
            btn_Close.onClick.RemoveListener(OnClickCloseEvent);
        }

        protected override void OnShow()
        {
            inp_Phone.text = curCodeData.Phone;
            inp_ICode.text = curCodeData.ICode;
            inp_ResCode.text = curCodeData.ResICode;
            inp_UserName.text = curCodeData.UserName;
            inp_ResUsername.text = curCodeData.ResUsername;
        }

        private void OnClickCloseEvent()
        {
            Debug.Log("关闭");
            Close();
        }
    }
}