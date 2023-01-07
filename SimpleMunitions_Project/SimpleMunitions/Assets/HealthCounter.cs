using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> HeartList;

    public void UpdateHearts(int hearts)
    {
        int _hearts = hearts;

        for (int i = 0; i < HeartList.Count ; i++)
        {
            GameObject heart = HeartList[i].gameObject;
            if (i < _hearts) { heart.SetActive(true); }
            else { heart.SetActive(false); }
        }
    }
}
