//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EstateAgency
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstatePriceLog
    {
        public int ID { get; set; }
        public int EstateID { get; set; }
        public decimal OldCost { get; set; }
        public decimal NewCost { get; set; }
        public System.DateTime ChangeDate { get; set; }
    
        public virtual Estate Estate { get; set; }
    }
}
