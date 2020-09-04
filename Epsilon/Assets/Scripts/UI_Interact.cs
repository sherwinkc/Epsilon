using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Interact : MonoBehaviour
{
    public TMP_Text textUI;
    public Transform boxTransorm;
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        textUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            textUI.transform.position = new Vector3(boxTransorm.position.x, boxTransorm.position.y + 1f, boxTransorm.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            textUI.enabled = true;
            isOn = true;
        }
    }
}
