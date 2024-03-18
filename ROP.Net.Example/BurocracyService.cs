using ROP.Net.Example.Domain;
using ROP.Net.Extensions;

namespace ROP.Net.Example
{
    public class BurocracyService
    {
        public static async Task<IRail<BurocracyProcess, string>> GetBuildingPermit (IRail<BurocracyProcess, string> burocracy)
        {
            BurocracyProcess burocracyProcess = await burocracy.Result?.GetBuildingPermit()!;
            return burocracyProcess.ToSuccessRail<BurocracyProcess, string>() ??
                 "Result is null".ToFailureRail<BurocracyProcess, string>(); ;
        }

        public static async Task<IRail<BurocracyProcess, string>> GetEnergyCertification (IRail<BurocracyProcess, string> burocracy)
        {
            try
            {
                BurocracyProcess burocracyProcess = await burocracy.Result?.GetEnergyCertification()!;
                return burocracyProcess.ToSuccessRail<BurocracyProcess, string>();
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<BurocracyProcess, string>();
            }
        }

        public static async Task<IRail<BurocracyProcess, string>> GetHabitability(IRail<BurocracyProcess, string> burocracy)
        {
            try
            {
                BurocracyProcess burocracyProcess = await burocracy.Result?.GetHabitability()!;
                return burocracyProcess.ToSuccessRail<BurocracyProcess, string>();
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<BurocracyProcess, string>();
            }
        }

        public static async Task<IRail<BurocracyProcess, string>> DealWithBurocracy()
        {
            try
            {
                return await new BurocracyProcess().ToSuccessRail<BurocracyProcess, string>()
                    .ThenAsync(GetBuildingPermit)
                    .ThenAsync(GetEnergyCertification)
                    .ThenAsync(GetHabitability);
            }
            catch (Exception ex)
            {
                return ex.Message.ToFailureRail<BurocracyProcess, string>();
            }
        }
    }
}
