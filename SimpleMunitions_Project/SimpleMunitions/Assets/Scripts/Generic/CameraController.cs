using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject ActivePlayer;
    public GameObject Crosshair;

    private void Start()
    {
        Crosshair = GameObject.Find("Crosshair");
    }

    //Goto Menu Off Position
    public void MenuOff()
    {
        transform.DOLocalMoveX(-5, 1);
    }

    //Goto Menu On position
    public void MenuOn()
    {
        transform.DOLocalMoveX(-19, 1);
    }
}
