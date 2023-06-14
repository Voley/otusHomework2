using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelUpPopupPresentable
{
    public void SetPopupView(LevelUpPopup popup);

    public string AccountName();
    public string LevelText();
    public string Description();
    public Sprite Portrait();

    public float XPSliderValue();
    public string XPSliderText();
    public IStatItem[] StatItems();

    public string BottomButtonText();
    public bool BottomButtonActive();
}

public interface IStatItem
{
    public string Text();
}