using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IScoreService
    {
        public Task<EvaluationScore?> AddScore(float score, Guid SourceId, Guid TargetId, Guid CriteriaId);
    }
}