using System;
using System.Collections.Generic;
using HC.DataAccess.Models;

namespace HC.Domain.Entities
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
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public virtual ICollection<StorageProduct> StorageProducts { get; set; }
    }
}