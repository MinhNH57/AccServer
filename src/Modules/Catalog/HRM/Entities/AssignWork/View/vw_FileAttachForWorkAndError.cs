using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities.AssignWork.View;
public class vw_FileAttachForWorkAndError
{
    public Guid? Id { get; set; }
    public string? FileNames { get; set; }
    public string? Description { get; set; }
    public Guid? IdContents { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public Guid? CardIdContents { get; set; }
    public Guid? ErrorIdContents { get; set; }
    public string? Title { get; set; }
    public string? ErrorName { get; set; }
    public string? PathFile { get; set; }
    public string? SizeFile { get; set; }
    public string? TypeFile { get; set; }
}
