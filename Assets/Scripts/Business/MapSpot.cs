using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MapSpot
{
    public int CoordenadaX { get; set; }
    public int CoordenadaY { get; set; }
    public int CoordenadaZ { get; set; }

    public MapSpot(int coordenadaX, int coordenadaY, int coordenadaZ)
    {
        CoordenadaX = coordenadaX;
        CoordenadaY = coordenadaY;
        CoordenadaZ = coordenadaZ;
    }
}
