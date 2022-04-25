using System;
using System.Collections.Generic;

namespace HC.DomainModels.Entities
{
    /// <summary>
    /// Накладная товара
    /// </summary>
    public class ConsignmentNote : BaseDbEntity
    {
        /// <summary>
        /// Уникальный номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата прибытия
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public int SupplierId { get; set; }
        
        /// <summary>
        /// Поставщик
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public List<StorageProduct> StorageProducts { get; set; }
    }
}