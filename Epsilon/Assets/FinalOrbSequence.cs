using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalOrbSequence : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerStateMachine player;

    [SerializeField] GameObject gunk1;
    [SerializeField] GameObject helperLegs;
    
    public float waitTime = 5f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }

    public void StartCoroutineSequence()
    {
        StartCoroutine(FinalOrbSequenceCo());
    }

    public IEnumerator FinalOrbSequenceCo()
    {
        player.inCinematic = true;
        yield return new WaitForSeconds(7.5f);

        anim.Play("Player_ScriptedDeath");
        player.inCinematic = true;

        yield return new WaitForSeconds(6.5f);

        gunk1.SetActive(true);
        helperLegs.SetActive(true);        
    }
}
