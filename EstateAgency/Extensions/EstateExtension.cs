using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency
{
    public partial class Estate
    {
        public string DisplayTotalArea { 

            get {
                var estateType = this.EstateTypeID;
                String measurement;
                if (estateType == 1 || estateType == 3)
                {
                    measurement = " сот.";
                }
                else{
                    measurement = " м²";
                }
                return this.TotalArea.ToString() + measurement;
            } private set { } }
    }
}
