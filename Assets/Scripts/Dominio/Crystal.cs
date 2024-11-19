using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //TODO: calcular coordenadas fuera del mapa
        Position.CoordenadaX = -1000;
        Position.CoordenadaY = -1000;
        Position.CoordenadaZ = -1000;
    }

    public void comeback()
    {
        //TODO: que vuelva a su posicion incial
        Position = SpotInicial;
    }

}
