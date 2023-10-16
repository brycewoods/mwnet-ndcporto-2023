using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Controllers {
	public class VenueController : Controller {
		private readonly RockawayDbContext _context;

		public VenueController(RockawayDbContext context) {
			_context = context;
		}

		// GET: Venue
		public async Task<IActionResult> Index() {
			return _context.Venue != null ?
						View(await _context.Venue.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Venue'  is null.");
		}

		// GET: Venue/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null || _context.Venue == null) {
				return NotFound();
			}

			var venue = await _context.Venue
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// GET: Venue/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Venue/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Slug,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (ModelState.IsValid) {
				venue.Id = Guid.NewGuid();
				_context.Add(venue);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Venue/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null || _context.Venue == null) {
				return NotFound();
			}

			var venue = await _context.Venue.FindAsync(id);
			if (venue == null) {
				return NotFound();
			}
			return View(venue);
		}

		// POST: Venue/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (id != venue.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					_context.Update(venue);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException) {
					if (!VenueExists(venue.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Venue/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null || _context.Venue == null) {
				return NotFound();
			}

			var venue = await _context.Venue
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// POST: Venue/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			if (_context.Venue == null) {
				return Problem("Entity set 'RockawayDbContext.Venue'  is null.");
			}
			var venue = await _context.Venue.FindAsync(id);
			if (venue != null) {
				_context.Venue.Remove(venue);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool VenueExists(Guid id) {
			return (_context.Venue?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}