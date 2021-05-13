using System.Collections;
using System.Collections.Generic;
using UI.UIPanel.InvicaationCode;
using UnityEngine;
using UnityEngine.UI;

public class UIDecryptWritePanel : MonoBehaviour
{
    [SerializeField] private InputField int_Username;
    [SerializeField] private InputField int_Phone;
    [SerializeField] private InputField int_ICode;
    [SerializeField] private InputField int_ResCode;
    [SerializeField] private InputField int_ResUsername;
    [SerializeField] private Button btnSubmit;

    private void OnEnable()
    {
        if(btnSubmit != null) btnSubmit.onClick.AddListener(OnClickSubmitEvent);
    }

    private void OnDisable()
    {
        if(btnSubmit != null) btnSubmit.onClick.RemoveListener(OnClickSubmitEvent);
    }

    private void OnClickSubmitEvent()
    {
        CodeData codeData = new CodeData();
        if (int_Username != null) codeData.UserName = int_Username.text;
        if (int_ResUsername != null) codeData.ResUsername = int_ResUsername.text;
        if (int_ICode != null) codeData.ICode = int_ICode.text;
        if (int_ResCode != null) codeData.ResICode = int_ResCode.text;
        if (int_Phone != null) codeData.Phone = int_Phone.text;
        
        InvicationMgr.Instance.WriteICodeData(codeData);
    }
}
