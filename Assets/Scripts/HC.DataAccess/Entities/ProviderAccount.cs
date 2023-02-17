using DataAccess.Models;

namespace DataAccess
{
    public class ProviderAccount : BaseDbEntity
    {
        public string Number { get; set; }
        
        public int ProviderId { get; set; }
        
        public int CountryId { get; set; }
        
        public int AccountTypeId { get; set; }
    }
}