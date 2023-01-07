using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject Crosshair;

    public Rigidbody2D PlayerRB;
    public WeaponManager WeaponManager;


    public float MoveSpeed;
    public float DodgeSpeed;



    /// Unity Game Loop
    ///------------------------------------------
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        Crosshair = GameObject.Find("Crosshair");
        WeaponManager = gameObject.GetComponent<WeaponManager>();
    }

    
    private void FixedUpdate() // Fixed Update
    {
        lookAtCrosshair();
        FixedInputs();
    }

    
    private void Update()  //Update each frame
    {
        mouseInputs();
        dodgeInput();
    }
    ///------------------------------------------



    void lookAtCrosshair()  //Point towards the crosshair
    {
        float _rotateAngle = Vector2.SignedAngle(Crosshair.transform.position - transform.position, Vector2.up);
        transform.rotation = Quaternion.Euler(0, 0, -_rotateAngle);
    }

    
    void FixedInputs() //Inputs to run on FixedUpdate
    {
        PlayerRB.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * MoveSpeed);  //Get movement Input
    }

    
    void dodgeInput()  //Inputs to run on every frame
    {
        if (Input.GetButtonDown("Dodge")) { Dodge(); }  //Dodge
    }

    void Dodge()   //Dodge player in input direction
    {
        PlayerRB.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * DodgeSpeed, ForceMode2D.Impulse);
    }



    void mouseInputs()
    {
        if (!isMouseOverUI())
        {
            if (Input.GetMouseButtonDown(0)) { WeaponManager.SetWeaponsActive(true); }  //On
            else if (Input.GetMouseButtonUp(0)) { WeaponManager.SetWeaponsActive(false); } //Off
        }
        
    }

    //Check if mouse is over UI element
    private bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
