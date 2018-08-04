using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum Player
{
    Player1,
    Player2
}

public class Controller : MonoBehaviour {

    public Player playerSlot;
    public DOTweenPath track;
    public GameManager gameMan;
    private Tween t;
    private int laps;
    public int curLap;
    public UIController ui;
    public GameObject avatar;
    public GameObject spinOutParticle;
    public GameObject perfectTurnParticle;
    public ParticleSystem exhaust;
    public AudioSource carAudio;
    [Header("Car Variables")]
    public float maxSpeed;
    public float accel;
    public float decel;
    public float spinTime;
    private bool canAccel;

	// Use this for initialization
	void Start ()
    {
        t = track.GetTween();
        t.timeScale = 0;
        canAccel = false;
        laps = gameMan.numOfLaps;
	}


    // Update is called once per frame
    void Update()
    {
        if (playerSlot == Player.Player1)
        {
            if (canAccel)
            {
                float vInput = Mathf.Abs(Input.GetAxis("Controller1"));
                t.timeScale = (vInput) * accel;
                if (t.timeScale >= 0)
                {
                    t.timeScale -= decel;
                }
                exhaust.startSpeed = vInput;
            }
            carAudio.volume = t.timeScale * 0.08f; ;
            carAudio.pitch = t.timeScale * 0.09f;
        }
        if (playerSlot == Player.Player2)
        {
            if (canAccel)
            {
                float vInput = Mathf.Abs(Input.GetAxis("Controller2"));
                t.timeScale = (vInput) * accel;
                if (t.timeScale >= 0)
                {
                    t.timeScale -= decel;
                }
                exhaust.startSpeed = vInput;
            }
                carAudio.volume = t.timeScale * 0.07f; ;
                carAudio.pitch = t.timeScale * 0.08f;
        }
    }

    #region Spin

    public void CheckSpin(float spinThresh)
    {
        if (t.timeScale >= spinThresh)
        {
            SpinCar();
        }
        else if(t.timeScale >= spinThresh-2)
        {
            PerfectTurn();
        }
    }

    public void SpinCar()
    {
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.SPINOUT);
        Instantiate(spinOutParticle, avatar.transform.position, Quaternion.identity);
        canAccel = false;
        StartCoroutine(AccelDelay());
        t.timeScale = 0;
        avatar.transform.DOLocalRotate(new Vector3(0,1080, 0), spinTime, RotateMode.FastBeyond360);
    }
    public void PerfectTurn()
    {
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.PERFECTTURN);
        Instantiate(perfectTurnParticle, avatar.transform.position, Quaternion.identity);
    }
    #endregion

    public void FinishedLap()
    {
        curLap++;
       
        if (curLap > laps)
        {
            gameMan.FinishedRace(gameObject.name);
        }
        else
        {
            ui.UpdateLap(playerSlot, curLap);
        }
    }

    public void SlowCar()
    {
        DOTween.To(x => t.timeScale = x, t.timeScale, 0, 2f);
        canAccel = false;
    }

    public void SetCanAccel(bool b)
    {
        canAccel = b;
        if(b)
        {
            carAudio.Play();
        }
    }

    IEnumerator AccelDelay()
    {
        yield return new WaitForSeconds(spinTime + 0.25f);
        canAccel = true;
    }

}
