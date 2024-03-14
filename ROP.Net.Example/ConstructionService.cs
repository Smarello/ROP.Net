
using ROP.Net.Example.Domain;
using ROP.Net.Extensions;

namespace ROP.Net.Example
{
    public class ConstructionService
    {
        

        public static IRail<ConstructionSite, string> BuildFoundation(IRail<ConstructionSite, string> construction)
        {
            return construction.Result?.BuildFoundation().ToSuccessRail<ConstructionSite, string>() ??
                 "Result is null".ToFailureRail<ConstructionSite, string>(); ;
        }

        public static IRail<ConstructionSite, string> BuildWalls(IRail<ConstructionSite, string> construction)
        {
            try
            {
                return construction.Result!.BuildWalls().ToSuccessRail<ConstructionSite, string>();
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<ConstructionSite, string>();
            }
        }

        public static IRail<House, string> BuildRoof(IRail<ConstructionSite, string> construction)
        {
            try
            {
                return construction.Result!.BuildRoof().ToSuccessRail<House, string>();
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<House, string>();
            }
        }

        public static IRail<House, string> BuildHouse()
        {
            try
            {
                return new ConstructionSite().ToSuccessRail<ConstructionSite, string>()
                    .Then(BuildFoundation)
                    .Then(BuildWalls)
                    .Then(BuildRoof);
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<House, string>();
            }
        }
    }
}