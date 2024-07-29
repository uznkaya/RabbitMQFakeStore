using System;
using AutoMapper;
using Shared.RequestResponseMessageModel.Product;
using Shared.RequestResponseMessages;
using Shared.RequestViewModel.Product;

namespace Publisher.Mapping
{
    public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<Product, CreateProductVM>().ReverseMap();
		}
	}
}

