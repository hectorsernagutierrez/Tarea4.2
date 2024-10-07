using GenerohectorsOntology;
using Gnoss.ApiWrapper.Model;
using Gnoss.ApiWrapper;
using Newtonsoft.Json;
using PeliculahectorsOntology;
using PersonahectorsOntology;
using static GnossBase.GnossOCBase;
using System.Globalization;
using Gnoss.ApiWrapper.ApiModel;

public class PeliculaPersistencia
{
    private ResourceApi apiRecursos;
    private string ontologiaPelicula = "peliculahectors";
    private string ontologiaPersona = "personahectors";
    private string ontologiaGenero = "generohectors";

    public PeliculaPersistencia(ResourceApi api)
    {
        this.apiRecursos = api;
    }

    public void CargarPeliculasDesdeJson(string rutaDirectorio)
    {
        foreach (var archivo in Directory.GetFiles(rutaDirectorio, "*.json"))
        {
            string contenidoJson = File.ReadAllText(archivo);
            dynamic datosJson = JsonConvert.DeserializeObject(contenidoJson);

            


            // Mapeo de los datos JSON a la clase Movie
            Movie pelicula = MapearJsonAPelicula(datosJson);



            // Validar campos obligatorios
            ValidarCamposPelicula(pelicula);

            //// Inicializar listas vacías
            //pelicula.IdsSchema_genre = new List<string>();
            //pelicula.IdsSchema_actor = new List<string>();
            //pelicula.IdsSchema_director = new List<string>();
            //pelicula.IdsSchema_author = new List<string>();


            // Primero, cargar géneros
            List<string> urisGeneros = VerificarOCrearGeneros(pelicula.IdsSchema_genre);

            // Segundo, cargar actores, directores, autores
            List<string> urisActores = VerificarOCrearPersonas(pelicula.IdsSchema_actor);
            List<string> urisDirectores = VerificarOCrearPersonas(pelicula.IdsSchema_director);
            List<string> urisAutores = VerificarOCrearPersonas(pelicula.IdsSchema_author);

            // Agregar la película al grafo con todos los datos
            AgregarPeliculaAGrafo(pelicula, urisGeneros, urisActores, urisDirectores, urisAutores);
        }
    }

    private Movie MapearJsonAPelicula(dynamic datosJson)
    {
        Movie pelicula = new Movie();
        // Inicializar listas vacías
        pelicula.IdsSchema_genre = new List<string>();
        pelicula.IdsSchema_actor = new List<string>();
        pelicula.IdsSchema_director = new List<string>();
        pelicula.IdsSchema_author = new List<string>();

        pelicula.Schema_name = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Title.ToString() } };
        pelicula.Schema_datePublished = DateTime.Parse(datosJson.Released.ToString()) ?? new DateTime();
        
        pelicula.Schema_duration = new List<int> { int.Parse(datosJson.Runtime.ToString().Split(' ')[0]) };

        datosJson.
        // Mapeo de géneros: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_genre = new List<string>();
        foreach (var genero in datosJson.Genre.ToString().Split(','))
        {
            pelicula.IdsSchema_genre.Add(genero.Trim());  // Aquí guardamos los nombres de los géneros
        }

        // Mapeo de actores: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_actor = new List<string>();
        foreach (var actor in datosJson.Actors.ToString().Split(','))
        {
            pelicula.IdsSchema_actor.Add(actor.Trim());  // Aquí guardamos los nombres de los actores
        }

        // Mapeo de directores: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_director = new List<string>();
        foreach (var director in datosJson.Director.ToString().Split(','))
        {
            pelicula.IdsSchema_author.Add(director.Trim());  // Aquí guardamos los nombres de los directores
        }

        // Mapeo de autores (escritores): obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_author = new List<string>();
        foreach (var autor in datosJson.Writer.ToString().Split(','))
        {
            pelicula.IdsSchema_author.Add(autor.Trim());  // Aquí guardamos los nombres de los autores
        }





        pelicula.Schema_description = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Plot.ToString() } };
        pelicula.Schema_image = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Poster.ToString() } };
        pelicula.Schema_contentRating = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Rated.ToString() } };

        // Mapeo de Ratings
        pelicula.Schema_rating = new List<Rating>();
        foreach (var valoracion in datosJson.Ratings)
        {
            //float n = float.Parse(valoracion.Value.ToString().Split('/')[0]);
            Rating valoracionPelicula = new Rating
            {
                Schema_ratingValue = (float)( ParseRatingAPorcentaje(valoracion.Value.ToString().Split('/')[0])),
                Schema_ratingSource = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, valoracion.Source.ToString() } }
            };
            pelicula.Schema_rating.Add(valoracionPelicula);
        }

        return pelicula;
    }

    private void ValidarCamposPelicula(Movie pelicula)
    {
        // Validaciones para asegurarse de que los campos obligatorios están presentes.
        if (pelicula.Schema_description == null || !pelicula.Schema_description.Any())
        {
            apiRecursos.Log.Warn("Descripción ausente. Se proporcionará una descripción predeterminada.");
            pelicula.Schema_description = new Dictionary<LanguageEnum, string>
            {
                { LanguageEnum.en, "Description not available" }
            };
        }

        if (pelicula.Schema_contentRating == null || !pelicula.Schema_contentRating.Any())
        {
            apiRecursos.Log.Warn("Content rating ausente. Se proporcionará un rating predeterminado.");
            pelicula.Schema_contentRating = new Dictionary<LanguageEnum, string>
            {
                { LanguageEnum.en, "Rating not available" }
            };
        }

        if (pelicula.Schema_name == null || !pelicula.Schema_name.Any())
        {
            apiRecursos.Log.Warn("Nombre de la película ausente. Se proporcionará un nombre predeterminado.");
            pelicula.Schema_name = new Dictionary<LanguageEnum, string>
            {
                { LanguageEnum.en, "Title not available" }
            };
        }
    }


    private List<string> VerificarOCrearPersonas(List<string> nombresPersonas)
    {
        List<string> uris = new List<string>();
        foreach (var nombrePersona in nombresPersonas)
        {
            if (string.IsNullOrEmpty(nombrePersona)) continue;

            SparqlObject resultado = null;
            string uri = "";


            string select = string.Empty, where = string.Empty;
            //nombrePersona.Replace("\'", "  ");
            select += $@"SELECT *";
            where += $@" WHERE {{ ";
            where += $@"?s ?p ?o.";
            where += $@"FILTER(?o LIKE '{nombrePersona.Replace("\'", "  ")}')";
            where += $@"}}";
            resultado = apiRecursos.VirtuosoQuery(select, where, ontologiaPersona);


           
            if (resultado != null && resultado.results != null && resultado.results.bindings.Count > 0)
            {
                uri = resultado.results.bindings[0]["s"].value;
            }
            else
            {
                apiRecursos.ChangeOntology(ontologiaPersona);
                Person nuevaPersona = new Person();
                //nuevaPersona.Schema_name.Add(LanguageEnum.en, nombrePersona);
                nuevaPersona.Schema_name = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, nombrePersona } };
                ComplexOntologyResource recursoPersona = nuevaPersona.ToGnossApiResource(apiRecursos, new List<string> { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
                uri = apiRecursos.LoadComplexSemanticResource(recursoPersona,false);
                
                

                
            }
            
            uris.Add(uri);
        }
        return uris;
    }

    private List<string> VerificarOCrearGeneros(List<string> nombresGeneros)
    {
        List<string> uris = new List<string>();
        foreach (var nombreGenero in nombresGeneros)
        {
            if (string.IsNullOrEmpty(nombreGenero)) continue;

           
            SparqlObject resultado = null;
            string uri = "";

            
                string select = string.Empty, where = string.Empty;
                select += $@"SELECT *";
                where += $@" WHERE {{ ";
                where += $@"?s ?p ?o.";
                where += $@"FILTER(?o LIKE '{nombreGenero}')";
                where += $@"}}";
                 resultado = apiRecursos.VirtuosoQuery(select, where, ontologiaGenero);
            
            

            if (resultado != null && resultado.results != null && resultado.results.bindings.Count > 0)
            {
                uri = resultado.results.bindings[0]["s"].value;
            }
            else
            {
                string identificador = Guid.NewGuid().ToString();
                Genre nuevoGenero = new Genre(identificador);
                nuevoGenero.Schema_name = nombreGenero;
                apiRecursos.ChangeOntology(ontologiaGenero);

                SecondaryResource generoSR = nuevoGenero.ToGnossApiResource(apiRecursos, $"Genre_{identificador}");
                 apiRecursos.LoadSecondaryResource(generoSR);
                uri = nuevoGenero.GNOSSID;
            }
            uris.Add(uri);
        }
        return uris;
    }

    private void AgregarPeliculaAGrafo(Movie pelicula, List<string> urisGeneros, List<string> urisActores, List<string> urisDirectores, List<string> urisAutores)
    {
        pelicula.IdsSchema_genre = urisGeneros; 
        pelicula.IdsSchema_actor = urisActores; 
        pelicula.IdsSchema_director = urisDirectores; 
        pelicula.IdsSchema_author = urisAutores;

        apiRecursos.ChangeOntology(ontologiaPelicula);
        ComplexOntologyResource recursoPelicula = pelicula.ToGnossApiResource(apiRecursos, new List<string> { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
        apiRecursos.LoadComplexSemanticResource(recursoPelicula);
    }




    public static double ParseRatingAPorcentaje(string value)
    {
        value = value.Trim();

        // Caso 1: Ya es un porcentaje, como "79%"
        if (value.EndsWith("%"))
        {
            return double.Parse(value.Replace("%", ""), CultureInfo.InvariantCulture);
        }

        // Caso 2: Es una fracción, como "8.3/10" o "68/100"
        if (value.Contains("/"))
        {
            var parts = value.Split('/');
            if (parts.Length == 2 && double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double numerator)
                                  && double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double denominator))
            {
                return (numerator / denominator) * 100;
            }
        }else if (value.Contains("."))
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double entero);
            return entero * 10;
        }
        else
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double entero);
            return entero;
        }


        throw new FormatException("Formato de rating no reconocido.");
    }


}






