namespace ProEventos.API.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        //public IEnumerable<Lote> Lote { get; set; }
        //public IEnumerable<RedeSocial> RedesSociais { get; set; }
        //public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}