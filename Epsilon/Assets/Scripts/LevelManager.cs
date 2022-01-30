using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public Transform respawnPosition;
    public Transform playerTransform;
    public GameObject playerMov;
    public Animator anim;
    public Player player;
    public JetPack jetPack;

    public PlayerMovement playerMovScript;
    public JetPack playerJet;

    public HUDController hudController;

    public CinemachineVirtualCamera cam1;
    //public CinemachineVirtualCamera deathCam;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;

        playerMovScript = FindObjectOfType<PlayerMovement>();
        playerJet = FindObjectOfType<JetPack>();
        player = FindObjectOfType<Player>();
        hudController = FindObjectOfType<HUDController>();
        jetPack = FindObjectOfType<JetPack>();
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
        //jetPack.boostTime = 1.5f;

        cam1.m_Lens.OrthographicSize = 7f;

        yield return new WaitForSeconds(3);

        cam1.m_Lens.OrthographicSize = 9f;

        anim.SetTrigger("Idle");
        //playerMov.transform.position = respawnPosition.transform.position;
        playerTransform.position = respawnPosition.transform.position;

        playerMovScript.enabled = true;
        if (hudController.jetpackOffline.activeSelf == false)
        {
            playerJet.enabled = true;
        }

        //playerMovScript.isDying = false;

        yield return null;
    }
}
