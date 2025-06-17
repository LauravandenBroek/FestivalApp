using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILineUpRepository
    {
        public void AddLineUp(LineUp lineUp);
        public List<LineUp> GetLineUpByRaveId(int raveId);
        public void DeleteLineUp(int id);

    }
}
