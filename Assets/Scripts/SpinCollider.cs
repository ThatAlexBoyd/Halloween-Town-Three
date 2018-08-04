using UnityEngine;
using System.Collections;
using System;

public class SpinCollider : MonoBehaviour
{
    public float easyThresh;
    public float medThresh;
    public float hardThresh;
    private float speedThreshold;
    private GameManager gameMan;

    private void Awake()
    {
        gameMan = GameObject.FindObjectOfType<GameManager>();
    }

    public void Init(Difficulty dif)
    {
        switch(dif)
        {
            case Difficulty.Easy:
                speedThreshold = easyThresh;
                break;
            case Difficulty.Medium:
                speedThreshold = medThresh;
                break;
            case Difficulty.Hard:
                speedThreshold = hardThresh;
                break;
            case Difficulty.Chaos:
                speedThreshold = RandomThresh();
                break;
        }
    }

    private float RandomThresh()
    {
        return UnityEngine.Random.Range(4, 8);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Controller>().CheckSpin(speedThreshold);
    }
}
