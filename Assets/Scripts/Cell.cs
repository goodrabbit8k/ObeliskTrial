using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Vector3 wayPos;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Way") 
        {
            wayPos = other.gameObject.GetComponent<Way>().startPos;

            transform.position = wayPos;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Way") 
        {
            other.gameObject.GetComponent<Way>().startPos = other.gameObject.GetComponent<Way>().currentPos;
        }
    }
}
