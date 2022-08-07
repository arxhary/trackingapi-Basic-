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
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/issue",issueModel);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
               // var issuesFromDb = await response.Content.ReadFromJsonAsync<IssueModel>();

                return RedirectToAction(nameof(Index));

            }
            else
            {
                
                return NotFound();
            }

        }

        // GET: IssueModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
                return NotFound();
            }
        }

        // POST: IssueModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,IssueType,Created,Completed")] IssueModel issueModel)
        {
            IssueModel issueReturned = new IssueModel();
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PutAsJsonAsync("api/issue/"+id,issueModel);
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(Index));

            }
            else
            {
                
                return NotFound();
            }
        }

        // GET: IssueModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            IssueModel issueReturned = new IssueModel();
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/issue/id?id=" + id);
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {
                issueReturned = await response.Content.ReadFromJsonAsync<IssueModel>();

                return View(issueReturned);

            }
            else
            {
                return NotFound();
            }
        }

        // POST: IssueModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IssueModel issueReturned = new IssueModel();
            client.BaseAddress = new Uri(_configuration["Tracking:URL"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync("api/issue/" + id);
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(Index));

            }
            else
            {

                return NotFound();
            }
        }

    }
}
