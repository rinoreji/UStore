using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.StorageService;
using Services.DataService;

namespace ServicesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var account = new GoogleAccount();
            account.DisplayName = "droidrrc";

            var repo = new UStoreRepository();
            //var result = repo.Store(account);
            //var result3 = repo.Store(account);
            //var result2 = repo.Store(result);


            var re = repo.GetAll<GoogleAccount>();

        }
    }
}
