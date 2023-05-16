using AppDrones.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Interfaces
{
    public interface IDrone
    {
        public Task<RegistryResDto> Registry(RegistryReqDto drone);
        public Task LoadingMedication(IEnumerable<LoadMedicationReqDto> medications);
        public Task<IEnumerable<LoadedMedicationsResDto>> LoadedMedications(int droneId);
        public Task<IEnumerable<DroneAvailableDto>> CheckAvailability();
        public Task<DroneBatteryDto> BatteryLevel(int droneId);
    }
}
