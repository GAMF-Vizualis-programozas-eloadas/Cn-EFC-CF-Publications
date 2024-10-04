using System.Collections.Generic;

namespace CnEFCF_Publications
{
	public class Author
	{
		public Author()
		{
			Articles = new List<Article>();
		}
		public int Id	{	get; set;	}
		public string Name { get; set; }
		public List<Article> Articles
		{
			get; set;
		}
	}
}