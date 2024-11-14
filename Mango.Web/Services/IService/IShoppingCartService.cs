﻿using Mango.Web.Models;
using Mango.Web.Models.DTO;

namespace Mango.Web.Services.IService
{
	public interface IShoppingCartService
	{
		Task<ResponseDto?> GetCartByUserIdAsnyc(string userId);
		Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
		Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
		Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
		Task<ResponseDto?> EmailCart(CartDto cartDto);


	}
}
