using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private int direction = 1;
    public float speed;
    private float speedSaved;
    public GameObject sprite;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    private bool move = true;
    public Animator animator;
    public float offset;
    public int health;

    private void Update()
    {
        if (move == true)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Move()
    {
        // The enemy's speed is set to the speed variable times direction
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        // This ensures that the sprite is facing the correct way and is in the correct place
        if (direction == 1)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            sprite.transform.localPosition = new Vector3(offset, 0.95f, 0);
        }
        if (direction == -1)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
            sprite.transform.localPosition = new Vector3(-offset, 0.95f, 0);
        }
    }

    public void ChangeDirection()
    {
        // The direction variable goes between positive and negative to change the enemy's direction
        direction *= -1;
    }

    public void SetSpeed(float randomSpeed)
    {
        // The speed variabe is set
        speed = randomSpeed;
        speedSaved = randomSpeed;
    }

    public void Die()
    {
        // Whenever the enemy is attacked it's health goes down by one
        // If the health goes to zero all the colliders are turned off and it stops moving
        health = health - 1;

        StartCoroutine(Damaged());

        if (health <= 0)
        {
            move = false;
            rb.velocity = new Vector2(0, 0);
            boxCollider1.enabled = false;
            boxCollider2.enabled = false;
            boxCollider3.enabled = false;

            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        // The death animation is played
        yield return new WaitForSeconds(0.1f);

        animator.SetBool("Dead", true);
    }

    IEnumerator Damaged()
    {
        // When damaged the enemy moves at half speed and flashes red
        speed = speed / 2;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.15f);

        speed = speedSaved;
    }

    public void Attack()
    {
        // The enemy's attack animation is played
        animator.SetBool("Attack", true);

        StartCoroutine(AttackWait());

        IEnumerator AttackWait()
        {
            yield return new WaitForSeconds(0.1f);

            animator.SetBool("Attack", false);
        }
    }
}