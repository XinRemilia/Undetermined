using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    //public GameObject bulletPrefabe;
    public bool isAttack = false;
    //public float interval = 0.5f;
    public float resetAttack;

    public cItem currWeapon;
    void Start()
    {
        resetAttack = currWeapon.coolDownTime;
    }

    void FixedUpdate()
    {
        isAttack = Input.GetButton("Attack");
      
        resetAttack += Time.fixedDeltaTime;
    }
    void Update()
    {
        if (isAttack && resetAttack <= currWeapon.coolDownTime && currWeapon.itemHeldNumber > 0)
        {
            Instantiate(currWeapon.itemPrefabe, firePoint.position, firePoint.rotation);
            currWeapon.itemHeldNumber--;
            cInventoryManager.RefreshItem();
            resetAttack = 0;
        }

     
    }
}
