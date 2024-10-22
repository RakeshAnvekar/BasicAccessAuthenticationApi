using BasicAccessAuthenticationApiServer.Models.Account;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers.Interfaces;

public interface IAccountInformationMapper
{
    public AccountInformation? MapAccountInformation(IDataReader reader);
    public List<AccountInformation> MapAccountInformations(IDataReader reader);
}
