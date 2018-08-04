using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator: MonoBehaviour {

    public float rotSpeed;
    public Transform spinnything;

    public void ChangeRotSpeed(float newRot)
    {
        rotSpeed = newRot;
    }

    void Update()
    {
        spinnything.Rotate(new Vector3(rotSpeed, 0, 0));
    }

}
