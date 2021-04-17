using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class ResultData
    {
        //business layer class that pass data between the controller and database communications layer.
        DatabaseComs resultsDAL = new DatabaseComs();
        public void addResult(ResultsDTO rDTO)
        {
            resultsDAL.addResult(rDTO);
        }

        public IEnumerable<ResultsDTO> GetAllResults()
        {
            IEnumerable<ResultsDTO> allResults = resultsDAL.GetAllResults();
            return allResults;
        }
    }
}
