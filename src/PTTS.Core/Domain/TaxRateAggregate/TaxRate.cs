using PTTS.Core.Domain.Constants;
using PTTS.Core.Domain.TaxRateAggregate.DTOs;

namespace PTTS.Core.Domain.TaxRateAggregate
{
	public class TaxRate
	{
		public int Id { get; private set; }
		public string VehicleType { get; private set; }
		public decimal Rate { get; private set; }
		public string State { get; private set; }
		public string LocalGovernment { get; private set; }

		private TaxRate(string localGovernment, string vehicleType, decimal rate)
		{
			ValidateState("enugu");
			ValidateCreateInputs(localGovernment, vehicleType, rate);

			VehicleType = vehicleType;
			Rate = rate;
			State = "enugu";
			LocalGovernment = localGovernment;
		}

		public static TaxRate Create(string localGovernment, string vehicleType, decimal rate)
		{
			return new TaxRate(localGovernment, vehicleType, rate);
		}

		public void Update(UpdateTaxRateDto updateDto)
		{
			ValidateState("enugu");
			ValidateUpdateInputs(updateDto);

			if (!string.IsNullOrEmpty(updateDto.VehicleType))
				UpdateVehicleType(updateDto.VehicleType);
			if (updateDto.Rate.HasValue)
				UpdateRate(updateDto.Rate.Value);
			if (!string.IsNullOrEmpty(updateDto.LocalGovernment))
				UpdateLocalGovernment(updateDto.LocalGovernment);
		}

		private void UpdateVehicleType(string vehicleType)
		{
			VehicleType = vehicleType;
		}

		private void UpdateRate(decimal rate)
		{
			Rate = rate;
		}

		private void UpdateLocalGovernment(string localGovernment)
		{
			LocalGovernment = localGovernment;
		}

		private static void ValidateState(string state)
		{
			if (!AppConstants.States.Contains(state))
				throw new ArgumentException($"Invalid state: {state}.", nameof(state));
		}

		private static void ValidateCreateInputs(string localGovernment, string vehicleType, decimal rate)
		{
			if (rate < 0)
				throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be non-negative.");
			if (!AppConstants.EnuguLocalGovernments.Contains(localGovernment))
				throw new ArgumentException($"Invalid local government: {localGovernment}. Valid options are: {string.Join(", ", AppConstants.EnuguLocalGovernments)}.", nameof(localGovernment));
			if (!AppConstants.VehicleTypes.Contains(vehicleType))
				throw new ArgumentException($"Invalid vehicle type: {vehicleType}. Valid options are: {string.Join(", ", AppConstants.VehicleTypes)}.", nameof(vehicleType));
		}

		private static void ValidateUpdateInputs(UpdateTaxRateDto updateDto)
		{
			if (updateDto.Rate.HasValue && updateDto.Rate < 0)
				throw new ArgumentOutOfRangeException(nameof(updateDto.Rate), "Rate must be non-negative.");
			if (!string.IsNullOrEmpty(updateDto.LocalGovernment) &&
			!AppConstants.EnuguLocalGovernments.Contains(updateDto.LocalGovernment))
				throw new ArgumentException($"Invalid local government: {updateDto.LocalGovernment}. Valid options are: {string.Join(", ", AppConstants.EnuguLocalGovernments)}.", nameof(updateDto.LocalGovernment));
			if (!string.IsNullOrEmpty(updateDto.VehicleType) &&
			!AppConstants.VehicleTypes.Contains(updateDto.VehicleType))
				throw new ArgumentException($"Invalid vehicle type: {updateDto.VehicleType}. Valid options are: {string.Join(", ", AppConstants.VehicleTypes)}.", nameof(updateDto.VehicleType));
		}
	}
}
