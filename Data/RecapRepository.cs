using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RecapRepository : DatabaseConnection, IRecapRepository
    {
        public RecapRepository(string connectionString) : base(connectionString)
        {

        }
        void IRecapRepository.AddRecap(Recap recap)
        {
            throw new NotImplementedException();
        }

        void IRecapRepository.DeleteRecap(int id)
        {
            throw new NotImplementedException();
        }

        Recap? IRecapRepository.GetRecapById(int id)
        {
            throw new NotImplementedException();
        }

        List<Recap> IRecapRepository.GetRecaps()
        {
            throw new NotImplementedException();
        }

        void IRecapRepository.UpdateRecap(Recap recap)
        {
            throw new NotImplementedException();
        }
    }
}
