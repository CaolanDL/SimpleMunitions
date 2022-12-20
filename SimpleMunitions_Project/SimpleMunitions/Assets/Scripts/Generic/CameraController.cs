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

    //Go to player postition at end of frame
    void LateUpdate()
    {
        //TrackToPlayer();

        Crosshair.GetComponent<CrosshairControl>().UpdateCrosshair();
    }

    public void MenuOff()
    {
        transform.DOLocalMoveX(-5, 1);
    }

    public void MenuOn()
    {
        transform.DOLocalMoveX(-19, 1);
    }
}
