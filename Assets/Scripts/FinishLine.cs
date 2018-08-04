using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public GameManager gameMan;

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Controller>().FinishedLap();
    }
}
