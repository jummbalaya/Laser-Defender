using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDurration = 0.3f;
    [SerializeField] float shakeMagnitude = 0.2f;

    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTIme = 0;

        while(elapsedTIme < shakeDurration)
        {
            transform.position = initPos + (Vector3)UnityEngine.Random.insideUnitCircle * shakeMagnitude;
            elapsedTIme += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = initPos;
    }
}
