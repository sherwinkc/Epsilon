using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject helperTransform;
    public HelperMovement helper;

    [SerializeField] bool isMovingWithHelper = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        helper = FindObjectOfType<HelperMovement>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingWithHelper && helperTransform != null) transform.position = helperTransform.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rover"))
        {
            helper.isPickingUpItem = false;
            helper.isDepositingToRover = false;
            audioManager.helperCollectSFX.Play();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Helper"))
        {
            collision.transform.position = helperTransform.transform.position;
            isMovingWithHelper = true;

            helper.isPickingUpItem = false;
            helper.isDepositingToRover = false;

            audioManager.helperCollectSFX.Play();
        }
    }
}
