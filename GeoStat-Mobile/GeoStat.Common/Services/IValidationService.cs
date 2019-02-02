namespace GeoStat.Common.Services
{
    public interface IValidationService
    {
        bool IsEmailValid(string email);
        bool IsPasswordValid(string password);
    }
}