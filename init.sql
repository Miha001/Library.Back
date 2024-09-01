CREATE TABLE "Roles" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);

INSERT INTO "Roles" ("Name") VALUES ('Admin'), ('User');

CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "PasswordHash" VARCHAR(256) NOT NULL,
    "FirstName" VARCHAR(100),
    "SecondName" VARCHAR(100),
    "MiddleName" VARCHAR(100),
    "RoleId" INT NOT NULL,
	"Username" VARCHAR(100) NOT NULL UNIQUE,
    "IsDeleted" BOOLEAN DEFAULT FALSE,
	"Email" VARCHAR(100) NOT NULL,
    FOREIGN KEY ("RoleId") REFERENCES "Roles"("Id")
);

CREATE TABLE "AuthorizationTokens" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "Token" VARCHAR(512) NOT NULL,
    "Expiration" TIMESTAMP NOT NULL,
    "IsStatic" BOOLEAN DEFAULT FALSE,
    CONSTRAINT "FK_AuthorizationTokens_Users" FOREIGN KEY ("UserId") REFERENCES "Users"("Id") ON DELETE CASCADE
);

CREATE TABLE "Books" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Author" VARCHAR(200) NOT NULL,
    "PublishedDate" DATE,
    "IsDeleted" BOOLEAN DEFAULT FALSE
);

CREATE TABLE "Comments" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "BookId" INT NOT NULL,
    "Content" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsDeleted" BOOLEAN DEFAULT FALSE,
    FOREIGN KEY ("UserId") REFERENCES "Users"("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id")
);

CREATE TABLE "FavoriteBooks" (
    "UserId" INT NOT NULL,
    "BookId" INT NOT NULL,
    PRIMARY KEY ("UserId", "BookId"),
    FOREIGN KEY ("UserId") REFERENCES "Users"("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id")
);