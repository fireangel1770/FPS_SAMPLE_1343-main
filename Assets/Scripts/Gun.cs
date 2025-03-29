using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //Added

public class Gun : MonoBehaviour
{
    // references
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator anim;

    // stats
    [SerializeField] public int maxAmmo;
    [SerializeField] float timeBetweenShots = 0.1f;

    [SerializeField] UnityEvent gunUse; //Added
    public UnityEvent<int> ammoUpdate;
    [SerializeField] UnityEvent ScreenShake;
    

    // private variables
    int ammo;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public bool AttemptFire()
    {
        if (ammo <= 0)
        {
            return false;
        }

        if(elapsed < timeBetweenShots)
        {
            return false;
        }

        Debug.Log("Bang");
        Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        anim.SetTrigger("shoot");
        timeBetweenShots = 0;
        ammo -= 1;
        
        gunUse?.Invoke(); //Added
        ammoUpdate?.Invoke(ammo); //
        ScreenShake?.Invoke();
        return true;
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
            ammoUpdate?.Invoke(ammo);
        }
        else
        {
            ammoUpdate?.Invoke(ammo);
        }
    }
}
