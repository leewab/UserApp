using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProtocolUserData userData = new ProtocolUserData()
        {
            UserDatas = new List<UserData>()
            {
                new UserData()
                {
                    Username = "000",
                    Password = "999",
                    IdCard = 88,
                    PhoneNo = 7654
                }
            },
        };
        
        Protocol protocol = new Protocol
        {
            Data = userData,
            Id = 90983872
        };

        var str = JsonConvert.SerializeObject(protocol);
        Debug.Log(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
