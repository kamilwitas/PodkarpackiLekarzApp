namespace PodkarpackiLekarz.Core.Calendars
{
    public class Appoinment
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public AppoinmentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? AcceptedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }        
        public bool IsWaiting => Status == AppoinmentStatus.Created;
        public bool IsAccepted => Status == AppoinmentStatus.Accepted;
        public bool IsClosed => Status == AppoinmentStatus.Closed;
        public bool IsCancelled => Status == AppoinmentStatus.Cancelled;

        public Appoinment()
        {
            
        }

        private Appoinment(
            Guid id,
            Guid patientId,
            DateTime createdAt,
            AppoinmentStatus status = AppoinmentStatus.Created,            
            DateTime? acceptedAt = null,
            DateTime? cancelledAt = null)
        {
            Id = id;
            PatientId = patientId;
            Status = status;
            CreatedAt = createdAt;
            AcceptedAt = acceptedAt;
            CancelledAt = cancelledAt;
        }

        public static Appoinment Create(
            Guid patientId,
            DateTime currentDateTime)
            => new Appoinment(
                Guid.NewGuid(),
                patientId,
                currentDateTime);

        internal void Accept(DateTime currentDateTime)
        {
            Status = AppoinmentStatus.Accepted;
            AcceptedAt = currentDateTime;
        }

        internal void Cancel(DateTime currentDateTime)
        {
            Status = AppoinmentStatus.Cancelled;
            CancelledAt = currentDateTime;
        }

        internal void Close(DateTime currentDateTime)
        {
            Status = AppoinmentStatus.Closed;
            ClosedAt = currentDateTime;
        }
    }
}
