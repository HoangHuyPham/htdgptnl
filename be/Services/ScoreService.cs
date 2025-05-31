using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Contexts;
using be.Models;
using be.Repos.Interfaces;

namespace be.Services.Interfaces
{
    public class ScoreService(ApplicationDbContext _dbContext, IRepository<Criteria> _criteriaRepository, IUserRepository _userRepository, IEvaluationScoreRepository _evaluationScoreRepo, IRoleScheduleRepository _roleScheduleRepository) : IScoreService
    {
        private readonly IEvaluationScoreRepository evaluationScoreRepo = _evaluationScoreRepo;
        private readonly ApplicationDbContext dbContext = _dbContext;
        private readonly IRoleScheduleRepository roleScheduleRepository = _roleScheduleRepository;
        private readonly IUserRepository userRepository = _userRepository;
        private readonly IRepository<Criteria> criteriaRepository = _criteriaRepository;
        public async Task<EvaluationScore?> AddScore(float score, Guid SourceId, Guid TargetId, Guid CriteriaId)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var Source = await userRepository.FindById(SourceId);
                var Target = await userRepository.FindById(TargetId);
                var Criteria = await criteriaRepository.FindById(CriteriaId);

                var SourceRoleId = Source?.RoleId;
                var TargetRoleId = Target?.RoleId;

                if (SourceRoleId == null)
                {
                    throw new ArgumentNullException(nameof(SourceRoleId));
                }

                var RoleSchedulesExist = await roleScheduleRepository.FindAllByRoleId(SourceRoleId.Value);
                var RoleScheduleExistByRoleId = RoleSchedulesExist.First();

                if (RoleScheduleExistByRoleId == null)
                {
                    throw new ArgumentNullException(nameof(RoleScheduleExistByRoleId));
                }

                var RoleScheduleExistByCriteria = Criteria?.AchievementItem?.Achievement?.PerformanceEvaluation?.EvaluationSchedule?.RoleSchedules?.First(x => x.Id == RoleScheduleExistByRoleId.Id);

                if (RoleScheduleExistByCriteria == null)
                {
                    throw new ArgumentNullException(nameof(RoleScheduleExistByCriteria));
                }

                if (Source?.Role?.Level <= Target?.Role?.Level && Source?.Employee?.Supervisor != null)
                {
                    throw new ArgumentNullException("Can't evaluate with the same role level");
                }

                var EvaluationScore = await evaluationScoreRepo.Create(new()
                {
                    SourceId = Source?.Id,
                    TargetId = Target?.Id,
                    CriteriaId = Criteria!.Id,
                    SourceRoleTypeId = RoleScheduleExistByRoleId.RoleType?.Id,
                    Score = score
                });

                if (EvaluationScore == null)
                {
                    throw new Exception("Create EvaluationScore Failed.");
                }

                await transaction.CommitAsync();

                return await evaluationScoreRepo.FindById(EvaluationScore.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
            }

            return null;
        }
    }
}