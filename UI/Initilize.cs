using Services.DataService;
using Services.StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Initilize
    {
        public void InsertTestData()
        {
            var repo = new UStoreRepository();

            var droidrrcAccount = new GoogleAccount();
            droidrrcAccount.DisplayName = "DroidRRc";
            
            var acc = repo.GetAll<GoogleAccount>(a => a.DisplayName == droidrrcAccount.DisplayName);
            if (acc.Count() == 0)
            {
                repo.Store(droidrrcAccount);
            }

            var rino13Acc = new GoogleAccount { DisplayName = "rinorc13" };
            acc = repo.GetAll<GoogleAccount>(a => a.DisplayName == rino13Acc.DisplayName);
            if (acc.Count() == 0)
            {
                repo.Store(rino13Acc);
            }

        }
    }
}
