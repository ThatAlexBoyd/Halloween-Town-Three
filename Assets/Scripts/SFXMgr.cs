using UnityEngine;
using System.Collections;

public class SFXMgr : MonoBehaviour
{
    private static SFXMgr instance = null;

    public static SFXMgr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(null != instance && this != instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public enum SoundList { BEEP, GOSOUND, SPINOUT, PERFECTTURN, FIREWORK, CHEER, BGMUSIC, MUAHAHA};

    public AudioSource onshotSource;
    public AudioSource fireworkSource;
    public AudioSource onShotHighSource;

    public AudioClip beep;
    public AudioClip goSound;
    public AudioClip spinOut;
    public AudioClip perfectTurn;
    public AudioClip cheers;
    public AudioClip muahaha;

    public void PlaySound(SoundList soundToPlay)
    {
        switch (soundToPlay)
        {
            case SoundList.BEEP:
                onshotSource.PlayOneShot(beep);
                break;
            case SoundList.GOSOUND:
                onshotSource.PlayOneShot(goSound);
                break;
            case SoundList.SPINOUT:
                onshotSource.PlayOneShot(spinOut);
                break;
            case SoundList.PERFECTTURN:
                onshotSource.PlayOneShot(perfectTurn);
                break;
            case SoundList.FIREWORK:
                fireworkSource.Play();
                break;
            case SoundList.CHEER:
                onShotHighSource.PlayOneShot(cheers);
                break;
            case SoundList.MUAHAHA:
                onshotSource.PlayOneShot(muahaha);
                break;

            default:
                Debug.Log("Couldn't find the sound. Blame Alex.");
                break;
        }
    }

}
