using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  UnityEngine;


public abstract class CollectableObjetc : MonoBehaviour
{
    public abstract MapSpot Position { get; set; }

    public abstract void pickedUp(Character c);

}
