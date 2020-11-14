using System;

namespace Entities.Common
{
    public abstract class AutoincrementObject : IAutoincrementObject
    {
        public int Id { get; set; }

        protected AutoincrementObject()
        {
        }

        protected AutoincrementObject(IAutoincrementObject autoincrementObject)
        {
            if (autoincrementObject == null)
            {
                return;
            }

            Id = autoincrementObject.Id;
        }

        protected AutoincrementObject(int id)
        {
            Id = id;
        }

        protected AutoincrementObject(uint id)
        {
            Id = (int)id;
        }

        string IAutoincrementObject.ToString()
        {
            return Id.ToString("D");
        }

        public override string ToString()
        {
            return Id.ToString("D");
        }
    }

    public interface IAutoincrementObject
    {
        int Id { get; set; }

        string ToString();
    }

    public abstract class UniqueIdObject : IUniqueIdObject
    {
        public Guid Id { get; set; }

        protected UniqueIdObject()
        {
        }

        protected UniqueIdObject(Guid id)
        {
            Id = id;
        }

        protected UniqueIdObject(IUniqueIdObject entity)
        {
            if (entity == null)
            {
                return;
            }

            Id = entity.Id;
        }
    }

    public interface IUniqueIdObject
    {
        Guid Id { get; set; }
    }
}
