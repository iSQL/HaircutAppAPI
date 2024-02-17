namespace HaircutAppAPI.Models
{
	public class Appointment
	{
		private string _id;
		public string id
		{
			get => _id ??= Guid.NewGuid().ToString(); // Autogenerate if null
			set => _id = value;
		}
		public string CustomerID { get; set; }
		public string CustomerName { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string AppointmentTime { get; set; }
		public string ServiceType { get; set; }
	}
}
