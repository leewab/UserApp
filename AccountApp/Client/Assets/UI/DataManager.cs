//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Text;
//using Framework.Network.HttpMoudle;
//using Newtonsoft.Json;
//using UI.Framework;
//using UI.UIPanel;
//using UnityEngine;
//
//namespace UI
//{
//    public class DataManager : Singleton<DataManager>
//    {
//        public enum UserLoginState
//        {
//            SUCCESS,            //登录成功
//            UN_REPETITION,      //用户名重复
//            UN_EMPTY,           //用户名不存在
//            PSD_INVALID,        //密码错误
//        }
//        
//        private string userDataPath = Application.dataPath + "/Resources/Data/";
//        
//        /// <summary>
//        /// 数据存储
//        /// </summary>
//        private Dictionary<string, UserData> userDatas = null;
//
//        public Dictionary<string, UserData> UserDatas
//        {
//            get
//            {
//                if (userDatas == null)
//                {
//                    Debug.Log("没有数据 进行读取");
//                    userDatas = LoadAllUserData();
//                }
//
//                return userDatas ?? (userDatas = new Dictionary<string, UserData>());
//            }
//        }
//        
//        public void SetUserData(UserData userdata)
//        {
//            if (!UserDatas.ContainsKey(userdata.Username))
//            {
//                userDatas.Add(userdata.Username, userdata);
//            }
//        }                                                          
//        
//        public UserData GetUserData()
//        {
//            UserData userData = null;
//            if (UserDatas.TryGetValue(CurUsername, out userData))
//            {
////                Debug.Log(userData.AccountDatas.Count);
//            }
//
//            return userData;
//        }
//        
//        /// <summary>
//        /// 当前用户名
//        /// </summary>
//        private string curUsername = null;
//
//        public string CurUsername
//        {
//            get => curUsername;
//            set => curUsername = value;
//        }
//
//        public bool HasUser(string name)
//        {
//            return UserDatas.TryGetValue(name, out _);
//        }
//
//        public UserLoginState IsLoginSuccess(string username, string password)
//        {
//            if (!UserDatas.TryGetValue(username, out var user)) return UserLoginState.UN_EMPTY;
//            return user.Password.Equals(password) ? UserLoginState.SUCCESS : UserLoginState.PSD_INVALID;
//        }
//        
//        private Dictionary<string, UserData> LoadAllUserData()
//        {
//            var ta = Resources.Load<TextAsset>("Data/userdata");
//            if (ta == null) return null;
//            try
//            {
//                return JsonConvert.DeserializeObject<Dictionary<string, UserData>>(ta.text);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
//        }
//
//        public bool SaveUserData()
//        {
//            var file = userDataPath + "userdata.json";
//            Debug.Log(userDataPath);
//            Debug.Log(file);
//            string text = JsonConvert.SerializeObject(UserDatas);
//            Debug.Log(text);
//            FileStream fs = new FileStream(file, FileMode.Create);
//            StreamWriter sw = new StreamWriter(fs);
//            sw.WriteLine(text);
//            sw.Close();
//            sw.Dispose();
//            fs.Close();
//            fs.Dispose();
//            return true;
//        }
//        
//        public void AddAccountSingleData(AccountData data)
//        {
//            var userData = GetUserData();
//            userData.AccountDatas.Add(data);
//            SaveUserData();
//        }
//
//        public void RemoveAccoutnSingleData(AccountData data)
//        {
//            var userData = GetUserData();
//            userData.AccountDatas.Remove(data);
//            SaveUserData();
//        }
//    }
//}