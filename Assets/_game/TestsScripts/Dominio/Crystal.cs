using System;
using UnityEngine;


[Serializable]
public class Crystal : CollectableObjetc
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public override void pickedUp(Character c)
    {
        c.addCrystal(this);
    }

    public void comeback()
    {
        UpdatePositionServerRpc(initialPosition);
    }

}
