using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatVFX : MonoBehaviour
{
    public float attackRate = 0.5f;
    public float maxComboDelay = 2f;
    float lastComboTime = 0;
    int comboNumber = 0;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    float nextAttackTime = 0f;

    Animator animator;


    void Start() 
    {
        animator = GetComponent<Animator>();

    }

    void Update() 
    {
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
}
