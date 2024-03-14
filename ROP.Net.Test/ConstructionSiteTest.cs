using ROP.Net.Example.Domain;
using ROP.Net.Example;
using ROP.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net.Test
{
    internal class ConstructionSiteTest
    {
        ConstructionSite construction;

        [SetUp]
        public void SetUp()
        {
            construction = new ConstructionSite();
        }

        [Test(Description = "When I try to create a wall without foundation, I should be in the failure track")]
        public void CreateWallWithoutFoundation()
        {
            IRail<ConstructionSite, string> building = construction.ToSuccessRail<ConstructionSite, string>().Then(ConstructionService.BuildWalls);
            Assert.IsFalse(building.IsSuccess);
            Assert.That(building.Error, Is.EqualTo("Construction site must have foundation"));
        }

        [Test(Description = "When create a foundation, then walls, then a roof, I get a house")]
        public void CreateBasicHouse()
        {
            IRail<House, string> building = construction.ToSuccessRail<ConstructionSite, string>()
                .Then(ConstructionService.BuildFoundation)
                .Then(ConstructionService.BuildWalls)
                .Then(ConstructionService.BuildRoof);
            Assert.IsTrue(building.IsSuccess);
            Assert.That(building.Result, Is.InstanceOf<House>());
        }

        [Test(Description = "When ask for a house to ConstructionService, then I get a house")]
        public void CreateHouseViaService()
        {
            IRail<House, string> result = ConstructionService.BuildHouse();
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Result, Is.InstanceOf<House>());
        }


    }
}
