using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            MyNotifications.CallNotification("New Quest", 3);
        }
    }
}
