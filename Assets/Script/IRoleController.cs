using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IRoleController : MonoBehaviour
{
    public GameObject model;
    public Animator anim;
    public IUserInput pi;
    public CameraController camcon;

    public delegate void OnActionDelegaet();
    public event OnActionDelegaet OnAction;

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string boolName , bool value)
    {
        anim.SetBool(boolName, value);
    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }

    public void InvokeAction()
    {
        OnAction.Invoke();
    }
}
