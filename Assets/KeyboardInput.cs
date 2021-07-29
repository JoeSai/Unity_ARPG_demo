using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("===== keys settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string KeyRun;
    public string KeyJump;
    public string KeyAttack;
    public string KeyDefense;
    public string KeyLockon;

    public string KeyJRight;
    public string KeyJLeft;
    public string KeyJUp;
    public string KeyJDown;

    [Header("===== Mouser settings =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    //public MyButton ButtonRun = new MyButton();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ButtonRun.Tick(Input.GetKey(KeyRun));

        if (mouseEnable)
        {
            Jup = Input.GetAxis("Mouse Y") * 3f *  mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(KeyJUp) ? 1.0f : 0) - (Input.GetKey(KeyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(KeyJRight) ? 1.0f : 0) - (Input.GetKey(KeyJLeft) ? 1.0f : 0);
        }

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (!inputEnable)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(KeyRun);

        jump = Input.GetKeyDown(KeyJump);

        attackR = Input.GetKeyDown(KeyAttack);
        attackL = Input.GetKeyDown(KeyDefense);

        defense = Input.GetKey(KeyDefense);

        lockon = Input.GetKeyDown(KeyLockon);
        //roll = 

        //print(ButtonRun.OnPressed && ButtonRun.IsExtending);
        //print(ButtonRun.lastPressTime + " ----- " + ButtonRun.DoubleClick);

    }

    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;
    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
    //    return output;
    //}
}
