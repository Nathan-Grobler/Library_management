using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using static Library.Helper;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly LibraryDdContext _context;

        public LibraryController(LibraryDdContext context)
        {
            _context = context;
        }

        // GET: Library
        public async Task<IActionResult> Index()
        {
            return View(await _context.Library.ToListAsync());
        }

        // GET: Library/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryModel = await _context.Library
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (libraryModel == null)
            {
                return NotFound();
            }

            return View(libraryModel);
        }

        // GET: Library/AddorEdit(Insert)
        // GET: Library/AddorEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddorEdit(int id = 0)
        {
            if(id == 0)
                return View(new LibraryModel());
            else
            {
                var libraryModel = await _context.Library.FindAsync(id);
                if (libraryModel == null)
                {
                    return NotFound();
                }
                return View(libraryModel);
            }
        }

        // POST: Library/AddorEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit(int id, [Bind("BookID,BookName,Author,Pages,Read,Rating,Date")] LibraryModel libraryModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if(id == 0)
                {
                    libraryModel.Date = DateTime.Now;
                    _context.Add(libraryModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(libraryModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LibraryModelExists(libraryModel.BookID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"_ViewAll",_context.Library.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddorEdit", libraryModel) });
        }

        // POST: Library/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryModel = await _context.Library.FindAsync(id);
            _context.Library.Remove(libraryModel);
            await _context.SaveChangesAsync();
            return Json(new {isValid= true,html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Library.ToList()) });
        }

        private bool LibraryModelExists(int id)
        {
            return _context.Library.Any(e => e.BookID == id);
        }
    }
}
