using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCrosshair : MonoBehaviour
{
    public GameObject Crosshair;

    private void Update()
    {
        lookAtCrosshair();
    }

    private void Awake()
    {
        Crosshair = GameObject.Find("Crosshair");
    }

    void lookAtCrosshair()  //Point towards the crosshair
    {
        float _rotateAngle = Vector2.SignedAngle(Crosshair.transform.position - transform.position, Vector2.up);
        transform.rotation = Quaternion.Euler(0, 0, -_rotateAngle);
    }
}
