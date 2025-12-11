using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToyView : MonoBehaviour {
    [SerializeField]
    private Image _shape, _mask, _fill, _pattern;

    [SerializeField]
    private List<Sprite> _shapes, _masks, _patterns;

    [SerializeField]
    private List<Color> _shapeColors, _patternColors;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    private ToyData _toyData;

    private RectTransform _parent;
    private RectTransform _self;

    public void SetView(ToyData data, bool isInteractable) {
        _toyData = data;
        _canvasGroup.interactable = isInteractable;
        _canvasGroup.blocksRaycasts = isInteractable;

        _shape.sprite = _shapes[data.ShapeId];
        _mask.sprite = _masks[data.ShapeId];
        _fill.color = _shapeColors[data.FillColorId];

        _pattern.sprite = _patterns[data.PatternId];
        _pattern.color = _patternColors[data.PatternColorId];

        _self = (RectTransform)transform;
        _parent = (RectTransform)transform.parent;
    }

    public void OnDrag(BaseEventData data) {
        PointerEventData eventData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, eventData.position, eventData.pressEventCamera, out Vector2 localPos);

        Rect parentRect = _parent.rect;
        Vector2 halfSize = _self.rect.size * 0.5f;

        Vector2 clamped = new Vector2(Mathf.Clamp(localPos.x, parentRect.xMin + halfSize.x, parentRect.xMax - halfSize.x),
            Mathf.Clamp(localPos.y, parentRect.yMin + halfSize.y, parentRect.yMax - halfSize.y*1.5f));

        _self.localPosition = clamped;
        _toyData.Position = clamped;
        SaveLoadManager.SaveGame();
    }
}