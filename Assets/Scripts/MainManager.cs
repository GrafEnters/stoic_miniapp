using System;
using UnityEngine;

public class MainManager : MonoBehaviour {
    [SerializeField]
    private DecoratePanel _decoratePanel;

    private void Start() {
        _decoratePanel.Init();
    }
}