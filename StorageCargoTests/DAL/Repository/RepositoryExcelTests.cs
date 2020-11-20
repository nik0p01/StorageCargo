namespace StorageCargo.DAL.Repository.Tests
{
    [TestFixture()]
    public class RepositoryExcelTests
    {

        [Test()]
        public void GetCargosTest()
        {
            var rep = new RepositoryExcel(@"c:\Temp\Test.xlsx");
            var cargos = rep.GetCargos();
            Assert.AreEqual(29, cargos.Count());
        }
        [Test()]
        public void GetRatesTest()
        {
            var rep = new RepositoryExcel(@"c:\Temp\Test.xlsx");
            var rates = rep.GetRates();
            Assert.AreEqual(4, rates.Count());
        }
    }
}