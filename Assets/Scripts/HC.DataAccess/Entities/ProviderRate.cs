using DataAccess.Models;

namespace DataAccess
{
    public class ProviderRate : BaseDbEntity
    {
        public int ProviderId { get; set; }
        
        public int CallingAccountTypeId { get; set; }
        public float Rate { get; set; }
    }
}