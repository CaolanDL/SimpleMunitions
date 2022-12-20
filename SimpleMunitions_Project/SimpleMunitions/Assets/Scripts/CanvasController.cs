using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform menuOffset;
    [SerializeField]
    private GameObject playButton;

    private Camera rCamera;

    private void Start()
    {
        rCamera = Camera.main;
    }

    private void Update()
    {
        //MenuOn();
    }

    public void MenuOff()
    {
        playButton.SetActive(false);
        menuOffset.DOLocalMoveX(-960, 1);
    }

    public void MenuOn()
    {
        playButton.SetActive(true);
        menuOffset.DOLocalMoveX(-266, 1);
    }
}
