using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Response;

namespace Voucher.Sgas.Model.Invoice;

public class RoundUpMoneyRequest  
{//Guid RequestId, Guid Id, double Quantity, double AmountOfMoney
    public Guid Id { get; set; }
    public double Quantity { get; set; }
    public double AmountOfMoney { get; set; }

}
