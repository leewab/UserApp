using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel
{
    public class AccountItem : MonoBehaviour
    {
        [SerializeField] private Text txt_WebName;
        [SerializeField] private Text txt_WebUrl;
        [SerializeField] private TextSizeFitter txtC_Encipherment;
        [SerializeField] private Button btn_Item;
        [SerializeField] private Button btn_Delete;

        private RectTransform rectTrans = null;
        private Vector2 vec2_Max = new Vector2(990, 250);
        private Vector2 vec2_Min = new Vector2(990, 100);
        private Vector3 vec3_Offest = new Vector3(0, 80, 0);
        private bool isShowSecret = false;
        private Action<bool> onCalBackShowSercet;
        private Action onCallBackDelete;
        private AccountData accountData;
        
        public void InitItem(AccoutItemInfo info)
        {
            accountData = info.data;
            onCallBackDelete = info.callBackDelete;
            onCalBackShowSercet = info.callBackShowSecret;
            rectTrans = GetComponent<RectTransform>();
            txtC_Encipherment.gameObject.SetActive(false);
            txt_WebName.text = accountData.Webname;
            txt_WebUrl.text = accountData.WebURL;
            txtC_Encipherment.SetContent($"WebUsername: {accountData.WebUsername}\n" +
                                         $"WebPassword: {accountData.WebPassword}\n" +
                                         $"WebRemark: {accountData.WebRemark}");
        }

        private void OnEnable()
        {
            btn_Item.onClick.AddListener(OnClickItemEvent);
            btn_Delete.onClick.AddListener(OnClickDeleteEvent);
        }

        private void OnDisable()
        {
            btn_Item.onClick.RemoveListener(OnClickItemEvent);
            btn_Delete.onClick.RemoveListener(OnClickDeleteEvent);
        }

        private void OnClickItemEvent()
        {
            if (isShowSecret)
            {
                HideSectet();
                isShowSecret = false;
            }
            else
            {
                ShowSecret();
                isShowSecret = true;
            }
        }

        private void OnClickDeleteEvent()
        {
            DataManager.Instance.RemoveAccoutnSingleData(accountData);
            onCallBackDelete?.Invoke();
            Destroy(gameObject);
        }
        
        private void ShowSecret()
        {
            rectTrans.sizeDelta = vec2_Max;
            onCalBackShowSercet?.Invoke(true);
            txtC_Encipherment.gameObject.SetActive(true);
            txtC_Encipherment.transform.DOShakePosition(0.2f, vec3_Offest);
        }

        private void HideSectet()
        {
            txtC_Encipherment.transform
                .DOShakePosition(0.2f, vec3_Offest)
                .OnComplete(() =>
                {
                    txtC_Encipherment.gameObject.SetActive(false);
                    rectTrans.sizeDelta = vec2_Min;
                    onCalBackShowSercet?.Invoke(false);
                });
        }
        
    }

    public class AccoutItemInfo
    {
        public AccountData data;
        public Action<bool> callBackShowSecret;
        public Action callBackDelete;
    }
}