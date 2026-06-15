namespace TestDevCore.Core.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected Entity()
        {
        }

        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            }

            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        protected void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
