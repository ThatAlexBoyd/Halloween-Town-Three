using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameManager gameMan;
    public TextMeshProUGUI winnerText;
    public GameObject winnerPanel;
    public GameObject countDown;
    public GameObject raceInfoPanel;
    public TextMeshProUGUI countDownText;
    public GameObject mainMenu;
    public TextMeshProUGUI easyTime;
    public TextMeshProUGUI medTime;
    public TextMeshProUGUI hardTime;
    public TextMeshProUGUI chaosTime;
    public TextMeshProUGUI raceTimer;
    public TextMeshProUGUI playerOneLap;
    public TextMeshProUGUI playerTwoLap;
    public GameObject newRecordPanel;


    public void ToggleRaceInfo(bool toShow)
    {
        raceInfoPanel.SetActive(toShow);
    }
    public IEnumerator CountDown()
    {

        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.BEEP);
        yield return new WaitForSeconds(1f);
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.BEEP);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.BEEP);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        SFXMgr.Instance.PlaySound(SFXMgr.SoundList.GOSOUND);
        countDownText.text = "GO";
        gameMan.StartRace();
        yield return new WaitForSeconds(1.5f);
        countDown.SetActive(false);


    }

    internal void SetTimes()
    {
        easyTime.text = "Best Time" + "\n" + FormatTime(PlayerPrefs.GetFloat(Difficulty.Easy.ToString()));
        medTime.text = "Best Time" + "\n" + FormatTime(PlayerPrefs.GetFloat(Difficulty.Medium.ToString()));
        hardTime.text = "Best Time" + "\n" + FormatTime(PlayerPrefs.GetFloat(Difficulty.Hard.ToString()));
        chaosTime.text = "Best Time" + "\n" + FormatTime(PlayerPrefs.GetFloat(Difficulty.Chaos.ToString()));
    }

    private string FormatTime(float v)
    {
        return v.ToString("00.00");
    }

    public void ToggleWinPanel(bool toShow, string winner = "")
    {
        winnerPanel.SetActive(toShow);
        winnerText.text = winner;
    }

    public void UpdateTimer(float t)
    {
        raceTimer.text = FormatTime(t);
    }

    internal void UpdateLap(Player slot,int curLap)
    {
     switch(slot)
        {
            case Player.Player1:
                playerOneLap.text = curLap.ToString() + "/3";
                break;
            case Player.Player2:
                playerTwoLap.text = curLap.ToString() + "/3";
                break;
        }
    }

    public void NewRecord()
    {
        newRecordPanel.SetActive(true);
    }
}
