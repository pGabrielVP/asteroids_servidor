using Microsoft.AspNetCore.Mvc;
using servidor.Data;
using servidor.dto;

namespace servidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreBoardController(IScoreBoardDAO scoreboardDAO) : ControllerBase
    {
        private readonly IScoreBoardDAO _scoreboardDAO = scoreboardDAO;
        
        [HttpGet]
        public ActionResult<List<ScoreDTO>> GetAll()
        {
            return Ok(_scoreboardDAO.GetAll());
        }
        [HttpGet("{n}")]
        public ActionResult<List<ScoreDTO>> GetTopN(int n)
        {
            return Ok(_scoreboardDAO.GetTopN(n));
        }
        [HttpGet("score/{id}")]
        public ActionResult<ScoreDTO> GetById(string id)
        {
            var scoreDTO = _scoreboardDAO.GetById(id);
            if (scoreDTO == null)
                return NotFound();
            return Ok(scoreDTO);
        }
        [HttpPost]
        public ActionResult<ScoreDTO> SubmitScore(ScoreDTO scoreDTO)
        {
            if (scoreDTO == null)
                return BadRequest();
            var tuple = _scoreboardDAO.Create(scoreDTO);
            if (tuple == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = tuple.Value.Id }, tuple.Value.ScoreDTO);
        }
    }
}
