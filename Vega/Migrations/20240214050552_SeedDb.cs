using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vega.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Makes(Name) values('Make1')");
            migrationBuilder.Sql("Insert into Makes(Name) values('Make2')");
            migrationBuilder.Sql("Insert into Makes(Name) values('Make3')");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make1-ModelA',(select Id from Makes where Name='Make1'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make1-ModelB',(select Id from Makes where Name='Make1'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make1-ModelC',(select Id from Makes where Name='Make1'))");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make2-ModelA',(select Id from Makes where Name='Make2'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make2-ModelB',(select Id from Makes where Name='Make2'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make2-ModelC',(select Id from Makes where Name='Make2'))");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make3-ModelA',(select Id from Makes where Name='Make3'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make3-ModelB',(select Id from Makes where Name='Make3'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) values('Make3-ModelC',(select Id from Makes where Name='Make3'))");

            migrationBuilder.Sql("Insert into Features(Name) values('Feature1')");
            migrationBuilder.Sql("Insert into Features(Name) values('Feature2')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Makes where Name In ('Make1','Make2','Make3')");
            migrationBuilder.Sql("delete from Features where Name In ('Feature1','Feature2')");
        }
    }
}
