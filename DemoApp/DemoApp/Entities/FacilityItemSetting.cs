using System;
using System.Collections.Generic;

namespace DemoApp.Entities
{
    public partial class FacilityItemSetting
    {
        public FacilityItemSetting()
        {
            FacilitySettings = new HashSet<FacilitySettings>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<FacilitySettings> FacilitySettings { get; set; }
    }
}
