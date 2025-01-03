﻿using Allup.DAL;
using Allup.Models;
using Allup.Utilities.Enums;
using Allup.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slide> slides = _context.Slides.ToList();
            return View(slides);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM SlideVM)
        {
            // if (!ModelState.IsValid) return View();

            if (!SlideVM.Photo.IsFileTypeValid("image/"))
            {
                ModelState.AddModelError("Photo", "File type is incorrect");
                return View();
            }
            if (!SlideVM.Photo.IsFileSizeValid(Utilities.Enums.FileSize.Megabyte, 2))
            {
                ModelState.AddModelError("Photo", "File size must be less than 2 mb");
                return View();
            }
            Slide slide = new Slide
            {
                MainTitle = SlideVM.Title,
                SubTitle = SlideVM.SubTitle,
                Description = SlideVM.Description,
                Order = SlideVM.Order,
                ImageUrl = await SlideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };


            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) { return BadRequest(); }

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) { return NotFound(); }

            slide.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide is null) return NotFound();

            UpdateSlideVM slideVM = new()
            {
                Title = slide.MainTitle,
                SubTitle = slide.SubTitle,
                Description = slide.Description,
                Order = slide.Order,
                Image = slide.ImageUrl
            };

            return View(slideVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVM slideVM)
        {
            if (id == null || id < 1) { return BadRequest(); }

            //Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            //if (slide == null) { return NotFound(); }
            //slideVM.Image = slide.Image;

            if (!ModelState.IsValid)
            {

                return View(slideVM);

            }
            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) { return NotFound(); }
            slideVM.Image = slide.ImageUrl;
            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.IsFileTypeValid("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "Type is incorrect");
                    return View(slideVM);
                }

                if (!slideVM.Photo.IsFileSizeValid(FileSize.Megabyte, 2))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "Size is incorrect");
                    return View(slideVM);
                }

                string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");

                slide.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                slide.ImageUrl = fileName;
                Console.WriteLine("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDebug");
            }
            slide.SubTitle = slideVM.SubTitle;
            slide.MainTitle = slideVM.Title;
            slide.Order = slideVM.Order;
            slide.Description = slideVM.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
