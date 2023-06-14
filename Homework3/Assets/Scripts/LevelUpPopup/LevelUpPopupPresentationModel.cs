using Lessons.Architecture.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopupPresentationModel: ILevelUpPopupPresentable
{
    private LevelUpPopup _popup;
    private PlayerInfoAggregate _playerData;

    public LevelUpPopupPresentationModel(PlayerInfoAggregate playerData)
    {
        _playerData = playerData;
    }

    public void SetPopupView(LevelUpPopup popup)
    {
        _popup = popup;
        _popup.OnCloseButtonPressed += PlayerDidPressCloseButton;
        _popup.OnLevelUpButtonPressed += PlayerDidPressLevelUpButton;
    }

    public void SetData(PlayerInfoAggregate playerData)
    {
        _playerData = playerData;
    }

    public string AccountName()
    {
        return "@" + _playerData.UserInfo.Name;
    }

    public bool BottomButtonActive()
    {
        return _playerData.PlayerLevel.CanLevelUp();
    }

    public string BottomButtonText()
    {
        return "LEVEL UP";
    }

    public string Description()
    {
        return _playerData.UserInfo.Description;
    }

    public string LevelText()
    {
        return string.Format("Level: {0}", _playerData.PlayerLevel.CurrentLevel);
    }

    public Sprite Portrait()
    {
        return _playerData.UserInfo.Icon;
    }

    public IStatItem[] StatItems()
    {
        CharacterStat[] rawStats = _playerData.CharacterInfo.GetStats();

        StatItem[] statItems = new StatItem[rawStats.Length];

        for (var i = 0; i < rawStats.Length; i++)
        {
            var stat = rawStats[i];
            var statItem = new StatItem(string.Format("{0}: {1}", stat.Name, stat.Value));
            statItems[i] = statItem;
        }

        return statItems;
    }

    public string XPSliderText()
    {
        return string.Format("XP: {0}/{1}", _playerData.PlayerLevel.CurrentExperience, _playerData.PlayerLevel.RequiredExperience);
    }

    public float XPSliderValue()
    {
        return (float)_playerData.PlayerLevel.CurrentExperience / _playerData.PlayerLevel.RequiredExperience;
    }

    private void PlayerDidPressLevelUpButton()
    {
        _playerData.PlayerLevel.LevelUp();
        _popup.UpdateView();
    }

    private void PlayerDidPressCloseButton()
    {
        _popup.OnCloseButtonPressed -= PlayerDidPressCloseButton;
        _popup.OnLevelUpButtonPressed -= PlayerDidPressLevelUpButton;

        _popup.Hide();
    }
}
