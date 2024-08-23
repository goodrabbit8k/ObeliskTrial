using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool isHit;
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Way") 
        {
            isHit = true;
        }
        
        if (other.gameObject.tag == "Player") 
        {
            GetComponentInParent<WayMovement>().wayCanMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Way") 
        {
            isHit = false;
        }

        if (other.gameObject.tag == "Player") 
        {
            GetComponentInParent<WayMovement>().wayCanMove = false;
        }
    }
}
