using UnityEngine;

public class FinishLevelEffector {
    private AudioSource _winSource;
    private AudioSource _loseSource;
    private IMusicTuner _musicTuner;
    private Animator _animator;

    public FinishLevelEffector(AudioSource winSource, AudioSource loseSource, IMusicTuner tuner, Animator animator) {
        _winSource = winSource;
        _loseSource = loseSource;
        _musicTuner = tuner;
        _animator = animator;
    }

    public void EffectWin() {
        _musicTuner.StopMusic();
        _winSource.Play();
        _animator.SetTrigger("LevelFinish");
    }

    public void EffectLose() {
        _musicTuner.StopMusic();
        _loseSource.Play();
        _animator.SetTrigger("LevelFinish");
    }
}