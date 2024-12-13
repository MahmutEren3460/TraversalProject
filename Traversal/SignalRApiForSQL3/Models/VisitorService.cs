using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRApiForSQL3.DAL;
using SignalRApiForSQL3.Hubs;

namespace SignalRApiForSQL3.Models
{
	public class VisitorService
	{
		private readonly Context _context;
		private readonly IHubContext<VisitorHub> _hubContext;
		public VisitorService(Context context, IHubContext<VisitorHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		public IQueryable<Visitor> GetList()
		{
			return _context.Visitors.AsQueryable();
		}

		public async Task SaveVisitor(Visitor visitor)
		{
			try
			{
				await _context.Visitors.AddAsync(visitor);
				await _context.SaveChangesAsync();
				await _hubContext.Clients.All.SendAsync("ReceiveVisitorList", GetVisitorChartList());
			}
			catch (Exception ex)
			{
				// Hata yönetimi, loglama vb.
				throw new Exception("Error saving visitor", ex);
			}
		}

		public List<VisitorChart> GetVisitorChartList()
		{
			List<VisitorChart> visitorCharts = new List<VisitorChart>();
			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "SELECT tarih, [1], [2], [3], [4], [5] FROM (SELECT [City], CityVisitCount, CAST([VisitDate] AS Date) AS tarih FROM Visitors) AS visitTable PIVOT (SUM(CityVisitCount) FOR City IN ([1], [2], [3], [4], [5])) AS pivotTable ORDER BY tarih ASC";
				command.CommandType = System.Data.CommandType.Text;

				try
				{
					_context.Database.OpenConnection();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							VisitorChart visitorChart = new VisitorChart
							{
								VisitDate = reader.GetDateTime(0).ToShortDateString()
							};

							Enumerable.Range(1, 5).ToList().ForEach(x =>
							{
								visitorChart.Counts.Add(DBNull.Value.Equals(reader[x]) ? 0 : reader.GetInt32(x));
							});

							visitorCharts.Add(visitorChart);
						}
					}
				}
				catch (Exception ex)
				{
					// Hata yönetimi, loglama vb.
					throw new Exception("Error retrieving visitor chart list", ex);
				}
				finally
				{
					_context.Database.CloseConnection();
				}

				return visitorCharts;
			}
		}
	}
}
