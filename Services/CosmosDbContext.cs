using HaircutAppAPI.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HaircutAppAPI.Services
{
	public class CosmosDbContext
	{
		private Container _container;

		public CosmosDbContext(string connectionString, string databaseName, string containerName)
		{
			var cosmosClient = new CosmosClient(connectionString);
			_container = cosmosClient.GetContainer(databaseName, containerName);
		}

		public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
		{
			var query = _container.GetItemQueryIterator<Appointment>();
			List<Appointment> results = new List<Appointment>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				results.AddRange(response.ToList());
			}
			return results;
		}

		public async Task AddAppointmentAsync(Appointment appointment)
		{
			await _container.CreateItemAsync(appointment, new PartitionKey(appointment.customerId));
		}
	}
}
