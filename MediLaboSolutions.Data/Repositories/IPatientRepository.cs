using MediLaboSolutions.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediLaboSolutions.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
    }
}
