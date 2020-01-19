using Cinema.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.Data
{
    public class ModelBuilderExtensions
    {
        public ModelBuilderExtensions()
        {
        }
        public static IConfiguration _configuration { get; set; }
        public static DBContext _context { get; set; }

        public static string _contentRootPath { get; set; }

        public static ModelBuilder modelBuilder { get; set; }

        public static void Seed()
        {
            modelBuilder.Entity<Genre>().HasData(_genreData);
            modelBuilder.Entity<Movie>().HasData(_movieData);
            modelBuilder.Entity<MovieRoom>().HasData(_movieRoomData);
            modelBuilder.Entity<Room>().HasData(_roomData);
            modelBuilder.Entity<Seat>().HasData(_seatData);
            modelBuilder.Entity<Reservation>();

        }

        private readonly static List<Genre> _genreData = new List<Genre>
        {
            new Genre
            {
                GenreID = 1,
                GenreName = "Actie"
            },
            new Genre
            {
                GenreID = 2,
                GenreName="Komedie"
            },
            new Genre
            {
                GenreID= 3,
                GenreName="Animatie"
            },
            new Genre
            {
                GenreID= 4,
                GenreName="Muziek"
            },
            new Genre
            {
                GenreID= 5,
                GenreName="Sciencefiction"
            },
            new Genre
            {
                GenreID= 6,
                GenreName="Drama"
            }

        };

        private readonly static List<Movie> _movieData = new List<Movie> {
        new Movie {
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                Name = "Speedwagon",
                Description="Robert E. O. Speedwagon is a major ally featured in Phantom Blood and Battle Tendency. Formerly a Londoner thug residing in Ogre Street, Speedwagon befriends Jonathan Joestar and, despite remaining a powerless human, becomes a loyal ally to the Joestar Family during his whole life and beyond thanks to the company he's founded: the Speedwagon Foundation. His company supports the Joestars in Stardust Crusaders, Diamond is Unbreakable, and Stone Ocean.",
                Image="https://vignette.wikia.nocookie.net/jjba/images/6/6e/SpeedwagonA.png/revision/latest/scale-to-width-down/270?cb=20161028235946",
            Duration=45,
                Price=15,
                GenreID=1,
                Year=1863,

            },
            new Movie {
                 MovieID = Guid.Parse("bf1a024b-9229-4b34-a559-b4ae3a1b6004"),
                Name = "The World's End",
                Description="The World's End is een Amerikaans-Britse-Japans apocalyptische sciencefictionkomedie uit 2013, geregisseerd door Edgar Wright, geschreven door Wright en Simon Pegg, en met Pegg, Nick Frost, Martin Freeman, Paddy Considine, Eddie Marsan en Rosamund Pike in de hoofdrollen. De film volgt vijf vrienden van middelbare leeftijd, die proberen een mislukte poging uit hun jongere jaren, een epische kroegentocht in de stad waarin ze zijn opgegroeid, dit keer wel te laten slagen. Tijdens hun tocht ontdekken ze een invasie van aliens.The World's End is na Shaun of the Dead (2004) en Hot Fuzz (2007) de derde film in de Blood and Ice Cream-trilogie.",
                Image="https://m.media-amazon.com/images/M/MV5BNzA1MTk1MzY0OV5BMl5BanBnXkFtZTgwNjkzNTUwMDE@._V1_.jpg",
                Duration=109,
                Price=4,
                GenreID=2,
                Year=2013,
            },
            new Movie {
                MovieID = Guid.Parse("f882b046-1abd-4f38-a9cd-7b9dc8876138"),
                Name = "Grease ",
                Description="Grease is een Amerikaanse romantische filmmusical uit 1978 onder regie van Randal Kleiser. De productie is een verfilming van de gelijknamige musical Grease, over de jaren 1950. De film werd genomineerd voor de Oscar voor beste oorspronkelijke lied (Hopelessly Devoted to You van John Farrar) en de Golden Globes voor beste musical- of komediefilm, beste hoofdrolspeler in een musical- of komediefilm (John Travolta), beste actrice in een musical- of komediefilm (Olivia Newton-John), beste oorspronkelijke lied (Grease van Barry Gibb) en nogmaals voor beste oorspronkelijke lied (Hopelessly Devoted to You).",
                Image="https://upload.wikimedia.org/wikipedia/en/thumb/e/e2/Grease_ver2.jpg/220px-Grease_ver2.jpg",
                Duration=110,
                Price=1.5,
                GenreID=4,
                Year=1978,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Grease 2",
                Description="Grease 2 is een Amerikaanse filmmusical uit 1982 onder regie van Patricia Birch. De productie is het vervolg op Grease en speelt zich twee jaar na de gebeurtenissen daarin af.",
                Image="https://images.wehkamp.nl/i/wehkamp/16048693_pb_01/grease-2-dvd-5053083155285.jpg?w=1024",
                Duration=115,
                Price= 5,
                GenreID=4,
                Year=1982,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Frozen 2",
                Description="Frozen II , ook bekend als Frozen 2 , is een Amerikaanse 3D computer-geanimeerde muzikale fantasiefilm geproduceerd door Walt Disney Animation Studios . De 58e animatiefilm geproduceerd door de studio , het is het vervolg op de film Frozen 2013 en bevat de terugkeer van regisseurs Chris Buck en Jennifer Lee , producent Peter Del Vecho , songwriters Kristen Anderson-Lopez en Robert Lopez , en componist Christophe Beck . Lee keert ook terug als scenarioschrijver, penning het scenario van een verhaal van haar, Buck, Marc E. Smith, Anderson-Lopez en Lopez, [2] terwijl Byron Howard uitvoerend de film produceerde.",
                Image="https://upload.wikimedia.org/wikipedia/en/thumb/4/4f/Frozen_2_poster.jpg/220px-Frozen_2_poster.jpg",
                Duration=103,
                Price= 15,
                GenreID=3,
                Year=2019,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Star Wars: Episode IV: A New Hope",
                Description="Star Wars: Episode IV: A New Hope is een Amerikaanse sciencefictionfilm uit 1977. De film is chronologisch het vierde deel uit de Star Warsserie, maar het is de eerst gemaakte Star Warsfilm. De film werd geregisseerd door George Lucas naar een eigen scenario. De officiële titel van de film was oorspronkelijk Star Wars. De oorspronkelijke Nederlandstalige titel was Star Wars - De sterrenoorlog",
                Image="https://is5-ssl.mzstatic.com/image/thumb/Video113/v4/58/7f/97/587f97b3-1919-11f2-43c8-bbe89e68f3fc/pr_source.lsr/268x0w.jpg",
                Duration=121,
                Price= 4,
                GenreID=5,
                Year=1977,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Star Wars: Episode V: The Empire Strikes Back",
                Description = "Star Wars: Episode V: The Empire Strikes Back is het vijfde, maar als tweede gemaakte, deel uit de Star Warsserie. Deze Amerikaanse film werd gemaakt in 1980, en geregisseerd door Irvin Kershner. Het scenario werd geschreven door Leigh Brackett en Lawrence Kasdan.",
                Image="https://images-na.ssl-images-amazon.com/images/I/91eOgodm4nL.jpg",
                Duration=123,
                Price= 4,
                GenreID=5,
                Year=1980,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Star Wars: Episode VI: Return of the Jedi",
                Description= "Star Wars: Episode VI: Return of the Jedi is het zesde, maar als derde gemaakte deel uit de Star Warssaga. Deze Amerikaanse film werd gemaakt in 1983, en geregisseerd door Richard Marquand naar een scenario van George Lucas.",
                Image = "https://is3-ssl.mzstatic.com/image/thumb/Video123/v4/9f/f9/e8/9ff9e804-f7f5-2a01-54a8-c476a5a884fa/pr_source.lsr/268x0w.jpg",
                Duration=130,
                Price= 4,
                GenreID=5,
                Year=1983,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Shaun of the Dead",
                Description="Shaun of the Dead is een Britse-Franse horrorkomedie uit 2004. Shaun of the Dead is geregisseerd door het komische duo Edgar Wright en Simon Pegg, en is gebaseerd op klassieke zombiefilms zoals Night of the Living Dead en Dawn of the Dead.De film was een groot succes in zowel Groot-Brittannië als de Verenigde Staten en won verschillende prijzen, waaronder een Saturn Award (2005) voor beste horrorfilm en een Empire Award (2005) voor beste Britse film.Shaun of the Dead werd later onderdeel van de Blood and Ice Cream-trilogie, nadat tijdens de promotie van het 'vervolg' Hot Fuzz (2007) de terugkerende Cornetto ter sprake was gekomen.",
                Image="https://is2-ssl.mzstatic.com/image/thumb/Video113/v4/83/8e/48/838e487b-097b-3c24-bbd5-d4f3f881c3c7/pr_source.lsr/268x0w.jpg",
                Duration=99,
                Price= 4,
                GenreID=2,
                Year=2004,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Hot Fuzz",
                Description="Hot Fuzz is een Britse-Franse actie-komediefilm uit 2007 onder regie van Edgar Wright, die samen met Simon Pegg het scenario schreef. De productie is een parodie op het actiefilmgenre. Tot het einde wordt er toegewerkt naar een grote finale met grote explosies en opzettelijk overdreven. Er wordt daarbij meerdere malen gerefereerd aan grote commerciële actiefilms, zoals Bad Boys, The Matrix en Point Break. Hot Fuzz won de Empire Award voor beste filmkomedie en was genomineerd voor die voor beste Britse film, beste regisseur en beste hoofdrolspeler (Pegg).",
                Image="data:image/jpeg;base64,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",
                Duration=115,
                Price= 4,
                GenreID=2,
                Year=2007,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Viva Las Vegas",
                Description="Viva Las Vegas, is een film uit 1964 onder regie van George Sidney met in de hoofdrollen Elvis Presley en Ann-Margret. De film wordt beschouwd als een van Presleys betere films en is memorabel door de affaire van Presley en zijn tegenspeelster die tijdens het filmen opbloeide.Voor de film waren nogal wat liedjes opgenomen die verspreid door de jaren werden uitgebracht. Pas in 1993 zou een album verschijnen met de meeste liedjes uit de film.",
                Image="https://upload.wikimedia.org/wikipedia/en/thumb/c/c3/Viva_Las_Vegas_1964_Poster.jpg/220px-Viva_Las_Vegas_1964_Poster.jpg",
                Duration=85,
                Price= 4,
                GenreID=4,
                Year=1964,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Blue Hawai",
                Description="Na twee jaar legerdienst keert Chad Gates terug naar Hawaï. Hij wordt door zijn vriendin Maile Duval afgehaald op het vliegveld. Zijn rijke ouders willen hem een baan geven in de ananasplantage van de familie, maar Chad gaat zelf liever aan de slag als toeristische gids voor het reisagentschap van zijn vriendin.",
                Image="https://upload.wikimedia.org/wikipedia/en/thumb/7/75/Blue_hawaii_poster.jpg/220px-Blue_hawaii_poster.jpg",
                Duration=102,
                Price= 4,
                GenreID=4,
                Year=1961,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Jailhouse Rock",
                Description="Jailhouse Rock is een film uit 1957 onder regie van Richard Thorpe met in de hoofdrollen Elvis Presley en Judy Tyler.De film is gebaseerd op een verhaal van Nedrick Young. Het was de derde film van Presley (zijn eerste voor MGM) en een groot succes in de bioscopen. Alleen al in de VS werd 4 miljoen dollar omgezet. De tegenspeelster van Presley, Judy Tyler kwam kort nadat de opnames waren afgerond, vóór de première van de film om het leven bij een auto-ongeluk.,In 2004 werd de film geselecteerd voor conservatie in het National Film Registry van de Library of Congress.",
                Image="https://m.media-amazon.com/images/M/MV5BYjE5MGE5ODItYWY3OS00MTE5LTlkNWUtMzBkZWRmOTNmMDJhXkEyXkFqcGdeQXVyMDI2NDg0NQ@@._V1_.jpg",
                Duration=96,
                Price= 2,
                GenreID=4,
                Year=1957,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "The Outsiders",
                Description="Een groep jongeren is geboren aan wat lokaal beschouwd wordt als de verkeerde kant van de stad. Hierdoor maken de jongerenbendes de Greasers en de Socs elkaar voortdurend het leven zuur. Wanneer Ponyboy Curtis (C. Thomas Howell) en zijn vriend Johnny Cade (Ralph Macchio) verliefd worden op meisjes die behoren tot de wat snobistische Socs, wordt de druk tussen beide groepen groter. Als Johnny vervolgens een lid van de Socs vermoordt, ontstaat er een bende-oorlog.",
                Image="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxISEhUTExIVFRUWFxUVFhcVFRUVFRUWFxUXFhUWFhUYHSggGB0lHRcXITEhJSkrLi4uFx8zODMsNygtLisBCgoKDg0OGhAQGy8lHSUvLi0tKy0tLS0tLS8tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf/AABEIARQAtwMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAAFAAIDBAYBBwj/xABPEAACAQIEAwQFBAwLCAMBAAABAhEAAwQSITEFQVEGEyJhIzJxgZEUUqGxFTNCdJKTs8HR09TwByQ0U2JjgoOUorJDVHJzwuHj8WTD5CX/xAAbAQACAwEBAQAAAAAAAAAAAAABAgADBAUGB//EADIRAAICAQIDBQYHAQEBAAAAAAABAhEDBCESMUEFEyJRkSNhcaGx0RUyM1KBwfDhFAb/2gAMAwEAAhEDEQA/APNM1crk1yaw0ezTHClTZrs0Q2OBpTTZpTQCOmlNWLWAushuC25QAkvlOXTfxbadKqzUImh810GmTSmhQyZIDSmmr0FSX7LI2VwQRBIPmMw+gzQoloYaVIIddDpvodNY195AqS7hnUSykAllnzWAw92YfGoHiRHSqQ4ZwofKcrEgHqVAZoG8AEa7a1xbDkEhTpvp5gfnHxqE4kMrlPu2mXRlK+0EfXUc0SWhwrtMmlNSiWOpU2aU1KJY6lTZpVKA2MFKnRSimK+EbSp0UoqB4RtdinRSihYeEKcH46+GAyKphmbWfulCkaHaBRtOJ4o91iGskpbL3PCREOndrCg5gqkkxyzGshFaDs9xXELCWe6GRHIL6eGc7HUwY1PsFAz5sKriSXvsnPaXGAkC0RoRlKu3gAUQeuw1/pHrTMNxnErh2sGyx8LIrZXmLkl8/JjB0+OtSYy9jrVwv3bA2Va0xIz28ucsdWGq7RM6KNaemN4jpFtzz0VjqTb10OmttT09bkTQK+FUqUfUgucVxjIiG0oA8S+Er9p9IfuoBAtzl3g6DUVJe4vjbwA7kDuyjeqwPgcQCGbxDMVkax5a1HauY3NnCQ6FSBk8XpLRtAgHcZbUa8x1q3ZxXEcoIUQQbYUoJhMrBYI/pQOehnagFxS6R9SNeO44ZU7gaBbaylyCCcqAktDTtJ3kzNRcV4vjHV7b2QAz+IolyC3qgBpIIOXbY76nWpr2OxzKLbWTmkOhgLHcsuy/dQY9uYaHQ1E74+6qHJnyXSAoQSroimWWNiHHt+EyyRgruo+pIeI45st7uVMNdtgZSTmYqXBQmTBUbjT3CG43j2MssRctqphrOocSFP3JDQYzaONdd9qtYe/j7TXAbXeA3HLQoANxn1IMAwdemhkRoQA4xxG/dYrfjMjMD4FUhtmEgeW3lUGhjUpVUa9zGcV4s18LnAlc5kTu7lmjoNdtdZ5aUNp8VyKZM1rGoqkNrtOilFSw8I2uU+KQFSycI2K5UkUqlkcRoFdiuBqWapuLaOgUoo92c4XYu2r928bkWskC2VXRhcYk5lb5nLrU3ccN64n8O3+qoxhOXJGXLrsGKXDN0zORSitJ3HDf/lfjLf6mu/J+HdMT+Nt/qabucnkV/iul/cZqKlw99kJKmCVZToD4WUqw1HME0f8Ak/DumJ/G2/1FL5Nw7pifxtv9RU7nJ5E/FNK+pQxHH8RcDC4+bMGX1VWMzKzRlA3y7ba0xeN4iAvekgBVAIVoChlWJHRmE+dEvk/DumJ/G2/1FLuOH9MT+Ot/qKHc5PIVdoaPlt8gZb4xfDMwuQzZSxhfEVnKTpvqTPXXepV7Q4of7Y7ltlPiO51G/n5nqav/ACfh/TE/jrf7PS+TcP8Am4n8en7PQ7jJ5B/EdF7vkUL3Hr7FGzBWQMAyqJOYgnN11E+0moU4tfAYd4YYsWBAIbMArAyNQQq6beEdKJ9xw/5uI/Hp+z10WeH/ADMR+PX9no9xl8mRdo6JdV8ina7RYlWzd4W8WYhgCCRzMfvt0odirxuMzkAFiScoCgewDaj3dcP+Zf8A8Qv7PXMnD/5u/wD4gfs9TuMvkyLtPRRdpr5GeiuZa0WTh/8AN3/x6/s9cy8P/m7/AOPH7PQ7jL5D/i+l/d9PuZ4LTstH/wD+f/N3v8QP2elm4d/NXv8AEf8Agqdxl8gfi+l/d9PuZ8rXIo12o4fbs3LYtBgr2luQzZyCWYGGgSPCOVBJqqmdCGRSipLkx0UqaHpUaYW0QA0prgpVaZrNX2V/kmN/uvyeIoTkov2R/kuN/uvyeIodkrdpPys8n21b1H8EYSpEt1IqVNbSthyFYN4jihaA0ljsPLqaGfZK5O6j3ae+ljgXuXDBOUkSJIVV0k9K3vZZbFvCK11FUEEkkAlhOhaPqrNly8PI6Gn0/HsZHC40OwQrBPMGRtPtq93NS423hhdQ2SvizABZ1BBM6+yKkqzFPijZTqMTxz4SpehFLNsPpPQULbi2vq6e3WrXHFuM1tVViDMQDDPMQDsSPz0SHYS+Lee4crEqqqvizEkCSRsNfoOwGq5MiTDh07kuRQweJW4DAII3H/fap8lVMJhDYxL2SZgETETsZosUq2DtWZs0HCTRTKU1kq1dAUEkwANaF3uKqDopI6zFM2kVxhKS2JylcKVBa4qpMEZfOZ+NXsnwqWnyI4yjzKpWllqw1um5KjF3Cvbf7ZZ+97f+u5WauGtJ26PpbP3vb/13Ky01w63Po2nfsY/AdNKuUqNFtjK5Xa4aJQa3sd/JsZ/c/k8RVcJVjsb/ACbGf3X5PE1Aprbpn4Wea7VV5/4HZKkRaSmqz8VsqYzSf6IJ+mtLkcxQs5wd+4xpUoWS+CsTEk+KJkA6jY9RWjwoUWu6CZvuEU7jWBtvE/XQe3cRirLDFSrDquu/lpNFOJrctBr9opr4yrzqeeXzJ5DrWDOlxnZ0TaxttcgZ2jvKLlq0AM1tTLDQBIiMsaax9NDHv1BxfiDPddriBH0BAkif+KhxusdR1j31fifDFIyahd5kbN92f4hZdLVpge9tuzrpIyuCjmeR8YHuEUUxmGt4e3evW711ZUgZrjlQZE5Q5ILbx9FZHsNgi+Ick7W4JnYswK/6D8K03aPieHFhkLqrEjvbRjvCQQDlBHMDcRO871RP8+xqxbQVmZfBZcS7d6boCgZzBJLakTzgR8amYUL4dxMuzhgBmYsoEALP3A8gIj2Vfu3NK2Y3UUcvPHim2cuYU3gbYZVLDQsYBIIMT50D4pwa5YYLcQrqQNiDHQitj2fvW8l0Oi+IBczGGKndUB3Ok6eXlUnHQlyz3SwSCvdu2yxG31VnyZmp0+RtwaRSxWuZ5y9uNPjR3gpm3ryJA9mh/PVbtDwn5MUU3M1xgSygDwCYWSCdSNeVS4XFWraKubWJO8TudavhJPcyZ8bSovla4yRSs3Qwkajyp5aatsycNMl7e/bbP3vb/wBT1mK03b37dZ+97f8AqeszXIPd6d+yj8DtdrlKgXjTXKca4aJUazsd/JsZ/dfk8TUCVY7HfyXGf3f5LEVWSten/Kzznaf65X4ncbwWk0a62WeYGgP1/XVzHdhb1m332e2yKAGAzg689td6t8MwVt71gt62e4B0HowVJHtk/wBmtbfbE2ka3cNm4WKqgKlB4p9cgmRA2j9NDLkalQuDCnC2eQ4PF3LBzLsdGH3LrMj6QYPL41prfaywQudbnh8WWFYZhtrI093Kgnam2y3cpRbfhByKZAn9zv1oMqk0XCM1bCsssTcY8glisX3zEgQDrqdSfnHz1ptpCXAAnUR51BaMCpYNNVbCXe7DHAeLnB3yXU5WlHA9YQdxPMGdPM0T43xfAX0LHO1wSdEKmBt4joJ0+NZnE+HI4glSp6iRrBHSqzqAABpm8R02E+FRrrpr7x0peBN2FzcdkTO4MwuUchM6ctedOw+MZdPWXofzHlUL3Mx0EbQBrsK4jgVYVvc0uH40lpQy21csoUAFgQ8kkGQZO1Mv2r90qpTulOuViQzDyB1OoqPsRa7zG2EEa3FYyJ0Q5z9CkV7jdtgeIgac9BA99VuC59Sx55Lbp5HzrxSxkuMpBVgfECIgnWIPlVodnsUbYu9xcKZQwKidCYGgk1ueJcPsYrF4sFSPHhyrMuXOqlVud2dNDET0+NG0xt6ytwXLKizbUsHRzOVQSB3ZHQcm91K8tbIKxcStnj+AxDW3y6wTBBEEHbbrRhr1Q9qMb3mKztbe3oNHifI6EiKp3MRV8JbGPLDxB7t4fTWfve3/AKnrNVpO3J9LZ+97X+p6zdc89fp/0onaVKlULx2WuFakWmmlsoNV2QH8Wxf93+SxFUhV3sif4tjP7v8AJYmr3Z3hguA3HErMKOR6sesVr078J5/tFe2fwBeFuMty24UnI0kgEwIK8vaK0PHOMhQl03EZV8QyuRMAhZWYY+I1LxLAN3ZyTMg5RGwIMAcyP00KPArd1+8GHgjUu+i+0qD4jRy43KSYmnzxhjd8zB8RxD3bjXHmXM+XTT2bVHl0099esPwIXLQRlDKBMHSNPCZGx1O3XyrJ8L7Jav3gueEsVKsoVgsyDK7wN59w5s5xjsJCM8niox8VPafT31oOL9nWt22e6AjSckE+MmIUAjUDrp76z1/DtbYo2jKYI6EbiipKXILg48z0fgPZrD4nDWmcDNBkI7Q2p9bY5p3g1mO2mBa1iYKoq92mTIqr4VGXUDnIInyFR8C7T3sOAiwySdDMiddDyHu51Q4zxV8Tea6+hMACZCgCAoP770kYyTGnJNFJLhB0/eNaPDsnfbOYMgKVgMwcNEkMBA0KnWNDPI1b7M9nUurYvmWXvSt1Qdh9wTGo1ifaK9Bui+HPp0S0FMLlUQYhQSdgDQnlp0gwxXuzyXh9x8LiUaYe26mYI566GDtINezdouIqbSqxAuSGCgznUgjlsNfutNK8g7U2XzW7jXlum4pYFQBoDEEDTefhWy4F2XuPbF17rG44DEmSeUCd6Zzhwrj6lcsc+K4dPMgfihN0uy5TraK9CMjqCeckNr5+VWe0WOdsI5DGHUrMZso2ZW10jyigmN4ebbNbuNJY6MTHiGqk++ZPQnprmLeDuXTcBYoVUuUJJBOYArE8p+imlhjKpRFxamUFKEyrxTHNeuF2M7AewaDTlUHeaVaxHDWQopIl+hmNv0ip+McNSykhjJGoI+kGjyaQEnNOQe7b/bLP3va+t6ztaPtv9ts/e9r62rO1hPVaf9OIhSpCu0C4kUaUiK7b2pUvUqrY1HZJScNjANSe7A9vdYmtfw+yLVm0vkR79CfprN/wfLKYgedr8niaK47iU3FRFJS2DLaetpCircUvEkcjW4uJza57Bph06U3FRkJ30qpheII1vvAZUAz1EciOVQYDF9+rMQVRXgZgRmgBhA568udbpyUVZxsWN5JcKD3q2QTvH0xrWIxHEmVzbVvtjH1SoKkrBUM4K6g+6N5o72l4mLdhgwHq7Hr0MV51xdzeS2QmRmjwhpXVmXNJ9XUDTWN5rLgxxyJyfM358k8LjFVQS7U8R7whGIPdKA0GYJ9W2DJzMY1Pt8pzGKwToMxHhmPfE/CruDwjM9tSBkUySIg6zJOzHYT0ArQvcViVBhdSwIlSOek61sxYaic/Uav2iMWhp18RB6j9/wA1XeJYEW/EuxO3l1qQcOfuhca2+XkxRgg98UrVOi2M1KNoM/we4G4924w0QKVzawLhIKkdTAafJvOtomJtjvA9xUuxDI7lVYRExIkb6jqa864Rx29h2m2/tU6o3tH5xrUnabtJ8pTKbeUyDuGA3mNJ6VRkxNyvoaceaKjTBHEL4a5AIyqSqnXxDOTmk9Z+EV7ZwS96JI1BVSCNtq8FUya9a7CYoPhFSYKZl8xBlfoIqZo7JkxSttAntjiFbEW0ESC87/crmgga7gVnrOKRsQCCBnVtWMScpHiJ6wvvB60d7UYLLbuu6t3sZlc6RH3Om2hNAeL4hRiEYBbisi3hM5Vm0cy+4ienKrME0kkU6vC5Ny9wJ45is17w7WwFGs6jVv8AMSPcKk4nxw3bITKAY8RjptB91R4ng2IS331y0yqwDgnmGMgxMjfnQu5sfYad1J2JFOEeE3Pbj7da+97X/VWditH24+3Wvve1/wBVZ2uces036UTgFKuilQL6JkXSuU9NqUVXe5XWxq+w5izip/qvL/Z4mrOIGQZ0goJLLOondgentmqPZhJwuNB2i3+SxNR8E4WJAe9kJ1VWtgllPq5WOh0G9aMeFzVo42r1ccOVxl7voG+Hi2xzoxBO8GD76LXnFtDcOad5Y84j36VmeJcAvWiXw14f8DjaehGkeURWf4/x/EFVsujIy+tJkMeRUdOdNLTT89iiGtwy3rf6kXaDizYm6Ek5cwB8/Kr4thu7ULuLjwASAFt5D4GJ+6uKeU5dqC8H4a7MHIIAM6j6aO8O8WIfkFtAAbiXcABuoyqOh0rdjhwxpHJ1GfvJtt9CwLapakamYgAKQs6nf2ac5qtiGTQKPEQT605RykRoSQeewoxctqEDMABDaCYJ3Aj371mLl0AM49v0mI6bCtF0c+NS6bl8w4hgJj3e6t52R4yLqGzcAFxRA0EOsQdNpHMe+vL7fETIAKrMTm2B308qJ2MQ4KsCMwMhkmfIxBpMmOOSO3Msx5Z4Jb8mD+1GCWxi7iIIQ+JR80MJj3EH3AUJBBj2fmoh2ixRuX2dtyo+IkGP351Qw4kjoo1qmCdJPmdGUk948hmDtAlgeSn6HQfprYdkOIGyyq58LeECQYO/6ax9rRydAJYSdpIMT01oxgszL6sidDIMR4vdoPppnBTi4sr72WOakuRve2PD+/tqAYDEa+W5+isjxLDYW0FV/GEUqDMFgdwSNxVm/wBr8tg23U519WZ10jT41hcViWcknnWTHimnT2Olkzw4fDvf0DXFu0bXVNsSEy5QOgG29Z65sfYacBTbg0PsNXqKXIxym57s3vbkent/e9n6jWcitL25Hp7f3vZ+o1nYrmt7nr9MvZR+A0UqeBSpbL6JkXSutTlXSkRVV7iLkafstb/iuL21Cb/8rEb1Dhla3AGZiYJIf0aDWesGRyAqXsxcy4bGN0Fs/C1iajsY5ntwEgtufZ4fedK7PZyvHXvPE/8A0EnDU8S8i0vGnPhZLbROocofrqpi+J2dr1jQdPEo9xifaKZc4flUs0zynX9FAL4YkgbDUxyA3IrbkUYnI07yTe7Lt7EWll1YFApynRipOgAUgEcxE9ek1L2ZBK3bpEd6y5BEyEzLGnKSB/ZNBMNhe+cKAcvrsOYRWyqPM7/hUW+yGVUUHRFAKryIE68jLE1nU03ubZ4motLm/oX+L4lktM7FGCsFyiZzFYEeWn0UJtcOuG1DeEmC2aZ8W0D/ANb+yiGEv5CpuBXVozrAKnxBxvuQQD7qZxTj1kvdZJIYsuUwBBhgw56GR/Zoufi35Cwxvg8POyFcCoAOXMV2nlHQVBiL4QZiSpOoGxJ/fnQy9xRzopIH01P2f4Q2LvZM8c2Y6mPzmhLPGK2Gx6OcpeJg/E4kucx3qW0QRAnqT1rSdruyXydVe0CUAIcncHk3srN2RlnVfifzCqYZVLc2zxcGwS4Vw04i3iVUSyqrqPNeVRWrkjNGyop1kwBvPU61qP4LLBLX3O0KvvMk/RQ/tJw4WMS6icrgsoHnuB7D9YqY8i7xpgzY28SaA+LtBhBk6LAEZR4R02OgnzmaG37WkBAI57n6/wA1HcPanRp/R05fvFcxvC41kA6zMfHb89aWtjDHI4ypgDE2DkQxr4lPxzD6z8KpvsfYaIG8crW9ySpkf0Zk/T9NWcbYF4SuUPl1ygw3tI0nz/cVc+RpT4eZqO3A9Pb/AORZ+o1nYrS9uV9On/Is/UazoWuNJ7nuNKvZR+AwClTwK5QsvothK5kp2auZqo3ES2DnZ3HWbdu/bulgLmSCq59At1WnxLGlzeat4LEYC0IVrnvsk85/nqzGalnq7HqMkFUWZM/Z+DPLiyK/T7GwxXE8HcEM9z3WCP8A76GvhuHEEZ7uv9Ufr76gYenZ6L1eV82Vx7J0seS+n2DmGs8Pt+q97Yr9qOxBBn03macU4drrc1/qm9v+8UCzU5FLTAJgSY5Abk+WtL/6cvmM+zNP1X0+wWaxw8z472u/o2j4d/pUbYHhhEekA8rJn49/Joe2GuBsuRs05YgzJEgR1jWm9y3zTzO3ITJ+g/Cj/wCnKT8N03+r7F37GcM+de/Fn9fV3hZwGHYtba6CRBmySCPffoN3DyRlMqQCI1BJgA+dd+TXN8h6bdFzb/8ADr7KDz5Hsxl2fp1y/r7GxudpsOVylmI88P8A/ooQH4fmLE3DMghrTMsEyRlOIgCgXdP80zOXbXNsVjrqNKemEunZGOuXQc8uePwdfZQjmnHkGXZ+nf5v6+xo+C8RweFLG01wZ4kGxK6bEDv9KXFuI4LEMr3DclZjLZK77z6fWsy9pwJKkCJkjl4df8y/hDrUdwFSQQQRuDoRR77JdhXZ2nar7fYPA8PHO7+KP6+um7w/+t/FH9fWdz00tTrU5fMR9kaX9v0+wWOC4XMxe1/qzH5erNr7HLt334v/AM9Z/PSz1O/y+Yz7K0z5oL9p8emIvB7ebKLaJ4gFJKzJgEwNetCO7pZ6WeqW29zdCEYRUV0ELdcroelU3CxheuZ6jY71yat4Sm9iXPXc9QzXQ1DhJZYDV3NUIang0riGx5anW8QVn+kMpBnUSDGh8hUeaoyaCQbsvXOLXGmYMmTvqQMoO+8aU1uIuehJDb5p8RJYTPMkn3mqM0+wwDKWEgEEjaQDqJ8xT0KoR8gocekkgvq0zrJCr6M6HfNy6U1eJCANYlTGsaqUuRr82APKte3ycWsBd+R2Jxb3EcZBC/xhQuX2LK+c07ifYVL965cs3BbQX7yOndqq21tpn9HDQRsNY3nlVndPoYlqcSfjTS39/J10MWceomC05ncEkyWkZW30JjXXlUP2Qby5820lQh5/NAHsFS8U4aLd1bdq4L+ZEYd3DkFhrbOQkFlOhgkbHnQ06UjibYKDVovfZF9tNsu7HTw6b/0V/BHSoL+JLtmbcx15CBv5CoCa5NDhLEkuRJnppemTXCaKiGx+au5qimlNHhDZLmpBqimug1OEFkgalTVNKhQLGsdaQNNrlWUUdB5ropk04UKJY9aeKYKfSMJpf4P+DJisYqXBNtFa6y/OCwAp8szLPlIrXcFxC4vHYzBX0VrEOLaQALXdMEHdwPAYM6cxWI7G8c+R4pbzAlIZHA3yNGo8wQD7q1uE4ng8Nj8RjxiEe1ctk27aT3rPcKlkKESuqnUwPEKuxtUvjuc7VRm5y2f5fD8b+v8AQGx/YcWrJvtiDlF82GHdDMIvG0WHjgiRPLQ8qN8E7A2LWP7i/dN4raF8J3eVGBdkhyWOgIBjnPlBgs9osPieH3Ld6+tm98oN9gwdpBv97CQPFoSAPLWBrRo9rMD9lBeGIUocMbJaGyq4u94PHEEEMRI0BXzqxRhs/gUTy6pqUd78XJfCt6+Jn8JgrtzDW7ovM+Gwl0CzkwitdDaF7jW805AxkySSRMURsNizaxOItY2zdWzdLqLVkemusqg550AZXiAT7iKq8A4rbwYAw2PtOvfN36XgyIyHL6SzpmkCRpMwDFELfavA5MYyXUQ3bouW0ZbgzFFtgs0IQudkJ9+us1FVc/mDJ3luo2r28Pv3tV13dl/slw1sNjTbuXV718N3jWLVoJYtjvABlYHUglxtrmJmsVY7FNcUXrjugvXzatLbtd6xl2HeP4hlQQTOulbNe0XD/ly435WgDYVrRQhsyxcFyTpodxG5O00KxfHcNisH3FrHHDXLLyrvntC6ni+bJiG23lRprRajVfEGKeeMuJJq6t8PLn7gNhf4PGnE97ibdsYcwxAZoGUOHYaQuQ5oEnlpT+I/weC0Llzv2uWUKCbNoXbssAWJQOAAoIMyd9qs8A4phUwuOtNjAWvgpba6LmZmFkoXbwkqpY6TJCxOtVuxvGPktlTZxdlW75u/s3iQjW/CFu2jE5oBGm+mmkUiUP8AM0vJqfE+LlSrhq9vht1XUxnFsKlq66W7q3kEZbiiA4IBmJMbx7qpTR3trxKziMZdu2Fi2xEaZcxCgF8vKSJ+vWaAmkrc6WOTcE5c6VnZpTXKVGh0x1Km10UKDY4Uq4tKgCxVwGkaaKYovYdThTa6KDJZIKfNNFI1WGzpNNJpTTWNFINlrDW7RC53KkvDAcrcbjTff4jzqexZw5BzXWUy2WASMoy5SfDz8Q+FC5pyNBB0011Ej3jnRcQU/MKthcNp/GWgzqbTfCPjzNRpYw863yFIB1QzJDTy2BC/heVOPH7+niXwxHgWNBH7/pApo45ekmVkwPVEADNAC7DV2PvpaYntP8/+D/keHgkYkmBytPEzG/Llv1qK3h7BUE4gg8x3TGNNpnXpT7nHbxUqWWCCpAQDQ6EfChdMkx4qXV/T7FvG27Sx3dw3N8xKlQBplgH31UmuGuUyRYtjs1yaVcpiNnZrs02lUImOmkDXKQoUGxwNKuClUJYjTRSJpCiZrO1LbUnYEwJMAmB1PQUsIGLQi5zr4cueRGvh56URW5fVT6GFbf0UAlQW6chm9xafIMVyoq/JngHu3128DaztBjXcfEVxrLD7lvgfL9I+Ioxhcdi1QW8j92R3Z9GTmXmPMwD9O1XLPGnfT0zsuYSltJGYwJAkjRFO8yogjLS0hHlmui9TNfJ3OyP+C3mOnUH4GuNhbkfa3/Bb9HmK032TYoRbGIM5mHo0Kh85vW2BA2zOGMdRuNKHNxjGtIMtOpBtjYqCNAI9VJHkDymmSRFkm+i9QKLTa+E6GDodDroemx+Bopw3A2yCb630A1zqpyBZUfMJn1j00FRfZLE2XckZDdHjDIBmBmdCNjJ+J6mrdriLYiRcNxyVKsLVtZy5kYGRzzKvL66IZSl/HmmSXOF4YFPHfggZ5tsCHlMyjwcgbh5+qOs0xcBhdSXvBZlWCtBTxkEg2/K2Dr90Y2oieKucsriCpd3A7lfWaXOU+zvCfaSNooViMdjHUrlfLMEd1pPhbURE6qZ6N0bWbFcXN838/wDhbXhuBkA3b8GCDkYyptkhh6P5wI9gmaGcSwKqVFpbzCNWZSASQPVGUcyfbIq4nEMcoC922irbANnXKnqrtOmVv83nVvC8XvaK5vlvtgRbY0IZACgM+EC30ABHmamwyeSLu7/n/hmhhnOyMdJ0U7QDO3Qj4075Dd/mrn4DforVX+MtoMuIgFGWbVuQVKrmHh31A9pA56DF4jjggQIwQgoq9z4SDqVXw6+r/lqFiy5H0XqBbeGdhIRiJy6KTrEx7Y5U9sBeH+yufgN+iiFvHYxJWH9KzyGSe8a4ArCCPESAB7z1NXbHE8SU9ILpRsxhbKFStw5mOYjmXB946iiSWSa5V6gAYK7Md289MjTuR06g/Curg7p2tufYreXl5j41qE4o2ZmC4rvDJJFu3uqhczALyDjXkGUVSxGNxpYgW7hUTlDWQCF8ajQDT7Y4/tGgCOWb6L1M/cQqYYEEbgiCPaDTan4hfuXLjvd9cmWkQZ9nKoBUNEW63OilXBSqBs5FKpStPw91knKYnfbX40LM/Qm4VcupcD2swIMSoDEZgVIAOhJE6Uft4y4VCsMQwJZgBbs5T3mbxRyLKXkdZPlQOxi3nV2A3kKpIImCAYiMx1BB1q7b4kQwJv3Dqs+jUSAZjfzMdCSeZoOT6FM42+SCN3idxVOX5QGkkZhaC95m7zMQB7WJ576Amh+GbGLrbVgQZlbabid9PM/Hzqy3ErBWPSSJgESoJDgjf1TmAI2iab9kLOUAm8JL5gI1UhkQjX1spUE8wD5QrkwRjS/L8iWxxFrZGc4nLlcLCWlORQCRGohZb2DpqKqX+NoLZFprqvCrqLIWFCgaqJkQYO4086ku41cynNdUD1xkBJLmbmUn1Q0THPnNdTFWQE+2wq+KEEGCMkEkkAAEf2vKjxMnCufCB8RxW65zM4JECSqctuVW8PdxSjMikBoUEW01zQVUac/CY9lE8DdRhBS6SGQHRR695oEMRErlE9SasYNlYGBdhTBIZAAUtgMQWbQjMNekdKjkFzS24UULPFcYZLPcLQHSEtkklYBkiQCrxI+dVg4s5SuTEBdcyxb+15VUgbR6MICNhNStZGaYu+tlAJtyPECyRn8JM2yIG551Wx+KCkg9+G2zGGADQSCJI130MEBdCNROJi0m9kiPHcSvm4O5F0EQxzKhOcu14HQR62Zh7+VDMXisQrq9yVbLlWUVQVyldojZiPfRHAi8893duZoUGLaKTOi6lhPqwDVu1aJyFu+JCtmgIBq5VWAzerFrKfMDXaZxliqHRf2DsP2huR6S458YeFW2BOcXJkjQ5xm86nftEMgVM65JNsRbyq/iykCNIn/M1W8iAK8XF1BLA28uSFmGDaakbnmKf8kYgZbbxmAaO5EgFVuCM0zIYD2xU4xX3b6Ai5exrlCcxKEFfCsggrGka65ND5U58Tj4C+MAZQoCKu2QqBlE/c29B0FXb10oCVW5IQXFPgK92SqqdGOUEqnsyTz0gbi1nKVm5EmBp6uoGpmGgJ12NHiY3PlFehawnFnUAHvyCwQQtlQUYAZIAgEleW4AHIRQxWKxjsT4zlMaomYDOt0BoG8qre4+dOt8VshRJuswdnMganOzKZJJDarrrud6l+zFrXx3RLKSYBLKCnrCYJjvB+D0ocUgKFO1H5AHE2bg8bqRmMyRAJMnTl1qEVbu4+5sLhKgnLoBG+oHLeq1xyxk7/v0p0aVfU4DSpAUqgxYI+qinZ7CJcLZ0ZwBoE9YmGgCdNSAKGWyDz8v/VSWrjLOViJ3gkT8KrbKZRbjSNNd4TZEeiukagwoBBAmDmgdNvOornDbXK1e0ZQSyrsSMw02OUyPbQMX3+e34R/TXe/f57fhH9NLZUsM/M0C8Lsc7d+dtkidSRvI8ImKanCrWWWt3QY6LlzRrqeU0CF9/nt+Ef013v3+e34R/TQsPcz8zQrhdVAGIjxBvENIGka6c/dVPFslq+veC7kyElWZg0l2BPhYb5Y+BihXfv8APb8I1Hdcnck8tTOnTX2moGOF3uwueIYYDQXMwG4uXBJ6xrGsmJrj8Qw4Byi6GgxF24NSAJPXYfAUFIpRRHWFeb9Qrw/VBKXWMkStyByAWC2mgUf2R0qRyqrma3eAiSe9ieQP2zy21oKyez6KblHSoM8Nu7/3qHbYDAEJfMiQe+3mcpjvOhH07TpFYsmMztiQQSxK3ECgToZZpnagpQdKblHSiRYa6/71C13HWTl8eJECJBWTIgk+Kql68Gbw3rgWN7pJM5hPqT0U/wBnyFVCKUUUixY0ggt1YOa+X0IAzXRuCDuNiCR7zUi4C2wlUJ2270gzEahCOpGvOhRFck9T8aNAePyYasYK0plrLMBMiby6zoZyT1Ec9fKbYwuH/wB0Ya/PvmdNhCaHf/tWak9T8a6GPU/Gg4vzF7pvqaTD2MJ3q57ItrleO8e+LbOIyq7MoKj1tRziaE9oUsi8RYy5MqzkLFA8eMIW1InmaosDTKKVO7Gjip3YkpU4UqLGaIRualLnrSpUzKELOetJXPWlSpRbYhdPWud8etKlRoZM690jnTDdbrSpVEkBtje9PWnLebrSpU1IkWx5c9aTOaVKloMpOxpc03vDXaVGgqTOFzSzmu0qNDWxuc1wuaVKpRG2IOa7nNdpUSJsWc0lau0qAbY4GlSpUrFtn//Z",
                Duration=91,
                Price= 6,
                GenreID=6,
                Year=1983,
            },
            new Movie {
                MovieID = Guid.NewGuid(),
                Name = "Aladdin",
                Description="Aladdin is een Disney-animatiefilm uit 1992 geregisseerd door Ron Clements en John Musker. Het is de 31e lange animatiefilm van Disney. Voor de oorspronkelijke Amerikaanse productie werden de stemmen van Scott Weinger, Robin Williams en Linda Larkin gebruikt.Het script van de film is gebaseerd op het klassieke sprookje Aladin en de wonderlamp uit Duizend-en-een-nacht.",
                Image="https://musicimage.xboxlive.com/catalog/video.movie.8D6KGWZL5X9N/image?locale=nl-be&mode=crop&purposes=BoxArt&q=90&h=225&w=150&format=jpg",
                Duration=90,
                Price= 8,
                GenreID=3,
                Year=1992,
            }
            //new Movie {
            //    MovieID = Guid.NewGuid(),
            //    Name = "Hercules",
            //    Description="Hercules is een Amerikaanse animatiefilm uit 1997 van Walt Disney Pictures, en tevens het 38ste Disney classic van de Disney-animatiefilms. Het verhaal van de film is losjes gebaseerd op de Griekse mythe van Herakles (Latijn: Hercules). Het scenario van de film kwam van Ron Clements en John Musker.",
            //    Image="https://s.s-bol.com/imgbase0/imagebase3/large/FC/2/8/9/6/1002004000086982.jpg",
            //    Duration=89,
            //    Price= 8,
            //    GenreID=3,
            //    Year=1997,
            //}


        };

        private readonly static List<Room> _roomData = new List<Room>
        {
            new Room
            {
                RoomID= 1,

            },

            new Room
            {
                RoomID=2,
            }
        };

        private readonly static List<MovieRoom> _movieRoomData = new List<MovieRoom>
        {
            new MovieRoom
            {
                MovieRoomID =Guid.NewGuid(),
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                RoomID = 1,
                Date = DateTime.Now.AddHours(4).AddMinutes(25),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.Parse("5344e334-e87f-4fe3-93e5-205306162c20"),
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                RoomID = 1,
                Date = DateTime.Now.AddDays(1),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.NewGuid(),
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                RoomID = 1,
                Date = DateTime.Now.AddHours(7).AddMinutes(25),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.NewGuid(),
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                RoomID = 2,
                Date = DateTime.Now.AddHours(4),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.NewGuid(),
                MovieID = Guid.Parse("32d41497-c4a4-4b4f-8ab8-6f5ec77681e6"),
                RoomID = 2,
                Date = DateTime.Now.AddMinutes(30),
            },
            new MovieRoom
            {
                MovieRoomID =Guid.Parse("3ab9b692-3c04-4da0-b1d1-fad0f48dcb53"),
                MovieID = Guid.Parse("bf1a024b-9229-4b34-a559-b4ae3a1b6004"),
                RoomID = 1,
                Date = DateTime.Now.AddDays(1).AddHours(2),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.NewGuid(),
                MovieID = Guid.Parse("bf1a024b-9229-4b34-a559-b4ae3a1b6004"),
                RoomID = 1,
                Date = DateTime.Now.AddDays(1).AddHours(6),
            },
            new MovieRoom
            {
                MovieRoomID = Guid.NewGuid(),
                MovieID = Guid.Parse("bf1a024b-9229-4b34-a559-b4ae3a1b6004"),
                RoomID = 1,
                Date = DateTime.Now.AddDays(2).AddHours(6),
            },
            new MovieRoom
            {
                MovieRoomID =Guid.NewGuid(),
                MovieID = Guid.Parse("f882b046-1abd-4f38-a9cd-7b9dc8876138"),
                RoomID = 2,
                Date = DateTime.Now.AddDays(1).AddHours(4),
            },
             new MovieRoom
            {
                MovieRoomID =Guid.NewGuid(),
                MovieID = Guid.Parse("f882b046-1abd-4f38-a9cd-7b9dc8876138"),
                RoomID = 2,
                Date = DateTime.Now.AddHours(6),
            },
             new MovieRoom
            {
                MovieRoomID =Guid.NewGuid(),
                MovieID = Guid.Parse("f882b046-1abd-4f38-a9cd-7b9dc8876138"),
                RoomID = 2,
                Date = DateTime.Now.AddHours(8),
            },
             new MovieRoom
            {
                MovieRoomID =Guid.NewGuid(),
                MovieID = Guid.Parse("f882b046-1abd-4f38-a9cd-7b9dc8876138"),
                RoomID = 2,
                Date = DateTime.Now.AddDays(2).AddHours(8),
            }
        };

        private readonly static List<Seat> _seatData = new List<Seat>
        {
            new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 1,
                Special= false,
                RoomID = 1
            },
            new Seat
            {
                SeatID =  Guid.Parse("3cbd20d1-7590-4395-af55-fd637b9c111e"),
                SeatNumber = 2,
                Special= false,
                RoomID = 1
            },new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 3,
                Special= false,
                RoomID = 1
            },new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 4,
                Special= false,
                RoomID = 1
            },new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 5,
                Special= true,
                RoomID = 1
            },

            new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 1,
                Special= false,
                RoomID = 2
            },
            new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 2,
                Special= false,
                RoomID = 2
            },new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 3,
                Special= true,
                RoomID = 2
            },new Seat
            {
                SeatID =  Guid.NewGuid(),
                SeatNumber = 4,
                Special= true,
                RoomID = 2
            },
        };

    }
      
}
