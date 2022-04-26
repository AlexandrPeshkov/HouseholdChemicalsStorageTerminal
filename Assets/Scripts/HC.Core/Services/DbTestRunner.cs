using System;
using System.Threading.Tasks;
using HC.DataAccess.Logic;
using UnityEngine;

namespace HC.Core
{
    public class DbTestRunner
    {
        public DbTestRunner()
        {
            Run();
        }

        public async Task Run()
        {
            try
            {
                //new DatabaseContext();
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
#endif
                throw;
            }
        }
    }
}