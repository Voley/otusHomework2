using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(string text)
    {
        _text.text = text;
    }
}
