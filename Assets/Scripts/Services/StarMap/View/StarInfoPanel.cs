using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StarInfoPanel : UIPanel {
    [SerializeField] private TextMeshProUGUI _coordText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Button _travelButton;
    [SerializeField] private TextMeshProUGUI _travelButtonText;
    
    [SerializeField] private TextMeshProUGUI _biomeText;
    [SerializeField] private TextMeshProUGUI _SizeText;
    [SerializeField] private TextMeshProUGUI _modulesText;
    public event Action OnTravelRequested;

    protected override void Awake() {
        base.Awake();
        _travelButton.onClick.AddListener(HandleTravelClicked);
    }

    public void SetStarInfo(Star star) {
        if (_infoText != null) {
            _infoText.text = $"{star.Name}";
        }
        if (_coordText != null) {
            _coordText.text = $"Coordinates: {star.Coord}";
        }

        if (_biomeText != null) {
            string displayName = star.PlanetConfig.biomeData.displayName;
            _biomeText.text = $"Biome: {displayName}";
        }

        if (_SizeText != null) {
            float normalizedVolume = star.PlanetConfig.normalizedVolume;
            _SizeText.text = $"Size: {GetSize(normalizedVolume).ToString()}";
        }

        if (_modulesText != null) {
            List<string> modulesList = star.PlanetConfig.modulesList;
            _modulesText.text = GetModulesText(modulesList);
        }
    }

    private string GetModulesText(List<string> modulesList) {
        string resultString = "";

        if (modulesList == null || modulesList.Count > 0) {
            resultString = $"Modules: " + string.Join(", ", modulesList);
        }
        return resultString;
    }

    private Sizes GetSize(float normalizedValue) {
        if (normalizedValue > 0.7f) {
            return Sizes.Large;
        } else if (normalizedValue > 0.5) {
            return Sizes.Medium;
        }

        return Sizes.Small;
    }

    public void SetTravelAvailable(bool available) {
        if (_travelButton != null) {
            _travelButton.interactable = available;
        }
    }

    public void SetTravelButtonText(string text) {
        if (_travelButtonText != null) {
            _travelButtonText.text = text;
        }
    }

    private void HandleTravelClicked() {
        OnTravelRequested?.Invoke();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        _travelButton.onClick.RemoveListener(HandleTravelClicked);
    }
}

public enum Sizes {
    Small,
    Medium,
    Large
}