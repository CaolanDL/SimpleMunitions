using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The game starts (Menu > StartGame)
    /// The shop opens and player buys a starting weapon
    /// </summary>

    public bool GameActive; //GameActive indicates if in menu or in game
    public bool ShopActive; //Shopmode is for purchasing player upgrades
    public bool WaveSpawnActive; //Should enemy waves be spawning

    public int CurrentWave; //Current enemy wave

    public int Points = 100; //Points which can be spent in the shop


    [SerializeField]
    private GameObject gameWorld;
    private SpawnManager spawnManager;
    [SerializeField]
    private Camera mainCamera;
    private CameraController camera_Ctrl;
    [SerializeField]
    private Canvas ui_Canvas;
    private CanvasController ui_Ctrl;

    private void Start()
    {
        ui_Ctrl = ui_Canvas.GetComponent<CanvasController>();
        camera_Ctrl = mainCamera.GetComponent<CameraController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        GotoDefault();
    }

    private void Update()
    {

    }

    public void GotoDefault()
    {
        ui_Ctrl.MenuOff();
        camera_Ctrl.MenuOff();
        spawnManager.startNewWave();

        CurrentWave++;
        ShopActive = false;
    }

    public void GotoShop()
    {
        ui_Ctrl.MenuOn();
        camera_Ctrl.MenuOn();

        ShopActive = true;
    }
}
