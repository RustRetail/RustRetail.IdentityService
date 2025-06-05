-- Roles
INSERT INTO public."Roles"
("Id", "Name", "NormalizedName", "Description", "CreatedDateTime", "UpdatedDateTime")
VALUES('7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid, 'Administrator', 'ADMINISTRATOR', 'Has full access to all system features and settings.', '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Roles"
("Id", "Name", "NormalizedName", "Description", "CreatedDateTime", "UpdatedDateTime")
VALUES('6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, 'User', 'USER', 'Has limited access to system features, typically restricted to viewing and interacting with their own data.', '2025-06-01 00:00:00.000', NULL);

-- Permissions
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('49482324-9b9d-4a0e-8268-2ed7ca01f1f8'::uuid, 'manage_users', 'Full access to create, update, and delete user accounts.', '2025-06-01 00:00:00.000', '7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('8bf2c564-bc8f-4214-be2e-9fa024d2b2c6'::uuid, 'manage_products', 'Full control over adding, updating, or removing products.', '2025-06-01 00:00:00.000', '7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('34a1dbd0-f12a-46d2-8e7e-779a5e9b3649'::uuid, 'manage_roles', 'Can assign roles and permissions to other users.', '2025-06-01 00:00:00.000', '7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('4debffee-338b-4dc2-a53a-a3a75bd654ed'::uuid, 'manage_orders', 'Can view, update, and cancel any customer orders.', '2025-06-01 00:00:00.000', '7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('012608bc-de74-4c1f-a83d-ad2f69c6bf2e'::uuid, 'browse_products', 'Can view and search for products in the store.', '2025-06-01 00:00:00.000', '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('413f41f3-6f8f-40ef-a92d-15e69c75a402'::uuid, 'place_orders', 'Can add items to cart and complete checkout.', '2025-06-01 00:00:00.000', '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('33df5580-323e-4394-a0d0-7483fa09dbcf'::uuid, 'view_own_orders', 'Can view their own order history and status.', '2025-06-01 00:00:00.000', '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid);
INSERT INTO public."Permissions"
("Id", "Name", "Description", "CreatedDateTime", "RoleId")
VALUES('6a314214-b2d5-4aec-805f-82e22b2bb42c'::uuid, 'update_profile', 'Can edit their own account and profile information.', '2025-06-01 00:00:00.000', '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid);

-- Users
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('fd67165e-5f99-4c9d-b756-c017d1bf313c'::uuid, 'Admin', 'ADMIN', 'hoant.3010.dev@gmail.com', 'HOANT.3010.DEV@GMAIL.COM', true, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0346476019', true, false, false, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('dade8f41-94e8-4361-9637-76e0c6427730'::uuid, 'User', 'USER', 'hoant.3010.personal@gmail.com', 'HOANT.3010.PERSONAL@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0346476019', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('b3cdfb45-124e-43ab-9e75-cecef2641522'::uuid, 'TestUser1', 'TESTUSER1', 'testuser1@gmail.com', 'TESTUSER1@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0123456781', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('e1e7b6cb-1b0d-42b0-a2e7-4918bc39fcc2'::uuid, 'TestUser2', 'TESTUSER2', 'testuser2@gmail.com', 'TESTUSER2@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0123456782', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('2c5e47eb-b6e7-42f0-981b-ffff9ad66dc5'::uuid, 'TestUser3', 'TESTUSER3', 'testuser3@gmail.com', 'TESTUSER3@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0123456783', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('b7d0cbe5-0b33-4129-ae63-18c84e546913'::uuid, 'TestUser4', 'TESTUSER4', 'testuser4@gmail.com', 'TESTUSER4@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0123456784', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);
INSERT INTO public."Users"
("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "LockoutEnd", "AccessFailedCount", "CreatedDateTime", "UpdatedDateTime")
VALUES('1d234df2-8cc0-4ac4-a83b-bd6e67d809d5'::uuid, 'TestUser5', 'TESTUSER5', 'testuser5@gmail.com', 'TESTUSER5@GMAIL.COM', false, '$2a$11$erfRkWHiwIkya9qrnKH67e5J4BvcobCJVs4l1z6y60BMZcBpKaHcy', '0123456785', false, false, true, NULL, 0, '2025-06-01 00:00:00.000', NULL);

-- UserRoles
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('fd67165e-5f99-4c9d-b756-c017d1bf313c'::uuid, '7087c337-8c4d-4a38-9338-9d841deb5d6a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('dade8f41-94e8-4361-9637-76e0c6427730'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('b3cdfb45-124e-43ab-9e75-cecef2641522'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('e1e7b6cb-1b0d-42b0-a2e7-4918bc39fcc2'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('2c5e47eb-b6e7-42f0-981b-ffff9ad66dc5'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('b7d0cbe5-0b33-4129-ae63-18c84e546913'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');
INSERT INTO public."UserRoles"
("UserId", "RoleId", "AssignedDateTime")
VALUES('1d234df2-8cc0-4ac4-a83b-bd6e67d809d5'::uuid, '6f924082-0941-4b52-a3ab-31435f76e66a'::uuid, '2025-06-01 00:00:00.000');