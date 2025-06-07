namespace be.Models
{
    public class WorkingDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public int StandardWorkingDay { get; set; } = 0;
        public int ActualWorkingDay { get; set; } = 0;
        public int Hoic { get; set; } = 0;
        public int Pv { get; set; } = 0;
        public int Np { get; set; } = 0;
        public int Suspension { get; set; } = 0;
        public int Written { get; set; } = 0;
        public int Verbal { get; set; } = 0;
        public int Maternity { get; set; } = 0;
        public Guid? EmployeeDetailId { get; set; }
        public EmployeeDetail? EmployeeDetail { get; set; }
    }
}