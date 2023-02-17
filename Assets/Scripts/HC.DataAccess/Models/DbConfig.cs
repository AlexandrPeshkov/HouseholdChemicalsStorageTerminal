using System;
using UnityEngine;

namespace Data.Data
{
    [Serializable]
    public class DbConfig
    {
        /// <summary>
        /// Очистить и заполнить БД при запуске
        /// </summary>
        [SerializeField]
        public bool ClearAndSeedDb;
        
        [SerializeField]
        public bool UseConfig;
    }
}