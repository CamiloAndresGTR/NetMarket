using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    public class TradeMarksController : BaseApiController
    {
        private readonly IGenericRepository<TradeMark> _tradeMarkRepository;
        public TradeMarksController(IGenericRepository<TradeMark> tradeMarkRepository)
        {
            _tradeMarkRepository = tradeMarkRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TradeMark>>> GetTrademarks()
        {
            var tradeMarks = await _tradeMarkRepository.GetAllAsync();
            return Ok(tradeMarks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TradeMark>> GetTradeMark(int id)
        {
            return await _tradeMarkRepository.GetByIdAsync(id);
        }

    }
}
