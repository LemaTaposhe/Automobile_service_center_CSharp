using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Domain
{
    public interface IEntity<TKey>
    {
        TKey CarID { get; set; }
    }
}

