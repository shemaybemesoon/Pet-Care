using logic.objects;

namespace logic.Service
{
    public class VisitService
    {
        public Visit CreateVisit(
            Pet pet,
            Owner owner,
            Worker worker,
            ProcedureType procedureType,
            string description,
            decimal cost,
            DateTime startVisitTime,
            DateTime endVisitTime)
        {
            var procedure = new Procedure(procedureType, description, cost);
            return new Visit(
                startVisitTime,
                endVisitTime,
                VisitStatus.Scheduled,
                pet,
                owner,
                worker,
                procedure);
        }
    }
}
