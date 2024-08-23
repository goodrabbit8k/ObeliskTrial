using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaySelection : MonoBehaviour
{
    [SerializeField] GameObject puzzleCamera;

    int click = 0;

    public bool canClick;
    AudioSource cameraAudio;

    void Start() 
    {
        cameraAudio = GetComponent<AudioSource>();    
    }

    void Update()
    {
        Select();

        if (FindObjectOfType<GameManager>().puzzleStart)
        {
            if (cameraAudio != null)
            {
                cameraAudio.Play();
            }
        }
    }

    void Select() 
    {
        if (Input.GetMouseButtonDown(0) && FindObjectOfType<GameManager>().puzzleStart && puzzleCamera.activeSelf && canClick) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider.gameObject.GetComponent<Way>() != null) 
            {
                hit.collider.gameObject.GetComponent<Way>().selected = true;
                click++;
            }

            if (click == 2) 
            {
                hit.collider.gameObject.GetComponent<Way>().selected = false;
                click = 0;
            }
        }
    }

    public void CantClickPause(bool value) 
    {
        canClick = value;
    }
}
