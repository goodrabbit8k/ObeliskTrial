using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    Vector3 offset = new Vector3(0, 0, -1f);
    
    AudioSource cameraAudio;

    void Start() 
    {
        cameraAudio = GetComponent<AudioSource>();    
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
