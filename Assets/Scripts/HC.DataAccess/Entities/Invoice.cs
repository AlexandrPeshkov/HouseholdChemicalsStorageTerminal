using HC.DataAccess.Models;

namespace DefaultNamespace
{
    /// <summary>
    /// Счёт
    /// </summary>
    public class Invoice : BaseDbEntity
    {
        public int CallLogId { get; set; }
        
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal Cost { get; set; }
        
        /// <summary>
        /// Счет оплачен
        /// </summary>
        public bool IsPaid { get; set; }
    }
}