using AppDrones.Core.Dto;
using AppDrones.Core.Interfaces;
using AppDrones.Core.Mappings;
using AppDrones.Core.Models;
using AppDrones.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Services
{
    public class DroneService : IDrone
    {
        private readonly DatabaseContext _context;
        private readonly Mapper _mapper = null!;
        public DroneService(DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _mapper = DroneMappings.InitializerMapping();
        }
        public Task<DroneBatteryDto> BatteryLevel(int droneId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DroneAvailableDto>> CheckAvailability()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoadedMedicationsResDto>> LoadedMedications(int droneId)
        {
            throw new NotImplementedException();
        }

        public Task LoadingMedication(IEnumerable<LoadMedicationReqDto> medications)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistryResDto> Registry(RegistryReqDto drone)
        {
            var created = this._context.Drone.Add(_mapper.Map<Drone>(drone));
            await _context.SaveChangesAsync();
            return _mapper.Map<RegistryResDto>(created.Entity);
        }
    }
}
