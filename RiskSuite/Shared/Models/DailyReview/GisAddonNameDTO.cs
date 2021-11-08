using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models.DailyReview
{
	public class GisAddonNameDTO
	{
		public int Id { get; set; }
		public int GisAddonId { get; set; }
		public GisAddonDTO GisAddon { get; set; }
		public string Name { get; set; }
	}
}
