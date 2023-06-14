using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private LevelUpPopup _levelUpPopup;

    public void ShowLevelUpPopup(LevelUpPopupPresentationModel model)
    {
        _levelUpPopup.SetPresentationModel(model);
        _levelUpPopup.Show();
    }

    public void HideLevelUpPopup()
    {
        _levelUpPopup.Hide();
    }
}
