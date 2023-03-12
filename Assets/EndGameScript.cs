using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          StartCoroutine(LoadSceneAgain());
        }
    }

    IEnumerator LoadSceneAgain()
    {
        Debug.Log("load scene again ");
        yield return new WaitForSeconds(1f);

        // play sound
        SoundManager.instance.PlayFinishSound();
        Debug.Log("Play finish sound ");

        yield return new WaitForSeconds(3f);
        Debug.Log("reload the same active scene ");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //load level 2
        SceneManager.LoadScene("Level2");
    }
}
