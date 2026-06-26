using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_InputFieldRequest
{
    public Guid Id { get; set; }
    //public int IdAsc { get; set; }
    public Guid IdContents { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsObligatory { get; set; }
    public string FieldType { get; set; }
}
