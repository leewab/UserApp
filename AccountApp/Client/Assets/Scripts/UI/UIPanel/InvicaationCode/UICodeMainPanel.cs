using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPanel.InvicaationCode
{
    public class UICodeMainPanel : MonoBehaviour
    {
        [SerializeField] private Button btn_Find;
        [SerializeField] private Transform tran_Content;
        [SerializeField] private GameObject obj_Item;
        [SerializeField] private InputField inp_ResCode;

        private List<DecryptItem> objList = new List<DecryptItem>();
        private Dictionary<string, List<CodeData>> curCodeDatas = new Dictionary<string, List<CodeData>>();

        private void OnEnable()
        {
            if(btn_Find != null) btn_Find.onClick.AddListener(OnClickFindCodeEvent);
        }

        private void OnDisable()
        {
            if(btn_Find != null) btn_Find.onClick.RemoveListener(OnClickFindCodeEvent);
        }

        public void InitPanel()
        {
            gameObject.SetActive(true);
            curCodeDatas.Clear();
            curCodeDatas = InvicationMgr.Instance.GetCodeDatas();
            GenerateICodeItem();
        }
        
        private void GenerateICodeItem()
        {
            foreach (var keyValuePair in curCodeDatas)
            {
                var datas = keyValuePair.Value;
                if (datas == null || datas.Count <= 0) continue;
                for (int i = 0; i < datas.Count; i++)
                {
                    DecryptItem item = null;
                    if (objList.Count > i)
                    {
                        item = objList[i];
                    }
                    else
                    {
                        item = CreateItem();
                        objList.Add(item);
                    }
                    
                    if (item != null) item.InitItem(datas[i]);
                }
            }
        }

        private void OnClickFindCodeEvent()
        {
            if (string.IsNullOrEmpty(inp_ResCode.text) || inp_ResCode.text.Equals(" ")) return;
            curCodeDatas.Clear();
            curCodeDatas.Add(inp_ResCode.text, InvicationMgr.Instance.GetCodeData(inp_ResCode.text));
            GenerateICodeItem();
        }
        
        private DecryptItem CreateItem()
        {
            return Instantiate(obj_Item, tran_Content, false).GetComponent<DecryptItem>();
        }

        public void ClearPlane()
        {
            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i] != null) objList[i].Dispose();
            }
            gameObject.SetActive(false);
        }
    }
}