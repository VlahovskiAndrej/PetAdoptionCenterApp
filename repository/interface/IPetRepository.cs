using domain.Models;
using PetAdoptionCenter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionCenter.Repository.Interface
{
    public interface IPetRepository
    {
        List<Pet> GetAllPets();
        public Pet GetPetById(Guid? id);
        
    }
}
