using System.ComponentModel;

namespace NoteKeeper.Dominio.ModuloNota
{
    public enum TemaEnum
    {
        [Description("primary")]
        Basico,

        [Description("accent")]
        Realcada,

        [Description("warn")]
        Advertencia
    }
}
