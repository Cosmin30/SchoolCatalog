using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(SchoolCatalogContext context)
        {
            if (context.Clase.Any() || context.Elevi.Any() || context.Profesori.Any())
            {
                return;
            }

            var clase = new List<Clasa>
            {
                new Clasa { NumeClasa = "9A", ProfilClasa = "Real - Matematică-Informatică" },
                new Clasa { NumeClasa = "9B", ProfilClasa = "Real - Științe ale Naturii" },
                new Clasa { NumeClasa = "10A", ProfilClasa = "Real - Matematică-Informatică" },
                new Clasa { NumeClasa = "10B", ProfilClasa = "Uman - Filologie" },
                new Clasa { NumeClasa = "11A", ProfilClasa = "Real - Matematică-Informatică" },
                new Clasa { NumeClasa = "12A", ProfilClasa = "Real - Matematică-Informatică" }
            };
            context.Clase.AddRange(clase);
            await context.SaveChangesAsync();

            // 2. Creează Profesori
            var profesori = new List<Profesor>
            {
                new Profesor 
                { 
                    NumeProfesor = "Popescu", 
                    PrenumeProfesor = "Ion", 
                    EmailProfesor = "ion.popescu@scoala.ro",
                    DataNasterii = new DateTime(1980, 5, 15)
                },
                new Profesor 
                { 
                    NumeProfesor = "Ionescu", 
                    PrenumeProfesor = "Maria", 
                    EmailProfesor = "maria.ionescu@scoala.ro",
                    DataNasterii = new DateTime(1985, 8, 22)
                },
                new Profesor 
                { 
                    NumeProfesor = "Georgescu", 
                    PrenumeProfesor = "Andrei", 
                    EmailProfesor = "andrei.georgescu@scoala.ro",
                    DataNasterii = new DateTime(1978, 3, 10)
                },
                new Profesor 
                { 
                    NumeProfesor = "Dumitrescu", 
                    PrenumeProfesor = "Elena", 
                    EmailProfesor = "elena.dumitrescu@scoala.ro",
                    DataNasterii = new DateTime(1982, 11, 5)
                },
                new Profesor 
                { 
                    NumeProfesor = "Marinescu", 
                    PrenumeProfesor = "Mihai", 
                    EmailProfesor = "mihai.marinescu@scoala.ro",
                    DataNasterii = new DateTime(1975, 6, 18)
                }
            };
            context.Profesori.AddRange(profesori);
            await context.SaveChangesAsync();

            var materii = new List<Materie>
            {
                new Materie { NumeMaterie = "Matematică", ProfesorId = profesori[0].IdProfesor },
                new Materie { NumeMaterie = "Informatică", ProfesorId = profesori[1].IdProfesor },
                new Materie { NumeMaterie = "Fizică", ProfesorId = profesori[2].IdProfesor },
                new Materie { NumeMaterie = "Chimie", ProfesorId = profesori[2].IdProfesor },
                new Materie { NumeMaterie = "Limba Română", ProfesorId = profesori[3].IdProfesor },
                new Materie { NumeMaterie = "Limba Engleză", ProfesorId = profesori[3].IdProfesor },
                new Materie { NumeMaterie = "Istorie", ProfesorId = profesori[4].IdProfesor },
                new Materie { NumeMaterie = "Geografie", ProfesorId = profesori[4].IdProfesor },
                new Materie { NumeMaterie = "Biologie", ProfesorId = profesori[2].IdProfesor },
                new Materie { NumeMaterie = "Educație Fizică", ProfesorId = profesori[4].IdProfesor }
            };
            context.Materii.AddRange(materii);
            await context.SaveChangesAsync();

            // 4. Creează Elevi pentru clasa 9A
            var elevi9A = new List<Elev>
            {
                new Elev { NumeElev = "Popescu", PrenumeElev = "Alexandra", DataNasterii = new DateTime(2009, 3, 15), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Ionescu", PrenumeElev = "Andrei", DataNasterii = new DateTime(2009, 5, 22), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Georgescu", PrenumeElev = "Maria", DataNasterii = new DateTime(2009, 7, 10), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Dumitrescu", PrenumeElev = "Mihai", DataNasterii = new DateTime(2009, 1, 8), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Marinescu", PrenumeElev = "Elena", DataNasterii = new DateTime(2009, 9, 12), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Constantinescu", PrenumeElev = "Cristian", DataNasterii = new DateTime(2009, 4, 25), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Stanescu", PrenumeElev = "Diana", DataNasterii = new DateTime(2009, 6, 30), ClasaId = clase[0].IdClasa },
                new Elev { NumeElev = "Vladescu", PrenumeElev = "Gabriel", DataNasterii = new DateTime(2009, 2, 18), ClasaId = clase[0].IdClasa }
            };

            var elevi9B = new List<Elev>
            {
                new Elev { NumeElev = "Popa", PrenumeElev = "Ioana", DataNasterii = new DateTime(2009, 8, 5), ClasaId = clase[1].IdClasa },
                new Elev { NumeElev = "Radu", PrenumeElev = "Alexandru", DataNasterii = new DateTime(2009, 11, 20), ClasaId = clase[1].IdClasa },
                new Elev { NumeElev = "Tudor", PrenumeElev = "Sofia", DataNasterii = new DateTime(2009, 3, 14), ClasaId = clase[1].IdClasa },
                new Elev { NumeElev = "Munteanu", PrenumeElev = "David", DataNasterii = new DateTime(2009, 7, 28), ClasaId = clase[1].IdClasa },
                new Elev { NumeElev = "Stoica", PrenumeElev = "Ana", DataNasterii = new DateTime(2009, 5, 9), ClasaId = clase[1].IdClasa }
            };

            // Elevi pentru clasa 10A
            var elevi10A = new List<Elev>
            {
                new Elev { NumeElev = "Vasile", PrenumeElev = "Bogdan", DataNasterii = new DateTime(2008, 4, 12), ClasaId = clase[2].IdClasa },
                new Elev { NumeElev = "Nicola", PrenumeElev = "Andreea", DataNasterii = new DateTime(2008, 9, 7), ClasaId = clase[2].IdClasa },
                new Elev { NumeElev = "Stefan", PrenumeElev = "Catalin", DataNasterii = new DateTime(2008, 1, 23), ClasaId = clase[2].IdClasa },
                new Elev { NumeElev = "Mihai", PrenumeElev = "Laura", DataNasterii = new DateTime(2008, 6, 15), ClasaId = clase[2].IdClasa }
            };

            context.Elevi.AddRange(elevi9A);
            context.Elevi.AddRange(elevi9B);
            context.Elevi.AddRange(elevi10A);
            await context.SaveChangesAsync();

            var random = new Random(42); 
            var note = new List<Nota>();

            foreach (var elev in elevi9A)
            {
                for (int i = 0; i < random.Next(3, 6); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[0].IdMaterie, 
                        Valoare = random.Next(5, 11),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }

                for (int i = 0; i < random.Next(2, 5); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[1].IdMaterie, 
                        Valoare = random.Next(6, 11),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }

                for (int i = 0; i < random.Next(2, 4); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[2].IdMaterie,
                        Valoare = random.Next(5, 10),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }

                // Limba Română - 2-4 note
                for (int i = 0; i < random.Next(2, 5); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[4].IdMaterie, 
                        Valoare = random.Next(6, 11),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }

                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[5].IdMaterie, 
                        Valoare = random.Next(7, 11),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }
            }

            // Note pentru elevi din 9B
            foreach (var elev in elevi9B)
            {
                for (int i = 0; i < random.Next(2, 5); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[0].IdMaterie,
                        Valoare = random.Next(5, 10),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }

                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    note.Add(new Nota
                    {
                        IdElev = elev.IdElev,
                        IdMaterie = materii[8].IdMaterie, // Biologie
                        Valoare = random.Next(6, 11),
                        DataNotei = DateTime.Now.AddDays(-random.Next(1, 90)),
                        EsteAnulata = false
                    });
                }
            }

            note.Add(new Nota
            {
                IdElev = elevi9A[0].IdElev,
                IdMaterie = materii[0].IdMaterie,
                Valoare = 4,
                DataNotei = DateTime.Now.AddDays(-30),
                EsteAnulata = true 
            });

            context.Note.AddRange(note);
            await context.SaveChangesAsync();

            // 6. Creează Users pentru testare
            var users = new List<User>
            {
                // User Profesor
                new User
                {
                    Email = "profesor@test.ro",
                    Parola = BCrypt.Net.BCrypt.HashPassword("profesor123"),
                    Rol = "profesor",
                    IdProfesor = profesori[0].IdProfesor
                },
                // User Elev - primul elev din 9A
                new User
                {
                    Email = "elev@test.ro",
                    Parola = BCrypt.Net.BCrypt.HashPassword("elev123"),
                    Rol = "elev",
                    IdElev = elevi9A[0].IdElev
                },
                // User Elev 2 - al doilea elev din 9A
                new User
                {
                    Email = "andrei.ionescu@elev.ro",
                    Parola = BCrypt.Net.BCrypt.HashPassword("elev123"),
                    Rol = "elev",
                    IdElev = elevi9A[1].IdElev
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            Console.WriteLine("Date de test create cu succes!");
            Console.WriteLine($"Clase: {clase.Count}");
            Console.WriteLine($"Profesori: {profesori.Count}");
            Console.WriteLine($"Materii: {materii.Count}");
            Console.WriteLine($"Elevi: {elevi9A.Count + elevi9B.Count + elevi10A.Count}");
            Console.WriteLine($"Note: {note.Count}");
            Console.WriteLine($"Users: {users.Count}");
            Console.WriteLine("\nCredențiale de test:");
            Console.WriteLine("Profesor: profesor@test.ro / profesor123");
            Console.WriteLine("Elev: elev@test.ro / elev123");
        }
    }
}
