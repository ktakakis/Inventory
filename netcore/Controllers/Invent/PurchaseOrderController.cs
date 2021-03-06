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
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace netcore.Controllers.Invent
{


    [Authorize(Roles = "PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> ShowPurchaseOrder(string id)
        {
            PurchaseOrder obj = await _context.PurchaseOrder
                .Include(x => x.vendor)
                .Include(x => x.purchaseOrderLine).ThenInclude(x => x.product)
                .Include(x => x.branch)
                .SingleOrDefaultAsync(x => x.purchaseOrderId.Equals(id));

            obj.totalOrderAmount = obj.purchaseOrderLine.Sum(x => x.totalAmount);
            obj.totalDiscountAmount = obj.purchaseOrderLine.Sum(x => x.discountAmount);
            _context.Update(obj);

            return View(obj);
        }
        public async Task<IActionResult> PrintPurchaseOrder(string id)
        {
            PurchaseOrder obj = await _context.PurchaseOrder
                .Include(x => x.vendor)
                .Include(x => x.purchaseOrderLine).ThenInclude(x => x.product)
                .Include(x => x.branch)
                .SingleOrDefaultAsync(x => x.purchaseOrderId.Equals(id));
            return View(obj);
        }

        public ActionResult ByteArrayToFile(string id)
        {
            var po = _context.PurchaseOrder.Include(x=>x.vendor).Where(x => x.purchaseOrderId == id).FirstOrDefault();
            string fileName = po.purchaseOrderNumber + po.vendor.vendorName + ".pdf";

            if (po == null)
            {
                return NotFound();
            }

            Response.Headers.Append("content-disposition", "inline; filename=" + fileName);
            return new FileStreamResult(new MemoryStream(po.File.ToArray()), "application/pdf");
        }

        // GET: PurchaseOrder
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseOrder.OrderByDescending(x => x.createdAt).Include(p => p.vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                    .Include(x => x.purchaseOrderLine)
                    .Include(p => p.vendor)
                    .Include(x => x.branch)
                        .SingleOrDefaultAsync(m => m.purchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", purchaseOrder.branchId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", purchaseOrder.vendorId);

            purchaseOrder.totalOrderAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.totalAmount);
            purchaseOrder.totalDiscountAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.discountAmount);
            purchaseOrder.InvoiceBalance = purchaseOrder.totalOrderAmount - purchaseOrder.totalVendorPayment;
            _context.Update(purchaseOrder);
            await _context.SaveChangesAsync();

            return View(purchaseOrder);
        }


        // GET: PurchaseOrder/Create
        public IActionResult Create()
        {

            PurchaseOrder po = new PurchaseOrder();
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName");
            Branch defaultBranch = _context.Branch.Where(x => x.isDefaultBranch.Equals(true)).FirstOrDefault();
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", defaultBranch != null ? defaultBranch.branchId : null);
            return View(po);
        }



        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchaseOrderId,HasChild,branchId,createdAt,deliveryDate,description,picInternal,picVendor,poDate,purchaseOrderNumber,purchaseOrderStatus,purchaseReceiveNumber,referenceNumberExternal,top,totalDiscountAmount,totalOrderAmount,vendorId,InvoiceBalance,Paid,totalVendorPayment,File")] PurchaseOrder purchaseOrder, IFormFile File)
        {
            if (File != null)
            {
                using (var stream=new MemoryStream())
                {
                    await File.CopyToAsync(stream);
                    purchaseOrder.File = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Δημιουργία της παραγγελίας αγοράς(ΠΑ) " + purchaseOrder.purchaseOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Details), new { id = purchaseOrder.purchaseOrderId });
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", purchaseOrder.branchId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", purchaseOrder.vendorId);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(x => x.purchaseOrderLine)
                .SingleOrDefaultAsync(m => m.purchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            purchaseOrder.totalOrderAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.totalAmount);
            purchaseOrder.totalDiscountAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.discountAmount);
            _context.Update(purchaseOrder);
            await _context.SaveChangesAsync();
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", purchaseOrder.branchId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", purchaseOrder.vendorId);
            TempData["PurchaseOrderStatus"] = purchaseOrder.purchaseOrderStatus;
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("purchaseOrderId,HasChild,branchId,createdAt,deliveryDate,description,picInternal,picVendor,poDate,purchaseOrderNumber,purchaseOrderStatus,purchaseReceiveNumber,referenceNumberExternal,top,totalDiscountAmount,totalOrderAmount,vendorId,InvoiceBalance,Paid,totalVendorPayment,File")] PurchaseOrder purchaseOrder, IFormFile File)
        {
            if (id != purchaseOrder.purchaseOrderId)
            {
                return NotFound();
            }

            if (File != null)
            {
                using (var stream = new MemoryStream())
                {
                    await File.CopyToAsync(stream);
                    purchaseOrder.File = stream.ToArray();
                }
            }

            if ((PurchaseOrderStatus)TempData["PurchaseOrderStatus"] == PurchaseOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Δεν είναι δυνατή η επεξεργασία παραγγελίας που είναι [Ολοκληρωμένη]";
                return RedirectToAction(nameof(Edit), new { id = purchaseOrder.purchaseOrderId });
            }

            if (purchaseOrder.purchaseOrderStatus == PurchaseOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν μπορείτε να επεξεργαστείτε την κατάσταση στην [Ολοκληρωμένη] Παραγγελία.";
                return RedirectToAction(nameof(Edit), new { id = purchaseOrder.purchaseOrderId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["TransMessage"] = "Η Επεξεργασία της ΠΑ " + purchaseOrder.purchaseOrderNumber + " έγινε με Επιτυχία";
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.purchaseOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", purchaseOrder.branchId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", purchaseOrder.vendorId);
            return View(purchaseOrder);
        }


        // GET: PurchaseOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                    .Include(x => x.purchaseOrderLine)
                    .Include(p => p.vendor)
                    .SingleOrDefaultAsync(m => m.purchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", purchaseOrder.branchId);
            ViewData["vendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", purchaseOrder.vendorId);

            purchaseOrder.totalOrderAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.totalAmount);
            purchaseOrder.totalDiscountAmount = purchaseOrder.purchaseOrderLine.Sum(x => x.discountAmount);
            _context.Update(purchaseOrder);
            await _context.SaveChangesAsync();

            return View(purchaseOrder);
        }




        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var purchaseOrder = await _context.PurchaseOrder
                .Include(x => x.purchaseOrderLine)
                .SingleOrDefaultAsync(m => m.purchaseOrderId == id);
            try
            {
                _context.PurchaseOrderLine.RemoveRange(purchaseOrder.purchaseOrderLine);
                _context.PurchaseOrder.Remove(purchaseOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή της ΠΑ " + purchaseOrder.purchaseOrderNumber + " έγινε με Επιτυχία!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(purchaseOrder);
            }
            
        }

        private bool PurchaseOrderExists(string id)
        {
            return _context.PurchaseOrder.Any(e => e.purchaseOrderId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class PurchaseOrder
        {
            public const string Controller = "PurchaseOrder";
            public const string Action = "Index";
            public const string Role = "PurchaseOrder";
            public const string Url = "/PurchaseOrder/Index";
            public const string Name = "PurchaseOrder";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "PurchaseOrder")]
        public bool PurchaseOrderRole { get; set; } = false;
    }
}



