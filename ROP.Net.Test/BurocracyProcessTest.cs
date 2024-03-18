using ROP.Net.Example.Domain;
using ROP.Net.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net.Test
{
    internal class BurocracyProcessTest
    {

        BurocracyProcess burocracy;

        [SetUp]
        public void SetUp()
        {
            burocracy = new BurocracyProcess();
        }

        [Test(Description = "When deal with the burocracy process, the final burocracy has got Habitability")]
        public async Task DealWithBurocracyAndGetHabitability()
        {
            IRail<BurocracyProcess, string> result = await BurocracyService.DealWithBurocracy();
            Assert.IsTrue(result.Result!.HasHabitability);
            Assert.That(result.Result, Is.InstanceOf<BurocracyProcess>());
        }
    }
}
