﻿using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloNota;

namespace NoteKeeper.Dominio.ModuloCategoria
{
    public class Categoria : Entidade
    {
        public string Titulo { get; set; }

        public List<Nota> Notas { get; set; }
    }
}
