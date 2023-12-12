using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private float speed;
    private GameObject player;
    private float distance;
    private float offset;
    private Level1Manager levelManager;
    private GameObject redEnemy;
    private float separationOffset;
    private float impulseForce;
    void Start()
    {
        //setting level manager and offsets from the level manager
        levelManager = FindObjectOfType<Level1Manager>();
        if (levelManager == null)
        {
            Debug.LogError("Level1Manager not found in the scene!");
        }

        if(gameObject.CompareTag("redEnemy"))
        {
            speed=levelManager.redSpeed;
            offset=levelManager.redOffset;
        }
        else
        {
            speed=levelManager.greenSpeed;
            offset=levelManager.greenOffset;
        }
    
        player=levelManager.player;
        redEnemy=levelManager.redEnemy;
        separationOffset=levelManager.separationOffset;
        impulseForce=levelManager.impulseForce;
    }

    void Update()
    {
        distance =Vector2.Distance(transform. position, player.transform.position);
        Vector2 direction = player.transform.position - transform. position;
        direction. Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(distance < offset)
        {
            transform. position = Vector2.MoveTowards(this.transform. position, player. transform. position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward*angle);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet") && gameObject.CompareTag("redEnemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); 
        }
        else if(collision.gameObject.CompareTag("bullet") && gameObject.CompareTag("greenEnemy"))
        {
            Destroy(collision.gameObject); 
            gameObject.SetActive(false);
            Vector2 direction= new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            Vector2 orthogonal = new Vector2(-direction.y, direction.x);
            orthogonal.Normalize();
            SpawnEnemyWithOffset(orthogonal,direction);
            SpawnEnemyWithOffset(-orthogonal,direction);
            Destroy(gameObject); 
        }
             
    }
    void SpawnEnemyWithOffset(Vector2 offsetDirection,Vector2 direction)
    {
        Vector3 spawnPosition = transform.position + new Vector3(offsetDirection.x, offsetDirection.y, 0) * separationOffset;
        GameObject newEnemy = Instantiate(redEnemy, spawnPosition, Quaternion.identity);
        Vector2 impulseDirection = (5*offsetDirection + direction).normalized;
        Rigidbody2D enemyRb = newEnemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
             enemyRb.AddForce(impulseDirection * impulseForce, ForceMode2D.Impulse);
        }
    }

}
