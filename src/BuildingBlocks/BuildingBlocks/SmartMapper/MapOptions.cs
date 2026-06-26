using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.SmartMapper;
public class MapOptions
{
    public HashSet<string> IgnoreProperties { get; set; } = new();
    public List<PropertyMap> CustomPropertyMaps { get; set; } = new();

}