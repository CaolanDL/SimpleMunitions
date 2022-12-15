using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public float MouseSpeed = 0.1f;
    
    void Awake()
    {
        //Cursor.visible = false;  //Disable Mouse cursor
    }

    public void UpdateCrosshair()
    {
        SetCrosshairPosition();
        RotateAnimation(30f, 1.1f, 3f);
    }

    void SetCrosshairPosition()  //Convert Mouse to world position then move crosshair
    {
        Vector2 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MouseWorldPos;
    }

    void RotateAnimation(float rotSpeed, float bobSize, float bobSpeed) //Rotate crosshair continuously
    {
        //Rotate
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));

        //Rescale by sin
        float sizeMod = Mathf.Sin(Time.time * bobSpeed);
        sizeMod = Remap(sizeMod, 0, 1, 1, bobSize);
        Debug.Log(sizeMod);
        transform.localScale = Vector3.one * sizeMod;
    }


    //Remap Range Function
    float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
    {
        return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
    }
}
