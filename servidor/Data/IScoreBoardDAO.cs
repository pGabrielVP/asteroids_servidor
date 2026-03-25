using servidor.dto;

namespace servidor.Data
{
    public interface IScoreBoardDAO
    {
        List<ScoreDTO> GetAll();
        List<ScoreDTO> GetTopN(int n);
        ScoreDTO? GetById(string Id);
        (string Id, ScoreDTO ScoreDTO)? Create(ScoreDTO scoreDTO);
        List<ScoreDTO> PaginatedList(string idUltimoRegistro, int tamanhoPagina);
    }
}
