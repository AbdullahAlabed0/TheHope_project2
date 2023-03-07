using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCustomMovement : MonoBehaviour
{

    public Image lifeBar;
    public GameObject[] poinst;
    public float speed = 4;

    int maxMonsterLife = 5;
    public int monsterLife = 5;
    public int currentPointIndex = 0;

    private void Start()
    {
        maxMonsterLife = monsterLife;
    }
    // Update is called once per frame
    void Update()
    {
      float distance =  Vector2.Distance(transform.position, poinst[currentPointIndex].transform.position);
        
        if (distance < 0.3f)
        {
            currentPointIndex++;
            if (currentPointIndex >= poinst.Length)
            {
                currentPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, poinst[currentPointIndex].transform.position,speed*Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("destroy fireball");
            Destroy(collision.gameObject);
            monsterLife--;

            ChangeLifeBarAmount();

            if (monsterLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void ChangeLifeBarAmount()
    {
        float value = (float) monsterLife / (float) maxMonsterLife;
        Debug.Log("value " + value);
        lifeBar.fillAmount = value;
    }
}
