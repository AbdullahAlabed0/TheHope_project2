using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public float speed;
    public int direction = 1;
    public float lifeTimer=10;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerMovement.instance.isFlipX)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        GetComponent<SpriteRenderer>().flipX = PlayerMovement.instance.isFlipX;



        Destroy(gameObject, lifeTimer);

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
