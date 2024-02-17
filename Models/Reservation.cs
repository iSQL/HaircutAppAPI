namespace HaircutAppAPI.Models
{
	public class Reservation
	{
		private string _id;
		public string id
		{
			get => _id ??= Guid.NewGuid().ToString(); // Autogenerate if null
			set => _id = value;
		}
		public string CustomerName { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string AppointmentSlot { get; set; }
		public string ServiceType { get; set; }
	}
}
