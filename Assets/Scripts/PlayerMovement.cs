using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D playerRb;
    Animator playerAnim;

    Vector2 movement;

    void Start() 
    {
        playerRb = GetComponent<Rigidbody2D>();    
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveInput();
        PlayerAnimation();
        PlayerRestart();
    }

    void FixedUpdate() 
    {
        PlayerMoving();
    }

    void MoveInput() 
    {
        if (FindObjectOfType<Player>().playerCanMove) 
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void PlayerMoving() 
    {
        if (FindObjectOfType<Player>().playerCanMove) 
        {
            playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.deltaTime);  
        }
    }

    void PlayerAnimation() 
    {
        playerAnim.SetFloat("Horizontal", movement.x);
        playerAnim.SetFloat("Vertical", movement.y);
        playerAnim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void PlayerRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
