using servidor.dto;

namespace servidor.Model
{
    public class Score
    {
        public string? Id {  get; set; }
        public float Pontos { get; set; }
        public string? Jogador { get; set; }
        
        public static explicit operator Score(ScoreDTO scoreDTO)
        {
            return new Score { Pontos = scoreDTO.Pontos, Jogador = scoreDTO.Jogador};
        }
    }
}
