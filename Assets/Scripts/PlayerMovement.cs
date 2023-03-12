using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    SpriteRenderer renderer;
    Animator animator;
    Rigidbody2D rb2d;


    public Transform fireballContainer;
    public Transform Fireball_SpwnPoint;
    public GameObject FireballPrefap;
    public float Speed = 4;
    public float JumpStringth = 250;
    public int ReplayAfterY = -40;


    public bool isGrounded = false;
    public bool isDead = false;
    public bool isHurt = false;
    public bool isFlipX = false;


    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
            return;

        if (isHurt)
            return;

        Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += vector3 * Time.deltaTime * Speed;

        //right movement GetAxis("Horizontal") between 0 ,1 (float)
        //left movement GetAxis("Horizontal") between 0 , - 1 (float)


        if (transform.position.y < ReplayAfterY)
        {
            ReplayGame();
        }


        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            isGrounded = false;
            animator.Play("JumpAnimation");
            rb2d.AddForce(Vector2.up*JumpStringth);
        }

        if (Input.GetKeyDown(KeyCode.LeftCommand))
        {

            //first way to do the fireball
            //GameObject fireball_obj = Instantiate(FireballPrefap, Fireball_SpwnPoint.position,Quaternion.identity);
            //FireballMovement fireballMovement = fireball_obj.GetComponent<FireballMovement>();
            // SpriteRenderer spriteRenderer = fireball_obj.GetComponent<SpriteRenderer>();
            //  spriteRenderer.flipX = renderer.flipX;

            //  if (renderer.flipX == false)
            //  {
            //      fireballMovement.direction = 1;
            //  }
            //  else
            //  {
            //      fireballMovement.direction = -1;
            //  }

            //======================================================
            //second way to create the fireball
           GameObject fireballObj = Instantiate(FireballPrefap, Fireball_SpwnPoint.position, Quaternion.identity);
            fireballObj.transform.SetParent(fireballContainer);
        }


        if (isGrounded)
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                //idle
                animator.Play("IdleAnimation");
            }
            else
            {            // the player is moveing

                if (Input.GetAxis("Horizontal") > 0.01f)
                {
                    //right
                    // disable flip option
                    renderer.flipX = false;
                    isFlipX = false;
                }

                if (Input.GetAxis("Horizontal") < -0.01f)
                {
                    // left
                    // enable flip on X
                    renderer.flipX = true;
                    isFlipX = true;
                }

                animator.Play("RunAnimation");
            }

        }
        // enable jump animation
       
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collided with : "+ collision.gameObject.name);
        Debug.Log("collided with Tag : " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Mushroom") || collision.gameObject.CompareTag("Moster"))
        {
             isHurt = true;
            UI_Manager.instance.playerLifesCounter--;
            UI_Manager.instance.DeleteLatestPlayerLifesImage();


            animator.Play("HurtAnimation");

            renderer.color = Color.red;
            Invoke("DisableHurtFlag",0.9f);

            if (UI_Manager.instance.playerLifesCounter <= 0)
            {
                SoundManager.instance.PlayGameOverSound();
                animator.Play("DeadAnimation");
                isDead = true;
                Destroy(gameObject, 2);
            }
            else
            {
                SoundManager.instance.HittSoundStatus(true);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    Transform SpawnPoint_Temp;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with Tag : " + other.gameObject.tag);


        if (other.CompareTag("Water"))
        {

            WaterScript waterScript = other.gameObject.GetComponent<WaterScript>();
            int randomPoint = Random.Range(0, waterScript.SpawnPoint.Length);
            SpawnPoint_Temp = waterScript.SpawnPoint[randomPoint];

            isGrounded = false;
            animator.Play("FallAnimation");
            

            Invoke("ReCreatePlayer",3);
        }

        if (other.CompareTag("Coin"))
        {
            UI_Manager.instance.IncreaseCoinCointerText();
            Destroy(other.gameObject);//destroy coin
        }
    }

    void ReCreatePlayer()
    {
        transform.position = SpawnPoint_Temp.position;
    }

    void DisableHurtFlag()
    {
        isHurt = false;
        renderer.color = Color.white;
    }


    void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
