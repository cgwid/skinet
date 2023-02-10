using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        // This is used to get the full path to the picture and not use the relataive url, for AutoMapper
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }
    }
}