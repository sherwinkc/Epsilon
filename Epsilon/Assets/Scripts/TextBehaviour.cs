using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBehaviour : MonoBehaviour
{
    public TMP_Text text;

    public bool isTextFlashing = false; 

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTextFlashing)
        {
            StartCoroutine(FlashingTextCo());
        }
    }

    public IEnumerator FlashingTextCo()
    {
        isTextFlashing = true;

        text.alpha = 0;

        yield return new WaitForSeconds(0.5f);

        text.alpha = 1f;

        yield return new WaitForSeconds(1f);

        isTextFlashing = false;

        yield return null;
    }
}
