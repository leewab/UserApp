using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.UIPanel.InvicaationCode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DecryptItem : MonoBehaviour
{
    [SerializeField] private Button btn_Detail;
    [SerializeField] private Text txt_ResCode;
    [SerializeField] private Text txt_ICode;

    private CodeData iCodeData;

    public void InitItem(CodeData data)
    {
        if (data == null) return;
        iCodeData = data;
        if (txt_ResCode != null) txt_ResCode.text = data.ResICode;
        if (txt_ICode != null) txt_ICode.text = data.ICode; //MD5Helper.Decrypt(data.ResCode);
        gameObject.SetActive(true);
    }

    public void Dispose()
    {
        iCodeData = null;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(btn_Detail != null) btn_Detail.onClick.AddListener(OnClickDetailEvent);
    }

    private void OnDisable()
    {
        if (btn_Detail != null) btn_Detail.onClick.RemoveListener(OnClickDetailEvent);
    }

    private void OnClickDetailEvent()
    {
       var codeDetailPanel = UIManager.Instance.OpenUI<UIICodeDetailPanel>(); 
       codeDetailPanel.Init(iCodeData);
    }
}

public class CodeData
{
    public string UserName;
    public string ICode;                  //加密码
    public string Phone;                  //电话号码
    public string ResUsername;            //源用户 母用户名
    public string ResICode;               //源码  母用户的加密码
}
