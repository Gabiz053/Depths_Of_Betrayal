using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Platform
{
    public MapSpot Position { get; set; }
    //Contador de cristales recogidos
    public int Crystals { get; set; } = 0;

    public void pickUp(List<Crystal> cristales)
    {
        //TODO: contador de cristales sube
        Crystals += cristales.Count;

    }
}
