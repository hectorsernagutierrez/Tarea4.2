/*
 * Autor:Hector Serna Gutierrez
 * Tarea 4.2
 * version 1.0
 * Ultima mod 07/10/2024
 * Contacto: hectorserna@gnos.com
 * Documento: PeliculaPersistencia
 */
using GenerohectorsOntology;
using Gnoss.ApiWrapper.Model;
using Gnoss.ApiWrapper;
using Newtonsoft.Json;
using PeliculahectorsOntology;
using PersonahectorsOntology;
using static GnossBase.GnossOCBase;
using System.Globalization;
using Gnoss.ApiWrapper.ApiModel;
using SixLabors.ImageSharp.ColorSpaces;
using System.Drawing;

public class PeliculaPersistencia
{
    private ResourceApi apiRecursos;
    private string ontologiaPelicula = "peliculahectors";
    private string ontologiaPersona = "personahectors";
    private string ontologiaGenero = "generohectors";
    /// <summary>
    /// Clase que recibe la api de recursos y genera la instancia dela clase de persistenci para trabajo con json de peliculas
    /// </summary>
    /// <param name="api"></param>
    public PeliculaPersistencia(ResourceApi api)
    {
        this.apiRecursos = api;
    }

    /// <summary>
    /// Metodo principal que mapea la infromación de los json
    /// y refiere a metodos auxiliares para su carga en la api.
    /// Deserializa ,genera listas de los otros recursos relacionados y los crea o recoge meidante llamada a los metodos auxiliares.
    /// </summary>
    /// <param name="rutaDirectorio"></param>
    public void CargarPeliculasDesdeJson(string rutaDirectorio)
    {
        foreach (var archivo in Directory.GetFiles(rutaDirectorio, "*.json"))//Para cada archivo json se craga a información. 
            //ver referencia en csproj como he hecho copia siempre de los json al bin mediante código de copia siempre( Importante por que son muchos archivos)
        {
            string contenidoJson = File.ReadAllText(archivo);
            dynamic datosJson = JsonConvert.DeserializeObject(contenidoJson);//Deserializo los datos de los json usando la nuget de newton soft

            


            // Mapeo de los datos JSON a la clase Movie
            Movie pelicula = MapearJsonAPelicula(datosJson);



            // Validar campos obligatorios
            ValidarCamposPelicula(pelicula);

            //// Inicializar listas vacías | al final las inializo por cada peli
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

    /// <summary>
    /// Asocia el mapa de deserialización de json a las propiedades del OC Pelicula
    /// Genera las listas de los otros OC ( string con sus nombres) devuelve la pelicula creada a falta de unir las relaciones a otros recursos
    /// Tambien aplica manejo de errores de datos almacenados en el json
    /// </summary>
    /// <param name="datosJson"></param>
    /// <returns>Movie</returns>
    private Movie MapearJsonAPelicula(dynamic datosJson)
    {
        //Notar que si alguna etiqueta del json no aparece en algun ejemplo concreto este campor sera nulo , de ahñi los if de prevención de nulos.
        Movie pelicula = new Movie();
        // Inicializar listas vacías
        pelicula.IdsSchema_genre = new List<string>();
        pelicula.IdsSchema_actor = new List<string>();
        pelicula.IdsSchema_director = new List<string>();
        pelicula.IdsSchema_author = new List<string>();

        pelicula.Schema_name = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Title.ToString() } };

        string s = datosJson.Released ?? " ";
        string releasedDateString = s.ToString(); // o "texto inválido"
        DateTime releasedDate;

        // Intentamos parsear la fecha; si no es válida o es null, se usa una fecha por defecto.
        if (!DateTime.TryParse(releasedDateString, out releasedDate))
        {
            // Usar una fecha por defecto (por ejemplo, el 1 de enero de 800)
            releasedDate = new DateTime(800, 1, 1);
        }

        //pelicula.Schema_datePublished = DateTime.Parse(datosJson.Released.ToString());
        s = datosJson.Runtime ?? " ";
        var lista = s.Split(' ');
        int i = 0;
        List<int> l2 = new List<int>();
        foreach (var l in lista)
        {
            if (!int.TryParse(l, out i))
            {
                // Usar una fecha por defecto (por ejemplo, el 1 de enero de 800)
                l2.Add(0);
            }
            else { l2.Add(i); }



        }
        pelicula.Schema_duration = l2;


        // Mapeo de géneros: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_genre = new List<string>();
        if (!datosJson.Genre.Equals(null))
        {
            foreach (var genero in datosJson.Genre.ToString().Split(','))
            {
                pelicula.IdsSchema_genre.Add(genero.Trim());  // Aquí guardamos los nombres de los géneros
            }
        }
        // Mapeo de actores: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_actor = new List<string>();
        if (!datosJson.Actors.Equals( null))
        {
            foreach (var actor in datosJson.Actors.ToString().Split(','))
            {
                pelicula.IdsSchema_actor.Add(actor.Trim());  // Aquí guardamos los nombres de los actores
            }
        }
        // Mapeo de directores: obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_director = new List<string>();
        if (!datosJson.Director.Equals( null))
        {
            foreach (var director in datosJson.Director.ToString().Split(','))
            {
                pelicula.IdsSchema_author.Add(director.Trim());  // Aquí guardamos los nombres de los directores
            }
        }

        // Mapeo de autores (escritores): obteniendo solo los nombres para luego referenciar
        pelicula.IdsSchema_author = new List<string>();
        if (!datosJson.Writer.Equals(null))
        {
            foreach (var autor in datosJson.Writer.ToString().Split(','))
            {
                pelicula.IdsSchema_author.Add(autor.Trim());  // Aquí guardamos los nombres de los autores
            }
        }



        if (datosJson.Plot != null)
        {
            pelicula.Schema_description = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Plot.ToString() } };
        }
        if (datosJson.Poster != null)
        {
            pelicula.Schema_image = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Poster.ToString() } };
        }
        if (datosJson.Rated != null)
        {
            pelicula.Schema_contentRating = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, datosJson.Rated.ToString() } };
        }
        // Mapeo de Ratings
        pelicula.Schema_rating = new List<Rating>();
        if (datosJson.Ratings != null) { 
        foreach (var valoracion in datosJson.Ratings)
        {
            
            Rating valoracionPelicula = new Rating
            {
                Schema_ratingValue = (float)(ParseRatingAPorcentaje(valoracion.Value.ToString().Split('/')[0])),
                Schema_ratingSource = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, valoracion.Source.ToString() } }
            };
            pelicula.Schema_rating.Add(valoracionPelicula);
            } }

        return pelicula;
    }
    //Una vez creada la película con las propiedades no relacionadas con otros OC validamos
    /// <summary>
    /// Validación de que los datos obtenidos del json en aquellas propiedades no relacionales son correctas en cuanto a forma y formato.
    /// </summary>
    /// <param name="pelicula"></param>
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

    /// <summary>
    /// Lista con strings de nombre de personas de las que se sacan del grafo las personas ya creadas 
    /// o creas las que no aparezcan . Se devuelve la URI( el gnoss ID )de cada instancia de persona de la
    /// lista de nombres en el mismo orden.
    /// </summary>
    /// <param name="nombresPersonas"></param>
    /// <returns></returns>
    private List<string> VerificarOCrearPersonas(List<string> nombresPersonas)
    {
        List<string> uris = new List<string>();
        if (nombresPersonas != null)
        {
            foreach (var nombrePersona in nombresPersonas)
            {
                if (string.IsNullOrEmpty(nombrePersona)) continue;//si son comillas o vacio nos lo saltamos , para evitar persoanas sin nombre

                SparqlObject resultado = null;
                string uri = "";

                string nombre = nombrePersona.Replace("\'", "  ");//Metodo de control de apodos con ' o . para la consulta
                nombre = nombre.Replace(".", " ");//el el punto
                string select = string.Empty, where = string.Empty;//consulta sparql para sacar persona por nombre parecido.
                                                                   //nombrePersona.Replace("\'", "  ");
                select += $@"SELECT *";
                where += $@" WHERE {{ ";
                where += $@"?s ?p ?o.";
                where += $@"FILTER(?o LIKE '{nombrePersona.Replace("\'", "  ")}')";
                where += $@"}}";
                resultado = apiRecursos.VirtuosoQuery(select, where, ontologiaPersona);



                if (resultado != null && resultado.results != null && resultado.results.bindings.Count > 0)//Si existe
                {
                    uri = resultado.results.bindings[0]["s"].value;//Sacamos su uri
                }
                else // Si no existe lo creamos y sacamos su uri en el proceso.
                {
                    apiRecursos.ChangeOntology(ontologiaPersona);
                    Person nuevaPersona = new Person();
                    //nuevaPersona.Schema_name.Add(LanguageEnum.en, nombrePersona);
                    nuevaPersona.Schema_name = new Dictionary<LanguageEnum, string> { { LanguageEnum.en, nombrePersona } };
                    ComplexOntologyResource recursoPersona = nuevaPersona.ToGnossApiResource(apiRecursos, new List<string> { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
                    uri = apiRecursos.LoadComplexSemanticResource(recursoPersona, false);




                }

                uris.Add(uri);
            }
        }
        return uris;
    }

    /// <summary>
    ///  Lista con strings de nombre de generos de las que se sacan del grafo las generos ya creadas 
    /// o creas las que no aparezcan . Se devuelve la URI( el gnoss ID )de cada instancia de genro de la
    /// lista de nombres en el mismo orden.
    /// </summary>
    /// <param name="nombresGeneros"></param>
    /// <returns></returns>
    private List<string> VerificarOCrearGeneros(List<string> nombresGeneros)
    {
        //Forma de proceder analogo al de las personas
        List<string> uris = new List<string>();
        if (nombresGeneros != null)
        {
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
        }
        return uris;
    }

    /// <summary>
    /// Toma la pelicula y las listas de Uris de OCs une toda la infromación y añade la pelicula al grafo
    /// </summary>
    /// <param name="pelicula"></param>
    /// <param name="urisGeneros"></param>
    /// <param name="urisActores"></param>
    /// <param name="urisDirectores"></param>
    /// <param name="urisAutores"></param>
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


        throw new FormatException("Formato de rating no reconocido");
    }


}






