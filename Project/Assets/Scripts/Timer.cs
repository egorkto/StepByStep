using TMPro;
using UnityEngine;
using System;

public class Timer : MonoBehaviour {
    [SerializeField] private TMP_Text _minutesSeconds;
    [SerializeField] private TMP_Text _milliseconds;

    private float time;
    private bool _enabled = false;

    public void Enable() {
        _enabled = true;
    }

    public void Disable() {
        _enabled = false;
    }

    private void Update() {
        if(_enabled) {
            var span = TimeSpan.FromSeconds(time);
            _minutesSeconds.text = string.Format("{0:D2}:{1:D2}", span.Hours * 60 + span.Minutes, span.Seconds);
            _milliseconds.text = "." + (span.Milliseconds / 10).ToString("00");
            time += Time.deltaTime;
        }
    }
}