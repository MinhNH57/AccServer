using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.SmartMapper;
[AttributeUsage(AttributeTargets.Property)]
public class SmartMapIgnoreAttribute : Attribute
{
}