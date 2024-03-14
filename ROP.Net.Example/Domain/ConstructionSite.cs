using ROP.Net.Extensions;

namespace ROP.Net.Example.Domain
{
    public class ConstructionSite
    {
        private bool hasFoundation;
        private bool hasWall;
        private bool hasRoof;
        public bool HasFoundation { get; internal set; }

        public ConstructionSite BuildFoundation()
        {
            return new ConstructionSite { hasFoundation = true };
        }

        public ConstructionSite BuildWalls()
        {
            if (!hasFoundation)
                throw new Exception("Construction site must have foundation");

            return new ConstructionSite { hasFoundation = hasFoundation, hasWall = true };            
        }

        public House BuildRoof()
        {
            if (!hasFoundation)
                throw new Exception("Construction site must have foundation");
            if (!hasWall)
                throw new Exception("Construction site must have walls before building roof");

            return new House();
        }
    }
}
