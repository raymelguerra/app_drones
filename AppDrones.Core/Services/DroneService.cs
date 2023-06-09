﻿using AppDrones.Core.Dto;
using AppDrones.Core.Exceptions;
using AppDrones.Core.Extensions;
using AppDrones.Core.Interfaces;
using AppDrones.Core.Mappings;
using AppDrones.Core.Models;
using AppDrones.Data;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
        public async Task<DroneBatteryDto> BatteryLevel(int droneId)
        {
            var drone = await _context.Drone.FirstOrDefaultAsync(x => x.DroneId == droneId);
            return _mapper.Map<DroneBatteryDto>(drone);
        }

        public async Task<IEnumerable<DroneAvailableDto>> CheckAvailability()
        {
            var drones = await _context.Drone.Where(x => x.State == State.IDLE && x.BatteryCapacity > 25).ToListAsync();
            return _mapper.Map<List<DroneAvailableDto>>(drones);
        }

        public async Task<IEnumerable<LoadedMedicationsResDto>> LoadedMedications(int droneId)
        {
            var medications = await _context.Medication.Where(x => x.DroneId == droneId).ToListAsync();

            if (medications == null)
                throw new DroneNotFoundException("Drone not found");

            List<LoadedMedicationsResDto> list = new();
            foreach (var item in medications)
            {
                list.Add(_mapper.Map<LoadedMedicationsResDto>(item));
            }
            return list;
        }

        public async Task<bool> LoadingMedication(IEnumerable<LoadMedicationReqDto> medications, int droneId)
        {
            try
            {
                var drone = await _context.Drone.FirstOrDefaultAsync(x => x.DroneId == droneId);

                if (drone == null)
                    throw new DroneNotFoundException("Drone not found");

                if (drone.State != State.IDLE)
                    throw new DroneStatusException($"Drone <{drone.SerialNumber.ToUpper()}> is in {drone.State} state therefore it cannot be loaded with medicines");

                if (drone.BatteryCapacity < 25)
                    throw new LowBatteryException($"Drone <{drone.SerialNumber.ToUpper()}> is below 25%. ({drone.BatteryCapacity})");

                await ChangeDroneStatus(drone, State.LOADING);

                List<Medication> list = new();
                double weightVerify = 0;
                foreach (var item in medications)
                {
                    list.Add(_mapper.Map<Medication>(item));
                    weightVerify += item.Weight;
                }

                if (weightVerify > drone!.WeightLimit)
                {
                    await ChangeDroneStatus(drone, State.IDLE);
                    throw new WeightLimitException($"The maximum allowed weight of the drone <{drone.SerialNumber.ToUpper()}> has been exceeded");
                }

                drone!.Medications = list;
                drone!.State = State.LOADED;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<RegistryResDto> Registry(RegistryReqDto drone)
        {
            var created = _context.Drone.Add(_mapper.Map<Drone>(drone));
            await _context.SaveChangesAsync();
            return _mapper.Map<RegistryResDto>(created.Entity);
        }

        private async Task ChangeDroneStatus(Drone drone, State state)
        {
            drone!.State = state;
            await _context.SaveChangesAsync();
        }
    }
}
