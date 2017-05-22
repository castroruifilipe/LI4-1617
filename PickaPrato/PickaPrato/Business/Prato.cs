﻿using System;
using PickaPrato.Business;
using System.Collections.Generic;

namespace PickaPrato.Business {

    public class Prato {

        private int idPrato { get; set; }
        private string designacao { get; set; }
        private List<string> photos { get; set; } //terá de ser array para ter mais que uma foto
        private Dictionary<Ingrediente,bool> ingredientes { get; set; }


        public Prato(int idPrato, string designacao, List<string> photos, Dictionary<Ingrediente,bool> ingredientes )
        {
            this.idPrato = idPrato;
            this.designacao = designacao;
            //this.photo = Resource.Drawable.Francesinha1;
            this.photos = photos;
            this.ingredientes = ingredientes;

        }
    }
}
