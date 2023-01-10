using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private float rotation = -90f;

    [SerializeField] private GameObject startWeapon;

    private List<GameObject> OwnedWeapons = new List<GameObject>();

    private void Start()
    {
        OwnedWeapons.Clear(); //Clear weapon list
        AddWeapon(startWeapon); //Add starting weapon
    }


    //Loop through weapon array and set firing

    public void SetWeaponsActive(bool isFiring)
    {
        foreach (GameObject Weapon in OwnedWeapons)
        {
            Weapon.GetComponent<WeaponShooter>().SetFiring(isFiring);
        }
    }


    //Update Weapons List
    //Add weapon then adjust weapon positions, scale and rotation relative to player.

    public void AddWeapon(GameObject weaponToAdd)
    {
        GameObject newWeapon = Instantiate(weaponToAdd, gameObject.transform); //Create new weapon gameObject with weapon container as parent.
        OwnedWeapons.Add(newWeapon); //Add new weapon to list of weapons

        rotation += 90;

        UpdateWeapons();
    }


    //Update list of weapons and distribute around player

    private float spinSpeed = 80;

    private void UpdateWeapons()
    {
        float w_seperation = 360 / OwnedWeapons.Count;  //Angle of seperation between weapons
        int iteration = 0; //Value to iterate each loop
        
        foreach (GameObject Weapon in OwnedWeapons)
        {
            float w_rotPosition = w_seperation * iteration + rotation;
            Debug.Log(w_rotPosition);

            Weapon.transform.localPosition = new Vector2(0, radius);
            Weapon.transform.RotateAround(transform.position, new Vector3(0, 0, 1), w_rotPosition);

            iteration++;
        }
    }


    //Most Likely unused but useful to reference
    public void RemoveWeapon(GameObject weaponToRemove)
    {
        foreach (GameObject Weapon in OwnedWeapons)
        {
            if(Weapon.name == weaponToRemove.name)
            {
                OwnedWeapons.Remove(Weapon);
                Destroy(Weapon);
            }
        }
    }
}
