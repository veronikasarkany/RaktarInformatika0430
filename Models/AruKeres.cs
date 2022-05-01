using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RaktarInformatika0430.Models
{
    public class AruKeres
    {
        public string Megnevezes { get; set; }

        public string Beszallito { get; set; }

        public List<Aru> AruLista { get; set; }

        public SelectList BeszallitoLista { get; set; }


    }
    
}
