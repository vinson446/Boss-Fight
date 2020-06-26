using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;
    public Slider staminaBar;

    public Player player;

    public Text ammoText;
    public PlayerWeapon pWeapon;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = player.health;
        healthText.text = player.health.ToString();

        staminaBar.value = player.stamina;

        ammoText.text = "Ammo: " + pWeapon.currentClipSize + "/" + pWeapon.totalBullets;

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
