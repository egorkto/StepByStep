using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicTuner : MonoBehaviour, IMusicTuner
{
    [SerializeField] private float _smoothAmount;
    [SerializeField] private float _musicDelay;
    [SerializeField] private AudioSource _ambientSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioClip> _musics;

    private List<AudioClip> _remainingSongs = new List<AudioClip>();
    private float _startAmbientVolume;
    private float _startMusicVolume;

    public void StopMusic() {
        StopAllCoroutines();
        _musicSource.Stop();
        _ambientSource.Stop();
    }

    public void PauseMusic() {
        _musicSource.Pause();
        _ambientSource.Pause();
    }

    public void UnpauseMusic() {
        _musicSource.UnPause();
        _ambientSource.UnPause();
    }

    private void Start()
    {   
        _startAmbientVolume = _ambientSource.volume;
        _startMusicVolume = _musicSource.volume;
        StartCoroutine(MusicCycle());
    }

    private IEnumerator MusicCycle() {
        _ambientSource.volume = 0;
        _musicSource.volume = 0;
        _ambientSource.Play();
        while(true) {
            yield return StartCoroutine(SmoothTransit(_ambientSource, _startAmbientVolume));
            yield return new WaitForSeconds(_musicDelay);
            SetRandomMusic();
            _musicSource.Play();
            yield return StartCoroutine(SmoothSwitch(_ambientSource, _musicSource, _startMusicVolume));
            _ambientSource.Pause();
            yield return new WaitUntil(() => _musicSource.time == 0);
            _musicSource.Stop();
            _musicSource.volume = 0f;
            _ambientSource.UnPause();
        }
    }

    private void SetRandomMusic() {
        if(_remainingSongs.Count == 0) {
            foreach(var clip in _musics) {
                _remainingSongs.Add(clip);
            }
        }
        var currentClip = _remainingSongs[Random.Range(0, _remainingSongs.Count - 1)];
        _musicSource.clip = currentClip;
        _remainingSongs.Remove(currentClip);
    }

    private IEnumerator SmoothSwitch(AudioSource from, AudioSource to, float targetVolume) {
        var fromStartVolume = from.volume;
        var percents = 0f;
        while(from.volume > 0 || to.volume < targetVolume) {
            percents = Mathf.MoveTowards(percents, 100, _smoothAmount * Time.deltaTime);
            from.volume = math.remap(0, 100, fromStartVolume, 0, percents);
            to.volume = math.remap(0, 100, 0, targetVolume, percents);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SmoothTransit(AudioSource source, float targetVolume) {
        var startVolume = source.volume;
        var percents = 0f;
        while(source.volume < targetVolume) {
            percents = Mathf.MoveTowards(percents, 100, _smoothAmount * Time.deltaTime);
            source.volume = math.remap(0, 100, startVolume, targetVolume, percents);
            yield return new WaitForEndOfFrame();
        }
    }
}
