using SQLite;

namespace Inverso_20070401
{
    [Table("inverso_tabla")]
    public class InversoResult
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("valor_a")]
        public string? A { get; set; }

        [Column("valor_b")]
        public string? B { get; set; }

        [Column("valor_c")]
        public string? C { get; set; }

        [Column("valor_d")]
        public string? D { get; set; }

        [Column("resultado_inverso")]
        public string? ResultadoInverso { get; set; }
    }
}