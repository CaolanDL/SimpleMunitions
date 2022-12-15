using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Loop through weapon array and set firing

    public GameObject[] OwnedWeapons;

    public void SetWeaponsActive(bool isFiring)
    {
        foreach (GameObject Weapon in OwnedWeapons)
        {
            Weapon.GetComponent<WeaponShooter>().SetFiring(isFiring);
        }
    }


    //Update Weapons List
    //Add weapon then adjust weapon positions, scale and rotation relative to player.
    // remove = bool  [Remove specified weapon?]

    public void UpdateWeapons(GameObject weaponToAdd, bool remove)
    {

    }
    ///------------------------------------------
}
