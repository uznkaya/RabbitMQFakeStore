using System;
using AutoMapper;
using Shared.RequestResponseMessages;
using Shared.RequestViewModel;

namespace Publisher.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<RequestMessage, CreateProductVM>().ReverseMap();
		}
	}
}

