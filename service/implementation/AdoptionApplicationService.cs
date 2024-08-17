﻿using PetAdoptionCenter.Domain.Models;
using PetAdoptionCenter.Repository.Interface;
using PetAdoptionCenter.Service.Interface;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionCenter.Service.implementation
{
    public class AdoptionApplicationService : IAdoptionApplicationService
    {
        private readonly IRepository<AdoptionApplication> _adoptionApplicationRepository;
        private readonly IAdoptionApplicationRepository _adoptionApplicationRepoInclude;
        private readonly IUserRepository _userRepository;
        private readonly IPetService _petService;

        public AdoptionApplicationService(IRepository<AdoptionApplication> adoptionApplicationRepository, 
            IAdoptionApplicationRepository adoptionApplicationRepoInclude, 
            IUserRepository userRepository,
            IPetService petService)
        {
            _adoptionApplicationRepository = adoptionApplicationRepository; 
            _adoptionApplicationRepoInclude = adoptionApplicationRepoInclude;
            _userRepository = userRepository;
            _petService = petService;
        }

        

        public AdoptionApplication Apply(string loggedInUser, AdoptionApplication adoptionApplication)
        {
            var loggedInAdopter = _userRepository.Get(loggedInUser);
            adoptionApplication.AdopterId = loggedInAdopter.Id;
            adoptionApplication.Adopter = loggedInAdopter;
            adoptionApplication.Pet = _petService.GetPetById(adoptionApplication.PetId);

            adoptionApplication.AdoptionApplicationStatus = Domain.enums.AdoptionApplicationStatus.PENDING;
            return _adoptionApplicationRepository.Insert(adoptionApplication);
        }

        public AdoptionApplication CreateApplicationForm(AdoptionApplication adoptionApplication)
        {
            return _adoptionApplicationRepository.Insert(adoptionApplication);
        }

        public AdoptionApplication DeleteAdoptionApplication(Guid id)
        {
            return _adoptionApplicationRepository.Delete(GetAdoptionApplicationById(id));
        }

        public AdoptionApplication GetAdoptionApplicationById(Guid? id)
        {
            return _adoptionApplicationRepoInclude.GetAdoptionApplicationById(id);
        }

        public List<AdoptionApplication> GetAdoptionApplications()
        {
            return _adoptionApplicationRepoInclude.GetAllAdoptionApplications();
        }

        public List<AdoptionApplication> GetAdoptionApplicationsByAdopterId(string? adopterId)
        {
            return _adoptionApplicationRepoInclude.GetAdoptionApplicationByAdopterId(adopterId);
        }

        public List<AdoptionApplication> GetAdoptionApplicationsByPetId(Guid? petId)
        {
            return _adoptionApplicationRepoInclude.GetAdoptionApplicationByPetId(petId);
        }


        public AdoptionApplication UpdateAdoptionApplication(AdoptionApplication adoptionApplication)
        {
            return _adoptionApplicationRepository.Update(adoptionApplication);
        }
    }
}
