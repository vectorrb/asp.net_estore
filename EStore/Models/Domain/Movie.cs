﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Models.Domain
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		public string? Title { get; set; }
		[Required]
		public string? ReleaseYear { get; set; }

		public string? MovieImage { get; set; }
		[Required]
		public string? Cast { get; set; }
		[Required]
		public string? Director { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }

		[NotMapped]
		[Required]
		public List<int> Genres { get; set; }
		[NotMapped]

		public IEnumerable<SelectListItem> GenreList;
		[NotMapped]
		public string? GenreNames { get; set; }



	}
}
