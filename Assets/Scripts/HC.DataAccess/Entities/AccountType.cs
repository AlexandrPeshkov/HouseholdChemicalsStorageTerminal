using System;
using DataAccess.Models;
using UnityEngine;

namespace DataAccess
{
    public class AccountType : BaseDbEntity
    {
        public string Name { get; set; }

        public static implicit operator AccountTypeEnum(AccountType accountType)
        {
            if (Enum.TryParse(accountType.Name, out AccountTypeEnum result))
            {
                return result;
            }
            
            Debug.LogError($"Cannot parse {accountType.Name}");
            return (AccountTypeEnum)(-1);
        }
    }

    public enum AccountTypeEnum
    {
        Mobile = 1,

        Home = 2,

        Service = 3
    }
}