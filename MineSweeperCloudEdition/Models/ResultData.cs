using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class ResultData
    {
        DatabaseComs resultsDAL = new DatabaseComs();
        public void addResult(ResultsDTO rDTO)
        {
            resultsDAL.addResult(rDTO);
        }

        public IEnumerable<ResultsDTO> GetAllPlayers()
        {
            IEnumerable<ResultsDTO> allResults = resultsDAL.GetAllResults();
            return allResults;
        }
    }
}
