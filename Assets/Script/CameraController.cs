using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private IUserInput pi;
    public float horizontalSpeed = 120.0f;
    public float verticalSpeed = 50.0f;
    public Image lockDot;
    public bool lockState;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject gameCamera;

    private Vector3 cameraDampVelocity;

    private LockTarget lockTarget;
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

        lockDot.enabled = false;
        lockState = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null)
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
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }


        gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, transform.position, ref cameraDampVelocity, 0.2f);
        //gameCamera.transform.eulerAngles = transform.eulerAngles;
        gameCamera.transform.LookAt(cameraHandle.transform);
    }

    private void Update()
    {
        if(lockTarget != null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            if(Vector3.Distance(model.transform.position , lockTarget.obj.transform.position) > 10.0f)
            {
                ClearLockTarget();
            }
            if (lockTarget.am && lockTarget.am.sm.isDie)
            {
                ClearLockTarget();
            }
        }
    }

    public void ClearLockTarget()
    {
        lockTarget = null;
        lockDot.enabled = false;
        lockState = false;
    }

    public void ToggleLock()
    {

        //try to lock
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));

        if (cols.Length == 0)
        {
            ClearLockTarget();
        }
        else
        {
            foreach (var col in cols)
            {     
                if (lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    ClearLockTarget();
                    break;
                }
                lockTarget = new LockTarget(col.gameObject , col.bounds.extents.y);
                lockDot.enabled = true;
                lockState = true;
                break;
            }

        }
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public ActorManager am;

        public LockTarget(GameObject _obj , float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = _obj.GetComponent<ActorManager>();
        }
    }
}
