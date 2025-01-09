namespace SHO_Task.Domain.BuildingBlocks
{
    public interface ISHONumberGenerator
    {
        string GenerateSHONumber(DateTime createdDate);
    }
}
