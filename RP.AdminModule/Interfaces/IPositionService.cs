using System.Collections.Generic;
using RP.Entities.Recruitment;

namespace RP.AdminModule.Interfaces
{
    public interface IPositionService
    {
        void CreatePosition(string positionName);
        void DeletePosition(string positionName);
        List<Position> GetAllPositions();
        Position GetPositionById(int positionId);
        Position GetPositionByName(string positionName);
        string GetPositionName(int positionId);
        void SetPositionName(int positionId, string positionName);
    }
}