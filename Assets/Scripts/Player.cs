using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool isDead = false;
    private float lastAttackTime = 0;
    private float speed = 25.0f;

    public Animator AnimatorController;
    public Rigidbody rb;
    public GameObject attackButton;
    public GameObject doubleAttackButton;
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    private void Update()
    {
        Vector3 move = playerInput.Player.Move.ReadValue<Vector3>();
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        if (!isDead)
        {
            rb.MovePosition(transform.position + (move * speed * Time.deltaTime));

            for (int i = 0; i < enemies.Count; i++)
            {
                var enemie = enemies[i];
                if (enemie == null)
                {
                    continue;
                }

                if (closestEnemie == null)
                {
                    closestEnemie = enemie;
                    continue;
                }

                var distance = Vector3.Distance(transform.position, enemie.transform.position);
                var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemie = enemie;
                }
            }


            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
                if (distance <= AttackRange * 3)
                {
                    transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);
                }
                else
                {
                    transform.rotation = Quaternion.LookRotation(move);
                }
                if (distance <= AttackRange)
                {
                    if (Time.time - lastAttackTime > AtackSpeed && attackButton.GetComponent<AttackButtons>().attack)
                    {
                        lastAttackTime = Time.time;
                        closestEnemie.Hp -= Damage;
                        AnimatorController.SetTrigger("Attack");
                        attackButton.GetComponent<AttackButtons>().attack = false;
                    }
                }
                else
                {
                    if (Time.time - lastAttackTime > AtackSpeed && attackButton.GetComponent<AttackButtons>().attack)
                    {
                        lastAttackTime = Time.time;
                        AnimatorController.SetTrigger("Attack");
                        attackButton.GetComponent<AttackButtons>().attack = false;
                    }
                }
                if (distance <= AttackRange)
                {
                    doubleAttackButton.SetActive(true);
                    if (Time.time - lastAttackTime > AtackSpeed && doubleAttackButton.GetComponent<AttackButtons>().doubleAttack)
                    {
                        lastAttackTime = Time.time;
                        closestEnemie.Hp -= Damage * 2;
                        AnimatorController.SetTrigger("DoubleAttack");
                        doubleAttackButton.GetComponent<AttackButtons>().doubleAttack = false;
                    }
                }
                else
                {
                    doubleAttackButton.SetActive(false);
                }
            }
        }

        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }


}
