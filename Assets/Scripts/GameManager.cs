using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public enum Difficulty { Easy, Medium, Hard, Chaos}

public class GameManager : MonoBehaviour
{
    public List<Controller> players = new List<Controller>();
    public List<SpinCollider> spinners = new List<SpinCollider>();
    public GameObject finLine;
    public int numOfLaps;
    private bool raceOver;
    public Difficulty gameDifficulty;
    public UIController ui;
    public GameObject fireworkHolder;

    private bool keepTime;
    public float curTimer;


    public void Awake()
    {
        raceOver = true;
        ui.ToggleWinPanel(false);
        ui.ToggleRaceInfo(false);
        ui.SetTimes();

    }

    public void FixedUpdate()
    {
        if(keepTime)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRecords();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Celebrate();
        }

    }

    private void UpdateTimer()
    {
        curTimer += Time.deltaTime;
        ui.UpdateTimer(curTimer);
    }

    public void SetupRace(int dif)
    {
        if (raceOver)
        {
            gameDifficulty = (Difficulty)dif;

            SetupSpinners();
            ui.countDown.SetActive(true);
            ui.mainMenu.SetActive(false);
            StartCoroutine(ui.CountDown());

            if (gameDifficulty == Difficulty.Chaos)
            {
                SFXMgr.Instance.PlaySound(SFXMgr.SoundList.MUAHAHA); 
            }

        }
    }

    private void SetupSpinners()
    {
        for (int i = 0; i < spinners.Count; i++)
        {
            spinners[i].Init(gameDifficulty);
        }
    }



    public void StartRace()
    {
        raceOver = false;
        keepTime = true;
        ui.ToggleRaceInfo(true);
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetCanAccel(true);
        }

    }

    public void EndRace(string winPlayer)
    {
        finLine.SetActive(false);
        ui.ToggleWinPanel(true, winPlayer);
        keepTime = false;
        RecordTime(curTimer);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SlowCar();
        }
        Celebrate();
        StartCoroutine(Reload());
    }

    private void RecordTime(float curTimer)
    {
        if (curTimer < PlayerPrefs.GetFloat(gameDifficulty.ToString()))
        {
            PlayerPrefs.SetFloat(gameDifficulty.ToString(), curTimer);
            ui.NewRecord();
        } 
        
        curTimer = 0;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Main");
    }
     
    internal void FinishedRace(string playerName)
    {

        if(!raceOver)
        {
            EndRace(playerName);
        }
        raceOver = true;
    }

    public void ResetRecords()
    {
        PlayerPrefs.SetFloat(Difficulty.Easy.ToString(), 999.99f);
        PlayerPrefs.SetFloat(Difficulty.Medium.ToString(), 999.99f);
        PlayerPrefs.SetFloat(Difficulty.Hard.ToString(), 999.99f);
        PlayerPrefs.SetFloat(Difficulty.Chaos.ToString(), 999.99f);
        StartCoroutine(Reload());
    }

    public void Celebrate()
    {
        fireworkHolder.SetActive(true) ;
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.FIREWORK);
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.CHEER);
    }

}
