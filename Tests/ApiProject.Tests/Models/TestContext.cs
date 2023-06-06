using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.EquivalencyExpression;
using ApiProject.Buisness.Objects;

namespace ApiProject.Tests.Models
{
	internal class TestContext : IDisposable
	{
		private static TestContext? _context;

		public ApiDbContext AppDbContext { get; }
		public IMapper Mapper { get; }
		public static TestContext Instance
		{
			get
			{
				_context ??= new TestContext();
				return _context;
			}
		}
		private TestContext()
		{

			AppDbContext = CreateDbContext();
			Mapper = CreateMapper();
		}

		private static ApiDbContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<ApiDbContext>()
						 .UseSqlite("Data Source=testDb.db")
						 .Options;
			var context = new ApiDbContext(options);
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}

		private static IMapper CreateMapper()
		{
			var mapper = new MapperConfiguration(c =>
			{
				c.AllowNullCollections = true;
				c.AllowNullDestinationValues = true;
				c.AddMaps(typeof(BusinessLayer));
				c.AddCollectionMappers();
			});

			return mapper.CreateMapper();
		}
		public void Dispose()
		{
			AppDbContext?.Database.EnsureDeleted();
			GC.SuppressFinalize(this);
		}

		~TestContext()
		{
			Dispose();
		}
	}
}