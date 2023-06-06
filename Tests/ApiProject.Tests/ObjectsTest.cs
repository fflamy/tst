using ApiProject.Buisness.Objects;
using ApiProject.Core.Entities;

namespace ApiProject.Tests
{
    internal class GetObjectTests : BaseTest<GetObjects.Command, List<ApiObject>>
	{
		[Test]
		public async Task GetList_Success()
		{
			GetObjects.Command command = new();

			var list = await Act(command);
			Assert.That(list, Is.Not.Null);
			Assert.That(list, Has.Count.EqualTo(10));
		}
		[Test]
		public async Task Get_SecondPageList_Sucess()
		{
			GetObjects.Command command = new()
			{
				Skip=5,
				Take=5,
			};
			var list = await Act(command);
			Assert.That(list, Is.Not.Null);
			Assert.That(list, Has.Count.EqualTo(5));

		}
		protected override IRequestHandler<GetObjects.Command, List<ApiObject>> GetRequestHandler()
		{
			GetObjects.Handler handler = new(AppDbContext);
			return handler;
		}
		protected override void Setup()
		{
			base.Setup();
			SeedDbWithObjects(10);
		}
	}
}
