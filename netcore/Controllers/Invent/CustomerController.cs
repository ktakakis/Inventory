﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers.Invent
{


    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.OrderByDescending(x => x.createdAt).ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                        .SingleOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", customer.EmployeeId);
            return View(customer);
        }


        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            Customer customer = new Customer();
            return View(customer);
        }



        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("customerId,HasChild,city,country,customerName,description,province,size,street1,street2,Active,CompanyActivity,OrderDiscount,SalesCount,TaxDiscount,TaxOffice,VATRegNumber,WebSite,fax,mobilePhone,officePhone,workEmail,createdAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Create Customer " + customer.customerName + " Success";
                return RedirectToAction(nameof(Details), new { id = customer.customerId });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", customer.EmployeeId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", customer.EmployeeId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("customerId,HasChild,city,country,customerName,description,province,size,street1,street2,Active,CompanyActivity,OrderDiscount,SalesCount,TaxDiscount,TaxOffice,VATRegNumber,WebSite,fax,mobilePhone,officePhone,workEmail,createdAt")] Customer customer)
        {
            if (id != customer.customerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.customerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η Επεξεργασία του Πελάτη " + customer.customerName + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", customer.EmployeeId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                    .SingleOrDefaultAsync(m => m.customerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", customer.EmployeeId);
            return View(customer);
        }




        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customer
                .Include(x => x.CustomerLine)
                .SingleOrDefaultAsync(m => m.customerId == id);
            try
            {
                _context.CustomerLine.RemoveRange(customer.CustomerLine);
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή του Πελάτη " + customer.customerName + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(customer);
            }


            
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Customer
        {
            public const string Controller = "Customer";
            public const string Action = "Index";
            public const string Role = "Customer";
            public const string Url = "/Customer/Index";
            public const string Name = "Customer";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Customer")]
        public bool CustomerRole { get; set; } = false;
    }
}



