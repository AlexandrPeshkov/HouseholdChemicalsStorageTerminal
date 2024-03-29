﻿using DataAccess.Models;

namespace DataAccess
{
    /// <summary>
    /// Счёт
    /// </summary>
    public class Invoice : BaseDbEntity
    {
        public int CallLogId { get; set; }
        
        /// <summary>
        /// Счет оплачен
        /// </summary>
        public bool IsPaid { get; set; }
    }
}