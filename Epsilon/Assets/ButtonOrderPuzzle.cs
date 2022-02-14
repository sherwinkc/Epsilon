using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrderPuzzle : MonoBehaviour
{
    AudioManager audioManager;

    public bool correctHasPlayed = false;
    public bool correct2HasPlayed = false;
    public bool errorHasPlayed = false;


    public JumpButton[] buttons;
    public SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].isThisButtonActive)
        {
            Debug.Log("Correct So Far");
            spriteRenderers[0].color = Color.green;
            if (!correctHasPlayed) 
            { 
                audioManager.puzzleCorrect.Play();
                correctHasPlayed = true;            
            }
        }

        if (!buttons[0].isThisButtonActive && buttons[1].isThisButtonActive)
        {
            Debug.Log("Wrong Order");
            spriteRenderers[0].color = Color.red;
            spriteRenderers[1].color = Color.red;
            Invoke("ResetColors", 1f);
            if (!errorHasPlayed) 
            {
                audioManager.puzzleError.Play();
                errorHasPlayed = true;
            } 
        }

        /*if (buttons[0].isThisButtonActive && !buttons[1].isThisButtonActive)
        {
            Debug.Log("Sequence Correct So Far");
        }*/

        if (buttons[0].isThisButtonActive && buttons[1].isThisButtonActive)
        {
            Debug.Log("Sequence Correct. Door Unlocked");
            spriteRenderers[1].color = Color.green;
            if (!correct2HasPlayed) 
            { 
                audioManager.puzzleCorrect2.Play();
                correct2HasPlayed = true;
            }
        }
    }

    void ResetColors()
    {
        buttons[0].isThisButtonActive = false;
        buttons[1].isThisButtonActive = false;
        spriteRenderers[0].color = Color.white;
        spriteRenderers[1].color = Color.white;
        
        correctHasPlayed = false;
        correct2HasPlayed = false;
        errorHasPlayed = false;
    }
}
