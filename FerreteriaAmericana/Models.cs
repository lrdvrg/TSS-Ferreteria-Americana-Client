namespace FerreteriaAmericana
{
    internal class Header
    {
        public Header()
        {
        }
        public int IdHeader { get; set; }

        public string Proceso { get; set; }
        public int RNC { get; set; }
        public System.DateTime PeriodoAutodeterminacion { get; set; }
        public System.DateTime FechaTransmision { get; set; }
    }

    internal class HeaderDTO
    {
        public HeaderDTO()
        {
        }

        public string Proceso { get; set; }
        public int RNC { get; set; }
        public System.DateTime PeriodoAutodeterminacion { get; set; }
        public System.DateTime FechaTransmision { get; set; }
    }

    internal class Detail
    {
        public Detail()
        {
        }

        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public decimal Salario { get; set; }
        public string Moneda { get; set; }
        public int IdHeader { get; set; }
    }
}