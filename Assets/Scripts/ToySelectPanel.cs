using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToySelectPanel : MonoBehaviour {
    [SerializeField]
    private Image _shapePreview, _shapePreview2, _shapeColorPreview, _patternPreview, _patternPreview2;

    [SerializeField]
    private List<Sprite> _shapes, _patterns;

    [SerializeField]
    private List<Color> _shapeColors, _patternColors;

    [SerializeField]
    private List<Image> _toggleShapeColors, _togglePatternColors;

    [SerializeField]
    private ToyView _toyView;

    private ToyData _toyData = new ToyData();

    [SerializeField]
    private DecoratePanel _decoratePanel;

    private void Start() {
        for (int i = 0; i < _shapeColors.Count; i++) {
            _toggleShapeColors[i].color = _shapeColors[i];
        }

        for (int i = 0; i < _togglePatternColors.Count; i++) {
            _togglePatternColors[i].color = _patternColors[i];
        }

        SelectShape(0);
        SelectPattern(0);
        SelectShapeColor(0);
        SelectPatternColor(0);
    }

    public void Open() {
        gameObject.SetActive(true);
        _toyData = new ToyData();
    }

    public void SelectShape(int index) {
        _shapePreview.sprite = _shapes[index];
        _shapePreview2.sprite = _shapes[index];
        _toyData.ShapeId = index;
    }

    public void SelectPattern(int index) {
        _patternPreview.sprite = _patterns[index];
        _toyData.PatternId = index;
    }

    public void SelectShapeColor(int index) {
        _shapePreview.color = _shapeColors[index];
        _shapeColorPreview.color = _shapeColors[index];
        _toyData.FillColorId = index;
    }

    public void SelectPatternColor(int index) {
        _patternPreview.color = _patternColors[index];
        _patternPreview2.color = _patternColors[index];
        _toyData.PatternColorId = index;
    }

    public void EndEditingToy() {
        gameObject.SetActive(false);
        _decoratePanel.AddToy(_toyData);
    }
}

[Serializable]
public class ToyData {
    public int ShapeId;
    public int PatternId;
    public int FillColorId;
    public int PatternColorId;
    public Vector2 Position = Vector2.zero;
}