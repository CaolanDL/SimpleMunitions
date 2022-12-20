using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour
{
    [SerializeField]
    private float Timer;
    private float endTime;

    private void Start()
    {
        endTime = Time.time + Timer;
    }

    private void Update()
    {
        if(Time.time > endTime)
        {
            Destroy(gameObject);
        }
    }
}
