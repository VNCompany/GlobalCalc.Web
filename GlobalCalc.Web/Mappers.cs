using Riok.Mapperly.Abstractions;

using Entities = GlobalCalc.DataLayer.Entities;
using Models = GlobalCalc.Shared;

namespace GlobalCalc.Web
{
    [Mapper]
    public static partial class ScrewMapper
    {
        public static partial Models.Screw ToModel(Entities.Screw entity);
    }

    [Mapper]
    public static partial class MillingMapper
    {
        public static partial Models.Milling ToModel(Entities.Milling entity);
    }

    [Mapper]
    public static partial class ProfileMapper
    {
        public static partial Models.Profile ToModel(Entities.Profile entity);
    }

    [Mapper]
    public static partial class ProfileColorMapper
    {
        public static partial Models.ProfileColor ToModel(Entities.ProfileColor entity);
    }
}
