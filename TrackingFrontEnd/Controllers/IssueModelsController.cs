using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackingFrontEnd.Data;
using TrackingFrontEnd.Models;
using TrackingFrontEnd.Services;

namespace TrackingFrontEnd.Controllers
{
    public class IssueModelsController : Controller
    {
        private readonly TrackingFrontEndContext _context;
        private readonly ITrackingAccountService _itracking;
        private readonly IConfiguration _configuration;

        HttpClient client = new();

        public IssueModelsController(TrackingFrontEndContext context, ITrackingAccountService itracking, IConfiguration configuration)
        {
            _context = context;
            _itracking = itracking;
            _configuration = configuration;
        }

        // GET: IssueModels
        public async Task<IActionResult> Index()
        {
            List<IssueModel> issues = new List<IssueModel>();
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/issue");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var issuesFromDb = await response.Content.ReadFromJsonAsync<IEnumerable<IssueModel>>();

                foreach (var issue in issuesFromDb)
                {
                    issues.Add(issue);
                }

                return View(issues);

            }
            else
            {
                Console.WriteLine("No results");
                return View();
            }

            //try
            //{
            //    var token = await _itracking.GetToken(_configuration["Tracking:ClientID"], _configuration["Tracking:ClientSecret"]);
            //}
            //catch
            //{

            //}

            //return View();
        }

        // GET: IssueModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            IssueModel issueReturned = new IssueModel();
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/issue/id?id="+id);
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {
                issueReturned = await response.Content.ReadFromJsonAsync<IssueModel>();

                return View(issueReturned);

            }
            else
            {
                Console.WriteLine("No results");
                return View();
            }
        }

        // GET: IssueModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Priority,IssueType,Created,Completed")] IssueModel issueModel)
        {
            List<IssueModel> issues = new List<IssueModel>();
            client.BaseAddress = new Uri(_configuration["Tracking: URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/issue");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var issuesFromDb = await response.Content.ReadFromJsonAsync<IEnumerable<IssueModel>>();

                foreach (var issue in issuesFromDb)
                {
                    issues.Add(issue);
                }

                return RedirectToAction(nameof(Index));

            }
            else
            {
                Console.WriteLine("No results");
                return View(issueModel);
            }

        }

        // GET: IssueModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueModel == null)
            {
                return NotFound();
            }

            var issueModel = await _context.IssueModel.FindAsync(id);
            if (issueModel == null)
            {
                return NotFound();
            }
            return View(issueModel);
        }

        // POST: IssueModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,IssueType,Created,Completed")] IssueModel issueModel)
        {
            if (id != issueModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueModelExists(issueModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(issueModel);
        }

        // GET: IssueModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssueModel == null)
            {
                return NotFound();
            }

            var issueModel = await _context.IssueModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueModel == null)
            {
                return NotFound();
            }

            return View(issueModel);
        }

        // POST: IssueModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueModel == null)
            {
                return Problem("Entity set 'TrackingFrontEndContext.IssueModel'  is null.");
            }
            var issueModel = await _context.IssueModel.FindAsync(id);
            if (issueModel != null)
            {
                _context.IssueModel.Remove(issueModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueModelExists(int id)
        {
          return (_context.IssueModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
