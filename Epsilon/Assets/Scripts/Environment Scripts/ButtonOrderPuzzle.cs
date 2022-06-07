using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrderPuzzle : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] float timeToReset = 0.5f;

    bool isPuzzleComplete = false;
    bool correctHasPlayed = false;
    bool correct1HasPlayed = false;
    bool correct2HasPlayed = false;
    bool errorHasPlayed = false;

    public JumpButton[] buttons;
    public SpriteRenderer[] spriteRenderers;

    [SerializeField] GameObject cameraTrigger;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (cameraTrigger != null) cameraTrigger.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPuzzleComplete)
        {
            if (buttons[0].isThisButtonActive)
            {
                //Debug.Log("Correct So Far");
                spriteRenderers[0].color = Color.green;
                if (!correctHasPlayed)
                {
                    audioManager.puzzleCorrect.Play();
                    correctHasPlayed = true;
                }
            }

            if (!buttons[0].isThisButtonActive && (buttons[1].isThisButtonActive || buttons[2].isThisButtonActive))
            {
                //Debug.Log("Wrong Order");
                spriteRenderers[0].color = Color.red;
                spriteRenderers[1].color = Color.red;
                spriteRenderers[2].color = Color.red;
                Invoke("ResetColors", timeToReset);
                if (!errorHasPlayed)
                {
                    audioManager.puzzleError.Play();
                    errorHasPlayed = true;
                }
            }

            if (!buttons[0].isThisButtonActive && !buttons[1].isThisButtonActive && buttons[2].isThisButtonActive)
            {
                //Debug.Log("Wrong Order");
                spriteRenderers[0].color = Color.red;
                spriteRenderers[1].color = Color.red;
                spriteRenderers[2].color = Color.red;
                Invoke("ResetColors", timeToReset);
                if (!errorHasPlayed)
                {
                    audioManager.puzzleError.Play();
                    errorHasPlayed = true;
                }
            }

            if (buttons[0].isThisButtonActive && !buttons[1].isThisButtonActive && buttons[2].isThisButtonActive)
            {
                //Debug.Log("Wrong Order");
                spriteRenderers[0].color = Color.red;
                spriteRenderers[1].color = Color.red;
                spriteRenderers[2].color = Color.red;
                Invoke("ResetColors", timeToReset);
                if (!errorHasPlayed)
                {
                    audioManager.puzzleError.Play();
                    errorHasPlayed = true;
                }
            }

            if (buttons[0].isThisButtonActive && buttons[1].isThisButtonActive && !buttons[2].isThisButtonActive)
            {
                spriteRenderers[1].color = Color.green;
                if (!correct1HasPlayed)
                {
                    audioManager.puzzleCorrect.Play();
                    correct1HasPlayed = true;
                }
            }

            if (buttons[0].isThisButtonActive && buttons[1].isThisButtonActive && buttons[2].isThisButtonActive)
            {
                spriteRenderers[2].color = Color.green;
                if (!correct2HasPlayed)
                {
                    audioManager.puzzleCorrect2.Play();
                    correct2HasPlayed = true;
                }

                if (cameraTrigger != null) cameraTrigger.SetActive(false);
                isPuzzleComplete = true;
                DropPod dropPod = FindObjectOfType<DropPod>();
                dropPod.OpenDropPod();

            }
        }       
    }

    void ResetColors()
    {
        buttons[0].isThisButtonActive = false;
        buttons[1].isThisButtonActive = false;
        buttons[2].isThisButtonActive = false;
        spriteRenderers[0].color = Color.white;
        spriteRenderers[1].color = Color.white;
        spriteRenderers[2].color = Color.white;

        correctHasPlayed = false;
        correct2HasPlayed = false;
        errorHasPlayed = false;

        ResetAnimations();
    }

    void ResetAnimations()
    {
        JumpButton[] buttonList = FindObjectsOfType<JumpButton>();

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].ResetButton();
        }
    }
}
