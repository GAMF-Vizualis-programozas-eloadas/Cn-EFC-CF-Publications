using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CnEFCF_Publications
{
	
	public partial class Article
	{
		public Article()
		{
			Authors = new List<Author>();
		}
		public int Id	{	get; set;	}
		public string Title {	get; set;	}
		public virtual List<Author> Authors
		{
			get; set;
		}
	}
}