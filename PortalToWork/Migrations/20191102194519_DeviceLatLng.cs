using Microsoft.EntityFrameworkCore.Migrations;

namespace PortalToWork.Migrations
{
    public partial class DeviceLatLng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GeoLocation",
                table: "Devices",
                newName: "lng");

            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "lng",
                table: "Devices",
                newName: "GeoLocation");
        }
    }
}
