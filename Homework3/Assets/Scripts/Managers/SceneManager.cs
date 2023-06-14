using Lessons.Architecture.PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private PlayerInfoAggregateFactory _playerFactory;
    [SerializeField] private PopupManager _popupManager;

    [SerializeField] private Button _buttonBob;
    [SerializeField] private Button _buttonAlice;
    [SerializeField] private Button _buttonJoe;

    private void Awake()
    {
        _buttonBob.onClick.AddListener(ShowBobPopup);
        _buttonAlice.onClick.AddListener(ShowAlicePopup);
        _buttonJoe.onClick.AddListener(ShowJoePopup);
    }

    private void OnDestroy()
    {
        _buttonBob.onClick.RemoveListener(ShowBobPopup);
        _buttonAlice.onClick.RemoveListener(ShowAlicePopup);
        _buttonJoe.onClick.RemoveListener(ShowJoePopup);
    }

    private void ShowBobPopup()
    {
        var player = _playerFactory.Bob();
        var presentationModel = new LevelUpPopupPresentationModel(player);
        _popupManager.ShowLevelUpPopup(presentationModel);
    }

    private void ShowAlicePopup()
    {
        var player = _playerFactory.Alice();
        var presentationModel = new LevelUpPopupPresentationModel(player);
        _popupManager.ShowLevelUpPopup(presentationModel);
    }

    private void ShowJoePopup()
    {
        var player = _playerFactory.Joe();
        var presentationModel = new LevelUpPopupPresentationModel(player);
        _popupManager.ShowLevelUpPopup(presentationModel);
    }
}
