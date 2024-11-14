using Depths_Of_Betrayal_Samuel.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depths_Of_Betrayal_Samuel.Dominio
{
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
}
