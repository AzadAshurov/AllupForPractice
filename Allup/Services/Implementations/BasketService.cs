﻿using Allup.DAL;
using Allup.Services.Interfaces;
using Allup.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Allup.Services.Implementations
{

    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _http;
        private readonly ClaimsPrincipal _user;

        public BasketService(AppDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
            _user = http.HttpContext.User;
        }
        public async Task<List<BasketItemVM>> GetBasketAsync()
        {

            List<BasketItemVM> basketVM = new();

            if (_user.Identity.IsAuthenticated)
            {
                _http.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
                if (_context.BasketItems.Where(bi => bi.AppUserId == _user.FindFirstValue(ClaimTypes.NameIdentifier)) is not null)
                {
                    basketVM = await _context.BasketItems
                        .Where(bi => bi.AppUserId == _user.FindFirstValue(ClaimTypes.NameIdentifier))
                        .Select(bi => new BasketItemVM
                        {
                            Count = bi.Count,
                            Price = bi.Product.Price,
                            Image = bi.Product.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true).Image,
                            Name = bi.Product.Name,
                            SubTotal = bi.Product.Price * bi.Count,
                            Id = bi.ProductId
                        })
                        .ToListAsync();
                }
                else basketVM = new();
            }
            else
            {
                List<BasketCookieItemVM> cookiesVM;
                string cookie = _http.HttpContext.Request.Cookies["basket"];
                if (cookie == null)
                {
                    return basketVM;
                }
                cookiesVM = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(cookie);
                basketVM = await _context.Products.Where(p => cookiesVM.Select(c => c.Id).Contains(p.Id))
                    .Select(p => new BasketItemVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Image = p.ProductImages[0].Image,
                        Price = p.Price,
                    }).ToListAsync();

                basketVM.ForEach(bi =>
                {
                    bi.Count = cookiesVM.FirstOrDefault(c => c.Id == bi.Id).Count;
                    bi.SubTotal = bi.Price * bi.Count;
                });
            }
            return basketVM;
        }
    }
}
