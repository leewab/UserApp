using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPanel.InvicaationCode
{
    public class UIRecordCodePanel : MonoBehaviour
    {
        [SerializeField] private InputField inp_Username;
        [SerializeField] private InputField inp_Phone;
        [SerializeField] private InputField inp_ICode;
        [SerializeField] private InputField inp_ResICode;
        [SerializeField] private InputField inp_ResUsername;
        [SerializeField] private Button btn_Record;

        public void InitPanel()
        {
            gameObject.SetActive(true);
        }

        public void ClearPlane()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            btn_Record.onClick.AddListener(OnClickRecordCodePanel);
        }

        private void OnDisable()
        {
            btn_Record.onClick.RemoveListener(OnClickRecordCodePanel);
        }

        private void OnClickRecordCodePanel()
        {
            CodeData data = new CodeData();
            data.UserName = inp_Username.text;
            data.Phone = inp_Phone.text;
            data.ICode = inp_ICode.text;
            data.ResICode = inp_ResICode.text;
            data.ResUsername = inp_ResUsername.text;
            InvicationMgr.Instance.WriteICodeData(data);
        }
    }
}