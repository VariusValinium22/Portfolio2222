using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myoung_Portfolio2222.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string AuthorId { get; set; }
		[AllowHtml]
		public string Body { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedReason { get; set; }
		public bool IsDeleted { get; set; }

		public virtual Post Post { get; set; }
		public virtual ApplicationUser Author { get; set; }
		public string Slug { get; set; }
	}
}