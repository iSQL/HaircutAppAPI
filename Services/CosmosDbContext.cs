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

		public async Task<IEnumerable<Reservation>> GetAppointmentsAsync()
		{
			var query = _container.GetItemQueryIterator<Reservation>();
			List<Reservation> results = new List<Reservation>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				results.AddRange(response.ToList());
			}
			return results;
		}

		public async Task AddAppointmentAsync(Reservation appointment)
		{
			await _container.CreateItemAsync(appointment, new PartitionKey(appointment.id));
		}

		public async Task DeleteAllAppointmentsAsync()
		{
			var query = _container.GetItemQueryIterator<Reservation>();
			var tasks = new List<Task>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				foreach (var item in response)
				{
					tasks.Add(_container.DeleteItemAsync<Reservation>(item.id, new PartitionKey(item.id)));
				}
			}
			await Task.WhenAll(tasks);
		}

	}
}
