﻿using System;
using PickaPrato.Business;
using System.Collections.Generic;
using SQLite;

namespace PickaPrato.Business {
    
    public class PratoLite {

        [PrimaryKey]
        public int IdPrato { get; set; }
        public string Designacao { get; set; }
        public string Fotografia { get; set; }
        public string Restaurante { get; set; }


        public PratoLite(int IdPrato, string Designacao, string Fotografia, string Restaurante) {
            this.IdPrato = IdPrato;
            this.Designacao = Designacao;
            this.Fotografia = Fotografia;
            this.Restaurante = Restaurante;
        }

        public PratoLite() {
            
        }
    }
}
