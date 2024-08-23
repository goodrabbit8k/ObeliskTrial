using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public bool playerCanMove;
    public bool hasTreasure = false;

    [SerializeField] GameObject puzzleCamera;
    [SerializeField] GameObject followCamera;

    Animator playerAnim;
    [SerializeField] Animator treasureSceneAnim;

    public int click = 0;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update() 
    {
        if (!FindObjectOfType<GameManager>().pause)
        {
            SwitchCamera();
        }

        if (puzzleCamera.gameObject.activeInHierarchy)
        {
            playerCanMove = false;
        }
        else 
        {
            if (!FindObjectOfType<GameManager>().gameOver) 
            {
                playerCanMove = true;
            }
            else
            {
                playerCanMove = false;
            }
        }
    }

    public void PlayerDeath() 
    {
        FindObjectOfType<GameManager>().gameOver = true;
        playerAnim.SetBool("Death", true);
    }

    void SwitchCamera() 
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            puzzleCamera.SetActive(true);
            followCamera.SetActive(false);

            click++;
        }

        if (click == 2) 
        {
            puzzleCamera.SetActive(false);
            followCamera.SetActive(true);

            click = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Cell") 
        {
            PlayerDeath();
            transform.localScale -= new Vector3(0.3f, 0.3f);
        }

        if (other.gameObject.tag == "Treasure")
        {
            FindObjectOfType<WaySelection>().canClick = true;
            StartCoroutine(TreasureScene());
            Destroy(other.gameObject);
        }
    }

    IEnumerator TreasureScene() 
    {
        treasureSceneAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        FindObjectOfType<GameManager>().puzzleStart = true;
        treasureSceneAnim.SetTrigger("Start");
    }
}
