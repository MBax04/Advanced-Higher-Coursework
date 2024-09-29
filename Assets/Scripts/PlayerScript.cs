using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Other")]
    private Rigidbody2D rb;

    [Header("Movement")]
    public float speed;
    public float maxSpeed;
    private float speedToMaxSpeed;
    private float accelerationRate;
    private float moveSpeed;

    public float acceleration;
    public float decceleration;

    public float frictionAmount;
    private float friction;

    public Animator animator;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCutForce;
    private bool jumping;

    private float lastTimeOnGround;
    private float lastTimeJumping;
    public float coyateTime;
    public float jumpBufferTime;

    public float fallGravityMultiplier;
    private float gravity;

    [Header("Checks")]
    public Vector2 groundCheckerSize;
    public LayerMask groundLayer;
    public GameObject groundChecker;

    [Header("Scene Management")]
    private int level;
    private Scene scene;

    [Header("Attack")]
    public LayerMask enemyLayer;
    public Transform attackPos;
    private Collider2D[] EnemyColliders;
    public float attackRange;
    private int i;
    private bool attacking = false;

    private bool damageable = true;
    public int health;
    private bool canMove = true;
    public GameObject sprite;
    public SpriteRenderer spriteRenderer;
    private float time;
    private float startTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    private void Update()
    {
        Checks();

        if (lastTimeOnGround > 0 && jumping == false && Input.GetKeyDown(KeyCode.Space) && canMove == true)
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) && jumping == true && canMove == true)
        {
            JumpOff();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        if (Input.GetMouseButtonDown(0) && attacking == false && canMove == true)
        {
            Attack();
        }

        speed = Mathf.Abs(rb.velocity.x);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && canMove == true)
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D) && canMove == true)
        {
            MoveRight();
        }
        else
        {
            Stop();
        }

        Gravity();
        Friction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Threat"))
        {
            Death();
        }

        if (collision.gameObject.CompareTag("FinishItem"))
        {
            NextLevel1();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damaged(collision);
            startTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If the player stays inside an enemy for too long the player dies
            time = Time.time - startTime;
            if (time >= 0.5)
            {
                Death();
            }
        }
    }

    private void Checks()
    {
        // These are checks that are constantly being done and updated
        if (Physics2D.OverlapBox(groundChecker.transform.position, groundCheckerSize, 0, groundLayer))
        {
            lastTimeOnGround = coyateTime;
        }

        if (rb.velocity.y < 0)
        {
            jumping = false;
        }

        lastTimeOnGround -= Time.deltaTime;
        lastTimeJumping -= Time.deltaTime;

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (jumping == true)
        {
            animator.SetBool("JumpUp", true);
        }
        else
        {
            animator.SetBool("JumpUp", false);
        }
    }

    private void MoveLeft()
    {
        // A force is applied left and the force changes depnding on the current speed to give a smoother acceleration
        if (attacking == false)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        speedToMaxSpeed = -maxSpeed - rb.velocity.x;
        if (rb.velocity.x <= 0)
        {
            accelerationRate = acceleration;
        }
        else
        {
            accelerationRate = decceleration;
        }
        moveSpeed = -speedToMaxSpeed * accelerationRate;

        rb.AddForce(-moveSpeed * Vector2.right);
    }

    private void MoveRight()
    {
        // A force is applied right and the force changes depnding on the current speed to give a smoother acceleration
        if (attacking == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        speedToMaxSpeed = maxSpeed - rb.velocity.x;
        if (rb.velocity.x >= 0)
        {
            accelerationRate = acceleration;
        }
        else
        {
            accelerationRate = decceleration;
        }
        moveSpeed = speedToMaxSpeed * accelerationRate;

        rb.AddForce(moveSpeed * Vector2.right);
    }

    private void Stop()
    {
        // A force is applied to smoothly stop the player when there are no movement inputs
        speedToMaxSpeed = 0 - rb.velocity.x;
        accelerationRate = decceleration;
        moveSpeed = speedToMaxSpeed * accelerationRate;

        rb.AddForce(moveSpeed * Vector2.right);
    }

    private void Friction()
    {
        // AN artificial friction is applied whenever on the ground
        if (lastTimeOnGround > 0)
        {
            friction = Mathf.Min(Mathf.Abs(rb.velocity.x), frictionAmount);
            friction *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -friction, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        // If the player is on the ground (accounting coyate time) a force is applied upwards
        lastTimeJumping = jumpBufferTime;

        if (lastTimeJumping > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.AddForce(Vector2.up * Mathf.Abs(rb.velocity.y), ForceMode2D.Impulse);
            }
            jumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void JumpOff()
    {
        // If the player releases space before the jump peaks the jump is immediately peaked
        if (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * jumpCutForce, ForceMode2D.Impulse);
        }
    }

    private void Gravity()
    {
        // When falling the gravity is increased
        // When not falling gravity is normal
        if (rb.velocity.y < -1)
        {
            rb.gravityScale = gravity * fallGravityMultiplier;
            animator.SetBool("JumpDown", true);
        }
        else
        {
            rb.gravityScale = gravity;
            animator.SetBool("JumpDown", false);
        }
    }

    private void Attack()
    {
        // AN attack area is made and if there is an enemy inside the nemy is damaged
        animator.SetBool("Attacking", true);

        attacking = true;

        EnemyColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

        for (i = 0; i < EnemyColliders.Length; i++)
        {
            if (EnemyColliders[i].CompareTag("Enemy"))
            {
                EnemyColliders[i].GetComponent<EnemyScript>().Die();
            }
        }
        StartCoroutine(AttackWait());


        IEnumerator AttackWait()
        {
            // The attack is called a total of three times over the swing of the blade to allow better gameplay
            yield return new WaitForSeconds(0.1f);

            EnemyColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

            for (i = 0; i < EnemyColliders.Length; i++)
            {
                if (EnemyColliders[i].CompareTag("Enemy"))
                {
                    EnemyColliders[i].GetComponent<EnemyScript>().Die();
                }
            }

            yield return new WaitForSeconds(0.1f);

            EnemyColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

            for (i = 0; i < EnemyColliders.Length; i++)
            {
                if (EnemyColliders[i].CompareTag("Enemy"))
                {
                    EnemyColliders[i].GetComponent<EnemyScript>().Die();
                }
            }

            yield return new WaitForSeconds(0.1f);

            animator.SetBool("Attacking", false);

            attacking = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // This is useful to see the attack area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void NextLevel1()
    {
        // This allows the level time to be saved before moving to the next level
        GameObject.Find("TimerObject").GetComponent<TimerScript>().SaveTime();
    }
    public void NextLevel2()
    {
        // the ext level is loaded
        scene = SceneManager.GetActiveScene();
        level = scene.buildIndex;
        SceneManager.LoadScene(level + 1);
        GameObject.Find("TimerObject").GetComponent<TimerScript>().StartTimer();
    }

    private void ResetLevel()
    {
        // the current level is reloaded to reset everything
        scene = SceneManager.GetActiveScene();
        level = scene.buildIndex;
        SceneManager.LoadScene(level);
    }

    public void Damaged(Collider2D enemy)
    {
        // When the player comes in contact with an enemy it loses health
        // If health goes to zero the player dies
        if (damageable == true)
        {
            health = health - 1;
            damageable = false;

            enemy.GetComponent<EnemyScript>().Attack();

            if (health <= 0)
            {
                Death();
            }

            StartCoroutine(DamageWait());

            IEnumerator DamageWait()
            {
                // When damaged the player flashes red
                // And has a cooldown before it can be damaged again
                spriteRenderer.color = Color.red;

                yield return new WaitForSeconds(0.2f);

                spriteRenderer.color = Color.white;

                damageable = true;
            }
        }
    }

    private void Death()
    {
        // When the player dies they are forzen in place ad the death animaiton is played
        canMove = false;
        rb.velocity = new Vector2(0, 0);
        sprite.transform.localPosition = new Vector3(sprite.transform.localPosition.x, 1, sprite.transform.localPosition.z);
        animator.SetBool("Dead", true);

        StartCoroutine(DeathWait());

        IEnumerator DeathWait()
        {
            // After a delay for the aniamtion to play the level is reset
            yield return new WaitForSeconds(0.75f);

            ResetLevel();
        }
    }
}
