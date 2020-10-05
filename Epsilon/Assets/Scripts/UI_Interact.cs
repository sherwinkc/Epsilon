using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Interact : MonoBehaviour
{
    public GameObject icon;
    public bool isOn = false;

    public bool usedOnce = false;

    public AudioSource iconSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isOn)
        {
            //StartCoroutine(BounceCo());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(!usedOnce)
            {
                icon.SetActive(true);
                iconSFX.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            icon.SetActive(false);
            usedOnce = true;
        }
    }

    public IEnumerator BounceCo()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.01f);

        yield return new WaitForSeconds(1f);

        transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);

        yield return new WaitForSeconds(1f);

        transform.position = new Vector2(transform.position.x, transform.position.y - 0.01f);

        yield return new WaitForSeconds(1f);

        transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);

        isOn = false;

        yield return null;
    }
}
