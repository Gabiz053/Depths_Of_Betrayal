using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Crystal : CollectableObjetc
{

    public override MapSpot Position { get; set; }
    public MapSpot SpotInicial { get; set; }

    public Crystal()
    {
        // SpotInicial = GestionSpots.setSpot(this);
        // Position = SpotInicial;
    }

    public override void pickedUp(Character c)
    {
        c.Inventory.Cristales.Add(this);
        transform.position = new Vector3(0,0,0);
    }

    public void comeback()
    {
        //TODO: que vuelva a su posicion incial
        Position = SpotInicial;
    }

}
