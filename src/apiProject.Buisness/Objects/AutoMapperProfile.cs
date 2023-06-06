using ApiProject.Core;
using ApiProject.DAL.Entities;
using AutoMapper;

namespace ApiProject.Buisness.Objects
{
	/// <inheritdoc/>
	public class AutoMapperProfile : Profile
	{
		/// <summary>
		/// Конструктор <see cref="AutoMapperProfile"/>.
		/// </summary>
		public AutoMapperProfile()
		{
			CreateMap<KeyValuePair<int, string>, ApiObject>()
				.ForMember(dst => dst.Code, opt => opt.MapFrom(src => src.Key))
				.ForMember(dst => dst.Value, opt => opt.MapFrom(src => src.Value))
				.ForMember(dst => dst.Id, opt => opt.Ignore());

			CreateMap<ApiObject, KeyValuePair<int, string>>()
				.ConstructUsing(ao => new KeyValuePair<int, string>(ao.Code, ao.Value));
		}
	}
}
