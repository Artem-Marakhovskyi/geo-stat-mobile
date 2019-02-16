namespace GeoStat.Common.Abstractions
{
    public interface IValidationService
    {
        bool IsEmailValid(string email);
        bool IsPasswordValid(string password);
    }
}