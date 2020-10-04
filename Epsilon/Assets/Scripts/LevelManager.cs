using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public Transform respawnPosition;
    public GameObject playerMov;
    public Animator anim;
    public Player player;

    public PlayerMovement playerMovScript;
    public JetPack playerJet;

    public HUDController hudController;

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera deathCam;

    // Start is called before the first frame update
    void Start()
    {
        playerMovScript = FindObjectOfType<PlayerMovement>();
        playerJet = FindObjectOfType<JetPack>();
        player = FindObjectOfType<Player>();
        hudController = FindObjectOfType<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        player.currentAir = player.maxAir;

        yield return new WaitForSeconds(3);

        anim.SetTrigger("Idle");
        playerMov.transform.position = respawnPosition.transform.position;
        playerMovScript.enabled = true;

        if (hudController.jetpackOffline.activeSelf == false)
        {
            playerJet.enabled = true;
        }


        yield return null;
    }
}
