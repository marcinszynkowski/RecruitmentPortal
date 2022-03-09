using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class PositionService : IPositionService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public PositionService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public Position GetPositionByName(string positionName)
        {
            return _db.Positions.Where(ck => ck.Name == positionName).FirstOrDefault();
        }

        public Position GetPositionById(int positionId)
        {
            return _db.Positions.Find(positionId);
        }

        public string GetPositionName(int positionId)
        {
            return _db.Positions.Find(positionId).Name;
        }

        public List<Position> GetAllPositions()
        {
            return _db.Positions.ToList();
        }
        #endregion

        #region Set
        public void SetPositionName(int positionId, string positionName)
        {
            Position position = GetPositionById(positionId);
            position.Name = positionName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreatePosition(string positionName)
        {
            if (GetPositionByName(positionName) == null)
            {
                Position position = new Position
                {
                    Name = positionName,
                };
                _db.Positions.Add(position);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeletePosition(string positionName)
        {
            Position position = GetPositionByName(positionName);

            if (position != null)
            {
                _db.Positions.Remove(position);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}