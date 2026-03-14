using servidor.Model;

namespace servidor.dto
{
    public class ScoreDTO
    {
        public float Pontos { get; set; }
        public string? Jogador { get; set; }
        public int Colocacao { get; set; }
        
        public static explicit operator ScoreDTO(Score score)
        {
            return new ScoreDTO { Pontos = score.Pontos, Jogador = score.Jogador };
        }
    }
}
