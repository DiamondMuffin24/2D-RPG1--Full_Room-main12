using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Attack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    [SerializeField] private AudioSource Slash;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                Slash.Play();
                AttackP();
                nextAttackTime = Time.time + 1f / attackRate;




            }
        }
        
    }

        void AttackP()
        {
            //Attack animation
            animator.SetTrigger("Attack");

            //Detect enemies in range of Attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
            
            //Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

     private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;
            Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        }
    
}
