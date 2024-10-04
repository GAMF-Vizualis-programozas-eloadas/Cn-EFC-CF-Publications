using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;


namespace CnEFCF_Publications
{
	class Program
	{
		static void SeedDB(cnPublications cn)
		{
			try
			{
				var res = cn.Database.EnsureCreated();
				if (!cn.Articles.Any())
				{
					Article ar1 = new Article { Title = "Entity Framework Core" }; 
					Article ar2 = new Article { Title = "Entity Framework" };

					Author au1 = new Author { Name = "John Smith" };
					Author au2 = new Author { Name = "Jane Smith" };

					ar1.Authors.Add(au1);
					ar1.Authors.Add(au2);
					ar2.Authors.Add(au1);

					au1.Articles.Add(ar1);
					au1.Articles.Add(ar2);
					au2.Articles.Add(ar1);

					cn.Articles.Add(ar1);
					cn.Articles.Add(ar2);
					cn.Authors.Add(au1);
					cn.Authors.Add(au2);
				}
				cn.SaveChanges();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
		}
		private static void ShowArticles(cnPublications cn)
		{
			Console.WriteLine("The stored articles");
			var l=cn.Articles.Include(x => x.Authors).ToList();
			foreach (var a in l)
				Console.WriteLine(a.Authors.Aggregate("",
					(s,c)=>s+(s.Length>0?", ":"")+c.Name)+": "+a.Title);
			Console.WriteLine();
		}
		static void Main(string[] args)
		{
			Console.WriteLine("EF Core Application");
			using (cnPublications cn = new cnPublications())
			{
				SeedDB(cn);
				ShowArticles(cn);
				ShowArticlesOf(cn, "Jane Doe");
				ShowArticlesOf(cn, "John Doe");
			}
		}

		private static void ShowArticlesOf(cnPublications cn, string v)
		{
			Console.WriteLine("Articles written by " + v);
			foreach (var au in cn.Authors.Where(a => a.Name == v))
			{ foreach(var ar in au.Articles)
					Console.WriteLine(ar.Authors.Aggregate("",(s,c)=>s+(s.Length>0?", ":"")+c.Name)+": "+ar.Title);
			}
			Console.WriteLine();
		}
	}
}

