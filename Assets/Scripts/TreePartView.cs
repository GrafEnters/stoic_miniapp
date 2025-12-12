using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreePartView : MonoBehaviour {
    [SerializeField]
    private List<Sprite> _treeParts;

    [SerializeField]
    private ToyView _prefab;

    [SerializeField]
    private Image _back;

    private bool _isPlayer;
    private TreePartData _data;

    public int ToysAmount => _data.Toys.Count;

    public void SetData(TreePartData data, bool isPlayer) {
        _data = data;
        _isPlayer = isPlayer;
        foreach (var item in data.Toys) {
            CreateToyView(item);
        }
    }

    public void AddToy(ToyData data) {
        CreateToyView(data);
        _data.Toys.Add(data);
    }

    private void CreateToyView(ToyData data) {
        var toy = Instantiate(_prefab, transform);
        toy.SetView(data, _isPlayer);
        toy.transform.localPosition = data.Position;
    }

    public void RemoveToy() {
        var view = transform.GetComponentInChildren<ToyView>();
        Destroy(view.gameObject);
        _data.Toys.RemoveAt(0);
    }
}