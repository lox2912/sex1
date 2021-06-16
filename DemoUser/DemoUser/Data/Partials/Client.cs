using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUser.Data
{
    public partial class Client
    {
        public DateTime? LastVizit
        {
            get
            {
                if (Visit.Count == 0) return null;
                else return Visit.OrderByDescending(x => x.Id).First().Date;
            }
        }

        public int VisitsCount { get => this.Visit.Count; }
    }
}

