﻿using GlobalCalc.DataLayer;

namespace GlobalCalc.Web.BL
{
    internal class ProfilesBL
    {
        private readonly DataContext _db;

        public ProfilesBL(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Shared.Profile> GetProfiles()
            => _db.ProfilePositions.GetAll()
            .GroupBy(_ => _.Profile.Id)
            .Select(group =>
            {
                Shared.Profile profile = ProfileMapper.ToModel(group.First().Profile);
                profile.Colors = group
                    .Select(pp =>
                    {
                        Shared.ProfileColor profileColor = ProfileColorMapper.ToModel(pp.Color);
                        profileColor.Price = pp.Price;
                        return profileColor;
                    })
                    .ToArray();
                return profile;
            });
    }
}
