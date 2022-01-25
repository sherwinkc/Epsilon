using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) // when comparing a float == 0 use <= mathf.epsilon;
        {
            return;
        }
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // tau is a constant value of 6.283 (2 times Pi)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1 (cause of radians)

        movementFactor = (rawSinWave + 1f) / 2f; // recalcualted to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
