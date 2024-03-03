using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackButtons : MonoBehaviour
{
    private bool isCooldown = false;
    private float cooldownTimer = 0.0f;

    public Image AttackImage;
    public bool doubleAttack = false;
    public bool attack = false;
    public float cooldownTime;

    

    void Start()
    {
        AttackImage.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    public void Attack()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
            attack = true;
        }
    }

    public void DoubleAttack()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
            doubleAttack = true;
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0.0f)
        {
            isCooldown = false;
            AttackImage.fillAmount = 0.0f;
        }
        else
        {
            AttackImage.fillAmount = cooldownTimer / cooldownTime;
        }
    }
}
