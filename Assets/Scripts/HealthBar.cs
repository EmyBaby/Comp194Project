using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    int currentHealth;
    float timer;
    bool timerOn;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    // Start is called before the first frame update
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void Start()
    {
        currentHealth = 100;
        SetMaxHealth(currentHealth);
        SetHealth(currentHealth);
    }

    void Update()
    {
        SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            timerOn = true;
        }
    }

    void FixedUpdate()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Damage(int a)
    {
        currentHealth -= a;
    }
}
