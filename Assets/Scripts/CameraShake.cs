using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool startShaking = false;
    public AnimationCurve curve;
    public float duration = 1f;

    void Update()
    {
        if (startShaking) 
        {
            startShaking = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() 
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
