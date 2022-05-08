using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    public Button selectButton;

    private void OnEnable()
    {
        if (selectButton != null) selectButton.Select();
    }
}
