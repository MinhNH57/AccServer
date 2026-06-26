using SmartAccCloud.Application.Models.Catalogs.Object;

namespace SmartAccCloud.Application.MapProfile.CatalogsMapper
{
    public class RelationshipMap :Profile
    {
        public RelationshipMap()
        {
            CreateMap<CatalogRelationship, CatalogRelationshipDto>().ReverseMap();
        }
    }
}
