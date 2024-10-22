using Microsoft.EntityFrameworkCore;
using UNTELSLAB.Models;

namespace UNTELSLAB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EquipoLaboratorio> EquipoLaboratorio { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<DatosEquipo> DatosEquipo { get; set; }
        public DbSet<FichaTecnicaEquipo> FichaTecnicaEquipo { get; set; }
        public DbSet<InformeMantenimientoEquipo> InformeMantenimientoEquipo { get; set; }
        public DbSet<InformeCalibracion> InformeCalibracion { get; set; }
        public DbSet<HistoricoFallasEquipo> HistoricoFallasEquipo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EquipoLaboratorio>(entity =>
            {
                entity.ToTable("equipo_laboratorio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(255);
                entity.Property(e => e.IdLaboratorio).HasColumnName("idLaboratorio");

                entity.HasOne(e => e.Laboratorio)
                    .WithMany()
                    .HasForeignKey(e => e.IdLaboratorio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DatosEquipo)
                    .WithOne(d => d.EquipoLaboratorio)
                    .HasForeignKey<DatosEquipo>(d => d.IdEquipo)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.FichaTecnicaEquipo)
                    .WithOne(f => f.Equipo)
                    .HasForeignKey<FichaTecnicaEquipo>(f => f.IdEquipo)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.InformesMantenimiento)
                    .WithOne(i => i.Equipo)
                    .HasForeignKey(i => i.IdEquipo)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.InformesCalibracion)
                    .WithOne(i => i.Equipo)
                    .HasForeignKey(i => i.IdEquipo)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.HistoricoFallas)
                    .WithOne(h => h.Equipo)
                    .HasForeignKey(h => h.IdEquipo)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DatosEquipo>(entity =>
            {
                entity.ToTable("datos_equipo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Procesador).HasColumnName("procesador").HasMaxLength(255);
                entity.Property(e => e.SistemaOperativo).HasColumnName("sistema_operativo").HasMaxLength(255);
                entity.Property(e => e.Memoria).HasColumnName("memoria").HasMaxLength(255);
                entity.Property(e => e.Manual).HasColumnName("manual").HasMaxLength(255);
                entity.Property(e => e.AnoFabricacion).HasColumnName("ano_fabricacion");
                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            });

            modelBuilder.Entity<FichaTecnicaEquipo>(entity =>
            {
                entity.ToTable("ficha_tecnica_equipo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Marca).HasColumnName("marca").HasMaxLength(255);
                entity.Property(e => e.Serie).HasColumnName("serie").HasMaxLength(255);
                entity.Property(e => e.Tension).HasColumnName("tension").HasMaxLength(255);
                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia").HasMaxLength(255);
                entity.Property(e => e.Capacidad).HasColumnName("capacidad").HasMaxLength(255);
                entity.Property(e => e.Presion).HasColumnName("presion").HasMaxLength(255);
                entity.Property(e => e.Resolucion).HasColumnName("resolucion").HasMaxLength(255);
                entity.Property(e => e.AnoFabricacion).HasColumnName("ano_fabricacion");
                entity.Property(e => e.Ubicacion).HasColumnName("ubicacion").HasMaxLength(255);
                entity.Property(e => e.TipoModelo).HasColumnName("tipo_modelo").HasMaxLength(255);
                entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(255);
                entity.Property(e => e.Corriente).HasColumnName("corriente").HasMaxLength(255);
                entity.Property(e => e.Potencia).HasColumnName("potencia").HasMaxLength(255);
                entity.Property(e => e.Velocidad).HasColumnName("velocidad").HasMaxLength(255);
                entity.Property(e => e.Temperatura).HasColumnName("temperatura").HasMaxLength(255);
                entity.Property(e => e.Canales).HasColumnName("canales").HasMaxLength(255);
                entity.Property(e => e.FechaOperatividad).HasColumnName("fecha_operatividad");
                entity.Property(e => e.FechaVigencia).HasColumnName("fecha_vigencia");
                entity.Property(e => e.FrecuenciaCalibracion).HasColumnName("frecuencia_calibracion").HasMaxLength(255);
                entity.Property(e => e.InstruccionOperacion).HasColumnName("instruccion_operacion").HasMaxLength(255);
                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
                entity.Property(e => e.FrecuenciaMantenimiento).HasColumnName("frecuencia_mantenimiento");
            });

            modelBuilder.Entity<InformeMantenimientoEquipo>(entity =>
            {
                entity.ToTable("informe_mantenimiento_equipo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.LugarMantenimiento).HasColumnName("lugar_mantenimiento").HasMaxLength(255);
                entity.Property(e => e.NumeroInforme).HasColumnName("numero_informe").HasMaxLength(255);
                entity.Property(e => e.RevisadoPor).HasColumnName("revisado_por").HasMaxLength(255);
                entity.Property(e => e.Observaciones).HasColumnName("observaciones").HasMaxLength(255);
                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            });

            modelBuilder.Entity<InformeCalibracion>(entity =>
            {
                entity.ToTable("informe_calibracion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Lugar).HasColumnName("lugar").HasMaxLength(255);
                entity.Property(e => e.InformeCertificado).HasColumnName("informe_certificado").HasMaxLength(255);
                entity.Property(e => e.RevisadoPor).HasColumnName("revisado_por").HasMaxLength(255);
                entity.Property(e => e.Observaciones).HasColumnName("observaciones").HasMaxLength(255);
                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            });

            modelBuilder.Entity<HistoricoFallasEquipo>(entity =>
            {
                entity.ToTable("historico_fallas_equipo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Ocurrencia).HasColumnName("ocurrencia").HasMaxLength(255);
                entity.Property(e => e.RevisadoPor).HasColumnName("revisado_por").HasMaxLength(255);
                entity.Property(e => e.Observaciones).HasColumnName("observaciones").HasMaxLength(255);
                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            });
        }
    }
}