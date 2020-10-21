using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Transform leftHand;
    public Transform rightHand;
    public Transform blade;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenleftHandNBlade = Vector3.Distance(leftHand.position ,blade.position);
        float distanceBetweenrightHandNBlade = Vector3.Distance(rightHand.position ,blade.position);
        if(Input.GetKeyDown(KeyCode.Space))//(distanceBetweenleftHandNBlade < 0.2f && distanceBetweenrightHandNBlade < 0.2f) //condition for player to lose health
        {
            TakeDamage(10);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
