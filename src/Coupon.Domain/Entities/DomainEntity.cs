namespace Coupon.Domain.Entities
{
    public abstract class DomainEntity
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

        protected DomainEntity()
        {
            _domainEvents = [];
        }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? [];
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
