using System.ComponentModel.DataAnnotations.Schema;
using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.VehicleAggregate.DTOs;

namespace PTTS.Core.Domain.VehicleAggregate
{
	public class PublicTransportVehicle
	{
		public int Id { get; private set; }
		public string UserId { get; private set; }
		public string VehicleId { get; private set; }
		public string VehicleType { get; private set; }
		public string Make { get; private set; }
		public string Model { get; private set; }
		public string PlateNumber { get; private set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; } = null!;

		private PublicTransportVehicle(string vehicleType, string userId, string make, string model, string plateNumber)
		{
			ValidateVehicle(vehicleType);

			VehicleId = GenerateVehicleId(vehicleType);
			VehicleType = vehicleType;
			UserId = userId;
			Make = make;
			Model = model;
			PlateNumber = plateNumber;
		}

		public static PublicTransportVehicle Create(string vehicleType, string userId, string make, string model, string plateNumber)
		{
			return new PublicTransportVehicle(vehicleType, userId, make, model, plateNumber);
		}

		public void Update(UpdateVehicleDto updateVehicleDto)
		{
			ValidateUpdateInputs(updateVehicleDto);

			UpdateVehicleType(updateVehicleDto.VehicleType);
			UpdateMake(updateVehicleDto.Make);
			UpdateModel(updateVehicleDto.Model);
			UpdatePlateNumber(updateVehicleDto.PlateNumber);
		}

		private void UpdateVehicleType(string? vehicleType)
		{
			if (!string.IsNullOrEmpty(vehicleType)) VehicleType = vehicleType;
		}

		private void UpdateMake(string? make)
		{
			if (!string.IsNullOrEmpty(make)) Make = make;
		}

		private void UpdateModel(string? model)
		{
			if (!string.IsNullOrEmpty(model)) Model = model;
		}

		private void UpdatePlateNumber(string? plateNumber)
		{
			if (!string.IsNullOrEmpty(plateNumber)) PlateNumber = plateNumber;
		}

		private static string GenerateVehicleId(string vehicleType)
		{
			string pretext = vehicleType[..2].ToUpper();

			// Generate a random 5-digit number
			Random random = new Random();
			int randomNumber = random.Next(10000, 99999);

			return $"{pretext}{randomNumber}";
		}

		private static void ValidateVehicle(string vehicleType)
		{
			if (!AppConstants.VehicleTypes.Contains(vehicleType))
				throw new ArgumentException($"Invalid vehicle type: {vehicleType}. Must be one of {string.Join(", ", AppConstants.VehicleTypes)}", nameof(vehicleType));
		}

		private static void ValidateUpdateInputs(UpdateVehicleDto updateVehicleDto)
		{
			if (updateVehicleDto.VehicleType != null && !AppConstants.VehicleTypes.Contains(updateVehicleDto.VehicleType))
				throw new ArgumentException($"Invalid vehicle type: {updateVehicleDto.VehicleType}. Must be one of {string.Join(", ", AppConstants.VehicleTypes)}", nameof(updateVehicleDto.VehicleType));
		}
	}
}
