using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    PlayerStateMachine player;
    [SerializeField] ParticleSystem[] particles;

    [SerializeField] float distancFromVFX;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }

    void Start()
    {
        InvokeRepeating("CheckVFXDistanceFromPlayer", 0, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckVFXDistanceFromPlayer()
    {
        Debug.Log("Invoked");

        if (Vector2.Distance(transform.position, player.transform.position) < distancFromVFX)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Stop();
            }
        }
    }
}
