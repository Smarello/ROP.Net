namespace ROP.Net.Example.Domain
{
    public class BurocracyProcess
    {
        private bool hasBuildingPermit;
        private bool hasEnergyCertification;
        private bool hasHabitability;

        public bool HasHabitability => hasHabitability;

        public async Task<BurocracyProcess> GetBuildingPermit() {
            await Task.Delay(500);
            Console.WriteLine("You got building permit");
            return new BurocracyProcess() { hasBuildingPermit = true };
        }

        public async Task<BurocracyProcess> GetEnergyCertification() {
            if (!hasBuildingPermit)
                throw new Exception("Can't get energy certification before building permit");

            await Task.Delay(500);
            Console.WriteLine("You got energy certification");
            return new BurocracyProcess() { hasBuildingPermit = hasBuildingPermit, hasEnergyCertification = true };
        }
        public async Task<BurocracyProcess> GetHabitability() {
            if (!hasBuildingPermit)
                throw new Exception("Can't get habitability before building permit");
            if (!hasEnergyCertification)
                throw new Exception("Can't get habitability before energy certification");
            await Task.Delay(500);
            Console.WriteLine("You got habitability");
            return new BurocracyProcess() { hasBuildingPermit = hasBuildingPermit, hasEnergyCertification = hasEnergyCertification, hasHabitability = true  };
        }
    }
}
