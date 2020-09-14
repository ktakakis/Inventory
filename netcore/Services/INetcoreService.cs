using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using netcore.Models;
using netcore.Models.Invent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    public interface INetcoreService
    {
        Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env);

        Task UpdateRoles(ApplicationUser appUser, ApplicationUser currentUserLogin);

        Task CreateDefaultSuperAdmin();

        VMStock GetStockByProductAndWarehouse(string productId, string warehouseId);

        List<VMStock> GetStockPerWarehouse();

        List<TimelineViewModel> GetTimelinesByBranchId(string branchId);

        List<TimelineViewModel> GetTimelinesByProductId(string productId);

        List<TimelineViewModel> GetTimelinesByVendorId(string vendorId);

        List<TimelineViewModel> GetTimelinesByCustomerId(string customerId);

        List<TimelineViewModel> GetTimelinesByPurchaseId(string purchaseId);

        List<TimelineViewModel> GetTimelinesBySalesId(string salesId);

        List<TimelineViewModel> GetTimelinesByTransferId(string transferId);
        List<TimelineViewModel> GetTimelinesByInvoiceId(string invoiceId);
        List<TimelineViewModel> GetTimelinesByShipmentId(string shipmentId);
        List<TimelineViewModel> GetTimelinesByProductionOrderId(string productionOrderId);
        Task CreateDefaultRoles();
    }
}
