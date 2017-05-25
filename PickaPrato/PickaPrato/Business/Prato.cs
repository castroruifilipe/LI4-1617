﻿using System;
using PickaPrato.Business;
using System.Collections.Generic;

namespace PickaPrato.Business {
    
    public class Prato {

        public int IdPrato { get; set; }
        public string Designacao { get; set; }
        public string TipoComida { get; set; }
        public double Preco { get; set; }
        public double Classificacao { get; set; }
        public string Fotografia { get; set; }
        public Restaurante Restaurante { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
        public List<Classificacao> Classificacoes { get; set; }


        public Prato(int IdPrato, string Designacao, string TipoComida, double Preco, string Fotografia,
                     Restaurante Restaurante, List<Ingrediente> Ingredientes) {
            this.IdPrato = IdPrato;
            this.Designacao = Designacao;
            this.TipoComida = TipoComida;
            this.Preco = Preco;
            this.Fotografia = Fotografia;
            this.Restaurante = Restaurante;
            this.Ingredientes = Ingredientes;
            this.Classificacao = 0;
            this.Classificacoes = null;
        }

		public Prato(string Designacao, string TipoComida, double Preco, string Fotografia,
					 Restaurante Restaurante, List<Ingrediente> Ingredientes)
		{
            this.IdPrato = 0;
			this.Designacao = Designacao;
			this.TipoComida = TipoComida;
			this.Preco = Preco;
			this.Fotografia = Fotografia;
			this.Restaurante = Restaurante;
			this.Ingredientes = Ingredientes;
			this.Classificacao = 0;
			this.Classificacoes = null;
		}

        public Prato() {
            
        }
    }
}
