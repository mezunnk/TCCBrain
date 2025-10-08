using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainFlow.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioLoginAndCursoAndUniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CdAfiliado",
                table: "cursos",
                newName: "CD_AFILIADO");

            migrationBuilder.RenameIndex(
                name: "IX_cursos_CdAfiliado",
                table: "cursos",
                newName: "IX_cursos_CD_AFILIADO");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_CADASTRO",
                table: "usuario_login",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro.",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())",
                oldComment: "Data de criação do registro.");

            migrationBuilder.AlterColumn<int>(
                name: "CD_AFILIADO",
                table: "cursos",
                type: "int",
                nullable: false,
                comment: "FK para a tabela AFILIADO, indicando o autor do curso.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "UX_USUARIO_EMAIL",
                table: "usuarios",
                column: "TX_EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_USUARIO_EMAIL",
                table: "usuarios");

            migrationBuilder.RenameColumn(
                name: "CD_AFILIADO",
                table: "cursos",
                newName: "CdAfiliado");

            migrationBuilder.RenameIndex(
                name: "IX_cursos_CD_AFILIADO",
                table: "cursos",
                newName: "IX_cursos_CdAfiliado");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_CADASTRO",
                table: "usuario_login",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                comment: "Data de criação do registro.",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro.");

            migrationBuilder.AlterColumn<int>(
                name: "CdAfiliado",
                table: "cursos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "FK para a tabela AFILIADO, indicando o autor do curso.");
        }
    }
}
