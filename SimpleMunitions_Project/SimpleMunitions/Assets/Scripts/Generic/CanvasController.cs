using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;

public class CanvasController : MonoBehaviour
{

    [SerializeField] private RectTransform uiOffset;
    [SerializeField] private RectTransform shopOffset;
    [SerializeField] private RectTransform headerOffset;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject heartButton;
    [SerializeField] private GameObject pointCounter;
    [SerializeField] private GameObject waveCounter;
    [SerializeField] private GameObject shopScroller;
    [SerializeField] private GameObject DeadText;

    private GameManager gameManager;

    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiOffset.DOLocalMoveX(-414, 1);
    }


    public void MenuOff()
    {
        menuMode(false);
        headerOffset.DOLocalMoveX(-580, 1);
        shopOffset.DOLocalMoveY(-1000, 1.2f); 
    }

    public void MenuOn()
    {
        menuMode(true);
        headerOffset.DOLocalMoveX(0, 1);
        shopOffset.DOLocalMoveY(-100, 1.2f);
    }

    //Set the Text for Points Counter UI
    public void setPoints(int points)
    {
        pointCounter.GetComponent<TextMeshProUGUI>().text = points.ToString();
        pointCounter.transform.Find("f_AddPoints").GetComponent<MMF_Player>().PlayFeedbacks();
    }

    //Set the Text for Wave Counter UI
    public void setWave(int wave)
    {
        waveCounter.GetComponent<TextMeshProUGUI>().text = "Wave: " + wave.ToString();
    }

    public void purchaseHeart()
    {
        gameManager.PurchaseHeart();
    }

    public void purchaseWeapon(GameObject weapon)
    {
        gameManager.PurchaseWeapon(weapon);
    }

    public void updateHeartCost()
    {
        heartButton.transform.Find("BuyHeart_Text").GetComponent<TextMeshProUGUI>().text = gameManager.HeartCost.ToString();
    }

    public void updateWeaponCost()
    {
        Component[] textfields = shopScroller.transform.GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI text in textfields)
        {
            text.text = gameManager.WeaponCost.ToString();
        }
    }


    void menuMode(bool enabled)
    {
        playButton.SetActive(enabled);
        heartButton.SetActive(enabled);
    }


    [SerializeField] private AudioClip sound_MenuDenied;

    public void menuDeniedEffect()
    {
        audioSource.PlayOneShot(sound_MenuDenied);
    }


    public void DeadTextActive(bool isActive)
    {
        DeadText.SetActive(isActive);
    }
}
