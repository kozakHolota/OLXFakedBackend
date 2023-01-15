﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class Item
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
		public Image? Image { get; set; }
		public bool AutoContinue { get; set; }
		public ContactData ContactData { get; set; }
    }
}

