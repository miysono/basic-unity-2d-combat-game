using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    //Basic movement
    public float distance = 1.5f;
    public float speed = 1f;
    public float speedMultiplier = 3f;
    public bool movingRight = true;
    public bool flip = false;

    //Player Detection
    public bool playerInRange = false;
    bool playerInAttackRange = false;
    public float detectionRadius = 5f;

    //Health logic
    public float healthPoints = 150f;
    public float currentHealthPoints;

    //Attack logic
    public int attackDamage = 10;
    public float attackRange = 3f;
    public float attackRate = 2f;
    float lastAttackedTime = 0f;

    //Drags
    public Transform attackPoint;
    public Transform wallDetection;
    public Transform player;
    public GameObject[] itemDrop;



    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealthPoints = healthPoints;
    }

    void Update()
    {
        
        playerInRange = Physics2D.OverlapCircle(transform.position,detectionRadius, LayerMask.GetMask("Player"));
        playerInAttackRange = Physics2D.OverlapCircle(transform.position,attackRange, LayerMask.GetMask("Player"));
        if(playerInRange)
        {   
            if(!playerInAttackRange){
                animator.SetBool("IsWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * speedMultiplier * Time.deltaTime);
                Vector3 lastRotation = transform.eulerAngles;
                RaycastHit2D playerIsRight = Physics2D.Raycast(transform.position, Vector2.right, 15, LayerMask.GetMask("Player"));
                RaycastHit2D playerIsLeft = Physics2D.Raycast(transform.position, Vector2.left, 15, LayerMask.GetMask("Player"));
                if(playerIsRight.collider == null && playerIsLeft.collider !=null)
                    transform.eulerAngles = new Vector3 (0, -180, 0);
                        else if(playerIsRight.collider != null && playerIsLeft.collider ==null) transform.eulerAngles = new Vector3(0, 0, 0);
                            else transform.eulerAngles = lastRotation;
            }

            else if(playerInAttackRange){
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", false);
                if(Time.time - lastAttackedTime > attackRate)
                {
                animator.SetBool("IsAttacking", true);
                //Attack logic
                Attack();
                //Aniamtor logic
                Debug.Log("I attacked");
                lastAttackedTime = Time.time;


                }
                
            }
        }
        if(!playerInRange)
        {
            animator.SetBool("IsWalking", true);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, distance, LayerMask.GetMask("Wall"));
            RaycastHit2D platformInfo = Physics2D.Raycast(wallDetection.position, Vector2.down, distance, LayerMask.GetMask("Platform"));
            if(wallInfo.collider != null || platformInfo.collider != null)
            {
                if(movingRight){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                }
                else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                }
            }
        }
    }



    //Attack the player
    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Player"));
        foreach(Collider2D player in hitPlayer)
        {
            player.GetComponent<Combat>().TakeDamage(attackDamage);
        }
    }

    //Take damage from player
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("IsHurt");
        currentHealthPoints -= damage;
        if(currentHealthPoints <=0 )
        {
            Die();
            ItemDrop();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
    }

    void ItemDrop()
    {
        for(int i=1; i<=Random.Range(3,10); i++)
        {
            Instantiate(itemDrop[i], transform.position + new Vector3(Random.Range(0.1f, 1f), Random.Range(0f, 4f), 0), Quaternion.identity);

        }
    }

    //Draw radius
    void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}