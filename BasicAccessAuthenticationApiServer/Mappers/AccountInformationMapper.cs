using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Models.Account;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers
{
    public sealed class AccountInformationMapper : IAccountInformationMapper
    {
        public AccountInformation? MapAccountInformation(IDataReader reader)
        {
            if (reader.Read()) {
                return new AccountInformation
                {
                    AccountId = reader["AccountId"] != DBNull.Value ? Convert.ToInt32(reader["AccountId"]) : 0,
                    AccountHolderName = reader["AccountHolderName"] != DBNull.Value ? (string)(reader["AccountHolderName"]) : "NA",
                    AccountHolderAddress = reader["AccountHolderAddress"] != DBNull.Value ? (string)reader["AccountHolderAddress"] : "NA",
                    AcountHolderEmailId = reader["AcountHolderEmailId"] != DBNull.Value ? (string)reader["AccountHolderAddress"] : "NA",
                    PanNumber = reader["PanNumber"] != DBNull.Value ? (string)reader["PanNumber"] : "NA",
                    AdharNumber = reader["AdharNumber"] != DBNull.Value ? (string)reader["AdharNumber"] : "NA",
                    Gender = reader["Gender"] != DBNull.Value ? (string)reader["Gender"] : "NA",

                };
            }
            else
            {
                return null;
            }           
            
        }

        public List<AccountInformation> MapAccountInformations(IDataReader reader)
        {
            var items = new List<AccountInformation>();
            while (reader.Read())
            {
                var item = MapAccountInformation(reader);
                if (item!=null)
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
