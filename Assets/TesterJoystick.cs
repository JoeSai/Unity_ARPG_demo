using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterJoystick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Horizontal"));
        //print(Input.GetAxis("Vertical"));
        print("Jright : " + Input.GetAxis("Jright"));
        print("Btn0 :" + Input.GetButtonDown("Jup"));
    }
}
