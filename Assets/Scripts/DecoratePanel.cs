using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecoratePanel : MonoBehaviour {
    [SerializeField]
    private ToyView _prefab;

    [SerializeField]
    private TreePartView _treePartPrefab;

    private TreePartView _myTreePart;

    [SerializeField]
    private Transform _treeContainer;

    [SerializeField]
    private Transform _treeBottom, _treeTop;

    [SerializeField]
    private TextMeshProUGUI _counterText;

    [SerializeField]
    private GameObject _tools, _leaveButton;

    [SerializeField]
    private int _maxToys;

    [SerializeField]
    private ScrollRect _scrollRect;

    public async UniTask Init() {
        var treeData = await StoicServerApi.GetTreeData();
        string currentPlayerId = SaveLoadManager.PlayerId;
        if (treeData != null) {
            foreach (var partData in treeData.Parts) {
                var part = Instantiate(_treePartPrefab, _treeContainer);
                part.SetData(partData, partData.PlayerId == currentPlayerId);
                if (partData.PlayerId == currentPlayerId) {
                    _myTreePart = part;
                }
            }
        }

        if (_myTreePart == null) {
            _myTreePart = Instantiate(_treePartPrefab, _treeContainer);
            _myTreePart.SetData(SaveLoadManager.CurrentSave.TreePartData, true);
        }

        _treeBottom.SetAsFirstSibling();
        _treeTop.SetAsLastSibling();

        UpdateToolsAndCounter();
    }

    private void Start() {
        ScrollToMyPart();
    }

    private void ScrollToMyPart() {
        var content = _scrollRect.content;
        var viewport = _scrollRect.viewport;

        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;

        float itemPosY = Mathf.Abs(_myTreePart.GetComponent<RectTransform>().anchoredPosition.y);
        float itemHeight = _myTreePart.GetComponent<RectTransform>().rect.height;

// Центрируем элемент по вертикали
        float targetPos = (itemPosY + itemHeight * 0.5f - viewportHeight * 0.5f) / (contentHeight - viewportHeight);
        targetPos = Mathf.Clamp01(targetPos);

        _scrollRect.normalizedPosition = new Vector2(0f, 1f - targetPos);
    }

    public void AddToy(ToyData data) {
        _myTreePart.AddToy(data);
        SaveLoadManager.SaveGame();
        UpdateToolsAndCounter();
    }

    private void UpdateToolsAndCounter() {
        int amount = _myTreePart.ToysAmount;
        _leaveButton.gameObject.SetActive(amount == _maxToys);
        _tools.gameObject.SetActive(amount != 0);
        _counterText.text = $"{_myTreePart.ToysAmount}/{_maxToys}";
    }

    public void RemoveToy() {
        _myTreePart.RemoveToy();
        SaveLoadManager.SaveGame();
        UpdateToolsAndCounter();
    }
}