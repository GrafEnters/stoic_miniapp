using System;
using System.Collections.Generic;
using UnityEngine;

public class DecoratePanel : MonoBehaviour {
    [SerializeField]
    private List<Sprite> _treeParts;

    [SerializeField]
    private ToyView _prefab;

    [SerializeField]
    private Transform _myTreePart;

    private void Start() {
        var data = SaveLoadManager.CurrentSave.Toys;
        foreach (var item in data) {
            AddToy(item);
        }
    }

    public void AddToy(ToyData data) {
        var toy = Instantiate(_prefab, _myTreePart);
        toy.SetView(data, true);
        toy.transform.localPosition = data.Position;
    }
}