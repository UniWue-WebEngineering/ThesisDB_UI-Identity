using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ThesisDB.Models
{
    public static class DataSeeder
    {
        public static void SeedData(ThesisDbContext context)
        {
            // Sicherstellen, dass die Datenbank existiert und die Migrationen angewendet wurden
            context.Database.Migrate();

            // 1. Studiengänge (Programmes) seeden, falls noch keine existieren
            if (!context.Programmes.Any())
            {
                var programmes = new List<Programme>
                {
                    new Programme { Name = "B.Sc. Wirtschaftsinformatik" },
                    new Programme { Name = "B.Sc. Digital Business and Data Science" },
                    new Programme { Name = "M.Sc. Information Systems" },
                    new Programme { Name = "M.Sc. Management" }
                };

                context.Programmes.AddRange(programmes);
                context.SaveChanges();
            }

            // 2. Theses seeden, falls noch keine existieren
            if (!context.Theses.Any())
            {
                var programmes = context.Programmes.ToList();
                var random = new Random();

                var theses = new List<Thesis>
                {
                    new Thesis
                    {
                        Title = "Analyse von Blockchain-Technologien im Supply-Chain-Management",
                        Description = "Untersuchung der Einsatzmöglichkeiten und Potenziale von Blockchain zur Optimierung von Lieferketten.",
                        Status = Thesis.ThesisStatus.ausgeschrieben,
                        Type = Thesis.ThesisType.Master,
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Entwicklung eines KI-basierten Chatbots für den Kundenservice",
                        Description = "Konzeption und prototypische Implementierung eines Chatbots zur Automatisierung von Kundenanfragen.",
                        Status = Thesis.ThesisStatus.angemeldet,
                        Type = Thesis.ThesisType.Bachelor,
                        StartDate = new DateTime(2023, 10, 15),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Cloud-Migration-Strategien für KMU",
                        Description = "Vergleich und Bewertung von Strategien für die Migration von IT-Infrastrukturen kleiner und mittlerer Unternehmen in die Cloud.",
                        Status = Thesis.ThesisStatus.abgegeben,
                        Type = Thesis.ThesisType.Bachelor,
                        StartDate = new DateTime(2023, 04, 01),
                        EndDate = new DateTime(2023, 09, 30),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Big Data Analytics im E-Commerce zur Personalisierung von Kundenempfehlungen",
                        Description = "Analyse von Nutzerdaten zur Verbesserung von Produktempfehlungen in Online-Shops.",
                        Status = Thesis.ThesisStatus.bewertet,
                        Type = Thesis.ThesisType.Master,
                        StartDate = new DateTime(2022, 11, 02),
                        EndDate = new DateTime(2023, 05, 01),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "IT-Sicherheitskonzepte für mobile Anwendungen im Finanzsektor",
                        Description = "Erarbeitung eines umfassenden Sicherheitskonzepts für eine Banking-App.",
                        Status = Thesis.ThesisStatus.ausgeschrieben,
                        Type = Thesis.ThesisType.Master,
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Prozessoptimierung durch Robotic Process Automation (RPA) in der Verwaltung",
                        Description = "Identifikation und Automatisierung von repetitiven Verwaltungsprozessen mittels RPA.",
                        Status = Thesis.ThesisStatus.angemeldet,
                        Type = Thesis.ThesisType.Bachelor,
                        StartDate = new DateTime(2023, 11, 01),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Agiles Projektmanagement in der Softwareentwicklung: Eine kritische Analyse",
                        Description = "Vergleich von Scrum und Kanban in der Praxis und Ableitung von Handlungsempfehlungen.",
                        Status = Thesis.ThesisStatus.nicht_abgeschlossen,
                        Type = Thesis.ThesisType.Bachelor,
                        StartDate = new DateTime(2022, 08, 01),
                        EndDate = new DateTime(2023, 02, 15),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Digital-Twin-Konzepte für die Industrie 4.0",
                        Description = "Untersuchung, wie digitale Zwillinge zur Überwachung und Steuerung von Produktionsanlagen eingesetzt werden können.",
                        Status = Thesis.ThesisStatus.ausgeschrieben,
                        Type = Thesis.ThesisType.Master,
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Customer-Relationship-Management mit Salesforce",
                        Description = "Einführung und Anpassung von Salesforce zur Verbesserung der Kundenbeziehungen in einem mittelständischen Unternehmen.",
                        Status = Thesis.ThesisStatus.abgegeben,
                        Type = Thesis.ThesisType.Bachelor,
                        StartDate = new DateTime(2023, 05, 10),
                        EndDate = new DateTime(2023, 11, 09),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    },
                    new Thesis
                    {
                        Title = "Einsatz von maschinellem Lernen zur Vorhersage von Aktienkursen",
                        Description = "Entwicklung eines Prognosemodells auf Basis historischer Kursdaten und neuronaler Netze.",
                        Status = Thesis.ThesisStatus.angemeldet,
                        Type = Thesis.ThesisType.Master,
                        StartDate = new DateTime(2023, 09, 01),
                        LastModified = DateTime.UtcNow,
                        ProgrammeId = programmes[random.Next(programmes.Count)].Id
                    }
                };

                context.Theses.AddRange(theses);
                context.SaveChanges();
            }
        }
    }
}
