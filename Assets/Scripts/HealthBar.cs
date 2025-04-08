using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image redHealthBarImage;
    public Image yellowHealthBarImage;
    public float maxHealth = 100f;
    private float currentHealth;
    private float yellowHealth;
    [SerializeField] private float yellowBarSpeed = 1f;
    [SerializeField] private float dmg = 10;
    void Start()
    {
        currentHealth = maxHealth;
        yellowHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        redHealthBarImage.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(dmg);
        }

        if (yellowHealth > currentHealth)
        {
            yellowHealth = Mathf.Lerp(yellowHealth, currentHealth, Time.deltaTime * yellowBarSpeed);

            if (Mathf.Abs(yellowHealth - currentHealth) < 0.01f)
            {
                yellowHealth = currentHealth;
            }
            yellowHealthBarImage.fillAmount = yellowHealth / maxHealth;
        }
    }

    private void TakeDamage(float val)
    {
        currentHealth -= val;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();
    }
}
