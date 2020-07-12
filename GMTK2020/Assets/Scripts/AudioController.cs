using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is just attached to the gamecontroller prefab, no individual prefab for audio controller
public class AudioController : MonoBehaviour{
    public static AudioController Instance;
    public AudioSource ScoreBallBounce;
    public AudioSource ScoreAPoint;
    public AudioSource LostAPoint;
    public AudioSource SwitchBallBounce;
    public AudioSource SwitchBallHit;
    public AudioSource SwitchBallExplode;
    public AudioSource YouWinSound;
    public AudioSource YouLoseSound;
    public AudioSource PlayerPointLoss;
    public AudioSource OpponentScore;
    public AudioSource SpawnerSound;

    private void Awake() {
        Instance = this;
    }
    
    public void PlaySound(SFXType sfxType) {
        AudioSource source = null;
        switch (sfxType) {
            case SFXType.ScoreBallBounce:
                source = ScoreBallBounce;
                break;
            case SFXType.ScoreAPoint:
                source = ScoreAPoint;
                break;
            case SFXType.LostAPoint:
                source = LostAPoint;
                break;
            case SFXType.SwitchBallBounce:
                source = SwitchBallBounce;
                break;
            case SFXType.SwitchBallHit:
                source = SwitchBallHit;
                break;
            case SFXType.SwitchBallExplode:
                source = SwitchBallExplode;
                break;
            case SFXType.YouWinSound:
                source = YouWinSound;
                break;
            case SFXType.YouLoseSound:
                source = YouLoseSound;
                break;
            case SFXType.PlayerPointLoss:
                source = PlayerPointLoss;
                break;
            case SFXType.OpponentScore:
                source = OpponentScore;
                break;
            case SFXType.SpawnerSound:
                source = SpawnerSound;
                break;
            default:
                sfxType = SFXType.None;
                break;
        }
        if (source !=null) {
            PlayAudioSource(source);
        }
    }
    private void PlayAudioSource(AudioSource source) {
        if (source.isPlaying) {
            source.Stop();
        }
        source.Play();
    }

}
