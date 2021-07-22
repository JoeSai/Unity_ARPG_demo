using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private IUserInput pi;
    public float horizontalSpeed = 120.0f;
    public float verticalSpeed = 50.0f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject gameCamera;

    private Vector3 cameraDampVelocity;
    //public float t1;
    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        PaladinController pc = playerHandle.GetComponent<PaladinController>();
        model = pc.model;
        pi = pc.pi;
        gameCamera = Camera.main.gameObject;
        //cameraHandle.transform.eulerAngles = new Vector3(-30, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //tempEulerX = cameraHandle.transform.eulerAngles.x;
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -15, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        model.transform.eulerAngles = tempModelEuler;
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.fixedDeltaTime);
        
        //同位角问题？
        //cameraHandle.transform.eulerAngles = new Vector3(Mathf.Clamp(cameraHandle.transform.eulerAngles.x, -40, 30), 0, 0);
        //t1 += 0.01f * Time.fixedDeltaTime;
        //camera.transform.position = Vector3.Lerp(transform.transform.position, transform.position, t1);

        gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, transform.position, ref cameraDampVelocity, 0.2f);
        //gameCamera.transform.eulerAngles = transform.eulerAngles;
        gameCamera.transform.LookAt(cameraHandle.transform);
    }
}
