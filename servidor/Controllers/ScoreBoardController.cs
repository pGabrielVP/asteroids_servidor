using Microsoft.AspNetCore.Mvc;
using servidor.dto;
using servidor.Model;

namespace servidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreBoardController : ControllerBase
    {
        private static readonly IComparer<float> _scoreComparer = Comparer<float>.Create((x, y) => { var res = x.CompareTo(y); if (res == 0) { return -1; } else { return res * -1; } });
        private static readonly SortedList<float, Score> _scores = new(_scoreComparer);
        
        [HttpGet]
        public ActionResult<List<ScoreDTO>> getAll()
        {
            return Ok(_scores.Values.Select<Score, ScoreDTO>((s, i) => {
                var sDTO = new ScoreDTO
                {
                    Jogador = s.Jogador,
                    Pontos = s.Pontos,
                    Colocacao = i + 1
                };
                return sDTO;
            }
            ));
        }
        [HttpGet("{n}")]
        public ActionResult<List<ScoreDTO>> getTopN(int n)
        {
            return Ok(_scores.Values.Take<Score>(n).Select<Score, ScoreDTO>((s, i) => {
                var sDTO = new ScoreDTO
                {
                    Jogador = s.Jogador,
                    Pontos = s.Pontos,
                    Colocacao = i + 1
                };
                return sDTO; 
            }
            ));
        }
        [HttpGet("score/{id}")]
        public ActionResult<ScoreDTO> getById(int id)
        {
            var score = _scores.Values.FirstOrDefault(s => s.id == id);
            if ( score == null )
                return NotFound();
            var scoreDTO = new ScoreDTO
            {
                Jogador = score.Jogador,
                Pontos = score.Pontos,
                Colocacao = _scores.Values.IndexOf(score) + 1
            };
            return Ok(scoreDTO);
        }
        [HttpPost]
        public ActionResult<ScoreDTO> submitScore(ScoreDTO scoreDTO)
        {
            if (scoreDTO == null)
                return BadRequest();
            var score = new Score
            {
                id = new Random().Next(0, 10000),
                Pontos = scoreDTO.Pontos,
                Jogador = scoreDTO.Jogador
            };
            _scores.Add(score.Pontos, score);
            scoreDTO.Colocacao = _scores.Values.IndexOf(score) + 1;
            return CreatedAtAction(nameof(getById), new { id = score.id }, scoreDTO);
        }
    }
}
