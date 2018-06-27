using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myoung_Portfolio2222.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		[AllowHtml]
		[Required]
		public string Body { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string MediaURL { get; set; }
		public string Slug { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
	}
}