using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.API.Models
{
    public class BaseModelEntity
    {
        public BaseModelEntity()
        {

        }

        public BaseModelEntity(int id)
        {
            Id = id;
        }

        [Key]
        public int Id { get; private set; }
    }
}
