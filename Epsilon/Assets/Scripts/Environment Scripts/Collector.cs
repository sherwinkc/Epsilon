using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Collector : MonoBehaviour
{
    AudioManager audioManager;
    //[SerializeField] LevelManager levelManager;
    PlayerStateMachine player;

    FindOrbsToolTipManager orbManager;
    [SerializeField] GameObject endGate;
    [SerializeField] GameObject pickUpVFX;

    public PlayableDirector playableDirector;
    public int orbs = 0;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        orbManager = FindObjectOfType<FindOrbsToolTipManager>();

        player = FindObjectOfType<PlayerStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            playableDirector.Play();
            Instantiate(pickUpVFX, collision.gameObject.transform.position, Quaternion.identity);
            if (collision != null) Destroy(collision.gameObject);
            orbs++;

            if (orbs >= 2) 
            {
                FinalOrbSequence finalOrbSequence = FindObjectOfType<FinalOrbSequence>();
                finalOrbSequence.StartCoroutineSequence();                
            }

            audioManager.PlayCollectSFX();
        }
    }
}
