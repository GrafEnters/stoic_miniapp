using System;
using UnityEngine;

public class MainManager : MonoBehaviour {
    [SerializeField]
    private DecoratePanel _decoratePanel;

    private void Start() {
        WebGLInput.captureAllKeyboardInput = false;
        _decoratePanel.Init();
    }
}