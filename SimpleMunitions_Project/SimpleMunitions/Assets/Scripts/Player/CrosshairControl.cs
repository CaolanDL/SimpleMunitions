using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    public float MouseSpeed = 0.1f;

    [SerializeField]
    private float rotateSpeed = 30f, bobSpeed = 1.5f, bobSize = 1.5f;

    void Awake()
    {
        Cursor.visible = false;  //Disable Mouse cursor
    }

    void LateUpdate()
    {
        UpdateCrosshair();
    }

    public void UpdateCrosshair() //Update crosshair position
    {
        SetCrosshairPosition();
        RotateAnimation(rotateSpeed, bobSize, bobSpeed);
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
        float sizeMod = Mathf.Sin(Time.time * bobSpeed % 360);
        sizeMod = Remap(sizeMod, -1, 1, 1, bobSize); 
        transform.localScale = Vector3.one * sizeMod;
    }


    //Remap Range Function
    float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
    {
        return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
    }
}
