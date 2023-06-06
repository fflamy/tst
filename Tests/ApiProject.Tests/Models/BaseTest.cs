using ApiProject.Core.Entities;

namespace ApiProject.Tests.Models
{
    internal abstract class BaseTest<T, TResponse>
		where T : IRequest<TResponse>
	{
		protected static ApiDbContext AppDbContext => TestContext.Instance.AppDbContext;
		protected static IMapper Mapper => TestContext.Instance.Mapper;
		[SetUp]
		protected virtual void Setup()
		{
			SeedDbWithObjects(4);
		}

		protected static void SeedDbWithObjects(int count)
		{
			Random random = new();
			List<ApiObject> list = new();
			for (int i = 0; i < count; i++)
			{
				list.Add(new ApiObject
				{
					Code=random.Next(3,(i+1)*13),
					Value =Guid.NewGuid().ToString(),
				});
			}
			AppDbContext.ApiObjects.AddRange(list);
			AppDbContext.SaveChanges();
		}

		protected abstract IRequestHandler<T, TResponse> GetRequestHandler();
		protected Task<TResponse> Act(T request)
		{
			var handler = GetRequestHandler();

			return handler.Handle(request, CancellationToken.None);
		}
	}
}
