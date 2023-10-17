using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class AddDefaultADminUser : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "rockaway-sample-admin-user", 0, "5a254f57-ad9f-43b4-b405-38d756a98a55", "admin@rockaway.dev", true, true, null, "ADMIN@ROCKAWAY.DEV", "ADMIN@ROCKAWAY.DEV", "AQAAAAIAAYagAAAAEHKVlYe/Wiagre6rrsh3+c9HWfl8gpaZZ0EqOYB5mrE8lU3Z9XGeIxUhri/j9uFg6Q==", null, true, "5a5eab5c-abde-4038-998f-b0ce7a584392", false, "admin@rockaway.dev" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user");
		}
	}
}