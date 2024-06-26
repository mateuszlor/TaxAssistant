﻿namespace TaxAssistant.JPK.Shared.Model.Database
{
    public class Import : BaseModel
    {
        public Guid? KpirId { get; set; }

        public virtual Kpir.Kpir? Kpir { get; set; }
        public Guid? EwpId { get; set; }

        public virtual Ewp.Ewp? Ewp { get; set; }
    }
}
