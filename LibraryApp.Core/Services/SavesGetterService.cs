﻿using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SavesGetterService : ISavesGetterService
    {
        private readonly ISavesRepository _savesRepository;

        public SavesGetterService(ISavesRepository savesRepository)
        {
            _savesRepository = savesRepository;
        }

        public async Task<Save> GetSaveByUserIdAndObjectId(string userId, string objectId)
        {
            return await _savesRepository.GetSave(userId, objectId);
        }

        public async Task<List<Save>> GetSavesByObjectId(string objectId)
        {
            return await _savesRepository.GetSavesByObjectId(objectId);
        }

        public async Task<List<Save>> GetSavesByUserId(string userId)
        {
            return await _savesRepository.GetSavesByUserId(userId);
        }
    }
}
