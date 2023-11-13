using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Aplicacao.ModuloNota;
using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infra.Orm.Compartilhado;
using NoteKeeper.Infra.Orm.ModuloCategoria;
using NoteKeeper.Infra.Orm.ModuloNota;
using NoteKeeper.WebApi.Config;
using NoteKeeper.WebApi.Config.AutoMapperProfiles;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi
{    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, NoteKeeperDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<IRepositorioCategoria, RepositorioCategoriaOrm>();
            builder.Services.AddTransient<ServicoCategoria>();

            builder.Services.AddTransient<IRepositorioNota, RepositorioNotaOrm>();
            builder.Services.AddTransient<ServicoNota>();

            builder.Services.AddTransient<ConfigurarCategoriaMappingAction>();

            builder.Services.AddAutoMapper(config => 
            {
                config.AddProfile<CategoriaProfile>();
                config.AddProfile<NotaProfile>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}