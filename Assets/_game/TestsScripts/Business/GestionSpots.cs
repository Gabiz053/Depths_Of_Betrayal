using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class GestionSpots
{
    public Stack<MapSpot> Spots { get; set; } = new Stack<MapSpot>();

    public GestionSpots()
    {
        //crear 40 spots del mapa
        MapSpot mp1 = new MapSpot(1, 1, 1);
        MapSpot mp2 = new MapSpot(2, 2, 2);
        MapSpot mp3 = new MapSpot(3, 3, 3);
        MapSpot mp4 = new MapSpot(4, 4, 4);
        MapSpot mp5 = new MapSpot(5, 5, 5);
        MapSpot mp6 = new MapSpot(6, 6, 6);
        MapSpot mp7 = new MapSpot(7, 7, 7);
        MapSpot mp8 = new MapSpot(8, 8, 8);
        MapSpot mp9 = new MapSpot(9, 9, 9);
        MapSpot mp10 = new MapSpot(10, 10, 10);
        MapSpot mp11 = new MapSpot(11, 11, 11);
        MapSpot mp12 = new MapSpot(12, 12, 12);
        MapSpot mp13 = new MapSpot(13, 13, 13);
        MapSpot mp14 = new MapSpot(14, 14, 14);
        MapSpot mp15 = new MapSpot(15, 15, 15);

        Spots.Push(mp1);
        Spots.Push(mp2);
        Spots.Push(mp3);
        Spots.Push(mp4);
        Spots.Push(mp5);
        Spots.Push(mp6);
        Spots.Push(mp7);
        Spots.Push(mp8);
        Spots.Push(mp9);
        Spots.Push(mp10);
        Spots.Push(mp11);
        Spots.Push(mp12);
        Spots.Push(mp13);
        Spots.Push(mp14);
        Spots.Push(mp15);
    }

    public MapSpot setSpot(CollectableObjetc o)
    {
        return Spots.Pop();
    }
}
