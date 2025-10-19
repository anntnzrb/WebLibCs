using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebLibCs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Autores",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ImagenRuta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Autores", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Libros",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                AnioPublicacion = table.Column<int>(type: "int", nullable: false),
                AutorId = table.Column<int>(type: "int", nullable: false),
                ImagenRuta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Libros", x => x.Id);
                table.ForeignKey(
                    name: "FK_Libros_Autores_AutorId",
                    column: x => x.AutorId,
                    principalTable: "Autores",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Autores",
            columns: new[] { "Id", "ImagenRuta", "Nombre" },
            values: new object[,]
            {
                { 1, null, "Gabriel García Márquez" },
                { 2, "/imagenes/jkrowling.png", "J.K. Rowling" },
                { 3, null, "Stephen King" },
                { 4, null, "Isabel Allende" },
                { 5, "/imagenes/orwell.jpg", "George Orwell" },
                { 6, null, "Jane Austen" },
                { 7, "/imagenes/fitzgerald.jpg", "F. Scott Fitzgerald" },
                { 8, "/imagenes/murakami.webp", "Haruki Murakami" },
                { 9, "/imagenes/octavio_paz.jpg", "Octavio Paz" },
                { 10, "/imagenes/woolf.jpg", "Virginia Woolf" },
            });

        migrationBuilder.InsertData(
            table: "Libros",
            columns: new[] { "Id", "AnioPublicacion", "AutorId", "ImagenRuta", "Titulo" },
            values: new object[,]
            {
                { 1, 1967, 1, null, "Cien años de soledad" },
                { 2, 1997, 2, "/imagenes/harryp.webp", "Harry Potter y la piedra filosofal" },
                { 3, 1977, 3, null, "El resplandor" },
                { 4, 1982, 1, null, "El amor en los tiempos del cólera" },
                { 5, 1998, 2, null, "Harry Potter y la cámara secreta" },
                { 6, 1986, 3, null, "It" },
                { 7, 1982, 4, null, "La casa de los espíritus" },
                { 8, 1949, 5, null, "1984" },
                { 9, 1813, 6, null, "Orgullo y prejuicio" },
                { 10, 1925, 7, "/imagenes/gatsby.webp", "El gran Gatsby" },
                { 11, 1987, 8, "/imagenes/tokyo-blues.jpg", "Tokio Blues" },
                { 12, 1958, 9, "/imagenes/laberinto.webp", "El laberinto de la soledad" },
                { 13, 1925, 10, "/imagenes/dalloway.jpg", "Mrs Dalloway" },
                { 14, 2000, 2, "/imagenes/harryp.webp", "Harry Potter y el cáliz de fuego" },
                { 15, 1945, 5, "/imagenes/reb-granja.webp", "Rebelión en la granja" },
            });

        migrationBuilder.CreateIndex(
            name: "IX_Libros_AutorId",
            table: "Libros",
            column: "AutorId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Libros");

        migrationBuilder.DropTable(
            name: "Autores");
    }
}
