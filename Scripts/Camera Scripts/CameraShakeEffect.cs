using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    [SerializeField] public float power = 0.2f;
    [SerializeField] public float duration = 0.2f;
    [SerializeField] public float slowDownAmount = 1;
    [HideInInspector] public bool isShaking;
    private float initialDuraton;
    private Vector3 startPosition;
    void Start()
    {
        initialDuraton = duration;
        startPosition = transform.localPosition;
    }

    void Update()
    {
        Shake();
    }
    /// <summary>
    /// Camera shacking effect after players final attack
    /// </summary>
    void Shake()
    {
        if (isShaking)
        {
            if(duration> 0)
            {
                transform.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                isShaking= false;
                duration = initialDuraton;
                transform.localPosition = startPosition;
            }
        }
    }
}
