using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UI.Framework;
using UnityEngine;

namespace UI.UIPanel.InvicaationCode
{
    public class InvicationMgr : Singleton<InvicationMgr>
    {
        private Dictionary<string, List<CodeData>> iCodeDic = new Dictionary<string, List<CodeData>>();
        
        private string invicationFile = "invicationFile.json";
        
        public void WriteICodeData(CodeData data)
        {
            if (data == null || string.IsNullOrEmpty(data.ResICode)) return;
            string dectyptCode = data.ResICode;
            if (iCodeDic.ContainsKey(dectyptCode))
            {
                var d = iCodeDic[dectyptCode];
                if (d != null && d.Find(item => item.Phone == data.Phone) == null)
                {
                    d.Add(data);
                    WriteICodeData(iCodeDic);
                }
            }
        }
        
        public void WriteICodeData(Dictionary<string, List<CodeData>> dataDic)
        {
            var file = JsonConvert.SerializeObject(dataDic);
            var filePath = Application.persistentDataPath + "/UserApp/" + invicationFile;
            Debug.Log(filePath);
            Debug.Log(file);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(file);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

        public Dictionary<string, List<CodeData>> ReadICodeData()
        {
            var filePath = Application.persistentDataPath + "/UserApp/" + invicationFile;
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string result = sr.ReadToEnd();
            Debug.Log(result);
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, List<CodeData>>>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CodeData> GetCodeData(string resCode)
        {
            List<CodeData> codeData = null;
            if (iCodeDic.TryGetValue(resCode, out codeData))
            {
                
            }    
            return codeData;
        }

        public Dictionary<string, List<CodeData>> GetCodeDatas()
        {
            return iCodeDic;
        }
    }
}