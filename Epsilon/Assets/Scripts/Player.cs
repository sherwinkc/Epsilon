using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxAir = 100f;
    public float startingAir = 75f;
    public float currentAir;

    public AirBarScript airBar;
    public LevelManager levelMan;
    public Animator anim;
    public PlayerMovement playerMov;
    public JetPack playerJet;

    public AudioSource crunch;


    // Start is called before the first frame update
    void Start()
    {
        levelMan = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        playerMov = GetComponent<PlayerMovement>();
        playerJet = GetComponent<JetPack>();

        currentAir = startingAir;
        airBar.SetMaxAir(maxAir);
    }

    // Update is called once per frame
    void Update()
    {
        DepleteAir(0.005f);

        if(currentAir <= 0)
        {
            //Die();
        }
    }

    void DepleteAir(float airBreathed)
    {
        currentAir -= airBreathed;
        airBar.setAir(currentAir);
    }

    /*public void Die()
    {
        playerMov.isDying = true;

        if(!crunch.isPlaying)
        {
            crunch.Play();
        }

        playerMov.rb.velocity = Vector2.zero;
        playerMov.enabled = false;
        playerJet.enabled = false;
        anim.SetTrigger("Die");
        levelMan.Respawn();
    }*/
}
