using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowActivePlayer : MonoBehaviour
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
        Vector3 PlayerPosition = ActivePlayer.transform.position;
        transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y, transform.position.z);

        Crosshair.GetComponent<CrosshairControl>().UpdateCrosshair();
    }
}
