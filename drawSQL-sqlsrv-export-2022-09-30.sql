CREATE TABLE "Product"(
    "id" INT NOT NULL,
    "Name" NVARCHAR(255) NULL,
    "Price" FLOAT NULL,
    "Quantity" INT NOT NULL,
    "Description" NVARCHAR(255) NOT NULL,
    "CategroyId" INT NOT NULL,
    "StateId" BIT NOT NULL,
    "ProductImageId" INT NOT NULL
);
ALTER TABLE
    "Product" ADD CONSTRAINT "product_id_primary" PRIMARY KEY("id");
CREATE TABLE "ProductImage"(
    "id" INT NOT NULL,
    "Image" NVARCHAR(255) NOT NULL,
    "MainImage" BIT NOT NULL
);
ALTER TABLE
    "ProductImage" ADD CONSTRAINT "productimage_id_primary" PRIMARY KEY("id");
CREATE TABLE "Category"(
    "id" INT NOT NULL,
    "Name" INT NOT NULL
);
ALTER TABLE
    "Category" ADD CONSTRAINT "category_id_primary" PRIMARY KEY("id");
CREATE TABLE "Payment"(
    "id" INT NOT NULL,
    "PaymentMethodId" INT NOT NULL,
    "Details" INT NOT NULL
);
ALTER TABLE
    "Payment" ADD CONSTRAINT "payment_id_primary" PRIMARY KEY("id");
CREATE TABLE "Order"(
    "id" INT NOT NULL,
    "CreateDate" DATETIME NOT NULL,
    "Note" NVARCHAR(255) NOT NULL,
    "UserId" INT NOT NULL,
    "ShipperId" INT NOT NULL,
    "PaymentId" INT NOT NULL
);
ALTER TABLE
    "Order" ADD CONSTRAINT "order_id_primary" PRIMARY KEY("id");
CREATE TABLE "Shipper"(
    "id" INT NOT NULL,
    "UserId" INT NOT NULL,
    "IdentifyCard" NCHAR(255) NOT NULL,
    "ShippingUnit" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Shipper" ADD CONSTRAINT "shipper_id_primary" PRIMARY KEY("id");
CREATE TABLE "OrderDetail"(
    "OrderId" INT NOT NULL,
    "ProductId" INT NOT NULL,
    "Quantity" INT NOT NULL,
    "Price" FLOAT NOT NULL,
    "CouponId" INT NOT NULL
);
ALTER TABLE
    "OrderDetail" ADD CONSTRAINT "orderdetail_orderid_primary" PRIMARY KEY("OrderId");
ALTER TABLE
    "OrderDetail" ADD CONSTRAINT "orderdetail_productid_primary" PRIMARY KEY("ProductId");
CREATE TABLE "Role"(
    "id" INT NOT NULL,
    "Name" INT NOT NULL
);
ALTER TABLE
    "Role" ADD CONSTRAINT "role_id_primary" PRIMARY KEY("id");
CREATE TABLE "User"(
    "id" INT NOT NULL,
    "Username" NVARCHAR(255) NOT NULL,
    "Password" NVARCHAR(255) NOT NULL,
    "StateId" BIT NOT NULL
);
ALTER TABLE
    "User" ADD CONSTRAINT "user_id_primary" PRIMARY KEY("id");
CREATE TABLE "UserInformation"(
    "id" INT NOT NULL,
    "UserId" INT NOT NULL,
    "Email" INT NOT NULL,
    "Address" INT NOT NULL,
    "Phone" INT NOT NULL,
    "Avatar" INT NOT NULL,
    "FirstName" INT NOT NULL,
    "LastName" INT NOT NULL,
    "DateOfBirth" INT NOT NULL,
    "Gender" INT NOT NULL
);
ALTER TABLE
    "UserInformation" ADD CONSTRAINT "userinformation_id_primary" PRIMARY KEY("id");
CREATE TABLE "UserRole"(
    "id" INT NOT NULL,
    "UserId" INT NOT NULL,
    "RoleId" INT NOT NULL
);
ALTER TABLE
    "UserRole" ADD CONSTRAINT "userrole_id_primary" PRIMARY KEY("id");
CREATE TABLE "PaymentMethod"(
    "id" INT NOT NULL,
    "Name" INT NOT NULL,
    "Config" INT NOT NULL,
    "CreateDate" INT NOT NULL
);
ALTER TABLE
    "PaymentMethod" ADD CONSTRAINT "paymentmethod_id_primary" PRIMARY KEY("id");
CREATE TABLE "Coupon"(
    "id" INT NOT NULL,
    "percent" INT NOT NULL,
    "applyType" INT NOT NULL,
    "applyFor" INT NOT NULL
);
ALTER TABLE
    "Coupon" ADD CONSTRAINT "coupon_id_primary" PRIMARY KEY("id");
CREATE TABLE "Store"(
    "id" INT NOT NULL,
    "Name" INT NOT NULL,
    "Image" INT NOT NULL
);
ALTER TABLE
    "Store" ADD CONSTRAINT "store_id_primary" PRIMARY KEY("id");
ALTER TABLE
    "Product" ADD CONSTRAINT "product_categroyid_foreign" FOREIGN KEY("CategroyId") REFERENCES "Category"("id");
ALTER TABLE
    "Product" ADD CONSTRAINT "product_productimageid_foreign" FOREIGN KEY("ProductImageId") REFERENCES "ProductImage"("id");
ALTER TABLE
    "Order" ADD CONSTRAINT "order_paymentid_foreign" FOREIGN KEY("PaymentId") REFERENCES "Payment"("id");
ALTER TABLE
    "UserInformation" ADD CONSTRAINT "userinformation_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("id");
ALTER TABLE
    "UserRole" ADD CONSTRAINT "userrole_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("id");
ALTER TABLE
    "Order" ADD CONSTRAINT "order_userid_foreign" FOREIGN KEY("UserId") REFERENCES "User"("id");
ALTER TABLE
    "OrderDetail" ADD CONSTRAINT "orderdetail_couponid_foreign" FOREIGN KEY("CouponId") REFERENCES "Coupon"("id");
ALTER TABLE
    "Payment" ADD CONSTRAINT "payment_paymentmethodid_foreign" FOREIGN KEY("PaymentMethodId") REFERENCES "PaymentMethod"("id");
ALTER TABLE
    "UserRole" ADD CONSTRAINT "userrole_roleid_foreign" FOREIGN KEY("RoleId") REFERENCES "Role"("id");