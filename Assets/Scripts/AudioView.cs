using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour {
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private TextMeshProUGUI _timeText;

    [SerializeField]
    private Slider _slider;

    private void Start() {
        _slider.SetValueWithoutNotify(_audioSource.time / _audioSource.clip.length);
    }

    public void OnInput(float percent) {
        _audioSource.time = _audioSource.clip.length * Mathf.Min(percent, 0.99f);
    }

    public void Play() {
        _audioSource.time = 0;
        _audioSource.Play();
    }

    private void Update() {
        if (_audioSource.isPlaying) {
            _slider.SetValueWithoutNotify(_audioSource.time / _audioSource.clip.length);
            _timeText.text = $"00:{Mathf.FloorToInt(_audioSource.time)}";
        } else {
            _timeText.text = $"00:0{Mathf.FloorToInt(_audioSource.clip.length)}";
        }
    }
}