using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackRate = 0.5f;
    public float maxComboDelay = 2f;
    float lastComboTime = 0;
    int comboNumber = 0;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public HealthBar healthBar;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    float nextAttackTime = 0f;

    public float healthPoints = 300f;
    public float currentHealthPoints;
    Animator animator;


    void Start() 
    {
        animator = GetComponent<Animator>();
        currentHealthPoints = healthPoints;
        healthBar.SetMaxHealth(healthPoints);

    }

    void Update() 
    {


        RaycastHit2D enemyIsLeft = Physics2D.Raycast(transform.position, Vector2.left, 15, LayerMask.GetMask("Enemy"));
        RaycastHit2D enemyIsRight = Physics2D.Raycast(transform.position, Vector2.right, 15, LayerMask.GetMask("Enemy"));

        healthBar.SetHealth(currentHealthPoints);
        if(Input.GetMouseButtonDown(0) && Time.time > nextAttackTime)
        {
            if(enemyIsLeft.collider!=null && enemyIsRight.collider == null)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }else if(enemyIsLeft.collider == null && enemyIsRight.collider != null)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            Attack();
            nextAttackTime = Time.time + attackRate;
        }


        if(comboNumber >= 3 || Time.time - lastComboTime > maxComboDelay)
        {
            animator.SetBool("Slash1", false);
            animator.SetBool("Slash2", false);
            animator.SetBool("Slash3", false);
            comboNumber = 0;
        }
        
        if(Input.GetMouseButtonDown(0) && comboNumber == 0 && Time.time - lastComboTime > attackRate)
        {
            
            comboNumber++;
            animator.SetBool("Slash3", false);
            animator.SetBool("ComboDone", false);
            animator.SetBool("Slash1", true);
            lastComboTime = Time.time;
        }

        if(Input.GetMouseButtonDown(0) && comboNumber == 1 && Time.time - lastComboTime > attackRate)
        {
            comboNumber++;
            animator.SetBool("Slash1", false);
            animator.SetBool("Slash2", true);
            lastComboTime = Time.time;
        }

        if(Input.GetMouseButtonDown(0) && comboNumber == 2 && Time.time - lastComboTime > attackRate)
        {
            comboNumber=0;
            animator.SetBool("Slash2", false);
            animator.SetBool("Slash3", true);
            animator.SetBool("ComboDone", true);
            lastComboTime = Time.time;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("IsHurt");
        currentHealthPoints -= damage;
        healthBar.SetHealth(currentHealthPoints);
        if(currentHealthPoints <=0 )
        {
            Die();
        }
    }

    public void Die()
    {

    }



    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
