using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel.InvicaationCode
{
    public class UIGenerateCodePanel : MonoBehaviour
    {
        [SerializeField] private Button btn_GenerateICode;
        [SerializeField] private Transform tran_Content;
        [SerializeField] private GameObject obj_Item;
        [SerializeField] private InputField int_ResCode;

        private List<GameObject> objList = new List<GameObject>();

        public void InitPanel()
        {
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            if(btn_GenerateICode != null)btn_GenerateICode.onClick.AddListener(OnClickGenerateCodeEvent);
        }

        private void OnDisable()
        {
            if(btn_GenerateICode != null) btn_GenerateICode.onClick.RemoveListener(OnClickGenerateCodeEvent);
        }
        
        private void OnClickGenerateCodeEvent()
        {
            GenerateICodeItem();
        }
        
        private void GenerateICodeItem()
        {
            var obj = CreateItem();
            obj.SetActive(true);
            objList.Add(obj);
            var inpCode = obj.transform.Find("Txt_ICode").GetComponent<InputField>();
            var txtResCode = obj.transform.Find("Txt_ResCode").GetComponent<Text>();
            var btnCopy = obj.transform.Find("Btn_Copy").GetComponent<Button>();
            if (int_ResCode != null && inpCode != null)
            {
                inpCode.text = MD5Helper.Encrypt(int_ResCode.text);
            }

            if (txtResCode != null && int_ResCode != null)
            {
                txtResCode.text = int_ResCode.text;
            }
        
            btnCopy.onClick.AddListener(() =>
            {
            
            });
        }

        private GameObject CreateItem()
        {
            return Instantiate(obj_Item, tran_Content, false);
        }

        public void ClearPlane()
        {
            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i] != null) Destroy(objList[i]);
            }
            objList.Clear();
            gameObject.SetActive(false);
        }

    }
}