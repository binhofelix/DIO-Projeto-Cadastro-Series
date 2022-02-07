using System;

namespace DIO.Series
{
    public class Serie : EntidadeBase
    {        
        public Genero Genero {get; set;}
        public string Titulo {get; set;}
        public string Descricao {get; set;}
        public int Ano {get; set;}
        public bool Excluido {get; set;}

        public Serie(int id, Genero genero, string titulo, string desc, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = desc;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            //Environment.NewLine; //Percente a classe System. Como o S.O. interpreta uma quebra de linha, se é \n etc.
            string retorno = "";
            retorno +="Gênero: " + this.Genero + Environment.NewLine;
            retorno +="Título: " + this.Titulo + Environment.NewLine;
            retorno +="Descrição: " + this.Descricao + Environment.NewLine;
            retorno +="Ano de exibição: " + this.Ano + Environment.NewLine;
            retorno +="Excluído: " + this.Excluido + Environment.NewLine;
            return retorno;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public int retornaId()
        {
            return this.Id;
        }
        public bool retornaExcluido()
        {
            return this.Excluido;
        }        
        public void Exclui()
        {
            this.Excluido = true;
        }
        
    }

    public class JsonSerie 
    {
        public Genero Genero {get; set;}
        public string Titulo {get; set;}
        public string Descricao {get; set;}
        public int Ano {get; set;}
        public bool Excluido {get; set;}       
    }

}