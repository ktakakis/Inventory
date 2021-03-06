﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ApplicationUserRole = table.Column<bool>(nullable: false),
                    BranchRole = table.Column<bool>(nullable: false),
                    CatalogLineRole = table.Column<bool>(nullable: false),
                    CatalogRole = table.Column<bool>(nullable: false),
                    CitiesRole = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CustomerLineRole = table.Column<bool>(nullable: false),
                    CustomerRole = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    EmployeeRole = table.Column<bool>(nullable: false),
                    HomeRole = table.Column<bool>(nullable: false),
                    InvoiceLineRole = table.Column<bool>(nullable: false),
                    InvoiceRole = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NumberSequenceRole = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PaymentReceiveRole = table.Column<bool>(nullable: false),
                    PaymentTypeRole = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    ProductRole = table.Column<bool>(nullable: false),
                    PurchaseOrderLineRole = table.Column<bool>(nullable: false),
                    PurchaseOrderRole = table.Column<bool>(nullable: false),
                    ReceivingLineRole = table.Column<bool>(nullable: false),
                    ReceivingRole = table.Column<bool>(nullable: false),
                    SalesOrderLineRole = table.Column<bool>(nullable: false),
                    SalesOrderRole = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ShipmentLineRole = table.Column<bool>(nullable: false),
                    ShipmentRole = table.Column<bool>(nullable: false),
                    StockRole = table.Column<bool>(nullable: false),
                    TransferInLineRole = table.Column<bool>(nullable: false),
                    TransferInRole = table.Column<bool>(nullable: false),
                    TransferOrderLineRole = table.Column<bool>(nullable: false),
                    TransferOrderRole = table.Column<bool>(nullable: false),
                    TransferOutLineRole = table.Column<bool>(nullable: false),
                    TransferOutRole = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    VendorLineRole = table.Column<bool>(nullable: false),
                    VendorRole = table.Column<bool>(nullable: false),
                    WarehouseRole = table.Column<bool>(nullable: false),
                    isSuperAdmin = table.Column<bool>(nullable: false),
                    profilePictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    branchId = table.Column<string>(maxLength: 38, nullable: false),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    OfficePhone = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    TaxOffice = table.Column<string>(maxLength: 50, nullable: true),
                    VATNumber = table.Column<string>(maxLength: 50, nullable: true),
                    branchName = table.Column<string>(maxLength: 50, nullable: false),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    isDefaultBranch = table.Column<bool>(nullable: false),
                    province = table.Column<string>(maxLength: 30, nullable: true),
                    street1 = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.branchId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<string>(maxLength: 38, nullable: false),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    ProductVAT = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Commission = table.Column<decimal>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PaymentReceiver = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    fax = table.Column<string>(nullable: true),
                    mobilePhone = table.Column<string>(nullable: true),
                    officePhone = table.Column<string>(nullable: true),
                    personalEmail = table.Column<string>(nullable: true),
                    province = table.Column<string>(maxLength: 30, nullable: true),
                    street1 = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "NumberSequence",
                columns: table => new
                {
                    NumberSequenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastNumber = table.Column<int>(nullable: false),
                    Module = table.Column<string>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    NumberSequenceName = table.Column<string>(nullable: false),
                    Prefix = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSequence", x => x.NumberSequenceId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PaymentTypeName = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<string>(maxLength: 38, nullable: false),
                    Discontinued = table.Column<bool>(nullable: false),
                    ProductVAT = table.Column<decimal>(nullable: false),
                    ProductVolume = table.Column<decimal>(nullable: true),
                    ProductWeight = table.Column<decimal>(nullable: true),
                    ReorderLevel = table.Column<int>(nullable: true),
                    SpecialTaxValue = table.Column<decimal>(nullable: false),
                    UnitCost = table.Column<decimal>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: true),
                    barcode = table.Column<string>(maxLength: 50, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    productCode = table.Column<string>(maxLength: 50, nullable: false),
                    productName = table.Column<string>(maxLength: 50, nullable: false),
                    productType = table.Column<int>(nullable: false),
                    serialNumber = table.Column<string>(maxLength: 50, nullable: true),
                    uom = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    vendorId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(maxLength: 6, nullable: true),
                    TaxNumber = table.Column<string>(maxLength: 15, nullable: true),
                    TaxOffice = table.Column<string>(maxLength: 50, nullable: true),
                    WebSite = table.Column<string>(maxLength: 100, nullable: true),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    fax = table.Column<string>(nullable: true),
                    mobilePhone = table.Column<string>(nullable: true),
                    officePhone = table.Column<string>(nullable: true),
                    personalEmail = table.Column<string>(nullable: true),
                    province = table.Column<string>(maxLength: 30, nullable: true),
                    size = table.Column<int>(nullable: false),
                    street1 = table.Column<string>(maxLength: 50, nullable: false),
                    vendorName = table.Column<string>(maxLength: 50, nullable: false),
                    workEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.vendorId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    warehouseId = table.Column<string>(maxLength: 38, nullable: false),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    province = table.Column<string>(maxLength: 30, nullable: true),
                    street1 = table.Column<string>(maxLength: 50, nullable: false),
                    street2 = table.Column<string>(maxLength: 50, nullable: true),
                    warehouseName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.warehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouse_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashRepository",
                columns: table => new
                {
                    CashRepositoryId = table.Column<string>(maxLength: 38, nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    CashRepositoryName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    TotalPayments = table.Column<decimal>(nullable: false),
                    TotalReceipts = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRepository", x => x.CashRepositoryId);
                    table.ForeignKey(
                        name: "FK_CashRepository_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerId = table.Column<string>(maxLength: 38, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CompanyActivity = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    HasChild = table.Column<string>(nullable: true),
                    OrderDiscount = table.Column<decimal>(nullable: true),
                    SalesCount = table.Column<bool>(nullable: false),
                    TaxDiscount = table.Column<decimal>(nullable: false),
                    TaxOffice = table.Column<string>(maxLength: 50, nullable: true),
                    VATRegNumber = table.Column<string>(maxLength: 50, nullable: true),
                    WebSite = table.Column<string>(maxLength: 50, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerName = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    fax = table.Column<string>(maxLength: 20, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    officePhone = table.Column<string>(maxLength: 20, nullable: true),
                    size = table.Column<int>(nullable: false),
                    workEmail = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_Customer_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    purchaseOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    branchId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    deliveryAddress = table.Column<string>(maxLength: 50, nullable: true),
                    deliveryDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    picInternal = table.Column<string>(maxLength: 30, nullable: false),
                    picVendor = table.Column<string>(maxLength: 30, nullable: false),
                    poDate = table.Column<DateTime>(nullable: false),
                    purchaseOrderNumber = table.Column<string>(maxLength: 20, nullable: false),
                    purchaseOrderStatus = table.Column<int>(nullable: false),
                    purchaseReceiveNumber = table.Column<string>(nullable: true),
                    referenceNumberExternal = table.Column<string>(maxLength: 30, nullable: true),
                    referenceNumberInternal = table.Column<string>(maxLength: 30, nullable: true),
                    top = table.Column<int>(nullable: false),
                    totalDiscountAmount = table.Column<decimal>(nullable: false),
                    totalOrderAmount = table.Column<decimal>(nullable: false),
                    vendorId = table.Column<string>(maxLength: 38, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.purchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendor_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendor",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorLine",
                columns: table => new
                {
                    vendorLineId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    fax = table.Column<string>(maxLength: 20, nullable: true),
                    firstName = table.Column<string>(maxLength: 20, nullable: false),
                    gender = table.Column<int>(nullable: false),
                    jobTitle = table.Column<string>(maxLength: 20, nullable: false),
                    lastName = table.Column<string>(maxLength: 20, nullable: false),
                    middleName = table.Column<string>(maxLength: 20, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    nickName = table.Column<string>(maxLength: 20, nullable: true),
                    officePhone = table.Column<string>(maxLength: 20, nullable: true),
                    personalEmail = table.Column<string>(maxLength: 50, nullable: true),
                    salutation = table.Column<int>(nullable: false),
                    vendorId = table.Column<string>(maxLength: 38, nullable: true),
                    workEmail = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorLine", x => x.vendorLineId);
                    table.ForeignKey(
                        name: "FK_VendorLine_Vendor_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendor",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOrder",
                columns: table => new
                {
                    transferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    branchFrombranchId = table.Column<string>(nullable: true),
                    branchIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    branchIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    branchTobranchId = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    isIssued = table.Column<bool>(nullable: false),
                    isReceived = table.Column<bool>(nullable: false),
                    picName = table.Column<string>(maxLength: 50, nullable: false),
                    transferOrderDate = table.Column<DateTime>(nullable: false),
                    transferOrderNumber = table.Column<string>(maxLength: 20, nullable: false),
                    transferOrderStatus = table.Column<int>(nullable: false),
                    warehouseFromwarehouseId = table.Column<string>(nullable: true),
                    warehouseIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseTowarehouseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrder", x => x.transferOrderId);
                    table.ForeignKey(
                        name: "FK_TransferOrder_Branch_branchFrombranchId",
                        column: x => x.branchFrombranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrder_Branch_branchTobranchId",
                        column: x => x.branchTobranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrder_Warehouse_warehouseFromwarehouseId",
                        column: x => x.warehouseFromwarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrder_Warehouse_warehouseTowarehouseId",
                        column: x => x.warehouseTowarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    CatalogId = table.Column<string>(maxLength: 38, nullable: false),
                    CatalogName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.CatalogId);
                    table.ForeignKey(
                        name: "FK_Catalog_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLine",
                columns: table => new
                {
                    customerLineId = table.Column<string>(maxLength: 38, nullable: false),
                    LocLat = table.Column<string>(maxLength: 30, nullable: true),
                    LocLong = table.Column<string>(maxLength: 30, nullable: true),
                    PostCode = table.Column<string>(maxLength: 5, nullable: true),
                    ProductVAT = table.Column<string>(nullable: true),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerId = table.Column<string>(maxLength: 38, nullable: true),
                    province = table.Column<string>(maxLength: 30, nullable: true),
                    street1 = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLine", x => x.customerLineId);
                    table.ForeignKey(
                        name: "FK_CustomerLine_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLine",
                columns: table => new
                {
                    purchaseOrderLineId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    discountAmount = table.Column<decimal>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    purchaseOrderId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    totalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLine", x => x.purchaseOrderLineId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_PurchaseOrder_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receiving",
                columns: table => new
                {
                    receivingId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    purchaseOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    receivingDate = table.Column<DateTime>(nullable: false),
                    receivingNumber = table.Column<string>(maxLength: 20, nullable: false),
                    vendorDO = table.Column<string>(maxLength: 50, nullable: false),
                    vendorId = table.Column<string>(maxLength: 38, nullable: true),
                    vendorInvoice = table.Column<string>(maxLength: 50, nullable: true),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiving", x => x.receivingId);
                    table.ForeignKey(
                        name: "FK_Receiving_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receiving_PurchaseOrder_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receiving_Vendor_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendor",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receiving_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferIn",
                columns: table => new
                {
                    transferInId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    branchFrombranchId = table.Column<string>(nullable: true),
                    branchIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    branchIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    branchTobranchId = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    transferInDate = table.Column<DateTime>(nullable: false),
                    transferInNumber = table.Column<string>(maxLength: 20, nullable: false),
                    transferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    warehouseFromwarehouseId = table.Column<string>(nullable: true),
                    warehouseIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseTowarehouseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferIn", x => x.transferInId);
                    table.ForeignKey(
                        name: "FK_TransferIn_Branch_branchFrombranchId",
                        column: x => x.branchFrombranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIn_Branch_branchTobranchId",
                        column: x => x.branchTobranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIn_TransferOrder_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrder",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferIn_Warehouse_warehouseFromwarehouseId",
                        column: x => x.warehouseFromwarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferIn_Warehouse_warehouseTowarehouseId",
                        column: x => x.warehouseTowarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOrderLine",
                columns: table => new
                {
                    transferOrderLineId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    transferOrderId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrderLine", x => x.transferOrderLineId);
                    table.ForeignKey(
                        name: "FK_TransferOrderLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrderLine_TransferOrder_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrder",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOut",
                columns: table => new
                {
                    transferOutId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    branchFrombranchId = table.Column<string>(nullable: true),
                    branchIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    branchIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    branchTobranchId = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    transferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    transferOutDate = table.Column<DateTime>(nullable: false),
                    transferOutNumber = table.Column<string>(maxLength: 20, nullable: false),
                    warehouseFromwarehouseId = table.Column<string>(nullable: true),
                    warehouseIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseTowarehouseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOut", x => x.transferOutId);
                    table.ForeignKey(
                        name: "FK_TransferOut_Branch_branchFrombranchId",
                        column: x => x.branchFrombranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOut_Branch_branchTobranchId",
                        column: x => x.branchTobranchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOut_TransferOrder_transferOrderId",
                        column: x => x.transferOrderId,
                        principalTable: "TransferOrder",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferOut_Warehouse_warehouseFromwarehouseId",
                        column: x => x.warehouseFromwarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOut_Warehouse_warehouseTowarehouseId",
                        column: x => x.warehouseTowarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogLine",
                columns: table => new
                {
                    CatalogLineId = table.Column<string>(maxLength: 38, nullable: false),
                    CatalogId = table.Column<string>(maxLength: 38, nullable: true),
                    Discount = table.Column<decimal>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogLine", x => x.CatalogLineId);
                    table.ForeignKey(
                        name: "FK_CatalogLine_Catalog_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    salesOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    HasChild = table.Column<string>(nullable: true),
                    Invoicing = table.Column<bool>(nullable: false),
                    SalesOrderName = table.Column<string>(nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(nullable: false),
                    TotalProductVAT = table.Column<decimal>(nullable: false),
                    TotalWithSpecialTax = table.Column<decimal>(nullable: false),
                    branchId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerId = table.Column<string>(maxLength: 38, nullable: false),
                    customerLineId = table.Column<string>(maxLength: 38, nullable: false),
                    deliveryDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    salesOrderNumber = table.Column<string>(maxLength: 20, nullable: true),
                    salesOrderStatus = table.Column<int>(nullable: false),
                    salesShipmentNumber = table.Column<string>(nullable: true),
                    soDate = table.Column<DateTime>(nullable: false),
                    top = table.Column<int>(nullable: false),
                    totalDiscountAmount = table.Column<decimal>(nullable: false),
                    totalOrderAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.salesOrderId);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrder_CustomerLine_customerLineId",
                        column: x => x.customerLineId,
                        principalTable: "CustomerLine",
                        principalColumn: "customerLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivingLine",
                columns: table => new
                {
                    receivingLineId = table.Column<string>(maxLength: 38, nullable: false),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    qtyInventory = table.Column<float>(nullable: false),
                    qtyReceive = table.Column<float>(nullable: false),
                    receivingId = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingLine", x => x.receivingLineId);
                    table.ForeignKey(
                        name: "FK_ReceivingLine_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingLine_Receiving_receivingId",
                        column: x => x.receivingId,
                        principalTable: "Receiving",
                        principalColumn: "receivingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingLine_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferInLine",
                columns: table => new
                {
                    transferInLineId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    qtyInventory = table.Column<float>(nullable: false),
                    transferInId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferInLine", x => x.transferInLineId);
                    table.ForeignKey(
                        name: "FK_TransferInLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferInLine_TransferIn_transferInId",
                        column: x => x.transferInId,
                        principalTable: "TransferIn",
                        principalColumn: "transferInId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOutLine",
                columns: table => new
                {
                    transferOutLineId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    qtyInventory = table.Column<float>(nullable: false),
                    transferOutId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOutLine", x => x.transferOutLineId);
                    table.ForeignKey(
                        name: "FK_TransferOutLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOutLine_TransferOut_transferOutId",
                        column: x => x.transferOutId,
                        principalTable: "TransferOut",
                        principalColumn: "transferOutId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLine",
                columns: table => new
                {
                    SalesOrderLineId = table.Column<string>(maxLength: 38, nullable: false),
                    Discount = table.Column<decimal>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    ProductVAT = table.Column<decimal>(nullable: false),
                    ProductVATAmount = table.Column<decimal>(nullable: false),
                    Qty = table.Column<float>(nullable: false),
                    SalesOrderId = table.Column<string>(maxLength: 38, nullable: true),
                    SpecialTaxAmount = table.Column<decimal>(nullable: false),
                    SpecialTaxDiscount = table.Column<decimal>(nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalBeforeDiscount = table.Column<decimal>(nullable: false),
                    TotalSpecialTaxAmount = table.Column<decimal>(nullable: false),
                    TotalWithSpecialTax = table.Column<decimal>(nullable: false),
                    UnitCost = table.Column<decimal>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLine", x => x.SalesOrderLineId);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "salesOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    shipmentId = table.Column<string>(maxLength: 38, nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    HasChild = table.Column<string>(nullable: true),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerId = table.Column<string>(maxLength: 38, nullable: true),
                    customerPO = table.Column<string>(maxLength: 50, nullable: true),
                    expeditionMode = table.Column<int>(nullable: false),
                    expeditionType = table.Column<int>(nullable: false),
                    invoice = table.Column<string>(maxLength: 50, nullable: true),
                    salesOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    shipmentDate = table.Column<DateTime>(nullable: false),
                    shipmentNumber = table.Column<string>(maxLength: 20, nullable: true),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.shipmentId);
                    table.ForeignKey(
                        name: "FK_Shipment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipment_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipment_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipment_SalesOrder_salesOrderId",
                        column: x => x.salesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "salesOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipment_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: false),
                    Comments = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerCity = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerCompanyActivity = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerCountry = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerFax = table.Column<string>(maxLength: 20, nullable: true),
                    CustomerMobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    CustomerOfficePhone = table.Column<string>(maxLength: 20, nullable: true),
                    CustomerPostCode = table.Column<string>(maxLength: 5, nullable: true),
                    CustomerStreet = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerTaxOffice = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerVATRegNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerWorkEmail = table.Column<string>(maxLength: 50, nullable: true),
                    EmployeeName = table.Column<string>(maxLength: 30, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    Finalized = table.Column<bool>(nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    InvoiceBalance = table.Column<decimal>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    OfficePhone = table.Column<string>(maxLength: 50, nullable: true),
                    Paid = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    TaxOffice = table.Column<string>(maxLength: 50, nullable: true),
                    TotalBeforeDiscount = table.Column<decimal>(nullable: false),
                    TotalProductVAT = table.Column<decimal>(nullable: false),
                    VATNumber = table.Column<string>(maxLength: 50, nullable: true),
                    branchName = table.Column<string>(maxLength: 50, nullable: true),
                    city = table.Column<string>(maxLength: 30, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerName = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    shipmentId = table.Column<string>(nullable: true),
                    street1 = table.Column<string>(maxLength: 50, nullable: true),
                    totalDiscountAmount = table.Column<decimal>(nullable: false),
                    totalOrderAmount = table.Column<decimal>(nullable: false),
                    totalPaymentReceive = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Shipment_shipmentId",
                        column: x => x.shipmentId,
                        principalTable: "Shipment",
                        principalColumn: "shipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentLine",
                columns: table => new
                {
                    shipmentLineId = table.Column<string>(maxLength: 38, nullable: false),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    qtyInventory = table.Column<float>(nullable: false),
                    qtyShipment = table.Column<float>(nullable: false),
                    shipmentId = table.Column<string>(maxLength: 38, nullable: true),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentLine", x => x.shipmentLineId);
                    table.ForeignKey(
                        name: "FK_ShipmentLine_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentLine_Shipment_shipmentId",
                        column: x => x.shipmentId,
                        principalTable: "Shipment",
                        principalColumn: "shipmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentLine_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    InvoiceLineId = table.Column<string>(maxLength: 38, nullable: false),
                    Discount = table.Column<decimal>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    ProductVAT = table.Column<decimal>(nullable: false),
                    ProductVATAmount = table.Column<decimal>(nullable: false),
                    Qty = table.Column<float>(nullable: false),
                    SpecialTaxAmount = table.Column<decimal>(nullable: false),
                    SpecialTaxDiscount = table.Column<decimal>(nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalBeforeDiscount = table.Column<decimal>(nullable: true),
                    TotalSpecialTaxAmount = table.Column<decimal>(nullable: false),
                    TotalWithSpecialTax = table.Column<decimal>(nullable: false),
                    UnitCost = table.Column<decimal>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceive",
                columns: table => new
                {
                    PaymentReceiveId = table.Column<string>(maxLength: 38, nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: true),
                    IsFullPayment = table.Column<bool>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentReceiveName = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceive", x => x.PaymentReceiveId);
                    table.ForeignKey(
                        name: "FK_PaymentReceive_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentReceive_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentReceive_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CashRepository_EmployeeId",
                table: "CashRepository",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CustomerId",
                table: "Catalog",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogLine_CatalogId",
                table: "CatalogLine",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogLine_ProductId",
                table: "CatalogLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_EmployeeId",
                table: "Customer",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLine_customerId",
                table: "CustomerLine",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_shipmentId",
                table: "Invoice",
                column: "shipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ProductId",
                table: "InvoiceLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_EmployeeId",
                table: "PaymentReceive",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_InvoiceId",
                table: "PaymentReceive",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_PaymentTypeId",
                table: "PaymentReceive",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_branchId",
                table: "PurchaseOrder",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_vendorId",
                table: "PurchaseOrder",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_productId",
                table: "PurchaseOrderLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_purchaseOrderId",
                table: "PurchaseOrderLine",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receiving_branchId",
                table: "Receiving",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Receiving_purchaseOrderId",
                table: "Receiving",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receiving_vendorId",
                table: "Receiving",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Receiving_warehouseId",
                table: "Receiving",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLine_branchId",
                table: "ReceivingLine",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLine_productId",
                table: "ReceivingLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLine_receivingId",
                table: "ReceivingLine",
                column: "receivingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingLine_warehouseId",
                table: "ReceivingLine",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_EmployeeId",
                table: "SalesOrder",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_branchId",
                table: "SalesOrder",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_customerId",
                table: "SalesOrder",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_customerLineId",
                table: "SalesOrder",
                column: "customerLineId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_EmployeeId",
                table: "Shipment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_branchId",
                table: "Shipment",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_customerId",
                table: "Shipment",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_salesOrderId",
                table: "Shipment",
                column: "salesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_warehouseId",
                table: "Shipment",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLine_branchId",
                table: "ShipmentLine",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLine_productId",
                table: "ShipmentLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLine_shipmentId",
                table: "ShipmentLine",
                column: "shipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentLine_warehouseId",
                table: "ShipmentLine",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_branchFrombranchId",
                table: "TransferIn",
                column: "branchFrombranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_branchTobranchId",
                table: "TransferIn",
                column: "branchTobranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_transferOrderId",
                table: "TransferIn",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_warehouseFromwarehouseId",
                table: "TransferIn",
                column: "warehouseFromwarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_warehouseTowarehouseId",
                table: "TransferIn",
                column: "warehouseTowarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferInLine_productId",
                table: "TransferInLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferInLine_transferInId",
                table: "TransferInLine",
                column: "transferInId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_branchFrombranchId",
                table: "TransferOrder",
                column: "branchFrombranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_branchTobranchId",
                table: "TransferOrder",
                column: "branchTobranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_warehouseFromwarehouseId",
                table: "TransferOrder",
                column: "warehouseFromwarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_warehouseTowarehouseId",
                table: "TransferOrder",
                column: "warehouseTowarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderLine_productId",
                table: "TransferOrderLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderLine_transferOrderId",
                table: "TransferOrderLine",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_branchFrombranchId",
                table: "TransferOut",
                column: "branchFrombranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_branchTobranchId",
                table: "TransferOut",
                column: "branchTobranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_transferOrderId",
                table: "TransferOut",
                column: "transferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_warehouseFromwarehouseId",
                table: "TransferOut",
                column: "warehouseFromwarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_warehouseTowarehouseId",
                table: "TransferOut",
                column: "warehouseTowarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOutLine_productId",
                table: "TransferOutLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOutLine_transferOutId",
                table: "TransferOutLine",
                column: "transferOutId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorLine_vendorId",
                table: "VendorLine",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_branchId",
                table: "Warehouse",
                column: "branchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CatalogLine");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "NumberSequence");

            migrationBuilder.DropTable(
                name: "PaymentReceive");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLine");

            migrationBuilder.DropTable(
                name: "ReceivingLine");

            migrationBuilder.DropTable(
                name: "SalesOrderLine");

            migrationBuilder.DropTable(
                name: "ShipmentLine");

            migrationBuilder.DropTable(
                name: "TransferInLine");

            migrationBuilder.DropTable(
                name: "TransferOrderLine");

            migrationBuilder.DropTable(
                name: "TransferOutLine");

            migrationBuilder.DropTable(
                name: "VendorLine");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CashRepository");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Receiving");

            migrationBuilder.DropTable(
                name: "TransferIn");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "TransferOut");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "TransferOrder");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "CustomerLine");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
