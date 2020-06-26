using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public float stamina = 50f;
    public float maxStamina = 50f;
    public float loseStaminaRate = 25f;
    public float gainStaminaRate = 17.5f;

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= Time.deltaTime * loseStaminaRate;
        }

        stamina += Time.deltaTime * gainStaminaRate;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else if (stamina < -10)
        {
            stamina = -10;
        }

        if (stamina < 0)
        {
            player.m_RunSpeed = 5;
        }
        else
        {
            player.m_RunSpeed = 10;
        }

        Debug.Log(stamina);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
