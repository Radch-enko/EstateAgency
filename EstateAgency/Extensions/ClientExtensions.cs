using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency
{
    public partial class Client
    {
        public string DisplayName { get {
                return this.Surname + " " + this.Name + " " + this.MiddleName;
            } private set { } }
    }
}
