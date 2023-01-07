using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The game starts (Menu > StartGame)
    /// The shop opens and player buys a starting weapon
    /// </summary>

    #region Public Misc
    [Header("Public Misc")]

    public bool ShopActive; //Shopmode is for purchasing player upgrades

    public int CurrentWave; //Current enemy wave

    public int Points = 100; //Points which can be spent in the shop

    public int Hearts = 8; //Hearts !!Editme
    private int MaxHearts = 8;

    #endregion

    #region Shop Costs
    [Header("Shop Costs")]

    #region Heart Button
    [SerializeField, Tooltip("Starting Heart Cost")] 
    private int heartCostDefault = 300; 
    [SerializeField, Tooltip("How much should heart cost change each purchase?")] 
    private float heartCostChange = 1.5f; 
    [HideInInspector] public int HeartCost;
    #endregion

    #region Weapon Button
    [SerializeField, Tooltip("Starting Weapon Cost")] 
    private int weaponCostDefault = 100;
    [SerializeField, Tooltip("How much should Weapon cost change each purchase?")] 
    private int weaponCostChange = 2; 
    [HideInInspector] public int WeaponCost;
    #endregion

    #endregion

    #region Game Objects
    [Header ("GameObjects")]

    [SerializeField] private GameObject gameWorld;
    private SpawnManager spawnManager;
    [SerializeField] private GameObject mainCamera;
    private CameraController camera_Ctrl;
    [SerializeField] private Canvas ui_Canvas;
    public CanvasController ui_Ctrl;
    [SerializeField] private GameObject player;
    #endregion

    #region FeedBacks
    [Header ("FeedBacks")]

    public MMFeedbacks f_RoundEnd;
    public MMFeedbacks f_RoundStart;
    public MMFeedbacks f_ShakeSmall;
    public MMFeedbacks f_ShakeLarge;

    #endregion

    #region SoundEffects
    [Header("SoundEffects"), SerializeField] private AudioClip sound_RoundEnd;
    private AudioSource audioSource;
    #endregion


    private void Start()
    {
        #region
        audioSource = GetComponent<AudioSource>();
        #endregion

        ui_Ctrl = ui_Canvas.GetComponent<CanvasController>();
        camera_Ctrl = mainCamera.GetComponent<CameraController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();

        StartNewWave();
        StartGame();
    }

    public void StartGame()
    {
        setHearts(MaxHearts);
        setHeartCost(heartCostDefault);
        setWeaponCost(weaponCostDefault);

        ui_Ctrl.updateHeartCost();
        ui_Ctrl.updateWeaponCost();
    }


    public void StartNewWave()
    {
        f_RoundStart.PlayFeedbacks();

        //Disable the menu
        ui_Ctrl.MenuOff();
        camera_Ctrl.MenuOff();

        spawnManager.startNewWave(); //Start a new wave

        //Set the wave and update UI
        CurrentWave++;
        ui_Ctrl.setWave(CurrentWave);

        ShopActive = false;

        UpdateHearts(Hearts);
    }

    public void GotoShop()
    {
        // Add Camera Feel here  !!!
        f_RoundEnd.PlayFeedbacks();

        // Start Delay
        Sequence wait = DOTween.Sequence();
        wait.AppendInterval(f_RoundEnd.TotalDuration * 0.8f); // Insert Delay

        // Enable the menu
        wait.AppendCallback(() => ui_Ctrl.MenuOn());
        wait.AppendCallback(() => camera_Ctrl.MenuOn());

        ShopActive = true;
    }

    public void UpdatePoints(int pointChange)
    {
        Points += pointChange;
        ui_Ctrl.setPoints(Points);
    }

    bool tryPurchase(int cost)
    {
        if (Points - cost <= 0) //Not enough money, fail purchase
        {
            ui_Ctrl.menuDeniedEffect();
            return false;
        }

        //Make Price Adjustment
        UpdatePoints(-cost);
        return true;
    }


    //Heart Purchase Controller
    #region Buy Hearts 
    public void PurchaseHeart()
    {
        if (Hearts < MaxHearts && tryPurchase(HeartCost)) 
        {
            Hearts++;
            HeartCost = (int)(HeartCost * heartCostChange);
            UpdateHearts(Hearts);
        }
    }
    public void setHearts(int nhearts)
    { Hearts = nhearts; 
        UpdateHearts(Hearts); }
    public void setHeartCost(int heartCost)
    { HeartCost = heartCost; 
        UpdateHearts(Hearts); }
    void UpdateHearts(int hearts)
    {
        ui_Ctrl.GetComponent<HealthCounter>().UpdateHearts(hearts);
        ui_Ctrl.updateHeartCost();
    }
    #endregion

    //Weapon Purchase Controller
    #region Buy Weapons
    public void PurchaseWeapon(GameObject weapon)
    {
        if (tryPurchase(WeaponCost))
        {
            WeaponCost *= weaponCostChange;
            player.GetComponent<WeaponManager>().AddWeapon(weapon);

            ui_Ctrl.updateWeaponCost();
        }
    }
    public void setWeaponCost(int weaponCost)
    { WeaponCost = weaponCost; }

    #endregion


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
