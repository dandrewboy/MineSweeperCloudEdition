using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using MineSweeperCloudEdition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsAPIController : ControllerBase
    {
        DatabaseComs resultData = new DatabaseComs();

        [HttpGet]
        [ResponseType(typeof(List<ResultsDTO>))]
        public IEnumerable<ResultsDTO> Index()
        {
            IEnumerable<ResultsDTO> resultList = resultData.GetAllResults();

            IEnumerable<ResultsDTO> ResultDTOList = from p in resultList select new ResultsDTO(p.PlayerId, p.Results, p.Time, p.Clicks);

            return ResultDTOList;
        }
    }
}
