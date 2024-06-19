using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceWithDto(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreatedDto Dto)
        {
            var dto = _mapper.Map<Product>(Dto);
            await _productRepository.AddAsync(dto);
            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDto>(dto);
            return CustomResponseDto<ProductDto>.Succes(StatusCodes.Status201Created, productDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Succes(200, productDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto Dto)
        {
            var Entity = _mapper.Map<Product>(Dto);
            _productRepository.Update(Entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Succes(StatusCodes.Status200OK);
        }
    }
}
