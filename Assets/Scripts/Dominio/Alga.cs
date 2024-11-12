using Depths_Of_Betrayal_Samuel.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depths_Of_Betrayal_Samuel.Dominio
{
    public class Alga : CollectableObjetc
    {
        public override MapSpot Position { get; set; }

        public Alga() 
        {
            Position = GestionSpots.setSpot();
        }


        public override void pickedUp(Character c)
        {
            if (c.Health < 2)
            {
                c.Health += 1;
            }

            //TODO: calcular coordenadas fuera del mapa
            Position.CoordenadaX = -1000;
            Position.CoordenadaY = -1000;
            Position.CoordenadaZ = -1000;
        }
    }
}
