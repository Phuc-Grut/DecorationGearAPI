﻿using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearApplication.Interface
{
    public interface IProductRespository
    {
        Task<List<ProductDto>> GetAllProduct(ViewProductRequest? request,CancellationToken cancellationToken);
        Task<ProductDto> GetKeyProductById(int id, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetSoftProducts(string sortBy, bool isAscending, CancellationToken cancellationToken);
        Task<ResponseDto<ProductDto>> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken);
        Task<ResponseDto<ProductDto>> UpdateProduct(int id,UpdateProductRequest request, CancellationToken cancellationToken);
        Task<bool> AreSubCategoriesValid(int categoryId, List<int> subCategoryIds, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> UpdateCategoryProduct(int productId, UpdateProductCategoryRequest request, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> DeleteProduct(int id, CancellationToken cancellationToken);
        public bool IsValidImageFormat(string imagePaths);
    }
}
