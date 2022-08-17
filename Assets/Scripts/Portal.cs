using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class Portal:MonoBehaviour
{
    public Material[] materials;
    public Transform device;
    bool wasInfront;
    bool inOtherWorld;
    bool GetIsInFront(){
        Vector3 pos = transform.InverseTransformPoint(device.position);
        return pos.z>=0;
    }
    void SetMaterials(bool fullRender){
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
        foreach(var mat in materials){
            mat.SetInt("_StencilTest" , (int)stencilTest);
        }
    }
    void Start()
    {
       SetMaterials(false);
    }
    void OnTriggerEnter(Collider other){
        if(other.transform != device )
            return;
        wasInfront = GetIsInFront();
    }
    void OnTriggerStay(Collider other){
        if(other.transform != device )
            return;
        bool isInFront = GetIsInFront();
        if((isInFront && !wasInfront) || (!isInFront && wasInfront)){
            inOtherWorld=!inOtherWorld;
            SetMaterials(inOtherWorld);
        }
        wasInfront = isInFront;
    }
    void OnDestory()
    {
        SetMaterials(true);
    }
}
