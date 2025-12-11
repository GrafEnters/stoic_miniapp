using TMPro;
using UnityEngine;

public class WishPanel : MonoBehaviour {
    [SerializeField]
    private GameObject _afterSent;

    [SerializeField]
    private TMP_InputField _input;

    public void Open() {
        gameObject.SetActive(true);
        _afterSent.SetActive(SaveLoadManager.CurrentSave.MessageSent != null);
    }

    public void Sent() {
        SaveLoadManager.CurrentSave.MessageSent = _input.text;
        SaveLoadManager.SaveGame();
        _afterSent.SetActive(true);
    }
    
    
}