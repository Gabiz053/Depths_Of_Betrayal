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
        UpdatePositionServerRpc(new Vector3(100,100,100));
    }

    public void comeback()
    {
        UpdatePositionServerRpc(initialPosition);
    }

}
