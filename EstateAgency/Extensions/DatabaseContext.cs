using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency
{
    public partial class EstateAgencyEntities
    {
        public static EstateAgencyEntities _context;
        public static EstateAgencyEntities GetContext()
        {
            if (_context == null)
            {
                _context = new EstateAgencyEntities();
            }
            return _context;
        }
    }
}
