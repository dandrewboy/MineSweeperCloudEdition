using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using MineSweeperCloudEdition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace MineSweeperCloudEdition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsAPIController : ControllerBase
    {
        ResultData resultData = new ResultData();

        [HttpGet]
        [ResponseType(typeof(List<ResultsDTO>))]
        public IEnumerable<ResultsDTO> Index()
        {
            //returns all of the results
            IEnumerable<ResultsDTO> resultList = resultData.GetAllResults();

            //returns results ordered by win condition then shortest amount of time.
            IEnumerable<ResultsDTO> ResultDTOList = from p in resultList.OrderByDescending(p=>p.Results).ThenBy(p=>p.Time) select new ResultsDTO(p.PlayerId, p.Results, p.Time, p.Clicks);

            return ResultDTOList;
        }
    }
}
