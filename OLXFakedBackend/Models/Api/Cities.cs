﻿using System;
namespace OLXFakedBackend.Models.Api
{
	public class Cities
	{
		public int page { get; set; }
		public int pages { get; set; }
		public List<CityApi> cities { get; set; }
	}
}

