using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Way : MonoBehaviour
{
    [SerializeField] float lightOffTimer = 0.05f;

    public int wayNumber;

    public float wayFallCount = 10f;
    public bool selected;
    public bool lampOff;
    public Vector3 startPos;
    public Vector3 currentPos;

    [SerializeField] Sprite puzzleForm;

    [SerializeField] Color boxOriginalColor;
    [SerializeField] Color boxHighlightColor;

    SpriteRenderer spriteRenderer;
    Light2D light2D;
    CapsuleCollider2D[] capsuleCollider2D;

    void Start() 
    {
        startPos = transform.position; 
        light2D = transform.GetChild(4).GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponents<CapsuleCollider2D>();
    }

    void Update() 
    {
        currentPos = transform.position;

        ChangeColor();

        LightOff();

        WayDeath();

        ChangeSpriteAndCollider();
    }

    void ChangeSpriteAndCollider() 
    {
        if (FindObjectOfType<GameManager>().puzzleStart) 
        {
            spriteRenderer.sprite = puzzleForm;

            if (capsuleCollider2D != null) 
            {
                foreach(Collider2D col in GetComponents<Collider2D>())
                {
                    col.enabled = true;
                }
            }
        }
    }

    void WayDeath() 
    {
        if (lampOff == true && light2D.intensity == 0f) 
        {
            wayFallCount -= 1 * Time.deltaTime;
        }

        if (wayFallCount <= 0) 
        {
            Destroy(gameObject);
            FindObjectOfType<Player>().PlayerDeath();
            FindObjectOfType<Player>().transform.localScale -= new Vector3(0.3f, 0.3f);
        }
    }

    void LightOff() 
    {
        if (lampOff && FindObjectOfType<GameManager>().puzzleStart) 
        {
            light2D.intensity -= Time.deltaTime * lightOffTimer;

            if (light2D.intensity <= 0f) 
            {
                light2D.intensity = 0f;
            }
        }
    }

    void ChangeColor() 
    {
        if (selected) 
        {
            GetComponent<SpriteRenderer>().color = boxHighlightColor;
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = boxOriginalColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GetComponent<WayMovement>().wayCanMove = false;

            lampOff = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GetComponent<WayMovement>().wayCanMove = true;
            light2D.intensity = 1f;
            lampOff = false;
        }
    }
}
