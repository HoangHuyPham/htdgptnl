using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;
using be.Repos.Interfaces;

namespace be.Services.Interfaces
{
    public interface IEvaluationScoreRepository : IRepository<EvaluationScore>
    {
        Task<List<EvaluationScore>> FindAllBy(Guid? sourceId, Guid? targetId, Guid? criteriaId);
    }
}