using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatItem : IStatItem
{
    private string _text;

    public StatItem(string text)
    {
        _text = text;
    }

    string IStatItem.Text()
    {
        return _text;
    }
}
