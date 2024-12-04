using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Webb.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SecondAuthDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79a53136-31be-4d3f-a279-605e3c1e3e90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "420c54f4-e3bb-4667-9127-8801455dfed4", "AQAAAAIAAYagAAAAEMs7sh8S9pBAdrTThK6cfvHsi62tqzVxisT2o4o4PIMPUQzh2LfYQfUt5bDKTqAeCQ==", "99683213-bd7d-406d-ad78-5862cc3b0d1f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79a53136-31be-4d3f-a279-605e3c1e3e90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e145424d-4874-4b17-ad10-e23562e0c33a", "AQAAAAIAAYagAAAAEAm20Ea6aJRJdcPcQhUv3/rkd792ikrcTuaucLYIOpx/99eqXoaILF8iNRcNrutMAQ==", "8c8e4d52-0ca3-4106-b3c9-4a0b810f8c4b" });
        }
    }
}
