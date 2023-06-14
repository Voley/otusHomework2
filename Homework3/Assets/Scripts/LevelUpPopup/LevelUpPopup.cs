using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopup : MonoBehaviour
{
    public Action OnCloseButtonPressed;
    public Action OnLevelUpButtonPressed;

    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _accountNameText;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private Image _xpSliderFillImage;
    [SerializeField] private TMP_Text _xpSliderText;
    [SerializeField] private UIStatItem[] _uiStatItems;
    [SerializeField] private Button _bottomButton;
    [SerializeField] private TMP_Text _bottomButtonText;

    [SerializeField] private Sprite _sliderBlueFill;
    [SerializeField] private Sprite _sliderGreenFill;

    private ILevelUpPopupPresentable _model;

    public void SetPresentationModel(ILevelUpPopupPresentable model)
    {
        _model = model;
        _model.SetPopupView(this);
        UpdateView();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateView()
    {
        _accountNameText.text = _model.AccountName();
        _portraitImage.sprite = _model.Portrait();
        _levelText.text = _model.LevelText();
        _descriptionText.text = _model.Description();
        _xpSlider.value = _model.XPSliderValue();
        _xpSliderText.text = _model.XPSliderText();
        _bottomButton.interactable = _model.BottomButtonActive();

        UpdateStatItems();
        UpdateSliderColors();
    }

    private void Awake()
    {
        _bottomButton.onClick.AddListener(LevelUpButtonPressed);
        _closeButton.onClick.AddListener(CloseButtonPressed);
    }

    private void OnDestroy()
    {
        _bottomButton.onClick.RemoveListener(LevelUpButtonPressed);
        _closeButton.onClick.RemoveListener(CloseButtonPressed);
    }

    private void UpdateSliderColors()
    {
        _xpSliderFillImage.sprite = _model.XPSliderValue() >= 1f ? _sliderGreenFill : _sliderBlueFill;
    }

    private void UpdateStatItems()
    {
        var items = _model.StatItems();

        var assignedTextCounter = 0;

        for (int i = 0; i < items.Length; i++)
        {
            _uiStatItems[i].SetText(items[i].Text());
            _uiStatItems[i].gameObject.SetActive(true);
            assignedTextCounter++;
        }

        for (int i = assignedTextCounter; i < _uiStatItems.Length; i++)
        {
            _uiStatItems[i].gameObject.SetActive(false);
        }
    }

    private void CloseButtonPressed()
    {
        OnCloseButtonPressed?.Invoke();
    }

    private void LevelUpButtonPressed()
    {
        OnLevelUpButtonPressed?.Invoke();
    }
}