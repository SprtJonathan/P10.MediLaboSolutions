using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MediLaboSolutions.API.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BaseModelEntity
    {
        public BaseModelEntity()
        {

        }

        public BaseModelEntity(int id)
        {
            Id = id;
        }

        protected virtual string DebuggerDisplay
            => $"Id = {Id}, HashCode = {GetHashCode()}";

        [Key]
        public int Id { get; private set; }
    }
}
