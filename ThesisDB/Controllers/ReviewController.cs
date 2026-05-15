using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThesisDB.Models;

namespace ThesisDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ThesisDbContext _context;

        public ReviewController(ThesisDbContext context)
        {
            _context = context;
        }

        // GET: Review/Create/5 (5 is ThesisId)
        public IActionResult Create(int? thesisId)
        {
            if (thesisId == null)
            {
                return NotFound();
            }

            var thesis = _context.Theses.Find(thesisId);
            if (thesis == null)
            {
                return NotFound();
            }

            var review = new Review { ThesisId = thesis.Id };
            ViewBag.ThesisTitle = thesis.Title;
            PopulateGradeSelectList();
            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Summary,Strengths,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteratureVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StructureWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,Grade,ThesisId")] Review review)
        {
            ModelState.Remove("Thesis");
            
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Thesis");
            }
            
            var thesis = _context.Theses.Find(review.ThesisId);
            if(thesis != null) ViewBag.ThesisTitle = thesis.Title;
            
            PopulateGradeSelectList(review.Grade);
            return View(review);
        }

        // GET: Review/Edit/5 (5 is ReviewId)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.Include(r => r.Thesis).FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            ViewBag.ThesisTitle = review.Thesis.Title;
            PopulateGradeSelectList(review.Grade);
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Summary,Strengths,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteratureVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StructureWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,Grade,ThesisId")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Thesis");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Thesis");
            }
            
            var thesis = _context.Theses.Find(review.ThesisId);
            if(thesis != null) ViewBag.ThesisTitle = thesis.Title;
            
            PopulateGradeSelectList(review.Grade);
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Thesis");
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Thesis)
                .ThenInclude(t => t.Student)
                .Include(r => r.Thesis)
                .ThenInclude(t => t.Supervisor)
                .Include(r => r.Thesis)
                .ThenInclude(t => t.Programme)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }

        private void PopulateGradeSelectList(double? selectedGrade = null)
        {
            var grades = new[] { 1.0, 1.3, 1.7, 2.0, 2.3, 2.7, 3.0, 3.3, 3.7, 4.0, 5.0 };
            var gradeList = grades.Select(g => new SelectListItem
            {
                Value = g.ToString("0.#", new System.Globalization.CultureInfo("de-DE")),
                Text = g.ToString("0.0", new System.Globalization.CultureInfo("de-DE")),
                Selected = selectedGrade.HasValue && System.Math.Abs(selectedGrade.Value - g) < 0.01
            }).ToList();
            
            ViewBag.GradeList = gradeList;
        }
    }
}
