using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] int HealthAmount;
    [SerializeField] int MaxHealth;
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text maxAmmoText;
    [SerializeField] Image DamageIndicator;

    [SerializeField] TMP_Text InteractPrompt;

    FPSController player;
     
    public UnityAction<int> OnHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
        var gunCode = FindObjectOfType<Gun>();
        var ammo = gunCode.maxAmmo;
        maxAmmoText.text = ammo.ToString();
        currentAmmoText.text = ammo.ToString();

    }
    private void Update()
    {
        if (DamageIndicator.color.a > 0) {
            var tempColor = DamageIndicator.color;
            tempColor.a -= 0.05f;
            DamageIndicator.color = tempColor;
        }
    }
    public void DamageDone()
    {
        if (HealthAmount <= MaxHealth && HealthAmount > 0)
        {
            HealthAmount += -10;
            DamageShow();
            UpdateHealthImage(HealthAmount);
        }
        Debug.Log("HP problem");
    }
    public void InteractShow()
    {
        InteractPrompt.gameObject.SetActive(true);


    }
    public void InteractHide()
    {
        InteractPrompt.gameObject.SetActive(false);
    }
    void UpdateHealthImage(int newCurrentHealth)
    {
        healthBar.fillAmount = (float)newCurrentHealth / 100;
    }
    void DamageShow()
    {
        var tempColor = DamageIndicator.color;
        tempColor.a = 0.5f;
        DamageIndicator.color = tempColor;
    }
    public void AmmoAmountShow(int ammo)
    {

        currentAmmoText.text = ammo.ToString();
    }
 
}
