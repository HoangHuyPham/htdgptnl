using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;


namespace be.Repos.Interfaces
{
    public interface IEvaluationScoreQuery
    {
        public Guid? SourceId { get; set; }
        public Guid? TargetId { get; set; }
        public Guid? CriteriaId { get; set; }
        public Guid? SourceRoleTypeId { get; set; }
       
    }
    public interface IEvaluationScoreRepository : IRepository<EvaluationScore>
    {
        Task<List<EvaluationScore>> FindAllByQuery(IEvaluationScoreQuery query);
    }
}