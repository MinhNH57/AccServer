using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_TimekeepingConfiguration
{
        public Guid? Id { get; set; }
        //public int? IdAsc { get; set; }
        public string? CodeObj { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public string? Type { get; set; }
        public string? DeviceType { get; set; }
        public string? CodeUnit { get; set; }
        public string? TimekeepingPocilyCode { get; set; }
}
