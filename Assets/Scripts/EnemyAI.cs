using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float followSpeed = 3f;
    [SerializeField] float distanceBetween = 1f;

    Rigidbody2D enemyRb;
    Animator enemyAnim;

    float distance;

    void Start() 
    {
        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position);

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            Vector2 position = Vector2.MoveTowards(this.transform.position, player.transform.position, followSpeed * Time.deltaTime);
            enemyRb.MovePosition(position);

            enemyAnim.SetFloat("Horizontal", direction.x);
            enemyAnim.SetFloat("Vertical", direction.y);
            enemyAnim.SetFloat("Speed", direction.sqrMagnitude);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            followSpeed = 0f;
            player.GetComponent<Player>().PlayerDeath();
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Way")
        {
            transform.SetParent(collision.transform);
        }
    }
}
