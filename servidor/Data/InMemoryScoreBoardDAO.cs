using servidor.dto;
using servidor.Model;

namespace servidor.Data
{
    public class InMemoryScoreBoardDAO : IScoreBoardDAO
    {
        private static readonly IComparer<float> _scoreComparer = Comparer<float>.Create((x, y) => { var res = x.CompareTo(y); if (res == 0) { return -1; } else { return res * -1; } });
        private readonly SortedList<float, Score> _scores = new(_scoreComparer);
        
        public (string Id, ScoreDTO ScoreDTO)? Create(ScoreDTO scoreDTO)
        {
            if (scoreDTO == null)
                return null;
            var score = (Score)scoreDTO;
            score.Id = Guid.NewGuid().ToString();
            _scores.Add(score.Pontos, score);
            scoreDTO.Colocacao = _scores.Values.IndexOf(score) + 1;
            return ( score.Id, scoreDTO );
        }
        public List<ScoreDTO> GetAll()
        {
            return [.. _scores.Values.Select<Score, ScoreDTO>((score, i) =>
            {
                var scoreDTO = (ScoreDTO)score;
                scoreDTO.Colocacao = i + 1;
                return scoreDTO;
            })];
        }
        public ScoreDTO? GetById(string Id)
        {
            var score = _scores.Values.FirstOrDefault(s => s.Id == Id);
            if (score == null)
                return null;
            var scoreDTO = (ScoreDTO)score;
            scoreDTO.Colocacao = _scores.Values.IndexOf(score) + 1;
            return scoreDTO;
        }
        public List<ScoreDTO> GetTopN(int n)
        {
            return [.. _scores.Values.Take<Score>(n).Select<Score, ScoreDTO>((score, i) =>
            {
                var scoreDTO = (ScoreDTO)score;
                scoreDTO.Colocacao = i + 1;
                return scoreDTO;
            })];
        }
    }
}
