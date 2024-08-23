using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayMovement : MonoBehaviour
{
    public bool wayCanMove = true;

    [SerializeField] float moveSpeed = 1.75f;
    [SerializeField] float rightBoundary = 0f;
    [SerializeField] float leftBoundary = -1.75f;
    [SerializeField] float upBoundary = 1.75f;
    [SerializeField] float bottomBoundary = -1.75f;

    AudioSource wayAudio;

    
    bool hitRight;
    bool hitLeft;
    bool hitUp;
    bool hitBottom;

    void Start() 
    {
        wayAudio = GetComponent<AudioSource>();    
    }

    void Update()
    {
        Move();
    }

    void Move() 
    {
        hitRight = transform.GetChild(0).GetComponent<HitBox>().isHit;
        hitLeft = transform.GetChild(1).GetComponent<HitBox>().isHit;
        hitUp = transform.GetChild(2).GetComponent<HitBox>().isHit;
        hitBottom = transform.GetChild(3).GetComponent<HitBox>().isHit;


        if (GetComponent<Way>().selected && wayCanMove) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < upBoundary && !hitUp) 
            {
                transform.position += new Vector3(0, moveSpeed);
                wayAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > bottomBoundary && !hitBottom) 
            {
                transform.position += new Vector3(0, -moveSpeed);
                wayAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < rightBoundary && !hitRight) 
            {
                transform.position += new Vector3(moveSpeed, 0);
                wayAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > leftBoundary && !hitLeft) 
            {
                transform.position += new Vector3(-moveSpeed, 0);
                wayAudio.Play();
            }
        }
    }
}
